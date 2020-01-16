using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GamePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Dungeon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        void DungeonButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("SU", "Go RedHawks", "OK");
        }

		/// <summary>
		/// Jump to the Village
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void VillageButton_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("SU", "Go RedHawks", "OK");
		}

	}
}