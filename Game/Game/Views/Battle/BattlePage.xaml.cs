using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{

		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;



		public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			
				InitializeComponent();

				// Set up the UI to Defaults
				BindingContext = EngineViewModel;

				// Start the Battle Engine
				EngineViewModel.Engine.StartBattle(false);

				// Show the New Round Screen
				ShowModalNewRoundPage();

				// Ask the Game engine to select who goes first
				EngineViewModel.Engine.CurrentAttacker = null;

				// Game Starts with No Attacker or Defender selected

				// Add Players to Display
				DrawGameAttackerDefenderBoard();

				HideUIElements();

				StartBattleButton.IsVisible = true;
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