using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			DisplayAlert("SU", "Attck !!!", "OK");
		}

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void BattleOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ScorePage());
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Quit the Battle
		/// 
		/// Quitting out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void QuitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
				MessagingCenter.Send(this, "Quit", true);
				await Navigation.PopModalAsync();
			}
		}
	}
}