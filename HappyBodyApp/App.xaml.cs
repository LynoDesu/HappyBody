using System;
using HappyBodyApp.Abstractions;
using HappyBodyApp.Services;
using Xamarin.Forms;

namespace HappyBodyApp
{
    public partial class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public App()
        {
            CloudService = new AzureCloudService();
            MainPage = new NavigationPage(new Pages.EntryPage());
        }
    }
}
