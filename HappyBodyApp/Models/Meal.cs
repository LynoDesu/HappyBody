using System;
using System.Collections.Generic;
using System.Reflection;
using HappyBodyApp.Abstractions;

namespace HappyBodyApp.Models
{
    public class Meal : TableData
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
