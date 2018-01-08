using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Backend.DataObjects;
using Backend.Models;
using Owin;

namespace Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();
                
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var mobileConfig = new MobileAppConfiguration();

            mobileConfig
                .AddTablesWithEntityFramework()
                .ApplyTo(config);

            Database.SetInitializer(new MobileServiceInitializer());

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseAlways<MobileServiceContext>
    {
        public override void InitializeDatabase(MobileServiceContext context)
        {
            if (context.Database.Exists())
            {
                try
                {
                    if (!context.Database.CompatibleWithModel(true))
                    {
                        context.Database.Delete();
                    }
                }
                catch (Exception)
                {
                    context.Database.Delete();
                }
            }
            context.Database.Create();
        }

        //protected override void Seed(MobileServiceContext context)
        //{
        //    List<Meal> meals = new List<Meal>
        //    {
        //        new Meal { Id = Guid.NewGuid().ToString(), Description = "Breakfast", MealDate = new DateTime(2018, 01, 01) },
        //        new Meal { Id = Guid.NewGuid().ToString(), Description = "Lunch", MealDate = new DateTime(2018, 01, 01) },
        //        new Meal { Id = Guid.NewGuid().ToString(), Description = "Dinner", MealDate = new DateTime(2018, 01, 01) },
        //    };

        //    meals[0].Ingredients = new List<Ingredient>()
        //    {
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Tomato" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Egg" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Bacon" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Bread" },
        //    };

        //    meals[1].Ingredients = new List<Ingredient>()
        //    {
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Pasta" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Pesto" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Chicken" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Tomato" },
        //    };

        //    meals[2].Ingredients = new List<Ingredient>()
        //    {
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Potato" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Beans" },
        //        new Ingredient() { Id = Guid.NewGuid().ToString(), Description = "Cheese" },
        //    };

        //    foreach (Meal meal in meals)
        //    {
        //        context.Meals.Add(meal);
        //        context.Set<Meal>().Add(meal);
        //    }

        //    base.Seed(context);
        //}
    }
}