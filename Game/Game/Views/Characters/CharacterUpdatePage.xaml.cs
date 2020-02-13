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
        public CharacterUpdatePage(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}