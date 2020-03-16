using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {

        readonly GenericViewModel<BaseMonster> ViewModel;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        public MonsterUpdatePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            MonsterTypePicker.SelectedItem = data.Data.Attribute.ToString();
        }


        /// <summary>
        /// will sage the monster to the index list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length > 12 || ViewModel.Data.Name.Length < 1)
                await DisplayAlert("Name Too Long", "Must Be Less Than 13 Chars", "OK");
            else
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }


        /// <summary>
        /// this disables the back button for android becuase
        /// the page is modal
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}