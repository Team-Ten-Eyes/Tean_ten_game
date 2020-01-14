using Game.Models;

namespace Game.ViewModels
{
    /// <summary>
    /// About View Model
    /// </summary>
    public class HomeViewModel : BaseViewModel<DefaultModel>
    {
        /// <summary>
        /// Constructor
        /// 
        /// Title is About
        /// </summary>
        public HomeViewModel()
        {
            Title = "Home";
        }
    }
}