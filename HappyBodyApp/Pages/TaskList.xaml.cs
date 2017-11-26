using System;
using System.Collections.Generic;
using HappyBodyApp.ViewModels;

using Xamarin.Forms;

namespace HappyBodyApp.Pages
{
    public partial class TaskList : ContentPage
    {
        public TaskList()
        {
            InitializeComponent();
            BindingContext = new TaskListViewModel();
        }
    }
}
