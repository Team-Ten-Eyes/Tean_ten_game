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
        public MonsterCreatePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            data.Data = new BaseMonster();

            BindingContext = this.ViewModel = data;

            MonsterTypePicker.SelectedItem = data.Data.Attribute.ToString();
        }


        /// <summary>
        /// will save the monster the user just created the list
        /// </summary>
        /// <returns></returns>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
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