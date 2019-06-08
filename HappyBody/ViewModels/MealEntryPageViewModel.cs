using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyBody.Core.Models;
using HappyBody.Localisation;
using Xamarin.Forms;

namespace HappyBody.ViewModels
{
    public class MealEntryPageViewModel : BaseViewModel
    {
        public const string IngredientAddedMessage = "IngredientAdded";
        public const string IngredientAlreadyExistsMessage = "IngredientAlreadyExists";

        Meal _meal;
        public Meal Meal 
        {
            get => _meal;
            set => SetProperty(ref _meal, value);
        }

        bool _reviewNow;
        public bool ReviewNow 
        {
            get => _reviewNow;
            set => SetProperty(ref _reviewNow, value);
        }

        ObservableCollection<Ingredient> _ingredients;
        public ObservableCollection<Ingredient> Ingredients
        {
            get => _ingredients;
            set => SetProperty(ref _ingredients, value);
        }

        public ICommand SaveMealCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }

        public MealEntryPageViewModel()
        {
            Meal = new Meal { Description = MealStrings.DefaultMealText };
            Ingredients = new ObservableCollection<Ingredient>();

            Initialise();
        }

        public MealEntryPageViewModel(Meal meal = null)
        {
            Meal = meal;

            if (Meal?.Ingredients?.Any() == true)
            {
                Ingredients = new ObservableCollection<Ingredient>(Meal.Ingredients);
            }

            Initialise();
        }

        void Initialise()
        {
            Title = MealStrings.MealEntryTitle;

            SaveMealCommand = new Command(async () => await ExecuteSaveCommand());
            AddIngredientCommand = new Command<string>((ingredient) => ExecuteAddIngredient(ingredient));
        }

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //TODO: Save Meal
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{nameof(MealEntryPageViewModel)}] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                await Shell.Current.Navigation.PopModalAsync();
            }
        }

        void ExecuteAddIngredient(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var ingredient = text.Trim();
                if (!Ingredients.Any(x => x.Description.Equals(ingredient, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Ingredients.Add(new Ingredient { Description = ingredient });
                    MessagingCenter.Send(this, IngredientAddedMessage);
                }
                else
                {
                    MessagingCenter.Send(this, IngredientAlreadyExistsMessage);
                }
            }
        }
    }
}

