using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.Mobile.Server;

namespace Backend.DataObjects
{
    public class Ingredient : EntityData
    {
        public string MealId { get; set; }
        public string Description { get; set; }

        [ForeignKey("MealId")]
        public Meal Meal { get; set; }
    }
}
