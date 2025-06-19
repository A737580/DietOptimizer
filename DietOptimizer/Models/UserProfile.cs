namespace DietOptimizer.Models
{
    public class UserProfile
    {
        public Gender Gender { get; set; } = Gender.Male;
        public int Age { get; set; } = 30;
        public double WeightKg { get; set; } = 70;
        public double HeightCm { get; set; } = 175;
        public ActivityLevel ActivityLevel { get; set; } = ActivityLevel.LightlyActive;

        public double TargetCalories { get; set; }
        public double MinProteinGrams { get; set; }
        public double MaxProteinGrams { get; set; }
        public double MinFatGrams { get; set; }
        public double MaxFatGrams { get; set; }
        public double MinCarbsGrams { get; set; }
        public double MaxCarbsGrams { get; set; }

        public UserProfile() { }

        public UserProfile(Gender gender, int age, double weightKg, double heightCm, ActivityLevel activityLevel)
        {
            Gender = gender;
            Age = age;
            WeightKg = weightKg;
            HeightCm = heightCm;
            ActivityLevel = activityLevel;
        }
    }
}
