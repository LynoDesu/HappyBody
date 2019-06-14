using System;

namespace HappyBody.Core.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid MealId { get; set; }
        public string Description { get; set; }
    }
}
