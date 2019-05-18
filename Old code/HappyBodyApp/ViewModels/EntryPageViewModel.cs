using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HappyBodyApp.Abstractions;
using HappyBodyApp.Helpers;
using HappyBodyApp.Services;
using Xamarin.Forms;

namespace HappyBodyApp.ViewModels
{
    public class EntryPageViewModel : BaseViewModel
    {
        public EntryPageViewModel()
        {
            Title = "Add Meal";
        }

        Command loginCmd;
        public Command LoginCommand => loginCmd ?? (loginCmd = new Command(async () => await ExecuteLoginCommand()));

        async Task ExecuteLoginCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var cloudService = new AzureCloudService();
                var user = await cloudService.LoginAsync();

                if (user != null)
                {
                    Application.Current.MainPage = new NavigationPage(new Pages.MealList());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Login] Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
