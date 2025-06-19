using System;
using System.Collections.Generic;
using System.Linq;
using DietOptimizer.Models;
using Google.OrTools.LinearSolver;

namespace DietOptimizer.Solvers
{
    


    public class IntegerSimplexSolver
    {
        public SolutionResult SolveMinimization(List<Product> products, double proteinReq, double fatReq, double carbsReq)
        {
            var solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");
            if (solver == null)
                throw new Exception("Не удалось создать решатель");

            var variables = new Dictionary<Product, Variable>();
            foreach (var p in products)
            {
                variables[p] = solver.MakeIntVar(0, 100, p.Name);
            }

            // Целевая функция: минимизация стоимости
            var objective = solver.Objective();
            foreach (var p in products)
            {
                objective.SetCoefficient(variables[p], p.Price);
            }
            objective.SetMinimization();

            // Ограничения по питательным веществам
            var proteinConstraint = solver.MakeConstraint(proteinReq, double.PositiveInfinity, "protein");
            var fatConstraint = solver.MakeConstraint(fatReq, double.PositiveInfinity, "fat");
            var carbsConstraint = solver.MakeConstraint(carbsReq, double.PositiveInfinity, "carbs");

            foreach (var p in products)
            {
                proteinConstraint.SetCoefficient(variables[p], p.Protein);
                fatConstraint.SetCoefficient(variables[p], p.Fat);
                carbsConstraint.SetCoefficient(variables[p], p.Carbs);
            }

            // Решение
            var resultStatus = solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
                throw new Exception("Оптимальное решение не найдено");

            var result = new SolutionResult
            {
                ProblemType = ProblemType.Minimization,
                Products = products,
                ProductAmounts = new Dictionary<Product, double>(),
                TotalCost = solver.Objective().Value(),
                ProteinRequirement = proteinReq,
                FatRequirement = fatReq,
                CarbsRequirement = carbsReq
            };

            foreach (var p in products)
            {
                result.ProductAmounts[p] = (variables[p].SolutionValue());
            }

            result.TotalProtein = products.Sum(p => p.Protein * result.ProductAmounts[p]);
            result.TotalFat = products.Sum(p => p.Fat * result.ProductAmounts[p]);
            result.TotalCarbs = products.Sum(p => p.Carbs * result.ProductAmounts[p]);

            return result;
        }

        public SolutionResult SolveMaximization(List<Product> products, double budget,
            double proteinWeight, double fatWeight, double carbsWeight)
        {
            var solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");
            if (solver == null)
                throw new Exception("Не удалось создать решатель");

            var variables = new Dictionary<Product, Variable>();
            foreach (var p in products)
            {
                variables[p] = solver.MakeIntVar(0, 100, p.Name);
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
    }
}