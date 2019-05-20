using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HappyBody.Core.Models;
using HappyBody.Localisation;
using Xamarin.Forms;

namespace HappyBody.ViewModels
{
    public class MealEntryPageViewModel : BaseViewModel
    {
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

        public ICommand SaveMealCommand { get; set; }

        public MealEntryPageViewModel()
        {
            Meal = new Meal { Description = MealStrings.DefaultMealText };
            Initialise();
        }

        public MealEntryPageViewModel(Meal meal = null)
        {
            Meal = meal;

            Initialise();
        }

        void Initialise()
        {
            Title = MealStrings.MealEntryTitle;

            SaveMealCommand = new Command(async () => await ExecuteSaveCommand());
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
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}

