using DietOptimizer.Models;

namespace DietOptimizer.Solvers
{
    public class LpSimplexSolver
    {
        // Решение LP задач вида:
        // min c^T x s.t. A x >= b, x >= 0
        // Для удобства конвертируем в max -c^T x, Ax >= b -> -Ax <= -b

        public SolutionResult SolveMinimization(List<Product> products,double proteinReq,double fatReq,double carbsReq)
        {
            int n = products.Count;
            // 3 ограничения: protein, fat, carbs
            int m = 3;
            // Таблица размер (m+1) x (n + m + 1)
            int cols = n + m + 1;
            int rows = m + 1;
            double[,] T = new double[rows, cols];
            int[] basis = new int[m];

            // Заполнение первой m строк ограничений: -A x + s = -b
            double[] b = { proteinReq, fatReq, carbsReq };
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double aij = i == 0 ? products[j].Protein : i == 1 ? products[j].Fat : products[j].Carbs;
                    T[i, j] = -aij;
                }
                // slack
                T[i, n + i] = 1;
                // RHS
                T[i, cols - 1] = -b[i];
                basis[i] = n + i;
            }
            // Целевая функция в последней строк: max -c^T x -> row m
            for (int j = 0; j < n; j++)
            {
                T[m, j] = -products[j].Price;
            }
            // RHS функции
            T[m, cols - 1] = 0;

            // Симплекс
            while (true)
            {
                // Находим положительный коэффициент в целевой строке (для max)
                int pivotCol = -1;
                double maxVal = 0;
                for (int j = 0; j < cols - 1; j++)
                {
                    if (T[m, j] > maxVal)
                    {
                        maxVal = T[m, j];
                        pivotCol = j;
                    }
                }
                if (pivotCol < 0) break; // оптимально

                // Выбор строки по min ratio
                int pivotRow = -1;
                double minRatio = double.PositiveInfinity;
                for (int i = 0; i < m; i++)
                {
                    if (T[i, pivotCol] < 0)
                    {
                        double ratio = -T[i, cols - 1] / T[i, pivotCol];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }
                if (pivotRow < 0) throw new Exception("Симплекс неограничен");

                // Pivot
                Pivot(T, pivotRow, pivotCol, rows, cols);
                basis[pivotRow] = pivotCol;
            }

            // Чтение решения
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Minimization,
                Products = products,
                ProteinRequirement = proteinReq,
                FatRequirement = fatReq,
                CarbsRequirement = carbsReq,
                TotalCost = 0
            };
            for (int j = 0; j < n; j++)
                result.ProductAmounts[products[j]] = 0;

            // базисные переменные
            for (int i = 0; i < m; i++)
            {
                if (basis[i] < n)
                {
                    double val = T[i, cols - 1];
                    result.ProductAmounts[products[basis[i]]] = val > 0 ? (int)Math.Round(val) : 0;
                }
            }
            // посчитаем итоговые суммы
            result.TotalCost = result.ProductAmounts.Sum(kv => kv.Key.Price * kv.Value);
            result.TotalProtein = result.ProductAmounts.Sum(kv => kv.Key.Protein * kv.Value);
            result.TotalFat = result.ProductAmounts.Sum(kv => kv.Key.Fat * kv.Value);
            result.TotalCarbs = result.ProductAmounts.Sum(kv => kv.Key.Carbs * kv.Value);

            return result;
        }

        private void Pivot(double[,] T, int pivotRow, int pivotCol, int rows, int cols)
        {
            double pivot = T[pivotRow, pivotCol];
            // нормализация строки
            for (int j = 0; j < cols; j++)
                T[pivotRow, j] /= pivot;
            // обнуление остальных
            for (int i = 0; i < rows; i++)
            {
                if (i == pivotRow) continue;
                double factor = T[i, pivotCol];
                for (int j = 0; j < cols; j++)
                    T[i, j] -= factor * T[pivotRow, j];
            }
        }
        public SolutionResult SolveMaximizationPriority(List<Product> products,double budget,double proteinReq,double fatReq,double carbsReq,double proteinWeight,double fatWeight,double carbsWeight)
        {
            int n = products.Count;
            int m = 4; // ограничения: белки, жиры, углеводы, бюджет
            int cols = n + m + 1;
            int rows = m + 1;
            double[,] T = new double[rows, cols];
            int[] basis = new int[m];

            // Заполняем ограничения:
            // 1. Белки ≥ proteinReq  → -Protein * x + s1 = -proteinReq
            for (int j = 0; j < n; j++)
                T[0, j] = -products[j].Protein;
            T[0, n + 0] = 1;
            T[0, cols - 1] = -proteinReq;
            basis[0] = n + 0;

            // 2. Жиры ≥ fatReq
            for (int j = 0; j < n; j++)
                T[1, j] = -products[j].Fat;
            T[1, n + 1] = 1;
            T[1, cols - 1] = -fatReq;
            basis[1] = n + 1;

            // 3. Углеводы ≥ carbsReq
            for (int j = 0; j < n; j++)
                T[2, j] = -products[j].Carbs;
            T[2, n + 2] = 1;
            T[2, cols - 1] = -carbsReq;
            basis[2] = n + 2;

            // 4. Бюджет ≤ budget → Price * x + s4 = budget
            for (int j = 0; j < n; j++)
                T[3, j] = products[j].Price;
            T[3, n + 3] = 1;
            T[3, cols - 1] = budget;
            basis[3] = n + 3;

            // Целевая функция: max суммарная питательность = w1*P + w2*F + w3*C
            for (int j = 0; j < n; j++)
            {
                T[4, j] = products[j].Protein * proteinWeight
                        + products[j].Fat * fatWeight
                        + products[j].Carbs * carbsWeight;
            }

            // Симплекс-метод
            while (true)
            {
                // Находим столбец для развёртки (pivot)
                int pivotCol = -1;
                double maxVal = 0;
                for (int j = 0; j < cols - 1; j++)
                {
                    if (T[4, j] > maxVal)
                    {
                        maxVal = T[4, j];
                        pivotCol = j;
                    }
                }
                if (pivotCol < 0) break; // оптимум найден

                int pivotRow = -1;
                double minRatio = double.PositiveInfinity;
                for (int i = 0; i < m; i++)
                {
                    if (T[i, pivotCol] > 0)
                    {
                        double ratio = T[i, cols - 1] / T[i, pivotCol];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }

                if (pivotRow < 0)
                    throw new Exception("Симплекс: задача неограничена");

                Pivot(T, pivotRow, pivotCol, rows, cols);
                basis[pivotRow] = pivotCol;
            }

            // Чтение решения
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                Budget = budget,
                ProteinRequirement = proteinReq,
                FatRequirement = fatReq,
                CarbsRequirement = carbsReq,
                ProteinWeight = proteinWeight,
                FatWeight = fatWeight,
                CarbsWeight = carbsWeight,
                NutritionalValue = T[rows - 1, cols - 1]
            };

            for (int j = 0; j < n; j++)
                result.ProductAmounts[products[j]] = 0;

            for (int i = 0; i < m; i++)
            {
                if (basis[i] < n)
                {
                    double val = T[i, cols - 1];
                    result.ProductAmounts[products[basis[i]]] = val > 0 ? val : 0;
                }
            }

            result.TotalCost = result.ProductAmounts.Sum(kv => kv.Key.Price * kv.Value);
            result.TotalProtein = result.ProductAmounts.Sum(kv => kv.Key.Protein * kv.Value);
            result.TotalFat = result.ProductAmounts.Sum(kv => kv.Key.Fat * kv.Value);
            result.TotalCarbs = result.ProductAmounts.Sum(kv => kv.Key.Carbs * kv.Value);

            return result;
        }
        public SolutionResult SolveMaximization(List<Product> products,double budget,double proteinWeight,double fatWeight,double carbsWeight)
        {
            // Преобразуем max c^T x s.t. sum Price*x <= budget
            int n = products.Count;
            int m = 1;
            int cols = n + m + 1;
            int rows = m + 1;
            double[,] T = new double[rows, cols];
            int[] basis = new int[m];

            // constraint: sum Price*x + s = budget => A x + s = b
            for (int j = 0; j < n; j++)
                T[0, j] = products[j].Price;
            T[0, n] = 1;
            T[0, cols - 1] = budget;
            basis[0] = n;

            // objective: max nutritional value
            for (int j = 0; j < n; j++)
                T[1, j] = products[j].Protein * proteinWeight
                         + products[j].Fat * fatWeight
                         + products[j].Carbs * carbsWeight;
            T[1, cols - 1] = 0;

            // Симплекс аналогично
            while (true)
            {
                int pivotCol = -1;
                double maxVal = 0;
                for (int j = 0; j < cols - 1; j++)
                    if (T[1, j] > maxVal) { maxVal = T[1, j]; pivotCol = j; }
                if (pivotCol < 0) break;

                int pivotRow = -1;
                double minRatio = double.PositiveInfinity;
                for (int i = 0; i < m; i++)
                    if (T[i, pivotCol] > 0)
                    {
                        double ratio = T[i, cols - 1] / T[i, pivotCol];
                        if (ratio < minRatio) { minRatio = ratio; pivotRow = i; }
                    }
                if (pivotRow < 0) throw new Exception("Симплекс неограничен");

                Pivot(T, pivotRow, pivotCol, rows, cols);
                basis[pivotRow] = pivotCol;
            }

            // Чтение решения
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                Budget = budget,
                ProteinWeight = proteinWeight,
                FatWeight = fatWeight,
                CarbsWeight = carbsWeight,
                NutritionalValue = T[1, cols - 1]
            };
            for (int j = 0; j < n; j++)
                result.ProductAmounts[products[j]] = 0;
            for (int i = 0; i < m; i++)
                if (basis[i] < n)
                {
                    double val = T[i, cols - 1];
                    result.ProductAmounts[products[basis[i]]] = val > 0 ? (int)Math.Round(val) : 0;
                }

            result.TotalCost = result.ProductAmounts.Sum(kv => kv.Key.Price * kv.Value);
            result.TotalProtein = result.ProductAmounts.Sum(kv => kv.Key.Protein * kv.Value);
            result.TotalFat = result.ProductAmounts.Sum(kv => kv.Key.Fat * kv.Value);
            result.TotalCarbs = result.ProductAmounts.Sum(kv => kv.Key.Carbs * kv.Value);

            return result;
        }
    }
}
