using System;
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

            MessagingCenter.Subscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAlreadyExistsMessage, IngredientAlreadyExists);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAddedMessage);
            MessagingCenter.Unsubscribe<MealEntryPageViewModel>(this, MealEntryPageViewModel.IngredientAlreadyExistsMessage);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void IngredientAdded(MealEntryPageViewModel viewModel)
        {
            InputIngredient.Text = string.Empty;
            InputIngredient.Focus();
        }

        async void IngredientAlreadyExists(MealEntryPageViewModel viewModel)
        {
            await DisplayAlert(string.Empty, MealStrings.IngredientAlreadyExists, MealStrings.Ok);
        }
    }
}
