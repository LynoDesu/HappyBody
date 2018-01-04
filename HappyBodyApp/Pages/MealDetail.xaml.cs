using System;
using System.Collections.Generic;
using HappyBodyApp.Models;
using HappyBodyApp.ViewModels;

using Xamarin.Forms;

namespace HappyBodyApp.Pages
{
    public partial class MealDetail : ContentPage
    {
        public MealDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new MealDetailViewModel(item);
        }
    }
}
