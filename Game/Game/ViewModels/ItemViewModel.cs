using Game.Models;

namespace Game.ViewModels
{
    public class ItemViewModel : BaseViewModel<DefaultModel>
    {
        /// <summary>
        /// The Item Model
        /// </summary>
        public ItemModel Data { get; set; }

        /// <summary>
        /// Constructor takes an existing item and sets
        /// The Title for the page to match the text of data
        /// The Data to be the passed in data
        /// </summary>
        /// <param name="data"></param>
        public ItemViewModel(ItemModel data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
