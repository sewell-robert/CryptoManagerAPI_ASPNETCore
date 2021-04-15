using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoManagerAPI.Models;

namespace CryptoManagerAPI.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<ExistingInvestment>> GetItemsAsync(string query);
        Task<ExistingInvestment> GetItemAsync(string id);
        Task AddItemAsync(object obj, string dbGrouping);
        Task UpdateItemAsync(object obj, string dbGrouping);
        Task DeleteItemAsync(string id);
    }
}
