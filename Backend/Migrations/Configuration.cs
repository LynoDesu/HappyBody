namespace Backend.Migrations
{
    using Backend.DataObjects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Backend.Models.MobileServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Backend.Models.MobileServiceContext";
            CommandTimeout = 0;
        }

        protected override void Seed(Backend.Models.MobileServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            List<Meal> meals = new List<Meal>
            {
                new Meal { Id = Guid.NewGuid().ToString(), Description = "Breakfast", MealDate = new DateTime(2018, 01, 01) },
                new Meal { Id = Guid.NewGuid().ToString(), Description = "Lunch", MealDate = new DateTime(2018, 01, 01) },
                new Meal { Id = Guid.NewGuid().ToString(), Description = "Dinner", MealDate = new DateTime(2018, 01, 01) },
            };

            meals[0].Ingredients = new List<Ingredient>()
            {
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Tomato" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Egg" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Bacon" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Bread" },
            };

            meals[1].Ingredients = new List<Ingredient>()
            {
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Pasta" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Pesto" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Chicken" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Tomato" },
            };

            meals[2].Ingredients = new List<Ingredient>()
            {
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Potato" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Beans" },
                new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Cheese" },
            };

            foreach (Meal meal in meals)
            {
                context.Meals.AddOrUpdate(meal);
            }
        }
    }
}
