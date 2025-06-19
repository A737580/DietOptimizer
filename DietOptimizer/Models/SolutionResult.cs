
namespace DietOptimizer.Models
{
    public class SolutionResult
    {
        public ProblemType ProblemType { get; set; }
        public List<Product> Products { get; set; }
        public Dictionary<Product, double> ProductAmounts { get; set; } = new Dictionary<Product, double>(); // сделать тип double и удалить FractionalProductAmounts
        public double TotalCost { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbs { get; set; }
        public double ProteinRequirement { get; set; }
        public double FatRequirement { get; set; }
        public double CarbsRequirement { get; set; }
        public double Budget { get; set; }
        public double ProteinWeight { get; set; }
        public double FatWeight { get; set; }
        public double CarbsWeight { get; set; }
        public double NutritionalValue { get; set; }
    }

}
