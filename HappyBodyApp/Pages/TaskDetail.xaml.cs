using System;
using System.Collections.Generic;
using HappyBodyApp.Models;
using HappyBodyApp.ViewModels;

using Xamarin.Forms;

namespace HappyBodyApp.Pages
{
    public partial class TaskDetail : ContentPage
    {
        public TaskDetail(TodoItem item = null)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(item);
        }
    }
}
