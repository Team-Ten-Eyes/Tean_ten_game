using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Game.Services;
using System.Linq;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        GenericViewModel<BaseMonster> ViewModel { get; set; }

        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

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

            //This is the creation of the character image selection
            ObservableCollection<ImagePickerModel> imageList = new ObservableCollection<ImagePickerModel>();

            foreach (ImagePickerModel image in DefaultData.AllMonsterImages())
            {
                imageList.Add(image);
            }
            ImageView.ItemsSource = imageList;

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
            else
            {
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


        /// <summary>
        /// This is a function that once an image is picked it will change the character to 
        /// that image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnCharacterImageSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var image = args.SelectedItem as ImagePickerModel;
            ViewModel.Data.ImageURI = image.Url;

     
            // = image.Url;
        }
    }

}