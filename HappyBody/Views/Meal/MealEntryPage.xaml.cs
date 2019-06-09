using System;
using System.Threading.Tasks;
using HappyBody.Localisation;
using HappyBody.ViewModels;
using Xamarin.Forms;

namespace HappyBody.Views.Meal
{
    public partial class MealEntryPage : ContentPage
    {
        public MealEntryPage()
        {
            InitializeComponent();
        }

        public MealEntryPage(MealEntryPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAddedMessage, IngredientAdded);
            MessagingCenter.Subscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.MealSavedMessage, async (x) => await DismissPage());
            MessagingCenter.Subscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAlreadyExistsMessage, async (x) => await ShowIngredientExistsDialog());
            MessagingCenter.Subscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.MealSaveErrorMessage, async (x) => await ShowErrorSavingMealDialog());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAddedMessage);
            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAlreadyExistsMessage);
            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.MealSaveErrorMessage);
            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.MealSavedMessage);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await DismissPage();
        }

        async Task DismissPage()
        {
            if (Shell.Current.Navigation.ModalStack.Count > 0)
            {
                await Shell.Current.Navigation.PopModalAsync();
            }
            else
            {
                await Shell.Current.Navigation.PopAsync();
            }
        }

        void IngredientAdded(MealEntryPageViewModel viewModel)
        {
            InputIngredient.Text = string.Empty;
            InputIngredient.Focus();
        }

        async Task ShowIngredientExistsDialog()
        {
            await DisplayAlert(string.Empty, MealStrings.IngredientAlreadyExists, MealStrings.Ok);
        }

        async Task ShowErrorSavingMealDialog()
        {
            await DisplayAlert(MealStrings.Error, MealStrings.ProblemSavingMeal, MealStrings.Ok);
        }
    }
}
