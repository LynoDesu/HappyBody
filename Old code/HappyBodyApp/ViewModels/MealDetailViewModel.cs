using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HappyBodyApp.Abstractions;
using HappyBodyApp.Models;
using Xamarin.Forms;

namespace HappyBodyApp.ViewModels
{
    public class MealDetailViewModel : BaseViewModel
    {
        ICloudTable<Meal> table = App.CloudService.GetTable<Meal>();

        public MealDetailViewModel(Meal item = null)
        {
            if (item != null)
            {
                Item = item;
                Title = item.Description;
            }
            else
            {
                Item = new Meal { Description = "New Meal" };
                Title = "New Meal";
            }
        }

        public Meal Item { get; set; }
        public bool ReviewNow { get; set; }

        Command cmdSave;
        public Command SaveCommand => cmdSave ?? (cmdSave = new Command(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (Item.Id == null)
                {
                    await table.CreateItemAsync(Item);
                }
                else
                {
                    await table.UpdateItemAsync(Item);
                }
                MessagingCenter.Send<MealDetailViewModel>(this, "ItemsChanged");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[MealDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command cmdDelete;
        public Command DeleteCommand => cmdDelete ?? (cmdDelete = new Command(async () => await ExecuteDeleteCommand()));

        async Task ExecuteDeleteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (Item.Id != null)
                {
                    await table.DeleteItemAsync(Item);
                }
                MessagingCenter.Send<MealDetailViewModel>(this, "ItemsChanged");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[MealDetail] Save error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}