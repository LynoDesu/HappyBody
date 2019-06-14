using System;
using System.Collections.Generic;

namespace HappyBody.Core.Models
{
    public class Meal
    {
        public Meal()
        {
            Id = Guid.NewGuid();
            MealDate = DateTime.Now;
            Ingredients = new List<Ingredient>();
        }

        public Guid Id { get; private set; }
        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public string IngredientsText => $"{Ingredients.Count} Ingredients";
        public string TitleText => $"{Description} - {MealDate.ToString("D")}";
    }
}
