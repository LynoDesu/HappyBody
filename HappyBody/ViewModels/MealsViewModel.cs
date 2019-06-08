using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using HappyBody.Models;
using HappyBody.Views;
using HappyBody.Core.Models;
using HappyBody.Services;
using HappyBody.Localisation;

namespace HappyBody.ViewModels
{
    public class MealsViewModel : BaseViewModel
    {
        public IDataStore<Meal> DataStore => DependencyService.Get<IDataStore<Meal>>() ?? new MockDataStore();

        public ObservableCollection<Meal> Meals { get; set; }
        public Command LoadMealsCommand { get; set; }

        public MealsViewModel()
        {
            Title = MealStrings.MealsPageTitle;
            Meals = new ObservableCollection<Meal>();
            LoadMealsCommand = new Command(async () => await ExecuteLoadMealsCommand());
        }

        async Task ExecuteLoadMealsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Meals.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Meals.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}