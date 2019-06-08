using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HappyBody.Core.Models;
using HappyBody.Views;
using HappyBody.ViewModels;
using HappyBody.Views.Meal;

namespace HappyBody.Views
{
    public partial class MealsPage : ContentPage
    {
        MealsViewModel viewModel;

        public MealsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MealsViewModel();
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs args)
        {
            if (!(args.CurrentSelection.SingleOrDefault() is HappyBody.Core.Models.Meal meal))
                return;

            await Navigation.PushAsync(new MealEntryPage(new MealEntryPageViewModel(meal)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MealEntryPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Meals.Count == 0)
                viewModel.LoadMealsCommand.Execute(null);
        }
    }
}