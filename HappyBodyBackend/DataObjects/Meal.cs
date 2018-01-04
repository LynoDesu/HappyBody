using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Meal : EntityData
    {
        public Meal()
        {
        }

        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
