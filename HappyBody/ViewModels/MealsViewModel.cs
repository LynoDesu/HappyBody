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
using System.Windows.Input;
using HappyBody.Core.Enums;

namespace HappyBody.ViewModels
{
    public class MealsViewModel : BaseViewModel
    {
        public IDataStore<Meal> DataStore => DependencyService.Get<IDataStore<Meal>>() ?? new MockDataStore();

        public ObservableCollection<MealViewModel> Meals { get; set; }

        public ICommand LoadMealsCommand { get; }
        public ICommand GoodReactionCommand { get; }
        public ICommand BadReactionCommand { get; }
        public ICommand NeutralReactionCommand { get; }

        public MealsViewModel()
        {
            Title = MealStrings.MealsPageTitle;
            Meals = new ObservableCollection<MealViewModel>();
            LoadMealsCommand = new Command(async () => await ExecuteLoadMealsCommand());
            GoodReactionCommand = new Command<MealViewModel>(async (meal) => await ExecuteReactionCommand(meal, MealReactions.Positive));
            BadReactionCommand = new Command<MealViewModel>(async (meal) => await ExecuteReactionCommand(meal, MealReactions.Negative));
            NeutralReactionCommand = new Command<MealViewModel>(async (meal) => await ExecuteReactionCommand(meal, MealReactions.Neutral));

            MessagingCenter.Subscribe<MealEntryPageViewModel, Guid>(this, MealEntryPageViewModel.MealDeletedMessage, (vm, id) =>
            {
                Meals.Remove(Meals.SingleOrDefault(x => x.Id == id));
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

                Meals.Insert(index, new MealViewModel(meal));
            });
        }

        async Task ExecuteReactionCommand(MealViewModel meal, MealReactions reaction)
        {
            meal.Reaction = reaction;

            var storedMeal = await DataStore.GetItemAsync(meal.Id);
            storedMeal.Reaction = reaction;

            await DataStore.SaveItemAsync(storedMeal);
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
                    Meals.Add(new MealViewModel(item));
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