using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{

	/// <summary>
	/// The Main Game Page
	/// </summary>
	
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScorePage: ContentPage
	{
		ScoreModel endScore { get; set; } = null;


		readonly GenericViewModel<ScoreModel> ViewModel;
		/// <summary>
		/// Constructor
		/// </summary>
		public ScorePage()
		{
			
			InitializeComponent();
		}
		public ScorePage (GenericViewModel<ScoreModel> data)
		{
			BindingContext = this.ViewModel = data;

			InitializeComponent ();
		}

		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void CloseButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}