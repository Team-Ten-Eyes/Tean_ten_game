using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonstersInTheNextBattle : ContentPage
    {

        readonly MonsterViewModel ViewModel;
        public MonstersInTheNextBattle()
        {
            InitializeComponent();
            BindingContext = ViewModel = MonsterViewModel.Instance;
        }
    }
}