﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        public CharacterUpdatePage()
        {
            InitializeComponent();
        }

        public async void OnSaveButtonClicked(object sender, EventArgs e)
        {
        }
    }
}