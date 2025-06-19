using System;
using System.Collections.Generic;
using System.Linq;
using DietOptimizer.Models;
using Google.OrTools.LinearSolver;


namespace DietOptimizer.Solvers
{
    public class OrToolsLpSolver
    {
        public SolutionResult SolveMinimization(
            List<Product> products,
            double proteinReq,
            double fatReq,
            double carbsReq)
        {
            var solver = Solver.CreateSolver("GLOP");
            if (solver == null)
                throw new Exception("Не удалось создать LP-решатель OR-Tools");

            // Переменные x[j] >= 0
            var vars = products.ToDictionary(
                p => p,
                p => solver.MakeNumVar(0.0, double.PositiveInfinity, p.Name)
            );

            // Целевая функция: минимизация стоимости
            var objective = solver.Objective();
            foreach (var p in products)
                objective.SetCoefficient(vars[p], p.Price);
            objective.SetMinimization();

            // Ограничения по питательным веществам
            var proteinC = solver.MakeConstraint(proteinReq, double.PositiveInfinity, "protein");
            var fatC = solver.MakeConstraint(fatReq, double.PositiveInfinity, "fat");
            var carbsC = solver.MakeConstraint(carbsReq, double.PositiveInfinity, "carbs");

            foreach (var p in products)
            {
                proteinC.SetCoefficient(vars[p], p.Protein);
                fatC.SetCoefficient(vars[p], p.Fat);
                carbsC.SetCoefficient(vars[p], p.Carbs);
            }

            var status = solver.Solve();
            if (status != Solver.ResultStatus.OPTIMAL)
                throw new Exception("Оптимальное решение LP не найдено");

            var result = new SolutionResult
            {
                ProblemType = ProblemType.Minimization,
                Products = products,
                ProteinRequirement = proteinReq,
                FatRequirement = fatReq,
                CarbsRequirement = carbsReq,
                TotalCost = objective.Value()
            };

            foreach (var p in products)
            {
                result.ProductAmounts[p] = vars[p].SolutionValue();
            }

            result.TotalProtein = products.Sum(p => p.Protein * result.ProductAmounts[p]);
            result.TotalFat = products.Sum(p => p.Fat * result.ProductAmounts[p]);
            result.TotalCarbs = products.Sum(p => p.Carbs * result.ProductAmounts[p]);

            return result;
        }

        public SolutionResult SolveMaximizationPriority(List<Product> products, double budget,
            double proteinWeight, double fatWeight, double carbsWeight)
        {
            var solver = Solver.CreateSolver("GLOP");
            if (solver == null)
                throw new Exception("Не удалось создать LP-решатель OR-Tools");

            var variables = new Dictionary<Product, Variable>();
            foreach (var p in products)
            {
                variables[p] = solver.MakeNumVar(0, 100, p.Name);
            }

            // Целевая функция: максимизация питательности
            var objective = solver.Objective();
            foreach (var p in products)
            {
                objective.SetCoefficient(variables[p],
                    p.Protein * proteinWeight + p.Fat * fatWeight + p.Carbs * carbsWeight);
            }
            objective.SetMaximization();

            // Ограничение по бюджету
            var budgetConstraint = solver.MakeConstraint(0, budget, "budget");
            foreach (var p in products)
            {
                budgetConstraint.SetCoefficient(variables[p], p.Price);
            }

            // Решение
            var resultStatus = solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
                throw new Exception("Оптимальное решение не найдено");

            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                ProductAmounts = new Dictionary<Product, double>(),
                Budget = budget,
                ProteinWeight = proteinWeight,
                FatWeight = fatWeight,
                CarbsWeight = carbsWeight
            };

            foreach (var p in products)
            {
                result.ProductAmounts[p] = (variables[p].SolutionValue());
            }

            result.TotalCost = products.Sum(p => p.Price * result.ProductAmounts[p]);
            result.TotalProtein = products.Sum(p => p.Protein * result.ProductAmounts[p]);
            result.TotalFat = products.Sum(p => p.Fat * result.ProductAmounts[p]);
            result.TotalCarbs = products.Sum(p => p.Carbs * result.ProductAmounts[p]);
            result.NutritionalValue = solver.Objective().Value();

            return result;
        }
        public SolutionResult SolveMaximization(List<Product> products, double budget,
                                                 double maxProtein, double maxFat, double maxCarbs)
        {
            // 1. Создание решателя (GLOP - для линейного программирования)
            Solver solver = Solver.CreateSolver("GLOP");
            if (solver == null)
            {
                throw new Exception("Не удалось создать LP-решатель OR-Tools (GLOP). Убедитесь, что пакет Google.OrTools установлен и нативные библиотеки доступны.");
            }

            // 2. Создание переменных
            // Для каждого продукта создаем переменную, представляющую его количество.
            // MakeNumVar используется для непрерывных переменных (дробное количество).
            // Если нужно целочисленное количество (штуки), используйте MakeIntVar.
            // Нижняя граница 0 (нельзя взять отрицательное количество).
            // Верхняя граница - double.PositiveInfinity (не ограничено сверху, если не задано иное).
            var variables = new Dictionary<Product, Variable>();
            foreach (var p in products)
            {
                // Важно: Убедитесь, что имя уникально, если продукты могут иметь одинаковые Name
                variables[p] = solver.MakeNumVar(0.0, double.PositiveInfinity, p.Name);
            }

            // 3. Определение целевой функции
            // Цель: максимизировать Z = sum( (Protein_j  + Fat_j  + Carbs_j) * x_j )
            Objective objective = solver.Objective();
            foreach (var p in products)
            {
                double nutritionalCoefficient = p.Protein + p.Fat + p.Carbs ;
                objective.SetCoefficient(variables[p], nutritionalCoefficient);
            }
            objective.SetMaximization(); // Устанавливаем направление оптимизации - максимизация

            // 4. Добавление ограничений
            // Ограничение по бюджету: sum( Price_j * x_j ) <= budget
            Constraint budgetConstraint = solver.MakeConstraint(0.0, budget, "budget_constraint");
            foreach (var p in products)
            {
                budgetConstraint.SetCoefficient(variables[p], p.Price);
            }

            // (Сюда можно добавить другие ограничения при необходимости, например, по мин/макс нутриентам)
            // var proteinConstraint = solver.MakeConstraint(minProtein, maxProtein, "protein");
            // foreach (var p in products) { proteinConstraint.SetCoefficient(variables[p], p.Protein); }


            // Добавление ограничений по максимальным питательным веществам
            // Белки
            Constraint proteinConstraint = solver.MakeConstraint(0, maxProtein, "Protein");
            foreach (var p in products)
            {
                proteinConstraint.SetCoefficient(variables[p], p.Protein);
            }

            // Жиры
            Constraint fatConstraint = solver.MakeConstraint(0, maxFat, "Fat");
            foreach (var p in products)
            {
                fatConstraint.SetCoefficient(variables[p], p.Fat);
            }

            // Углеводы
            Constraint carbsConstraint = solver.MakeConstraint(0, maxCarbs, "Carbs");
            foreach (var p in products)
            {
                carbsConstraint.SetCoefficient(variables[p], p.Carbs);
            }



            // 5. Вызов решателя
            Solver.ResultStatus resultStatus = solver.Solve();

            // 6. Обработка результата
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                // Решение может быть FEASIBLE (допустимым, но не оптимальным), INFEASIBLE (недопустимым), UNBOUNDED (неограниченным) и т.д.
                // Для простоты выбрасываем исключение, если не найдено строго оптимальное.
                string statusMessage = resultStatus switch
                {
                    Solver.ResultStatus.INFEASIBLE => "Решение не найдено (недопустимо). Проверьте ограничения.",
                    Solver.ResultStatus.UNBOUNDED => "Решение не ограничено. Проверьте целевую функцию и ограничения.",
                    Solver.ResultStatus.FEASIBLE => "Найдено допустимое, но не оптимальное решение.", // Может быть при использовании MIP солвера с лимитом времени
                    _ => "Оптимальное решение не найдено. Статус: " + resultStatus
                };
                throw new Exception(statusMessage);
            }

            // 7. Формирование объекта с результатами
            var result = new SolutionResult
            {
                ProblemType = ProblemType.Maximization,
                Products = products,
                ProductAmounts = new Dictionary<Product, double>(),
                Budget = budget,
                ProteinRequirement = maxProtein,
                FatRequirement = maxFat,
                CarbsRequirement = maxCarbs
            };

            foreach (var p in products)
            {
                // Получаем количество каждого продукта в оптимальном решении
                double amount = variables[p].SolutionValue();
                // Добавляем в результат только те продукты, количество которых больше нуля (или небольшого эпсилон)
                if (amount > 1e-6) // Используем допуск для чисел с плавающей запятой
                {
                    result.ProductAmounts[p] = amount;
                }
                else
                {
                    result.ProductAmounts[p] = 0; // Явно указываем ноль для остальных
                }
            }

            // Рассчитываем итоговые показатели на основе найденных количеств
            result.TotalCost = products.Sum(p => p.Price * result.ProductAmounts[p]);
            result.TotalProtein = products.Sum(p => p.Protein * result.ProductAmounts[p]);
            result.TotalFat = products.Sum(p => p.Fat * result.ProductAmounts[p]);
            result.TotalCarbs = products.Sum(p => p.Carbs * result.ProductAmounts[p]);
            result.NutritionalValue = solver.Objective().Value(); // Сохраняем значение целевой функции


            return result;
        }
    }
}
