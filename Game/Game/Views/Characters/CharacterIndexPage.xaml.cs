using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterIndexPage : ContentPage
    {
        public CharacterIndexPage()
        {
            InitializeComponent();
        }

        async void Update_Character (object sender, EventArgs e)
        {
            await DisplayAlert("SwipeView", "update invoked", "ok");
        }

        async void Delete_Character(object sender, EventArgs e)
        {
            await DisplayAlert("SwipeView", "Delete invoked", "ok");
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {

        }
    }
}