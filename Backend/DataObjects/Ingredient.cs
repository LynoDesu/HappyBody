using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Ingredient : EntityData
    {
        public int MealId { get; set; }
        public string Description { get; set; }

        public virtual Meal Meal { get; set; }
    }
}
