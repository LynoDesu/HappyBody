using System;
using System.Collections.Generic;
using HappyBodyApp.ViewModels;

using Xamarin.Forms;

namespace HappyBodyApp.Pages
{
    public partial class MealList : ContentPage
    {
        public MealList()
        {
            InitializeComponent();
            BindingContext = new MealListViewModel();
        }
    }
}
