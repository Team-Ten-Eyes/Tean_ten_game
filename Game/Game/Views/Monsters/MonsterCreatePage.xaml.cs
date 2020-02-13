using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Models;
using Game.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        public MonsterCreatePage()
        {
            InitializeComponent(); 
        }
    }
}