using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using Game.Services;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterEquiped : ContentPage
    {
        readonly GenericViewModel<BaseCharacter> ViewModel;


        public CharacterEquiped(GenericViewModel<BaseCharacter> data)
        {
            InitializeComponent();
            BindingContext = this.ViewModel = data;
        }
    }
}