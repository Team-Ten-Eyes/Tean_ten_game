﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
       

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterIndexPage : ContentPage
    {
        readonly MonsterViewModel ViewModel;
        public MonsterIndexPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = MonsterViewModel.Instance;
        }
    }
}