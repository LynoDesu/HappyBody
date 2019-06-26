using System;
using System.Collections.Generic;
using HappyBody.Core.Enums;
using HappyBody.Core.Models;

namespace HappyBody.ViewModels
{
    public class MealViewModel : BaseViewModel
    {
        public MealViewModel()
        {
            Ingredients = new List<Ingredient>();
        }

        public MealViewModel(Meal meal)
            : this()
        {
            Id = meal.Id;
            MealDate = meal.MealDate;
            Description = meal.Description;
            ImgFilename = meal.ImgFilename;
            Ingredients = meal.Ingredients;
            LastUpdated = meal.LastUpdated;
            Reaction = meal.Reaction;
        }

        public Guid Id { get; private set; }
        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public DateTime LastUpdated { get; set; }
        public MealReactions Reaction { get; set; }

        public string IngredientsText => $"{Ingredients.Count} Ingredients";
        public string TitleText => $"{Description} - {MealDate.ToString("D")}";
    }
}
