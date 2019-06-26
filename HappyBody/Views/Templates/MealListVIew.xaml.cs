using System;
using System.Collections.Generic;
using HappyBody.ViewModels;
using Xamarin.Forms;

namespace HappyBody.Views.Templates
{
    public partial class MealListView : ContentView
    {
        private MealViewModel _meal;

        public static BindableProperty MealsVmProperty = BindableProperty.Create(
            propertyName: nameof(MealsVm),
            returnType: typeof(MealsViewModel),
            declaringType: typeof(ContentView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneTime,
            propertyChanged: HandleMealsVmPropertyChanged);

        private static void HandleMealsVmPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var listView = bindable as MealListView;
            var mealsVm = newValue as MealsViewModel;
            if (mealsVm != null)
            {
                listView.buttonBadReaction.Command = mealsVm.BadReactionCommand;
                listView.buttonGoodReaction.Command = mealsVm.GoodReactionCommand;
                listView.buttonNeutralReaction.Command = mealsVm.NeutralReactionCommand;
            }
            else
            {
                listView.buttonBadReaction.Command = null;
                listView.buttonGoodReaction.Command = null;
                listView.buttonNeutralReaction.Command = null;
            }
        }

        public MealsViewModel MealsVm
        {
            get { return (MealsViewModel)GetValue(MealsVmProperty); }
            set { SetValue(MealsVmProperty, value); }
        }

        public MealListView()
        {
            InitializeComponent();
            BindingContextChanged += MealListView_BindingContextChanged;
        }

        private void MealListView_BindingContextChanged(object sender, EventArgs e)
        {
            if (_meal != null)
            {
                _meal.PropertyChanged -= Meal_PropertyChanged;
            }

            _meal = BindingContext as MealViewModel;

            if (BindingContext != null)
            {
                _meal.PropertyChanged += Meal_PropertyChanged;
            }
        }

        private void Meal_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_meal.Reaction))
            {
                switch (_meal.Reaction)
                {
                    case Core.Enums.MealReactions.Negative:
                        buttonGoodReaction.IsVisible = false;
                        buttonNeutralReaction.IsVisible = false;
                        break;
                    case Core.Enums.MealReactions.Neutral:
                        buttonGoodReaction.IsVisible = false;
                        buttonBadReaction.IsVisible = false;
                        break;
                    case Core.Enums.MealReactions.Positive:
                        buttonBadReaction.IsVisible = false;
                        buttonNeutralReaction.IsVisible = false;
                        break;
                }
            }
        }
    }
}
