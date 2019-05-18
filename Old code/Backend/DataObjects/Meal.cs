using System;
using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Meal : EntityData
    {
        public DateTime MealDate { get; set; }
        public string Description { get; set; }
        public string ImgFilename { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
