using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage : ContentPage
	{

		public bool SelectedMonsterAlready { get; set; } = false;

		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		public AttackInfo attackinfo { get; set; } = null;

		public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage(bool selectedAlready = false)
		{
			InitializeComponent();

			SelectedMonsterAlready = selectedAlready;
			// Set up the UI to Defaults
			BindingContext = EngineViewModel;

			// Start the Battle Engine
			
				
			FixMonsterListAtRoundStart();

			// Show the New Round Screen
			//ShowModalNewRoundPage();

			// Ask the Game engine to select who goes first
			//EngineViewModel.Engine.CurrentAttacker = null;

			// Game Starts with No Attacker or Defender selected

			// Add Players to Display
			//DrawGameAttackerDefenderBoard();

			//HideUIElements();

			//StartBattleButton.IsVisible = true;
		}

		public void FixMonsterListAtRoundStart()
		{
			EngineViewModel.BattleMonsterList.Clear();
			
			// Load the Characters into the Engine
			foreach (var data in EngineViewModel.Engine.MonsterList)
			{
				EngineViewModel.BattleMonsterList.Add(new PlayerInfoModel(data));
			}
		}

		void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
		{
			PlayerInfoModel data = args.SelectedItem as PlayerInfoModel;

			if (args.SelectedItem == null)
				return;

			if (data.Alive == false)
			{
				return;
			}

			if (SelectedMonsterAlready)
				return;

			
			
			data.SelectedForBattle = true;
			SelectedMonsterAlready = true;
			

			for(int k = 0; k < EngineViewModel.Engine.MonsterList.Count; k++)
			{
				if (data.Guid == EngineViewModel.Engine.MonsterList[k].Guid)
				{
					EngineViewModel.Engine.MonsterList[k].SelectedForBattle = true;
					EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.MonsterList[k];
				}
			}


			//// Manually deselect item.
			//MonsterListView.SelectedItem = null;
			
			Navigation.PushModalAsync(new BattlePage(SelectedMonsterAlready));
		}

		
		


		//public void DrawPlayerBoxes()
		//{
		//	var CharacterBoxList = CharacterBox.Children.ToList();
		//	foreach (var data in CharacterBoxList)
		//	{
		//		CharacterBox.Children.Remove(data);
		//	}

		//	// Draw the Characters
		//	foreach (var data in EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
		//	{
		//		CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
		//	}

		//	var MonsterBoxList = MonsterBox.Children.ToList();
		//	foreach (var data in MonsterBoxList)
		//	{
		//		MonsterBox.Children.Remove(data);
		//	}

		//	// Draw the Monsters
		//	foreach (var data in EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
		//	{
		//		MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
		//	}

		//	// Add one black PlayerInfoDisplayBox to hold space incase the list is empty
		//	CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

		//	// Add one black PlayerInfoDisplayBox to hold space incase the list is empty
		//	MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

		//}


		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void AttackButton_Clicked(object sender, EventArgs e)
		{
			//MessagingCenter.Send(this, "Create", );
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");
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







	}
}