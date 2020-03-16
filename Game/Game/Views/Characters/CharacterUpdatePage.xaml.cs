using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {

        readonly GenericViewModel<BaseCharacter> ViewModel;

        // Empty Constructor for UTs
        public CharacterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Base constructor for Char Update Page
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            CharacterTypePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save button helper which sends to the message center an update request with context data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length > 12 || ViewModel.Data.Name.Length < 1)
                await DisplayAlert("Name Invalid", "Must Be Less Than 13 Chars And More Than 0", "OK");
            else
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Android back button supression
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        /// <summary>
        /// Cancel button helper to pop the current page from the modal stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}