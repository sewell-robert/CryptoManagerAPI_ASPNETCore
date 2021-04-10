using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoManagerAPI.Models;

namespace PhotoContestAPI.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<ExistingInvestment>> GetItemsAsync(string query);
        Task<ExistingInvestment> GetItemAsync(string id);
        Task AddItemAsync(ExistingInvestment investment);
        Task UpdateItemAsync(ExistingInvestment investment);
        Task DeleteItemAsync(string id);
    }
}
