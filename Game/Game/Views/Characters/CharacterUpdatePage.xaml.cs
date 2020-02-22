using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {

        readonly GenericViewModel<BaseCharacter> ViewModel;

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
            if (ViewModel.Data.Name.Length > 12)
                await DisplayAlert("Name Too Long", "Must Be Less Than 13 Chars", "OK");
            else {
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