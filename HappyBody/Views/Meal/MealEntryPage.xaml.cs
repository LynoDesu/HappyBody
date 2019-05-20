using System;
using System.Collections.Generic;
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

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
