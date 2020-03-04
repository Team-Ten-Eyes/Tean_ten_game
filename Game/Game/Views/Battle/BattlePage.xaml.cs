using System;
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
			DisplayAlert("Attack!!!", "Attack !!!", "OK");
		}

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void RoundOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RoundOverPage());
		}


		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NewRoundButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewRoundPage());
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
		}

		/// <summary>
		/// Battle Over, so Exit Button
		/// Need to show this for the user to click on.
		/// The Quit does a prompt, exit just exits
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void ExitButton_Clicked(object sender, EventArgs e)
		{
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
				await Navigation.PopModalAsync();
			}
		}

		async void Item_Pick_Page(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PickItemsPage());
		}

		void Mon1Targeted(object sender, EventArgs e)
		{
			Mon1XHair.IsVisible = true;
			Mon2XHair.IsVisible = false;
			Mon3XHair.IsVisible = false;
			Mon4XHair.IsVisible = false;
			Mon5XHair.IsVisible = false;
		}
		void Mon2Targeted(object sender, EventArgs e)
		{
			Mon1XHair.IsVisible = false;
			Mon2XHair.IsVisible = true;
			Mon3XHair.IsVisible = false;
			Mon4XHair.IsVisible = false;
			Mon5XHair.IsVisible = false;
		}
		void Mon3Targeted(object sender, EventArgs e)
		{
			Mon1XHair.IsVisible = false;
			Mon2XHair.IsVisible = false;
			Mon3XHair.IsVisible = true;
			Mon4XHair.IsVisible = false;
			Mon5XHair.IsVisible = false;
		}

		void Mon4Targeted(object sender, EventArgs e)
		{
			Mon1XHair.IsVisible = false;
			Mon2XHair.IsVisible = false;
			Mon3XHair.IsVisible = false;
			Mon4XHair.IsVisible = true;
			Mon5XHair.IsVisible = false;
		}

		void Mon5Targeted(object sender, EventArgs e)
		{
			Mon1XHair.IsVisible = false;
			Mon2XHair.IsVisible = false;
			Mon3XHair.IsVisible = false;
			Mon4XHair.IsVisible = false;
			Mon5XHair.IsVisible = true;
		}


	}
}