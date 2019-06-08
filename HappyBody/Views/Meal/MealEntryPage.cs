using System;

using Xamarin.Forms;

namespace HappyBody.Views.Meal
{
    public class MealEntryPage : ContentPage
    {
        public MealEntryPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

