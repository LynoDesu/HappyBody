using System;
using System.Collections.Generic;
using HappyBody.Core.Enums;

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

        public Guid Id { get; set; }
        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public DateTime LastUpdated { get; set; }
        public MealReactions Reaction { get; set; }
    }
}
