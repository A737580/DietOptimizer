using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietOptimizer.Models
{
    public enum ProblemType { Minimization, Maximization }

    public enum Gender
    {
        Male,
        Female
    }

    public enum ActivityLevel
    {
        Sedentary,     // Малоподвижный (сидячая работа, нет физ.нагрузок)
        LightlyActive, // Легкая активность (тренировки 1-3 раза в неделю)
        ModeratelyActive,// Умеренная активность (тренировки 3-5 раз в неделю)
        VeryActive,    // Высокая активность (интенсивные тренировки 6-7 раз в неделю)
        ExtraActive    // Очень высокая активность (тяжелая физ. работа, проф. спорт)
    }
}
