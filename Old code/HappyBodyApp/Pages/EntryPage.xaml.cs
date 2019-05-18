using System;
using System.Collections.Generic;
using HappyBodyApp.ViewModels;

using Xamarin.Forms;

namespace HappyBodyApp.Pages
{
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            InitializeComponent();
            BindingContext = new EntryPageViewModel();
        }
    }
}
