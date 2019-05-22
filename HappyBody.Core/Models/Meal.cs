using System;
using System.Collections.Generic;

namespace HappyBody.Core.Models
{
    public class Meal : BaseModel
    {
        public Meal()
        {
            MealDate = DateTime.Now;
            Ingredients = new List<Ingredient>();
        }

        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
