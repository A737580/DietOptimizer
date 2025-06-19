using DietOptimizer.Models;
using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietOptimizer.Solvers
{
    public class IntegerSimplexSolverGomori
    {

        // Метод для минимизации стоимости (требует только минимальные значения питательных веществ)
        public SolutionResult MinimizeCost(
            List<Product> products,
            double minProtein,
            double minFat,
            double minCarbs)
        {
            // Чтение решения
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Minimization,
                Products = products,
                ProteinRequirement = minProtein,
                FatRequirement = minFat,
                CarbsRequirement = minCarbs,
                TotalCost = 0
            };
            // Инициализация решателя
            Solver solver = Solver.CreateSolver("SCIP") ??
                           Solver.CreateSolver("CBC") ??
                           throw new Exception("No ILP solver available");

            int numProducts = products.Count;

            // Создание переменных (количество каждого продукта)
            Variable[] vars = new Variable[numProducts];
            for (int i = 0; i < numProducts; i++)
            {
                vars[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, products[i].Name);
            }

            // Добавление ограничений по минимальным питательным веществам
            // Белки
            Constraint proteinConstraint = solver.MakeConstraint(minProtein, double.PositiveInfinity, "Protein");
            for (int i = 0; i < numProducts; i++)
            {
                proteinConstraint.SetCoefficient(vars[i], products[i].Protein);
            }

            // Жиры
            Constraint fatConstraint = solver.MakeConstraint(minFat, double.PositiveInfinity, "Fat");
            for (int i = 0; i < numProducts; i++)
            {
                fatConstraint.SetCoefficient(vars[i], products[i].Fat);
            }

            // Углеводы
            Constraint carbsConstraint = solver.MakeConstraint(minCarbs, double.PositiveInfinity, "Carbs");
            for (int i = 0; i < numProducts; i++)
            {
                carbsConstraint.SetCoefficient(vars[i], products[i].Carbs);
            }

            // Настройка целевой функции (минимизация стоимости)
            Objective obj = solver.Objective();
            for (int i = 0; i < numProducts; i++)
            {
                obj.SetCoefficient(vars[i], products[i].Price);
            }
            obj.SetMinimization();

            List<double> information = SolveWithGomory(solver, vars, products, isMinimization: true);
            result.TotalCost = information[0];
            result.TotalProtein = information[1];
            result.TotalFat = information[2];
            result.TotalCarbs = information[3];
            for (int i = 5; i < information.Count; i++)
            {
                result.ProductAmounts[products[i-5]] = information[i];
            }
            return result;
        }


        public SolutionResult MaximizeNutrients(
    List<Product> products,
    double maxProtein,
    double maxFat,
    double maxCarbs,
    double budget)
        {
            // Подготовка результата
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                Budget = budget,
                ProteinRequirement = maxProtein,
                FatRequirement = maxFat,
                CarbsRequirement = maxCarbs,
                TotalCost = 0
            };

            // Инициализация ILP-решателя (SCIP или CBC)
            Solver solver = Solver.CreateSolver("SCIP")
                           ?? Solver.CreateSolver("CBC")
                           ?? throw new Exception("No ILP solver available");

            int n = products.Count;

            // 1) Целочисленные переменные x[i] ≥ 0
            Variable[] x = new Variable[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, products[i].Name);
            }

            // 2) Ограничения по макроэлементам (не превышать указанные макс. значения)
            var protC = solver.MakeConstraint(0.0, maxProtein, "Protein");
            var fatC = solver.MakeConstraint(0.0, maxFat, "Fat");
            var carbC = solver.MakeConstraint(0.0, maxCarbs, "Carbs");
            for (int i = 0; i < n; i++)
            {
                protC.SetCoefficient(x[i], products[i].Protein);
                fatC.SetCoefficient(x[i], products[i].Fat);
                carbC.SetCoefficient(x[i], products[i].Carbs);
            }

            // 3) Ограничение по бюджету
            var budgetC = solver.MakeConstraint(0.0, budget, "Budget");
            for (int i = 0; i < n; i++)
            {
                budgetC.SetCoefficient(x[i], products[i].Price);
            }

            // 4) Целевая функция — максимизация суммы всех макроэлементов
            var obj = solver.Objective();
            for (int i = 0; i < n; i++)
            {
                double sumMacros = products[i].Protein
                                 + products[i].Fat
                                 + products[i].Carbs;
                obj.SetCoefficient(x[i], sumMacros);
            }
            obj.SetMaximization();

            // 5) Решение с помощью метода Гомори (или прямой Solve, если нужна ILP)
            List<double> info = SolveWithGomory(solver, x, products, isMinimization: false);

            // 6) Распаковка результата
            result.TotalCost = info[0];
            result.TotalProtein = info[1];
            result.TotalFat = info[2];
            result.TotalCarbs = info[3];
            // Порции продуктов начинаются с индекса 5
            for (int i = 5; i < info.Count; i++)
            {
                result.ProductAmounts[products[i - 5]] = info[i];
            }

            return result;
        }

        // Метод для максимизации белков (требует максимальные значения питательных веществ и бюджет)
        public SolutionResult MaximizeNutrients2(
            List<Product> products,
            double maxProtein,
            double maxFat,
            double maxCarbs,
            double budget)
        {
            // Чтение решения
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                Budget = budget,
                ProteinRequirement = maxProtein,
                FatRequirement = maxFat,
                CarbsRequirement = maxCarbs,
                TotalCost = 0
            };
            // Инициализация решателя
            Solver solver = Solver.CreateSolver("SCIP") ??
                           Solver.CreateSolver("CBC") ??
                           throw new Exception("No ILP solver available");

            int numProducts = products.Count;

            // Создание переменных (количество каждого продукта)
            Variable[] vars = new Variable[numProducts];
            for (int i = 0; i < numProducts; i++)
            {
                vars[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, products[i].Name);
            }

            // Добавление ограничений по максимальным питательным веществам
            // Белки
            Constraint proteinConstraint = solver.MakeConstraint(0, maxProtein, "Protein");
            for (int i = 0; i < numProducts; i++)
            {
                proteinConstraint.SetCoefficient(vars[i], products[i].Protein);
            }

            // Жиры
            Constraint fatConstraint = solver.MakeConstraint(0, maxFat, "Fat");
            for (int i = 0; i < numProducts; i++)
            {
                fatConstraint.SetCoefficient(vars[i], products[i].Fat);
            }

            // Углеводы
            Constraint carbsConstraint = solver.MakeConstraint(0, maxCarbs, "Carbs");
            for (int i = 0; i < numProducts; i++)
            {
                carbsConstraint.SetCoefficient(vars[i], products[i].Carbs);
            }

            // Бюджетное ограничение
            Constraint budgetConstraint = solver.MakeConstraint(0, budget, "Budget");
            for (int i = 0; i < numProducts; i++)
            {
                budgetConstraint.SetCoefficient(vars[i], products[i].Price);
            }

            // Настройка целевой функции (максимизация белков)
            Objective obj = solver.Objective();
            for (int i = 0; i < numProducts; i++)
            {
                obj.SetCoefficient(vars[i], products[i].Protein
                                 + products[i].Fat
                                 + products[i].Carbs);
            }
            obj.SetMaximization();

            List<double> information = SolveWithGomory(solver, vars, products, isMinimization: false);
            result.TotalCost = information[0];
            result.TotalProtein = information[1];
            result.TotalFat = information[2];
            result.TotalCarbs = information[3];
            for(int i=5;i<information.Count;i++)
            {
                result.ProductAmounts[products[i-5]] = information[i];
            }
            return result;
        }

        List<double> SolveWithGomory(Solver solver, Variable[] vars, List<Product> products, bool isMinimization)
        {
            List<double> result = new List<double>();
            // Решение с отсечениями Гомори
            int iteration = 0;
            while (iteration < 50)
            {

                var status = solver.Solve();
                if (status != Solver.ResultStatus.OPTIMAL)
                {
                    throw new Exception("Оптимальное решение не найдено");
                }


                if (IsIntegerSolution(vars))
                {
                    result.Add(solver.Objective().Value());
                    result.AddRange(PrintNutritionalInfo(products, vars));
                    result.AddRange(PrintSolution(vars, solver.Objective().Value()));
                    return result;
                }

                // Добавляем отсечение Гомори
                AddGomoryCut(solver, vars, products, isMinimization);
                iteration++;
            }
            return null;
        }

        void AddGomoryCut(Solver solver, Variable[] vars, List<Product> products, bool isMinimization)
        {
            // Упрощенная версия отсечения Гомори по наиболее нарушенному ограничению
            if (isMinimization)
            {
                // Для минимизации добавляем отсечение по стоимости
                double sum = 0;
                for (int i = 0; i < vars.Length; i++)
                {
                    sum += vars[i].SolutionValue() * products[i].Price;
                }

                double fraction = sum - Math.Floor(sum);
                if (fraction < 1e-6) return;

                Constraint cut = solver.MakeConstraint(0, fraction, $"Gomory_Cost");
                for (int i = 0; i < vars.Length; i++)
                {
                    double coeff = products[i].Price;
                    double fractional = coeff - Math.Floor(coeff);
                    cut.SetCoefficient(vars[i], fractional);
                }
            }
            else
            {
                // Для максимизации добавляем отсечение по бюджету
                double sum = 0;
                for (int i = 0; i < vars.Length; i++)
                {
                    sum += vars[i].SolutionValue() * products[i].Price;
                }

                double fraction = sum - Math.Floor(sum);
                if (fraction < 1e-6) return;

                Constraint cut = solver.MakeConstraint(0, fraction, $"Gomory_Budget");
                for (int i = 0; i < vars.Length; i++)
                {
                    double coeff = products[i].Price;
                    double fractional = coeff - Math.Floor(coeff);
                    cut.SetCoefficient(vars[i], fractional);
                }
            }

            Console.WriteLine($"Добавлено отсечение Гомори");
        }

        bool IsIntegerSolution(Variable[] vars, double tolerance = 1e-6)
        {
            return vars.All(v => Math.Abs(v.SolutionValue() - Math.Round(v.SolutionValue())) < tolerance);
        }

        List<double> PrintSolution(Variable[] vars, double objective)
        {
            List<double> solution = new List<double>();
            for (int i = 0; i < vars.Length; i++)
            {
                solution.Add(vars[i].SolutionValue());
            }
            return solution;
        }

        List<double> PrintNutritionalInfo(List<Product> products, Variable[] vars)
        {
            List<double> doubles = new List<double> { 0,0,0,0};

            for (int i = 0; i < vars.Length; i++)
            {
                double amount = vars[i].SolutionValue();
                doubles[0] += products[i].Protein * amount;
                doubles[1] += products[i].Fat * amount;
                doubles[2] += products[i].Carbs * amount;
                doubles[3] += products[i].Price * amount;
            }
            return doubles;
        }

    }
}
