using System;
using HappyBodyApp.Abstractions;

namespace HappyBodyApp.Models
{
    public class Ingredient : TableData
    {
        public Ingredient()
        {
        }

        public int MealId { get; set; }
        public string Description { get; set; }
    }
}
