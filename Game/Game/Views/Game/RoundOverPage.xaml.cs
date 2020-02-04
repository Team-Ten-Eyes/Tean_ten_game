using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoundPage: ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RoundPage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Start next Round, returning to the battle screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NextButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}