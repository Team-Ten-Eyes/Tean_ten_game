using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickCharactersPage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PickCharactersPage()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Battle
		/// 
		/// Its Modal because don't want user to come back...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BattleButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));
			Navigation.PopAsync();
		}

		/// <summary>
		/// Quit the Battle
		/// 
		/// Quitting out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void QuitButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}