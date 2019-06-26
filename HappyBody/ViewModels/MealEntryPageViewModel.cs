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

        public MealViewModel Meal { get; set; }
        public bool ReviewNow { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public ICommand SaveMealCommand { get; private set; }
        public ICommand AddIngredientCommand { get; private set; }
        public ICommand DeleteMealCommand { get; private set; }
        public ICommand DeleteIngredientCommand { get; private set; }

        public MealEntryPageViewModel()
        {
            Meal = new MealViewModel { Description = MealStrings.DefaultMealText };
            Ingredients = new ObservableCollection<Ingredient>();

            Initialise();
        }

        public MealEntryPageViewModel(MealViewModel meal = null)
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
                bool isNew = Meal.Id == Guid.Empty;

                var mealDoc = new Meal
                {
                    Description = Meal.Description,
                    ImgFilename = Meal.ImgFilename,
                    Ingredients = Ingredients.ToList(),
                    LastUpdated = Meal.LastUpdated,
                    MealDate = Meal.MealDate,
                    Reaction = Meal.Reaction
                };

                if (isNew)
                {
                    await _dataStore.AddItemAsync(mealDoc);
                }
                else
                {
                    mealDoc.Id = Meal.Id;
                    await _dataStore.SaveItemAsync(mealDoc);
                }

                MessagingCenter.Send(this, MealSavedMessage, mealDoc);
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
                MessagingCenter.Send(this, MealDeletedMessage, Meal.Id);
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

