using System;

namespace HappyBody.Core.Models
{
    public class Ingredient : BaseModel
    {
        public Ingredient()
        {
        }

        public Guid MealId { get; set; }
        public string Description { get; set; }
    }
}
