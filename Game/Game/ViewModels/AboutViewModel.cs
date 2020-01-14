using Game.Models;

namespace Game.ViewModels
{
    /// <summary>
    /// About View Model
    /// </summary>
    public class AboutViewModel : BaseViewModel<DefaultModel>
    {
        /// <summary>
        /// Constructor
        /// 
        /// Title is About
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";
        }
    }
}