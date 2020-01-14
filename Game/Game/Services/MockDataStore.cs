using Game.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Services
{
    public class MockDataStore<T> : IDataStore<T> where T: new()
    {
        /// <summary>
        /// The Data List
        /// This is where the items are stored
        /// </summary>
        public List<T> datalist = new List<T>();

        /// <summary>
        /// Add the data to the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> CreateAsync(T data)
        {
            datalist.Add(data);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Update the data with the information passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> UpdateAsync(T data)
        {
            T oldData = await ReadAsync(((BaseModel<T>)(object)data).Id);
            if (oldData == null)
            {
                return await Task.FromResult(false);
            }

            datalist.Remove(oldData);
            datalist.Add(data);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Creates the Data if it does not Exists
        /// If it does Exist it updates the data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateAsync(T data)
        {
            // Check to see if the item exist
            var oldData = await ReadAsync(((BaseModel<T>)(object)data).Id);
            if (oldData == null)
            {
                await CreateAsync(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes the Data passed in by
        /// Removing it from the list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            T oldData = await ReadAsync(id);
            if (oldData == null)
            {
                return await Task.FromResult(false);
            }

            datalist.Remove(oldData);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Takes the ID and finds it in the data set
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Record if found else null</returns>
        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<T> ReadAsync(string id)
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            T oldData = datalist.Where((T arg) => ((BaseModel<T>)(object)arg).Id.Equals(id)).FirstOrDefault();
            return oldData;
        }

        /// <summary>
        /// Get the full list of data
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> IndexAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(datalist);
        }
    }
}