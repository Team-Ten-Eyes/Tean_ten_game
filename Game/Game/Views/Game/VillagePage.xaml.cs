using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VillagePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public VillagePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Scores
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        void ScoresButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("SU", "Go RedHawks", "OK");
        }

		/// <summary>
		/// Jump to the Monsters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MonstersButton_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("SU", "Go RedHawks", "OK");
		}

		/// <summary>
		/// Jump to the Characters
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CharactersButton_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("SU", "Go RedHawks", "OK");
		}

		/// <summary>
		/// Jump to the Items
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ItemsButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ItemIndexPage());
		}
	}
}