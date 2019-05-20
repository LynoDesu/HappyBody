using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyBody.Core.Models;
using HappyBody.Localisation;
using HappyBody.ViewModels;
using Xamarin.Forms;

namespace HappyBodyApp.ViewModels
{
    public class MealEntryPageViewModel : BaseViewModel
    {
        public Meal Meal { get; set; }
        public bool ReviewNow { get; set; }

        public ICommand SaveMealCommand;
        public ICommand DeleteMealCommand;

        public MealEntryPageViewModel()
        {
            Title = MealStrings.MealEntryTitle;

            Meal = new Meal { Description = MealStrings.DefaultMealText };

            SaveMealCommand = new Command(async () => await ExecuteSaveCommand());
            DeleteMealCommand = new Command(async () => await ExecuteDeleteCommand());
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
            }
        }

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //TODO: 
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
    }
}
}
