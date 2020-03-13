using Game.Models;
using Game.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRoundPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewRoundPage()
        {
            InitializeComponent();


            //PartyListFrame.Children.Add(CreatePlayerDisplayBox(new PlayerInfoModel()));
            //PartyListFrame.Children.Add(CreatePlayerDisplayBox(new PlayerInfoModel()));
            //PartyListFrame.Children.Add(CreatePlayerDisplayBox(new PlayerInfoModel()));
            //PartyListFrame.Children.Add(CreatePlayerDisplayBox(new PlayerInfoModel()));
            //PartyListFrame.Children.Add(CreatePlayerDisplayBox(new PlayerInfoModel()));

            //PlayerInfoModel monster1 = new PlayerInfoModel();
            //monster1.ImageURI = "fire_monster.png";
            //monster1.CurrHealth = 5;
            //PlayerInfoModel monster2 = new PlayerInfoModel();
            //monster2.ImageURI = "fire_monster.png";
            //monster2.CurrHealth = 5;
            //PlayerInfoModel monster3 = new PlayerInfoModel();
            //monster3.ImageURI = "fire_monster.png";
            //monster3.CurrHealth = 5;
            //PlayerInfoModel monster4 = new PlayerInfoModel();
            //monster4.ImageURI = "fire_monster.png";
            //monster4.CurrHealth = 5;
            //PlayerInfoModel monster5 = new PlayerInfoModel();
            //monster5.ImageURI = "fire_monster.png";
            //monster5.CurrHealth = 5;

            //MonsterListFrame.Children.Add(CreatePlayerDisplayBox(monster1));
            //MonsterListFrame.Children.Add(CreatePlayerDisplayBox(monster2));
            //MonsterListFrame.Children.Add(CreatePlayerDisplayBox(monster3));
            //MonsterListFrame.Children.Add(CreatePlayerDisplayBox(monster4));
            //MonsterListFrame.Children.Add(CreatePlayerDisplayBox(monster5));

            //COMMENTED OUT FOR Battle Screens UX 
            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.CharacterList)
            {
                PartyListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }

            //Draw the Monsters

            foreach (var data in EngineViewModel.Engine.MonsterList)

            {
                MonsterListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }

        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BeginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleLargeStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicroWhite"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the HP
            var PlayerHPLabel = new Label
            {
                Text = "HP : " + data.GetCurrentHealthTotal,
                Style = (Style)Application.Current.Resources["ValueStyleMicroWhite"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var PlayerNameLabel = new Label()
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["ValueStyleWhite"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerNameLabel,
                    PlayerLevelLabel,
                    PlayerHPLabel,
                },
            };

            return PlayerStack;
        }
    }
}