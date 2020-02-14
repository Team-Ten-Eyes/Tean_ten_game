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
    public partial class MonsterUpdatePage : ContentPage
    {

        readonly GenericViewModel<BaseMonster> ViewModel;

        /// <summary>
        /// Constructor for Update binds the new BaseMonster
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<BaseMonster> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }

        /// <summary>
        /// Event handler for save button interaction. Sends Update and pops page
        /// </summary>
        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }
        /// <summary>
        /// Android back button supression
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}