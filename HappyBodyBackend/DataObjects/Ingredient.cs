using System;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Ingredient : EntityData
    {
        public Ingredient()
        {
        }

        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
        public string Description { get; set; }
    }
}
