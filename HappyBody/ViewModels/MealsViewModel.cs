using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HappyBody.Models;
using HappyBody.Views;
using HappyBody.Core.Models;
using HappyBody.Services;
using HappyBody.Localisation;
using System.Collections.Generic;
using System.Linq;

namespace HappyBody.ViewModels
{
    public class MealsViewModel : BaseViewModel
    {
        public IDataStore<Meal> DataStore => DependencyService.Get<IDataStore<Meal>>() ?? new MockDataStore();

        public ObservableCollection<Meal> Meals { get; set; }
        public Command LoadMealsCommand { get; set; }

        public MealsViewModel()
        {
            Title = MealStrings.MealsPageTitle;
            Meals = new ObservableCollection<Meal>();
            LoadMealsCommand = new Command(async () => await ExecuteLoadMealsCommand());

            MessagingCenter.Subscribe<MealEntryPageViewModel, Meal>(this, MealEntryPageViewModel.MealDeletedMessage, (vm, meal) =>
            {
                Meals.Remove(meal);
            });

            MessagingCenter.Subscribe<MealEntryPageViewModel, Meal>(this, MealEntryPageViewModel.MealSavedMessage, (vm, meal) =>
            {
                int index = 0;

                var exisingMeal = Meals.SingleOrDefault(x => x.Id == meal.Id);
                if (exisingMeal != null)
                {
                    index = Meals.IndexOf(exisingMeal);
                    Meals.Remove(exisingMeal);
                }

                Meals.Insert(index, meal);
            });
        }

        async Task ExecuteLoadMealsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Meals.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}