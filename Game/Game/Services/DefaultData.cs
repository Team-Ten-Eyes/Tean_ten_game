using Game.Models;
using System.Collections.Generic;

namespace Game.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadItems()
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel { Name = "First item", Description="This is an item description." },
                new ItemModel { Name = "Second item", Description="This is an item description." },
                new ItemModel { Name = "Third item", Description="This is an item description." },
                new ItemModel { Name = "Fourth item", Description="This is an item description." },
                new ItemModel { Name = "Fifth item", Description="This is an item description." },
                new ItemModel { Name = "Sixth item", Description="This is an item description." }
            };

            return datalist;
        }
    }
}