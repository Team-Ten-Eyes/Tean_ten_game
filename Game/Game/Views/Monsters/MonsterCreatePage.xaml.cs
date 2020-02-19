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
    public partial class MonsterCreatePage : ContentPage
    {
        GenericViewModel<BaseMonster> ViewModel { get; set; }

        /// <summary>
        /// Base Constructor for the Monster Create Page
        /// </summary>
        /// <param name="data"></param>
        public MonsterCreatePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            data.Data = new BaseMonster();

            BindingContext = this.ViewModel = data;

            MonsterTypePicker.SelectedItem = data.Data.Attribute.ToString();
        }


        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.Data.Name.Length > 12)
                await DisplayAlert("Name Too Long", "Must Be Less Than 13 Chars", "OK");
            else {
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// the cancle button with back out of the create page and 
        /// Direct the user back to the list page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// this suppresses the back button on android becuase it is a modal page
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}