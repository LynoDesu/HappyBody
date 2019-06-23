using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyBody.Core.Models;
using HappyBody.Localisation;
using HappyBody.Services;
using Xamarin.Forms;

namespace HappyBody.ViewModels
{
    public class MealEntryPageViewModel : BaseViewModel
    {
        public const string IngredientAddedMessage = "IngredientAdded";
        public const string IngredientAlreadyExistsMessage = "IngredientAlreadyExists";
        public const string MealSaveErrorMessage = "MealSaveError";
        public const string MealSavedMessage = "MealSaved";
        public const string MealDeletedMessage = "MealDeleted";

        private IDataStore<Meal> _dataStore;

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
        public ICommand DeleteMealCommand { get; set; }
        public ICommand DeleteIngredientCommand { get; set; }

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
            _dataStore = DependencyService.Resolve<IDataStore<Meal>>();

            Title = MealStrings.MealEntryTitle;

            SaveMealCommand = new Command(async () => await ExecuteSaveCommand());
            AddIngredientCommand = new Command<string>((ingredient) => ExecuteAddIngredient(ingredient));
            DeleteMealCommand = new Command(async () => await ExecuteDeleteCommand());
            DeleteIngredientCommand = new Command<Ingredient>(ExecuteDeleteIngredientCommand);
        }

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Meal.Ingredients.Clear();
                Meal.Ingredients.AddRange(Ingredients);
                await _dataStore.SaveItemAsync(Meal);
                MessagingCenter.Send(this, MealSavedMessage, Meal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{nameof(MealEntryPageViewModel)}] Save error: {ex.Message}");
                MessagingCenter.Send(this, MealSaveErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await _dataStore.DeleteItemAsync(Meal.Id);
                MessagingCenter.Send(this, MealDeletedMessage, Meal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{nameof(MealEntryPageViewModel)}] Delete error: {ex.Message}");
                MessagingCenter.Send(this, MealSaveErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ExecuteDeleteIngredientCommand(Ingredient ingredient)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Ingredients.Remove(ingredient);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{nameof(MealEntryPageViewModel)}] Delete error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void ExecuteAddIngredient(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var ingredient = text.Trim();
                if (!Ingredients.Any(x => x.Description.Equals(ingredient, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Ingredients.Add(new Ingredient { Description = ingredient, MealId = Meal.Id });
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

