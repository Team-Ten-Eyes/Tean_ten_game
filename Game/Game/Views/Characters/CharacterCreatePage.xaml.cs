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
    public partial class CharacterCreatePage : ContentPage
    {
        GenericViewModel<BaseCharacter> ViewModel { get; set; }

        /// <summary>
        /// Base constructor for the create page
        /// </summary>
        public CharacterCreatePage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();

            data.Data = new BaseCharacter();

            BindingContext = this.ViewModel = data;

            CharacterTypePicker.SelectedItem = data.Data.Attribute.ToString();

            itemPickerModel ImageSelect = new itemPickerModel();
           

        }

        /// <summary>
        /// Save current data binding when the save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length > 12)
                await DisplayAlert("Name Too Long", "Must Be Less Than 13 Chars", "OK");
            else
            {
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }
        /// <summary>
        /// Override of back button for android
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        /// <summary>
        /// the cancel button with back out of the create page and 
        /// Direct the user back to the list page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}