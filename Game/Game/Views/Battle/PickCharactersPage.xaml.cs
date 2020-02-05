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
		async void BattleButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
			await Navigation.PopAsync();
		}
	}
}