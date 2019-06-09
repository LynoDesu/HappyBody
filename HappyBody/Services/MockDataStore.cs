using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyBody.Core.Models;
using HappyBody.Models;

namespace HappyBody.Services
{
    public class MockDataStore : IDataStore<Meal>
    {
        List<Meal> meals;

        public MockDataStore()
        {
            meals = new List<Meal>();
            var mockItems = new List<Meal>
            {
                new Meal { Description = "Lunch", MealDate = new DateTime(2019, 05, 20), 
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Description = "Carrots" },
                        new Ingredient { Description = "Broccoli" },
                        new Ingredient { Description = "Gravy" },
                        new Ingredient { Description = "Beef" },
                    } 
                },
                new Meal { Description = "Dinner", MealDate = new DateTime(2019, 05, 21),
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Description = "Ham" },
                        new Ingredient { Description = "Bread" },
                        new Ingredient { Description = "Butter" },
                        new Ingredient { Description = "Cheese" },
                    }
                },
                new Meal { Description = "Breakfast", MealDate = new DateTime(2019, 05, 21),
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Description = "Eggs" },
                        new Ingredient { Description = "Bacon" },
                        new Ingredient { Description = "Butter" }
                    }
                },
            };

            meals.AddRange(mockItems);
        }

        public async Task<bool> AddItemAsync(Meal meal)
        {
            meals.Add(meal);

            return await Task.FromResult(true);
        }

        public async Task<bool> SaveItemAsync(Meal meal)
        {
            var oldItem = meals.SingleOrDefault(x => x.Id == meal.Id);
            if (oldItem != null)
            {
                meals.Remove(oldItem);
            }

            meals.Add(meal);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var oldItem = meals.SingleOrDefault(x => x.Id == id);
            meals.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Meal> GetItemAsync(Guid id)
        {
            return await Task.FromResult(meals.SingleOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<Meal>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(meals.OrderByDescending(x => x.MealDate));
        }
    }
}