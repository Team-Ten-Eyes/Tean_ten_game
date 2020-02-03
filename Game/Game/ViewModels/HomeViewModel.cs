using Game.Models;

namespace Game.ViewModels
{
    /// <summary>
    /// Home View Model
    /// </summary>
    public class HomeViewModel : BaseViewModel<DefaultModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeViewModel()
        {
            Title = "Home";
        }
    }
}