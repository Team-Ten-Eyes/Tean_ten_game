﻿using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;
        public bool MonstahSelected = false;
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = EngineViewModel;

            if (EngineViewModel.Engine.BattleScore.RoundCount < 1)
            {
                // Start the Battle Engine
                EngineViewModel.Engine.StartBattle(false);
            }
            else
            {
                EngineViewModel.Engine.EndRound();

                // Populate New Monsters...
                EngineViewModel.Engine.AddMonstersToRound();
                EngineViewModel.Engine.populatePotionsList();
                var ListOrder = 0;

                
                foreach (var data in EngineViewModel.Engine.MonsterList)
                {
                    if (data.Alive)
                    {
                        EngineViewModel.Engine.PlayerList.Add(
                            new PlayerInfoModel(data)
                            {
                            // Remember the order
                            ListOrder = ListOrder
                            });

                        ListOrder++;
                    }
                }

                // Set Order for the Round
                EngineViewModel.Engine.OrderPlayerListByTurnOrder();

                for (int i = 0; i < EngineViewModel.Engine.PlayerList.Count; i++)
                {
                    if (EngineViewModel.Engine.PlayerList[i].PlayerType == PlayerTypeEnum.Character && EngineViewModel.Engine.PlayerList[i].Name == "Mike")
                    {
                        Debug.WriteLine("Mike Has Died");
                        EngineViewModel.Engine.PlayerList[i].Alive = false;
                    }
                }

                // Update Score for the RoundCount
                EngineViewModel.Engine.BattleScore.RoundCount++;
                //Roll for Hack 48 condition
                EngineViewModel.Engine.deathRollHack48 = DiceHelper.RollDice(1, 20);

            }

            // Ask the Game engine to select who goes first
            EngineViewModel.Engine.CurrentAttacker = null;

            // Add Players to Display
            DrawGameAttackerDefenderBoard();
            BattlePlayerBoxVersus.Text = "Click a monster to attack it next!";
            BattlePlayerBox.IsVisible = true;
            // Set the Battle Mode
            ShowBattleMode();
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
            if (MonstahSelected)
                return;
            data.SelectedForBattle = true;

          
            for (int k = 0; k < EngineViewModel.Engine.MonsterList.Count; k++)
            {
                if (data.Guid == EngineViewModel.Engine.PlayerList[k].Guid)
                {
                    EngineViewModel.Engine.PlayerList[k].SelectedForBattle = true;
                    //EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.MonsterList[k];
                    MonstahSelected = true;
                }

            }
            //// Manually deselect item.
            //MonsterListView.SelectedItem = null;
            DrawGameAttackerDefenderBoard();


        }
        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                CharacterBox.Children.Remove(data);
            }
            EngineViewModel.PartyCharacterList.Clear();
            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
                EngineViewModel.PartyCharacterList.Add(data);
            }
            

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                MonsterBox.Children.Remove(data);
             
            }
            EngineViewModel.BattleMonsterList.Clear();

            // Draw the Monsters
            foreach (var data in EngineViewModel.Engine.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
                EngineViewModel.BattleMonsterList.Add(data);
            }

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));
        
        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }
            
            var IButton = new ImageButton
            {
               
                Source = "crosshair.png",
                IsVisible = true,
                
            };

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                
                Source = data.ImageURI
            };

            var DumbStack = new Grid
            {
                
                Children = {

                    IButton,
                    PlayerImage
                },

            };


            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],

                Children = {
                  
                    DumbStack,
                },
            };

            return PlayerStack;
        }

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "";

            if (EngineViewModel.Engine.CurrentAttacker == null)
            {
                return;
            }

            if (EngineViewModel.Engine.CurrentDefender == null)
            {
                return;
            }

            AttackerImage.Source = EngineViewModel.Engine.CurrentAttacker.ImageURI;
            AttackerName.Text = EngineViewModel.Engine.CurrentAttacker.Name;
            AttackerHealth.Text = EngineViewModel.Engine.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentAttacker.GetMaxHealthTotal.ToString();

            DefenderImage.Source = EngineViewModel.Engine.CurrentDefender.ImageURI;
            DefenderName.Text = EngineViewModel.Engine.CurrentDefender.Name;
            DefenderHealth.Text = EngineViewModel.Engine.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentDefender.GetMaxHealthTotal.ToString();

            if (EngineViewModel.Engine.CurrentDefender.Alive == false)
            {
                DefenderImage.BackgroundColor = Color.Red;
            }

            BattlePlayerBoxVersus.Text = "vs";
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample();
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public void NextAttackExample()
        {
            EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender();

            // Hold the current state
            var RoundCondition = EngineViewModel.Engine.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                EngineViewModel.Engine.EndBattle();

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            EngineViewModel.Engine.CurrentAttacker = EngineViewModel.Engine.GetNextPlayerTurn();

            switch (EngineViewModel.Engine.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:


                    EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);

                    // User would select who to attack
                    if (MonstahSelected)
                    {
                        
                        for (int i = 0; i < EngineViewModel.Engine.PlayerList.Count(); i++)
                        {
                            if (EngineViewModel.Engine.PlayerList[i].SelectedForBattle)
                            {
                                EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.PlayerList[i];

                            }
                            EngineViewModel.Engine.PlayerList[i].SelectedForBattle = false;
                        }
                        
                        MonstahSelected = false;
                        

                    }

                    // for now just auto selecting
                    
                    break;

                case PlayerTypeEnum.Monster:
                default:
                   
                    // Monsters turn, so auto pick a Character to Attack
                    EngineViewModel.Engine.CurrentDefender = EngineViewModel.Engine.AttackChoice(EngineViewModel.Engine.CurrentAttacker);
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        async public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = EngineViewModel.Engine.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            await Navigation.PushModalAsync(new NavigationPage(new ScorePage(new GenericViewModel<ScoreModel>(Score))));
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            BattleMessages.Text = "";
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", EngineViewModel.Engine.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }


            //htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = EngineViewModel.Engine.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.Battling;
            //ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            EngineViewModel.Engine.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            //ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
      
        #endregion PageHandelers

        protected override void OnAppearing()
        {
            base.OnAppearing();

           ShowBattleMode();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            BattlePlayerInfomationBox.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

           
            switch (EngineViewModel.Engine.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    StartBattleButton.IsVisible = true;
                    break;

                case BattleStateEnum.NewRound:
                    NextRoundButton.IsVisible = true;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;

                    // Show the Game Over Display
                    GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    AttackButton.IsVisible = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }

            

        }
    }
}