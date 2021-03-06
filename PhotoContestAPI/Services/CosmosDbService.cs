using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using CryptoManagerAPI.Models;

namespace CryptoManagerAPI.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(object obj, string dbGrouping)
        {
            ContainerProperties properties = await _container.ReadContainerAsync();
            var path = properties.PartitionKeyPath;

            if (dbGrouping == "ExistingInvestment")
            {
                ExistingInvestment investment = new ExistingInvestment();

                investment = (ExistingInvestment)obj;

                await this._container.CreateItemAsync<ExistingInvestment>(investment, new PartitionKey(investment.PartitionKey));
            }
            else if (dbGrouping == "User")
            {
                CMUser user = new CMUser();

                user = (CMUser)obj;

                await this._container.CreateItemAsync<CMUser>(user, new PartitionKey(user.PartitionKey));
            }       
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Task>(id, new PartitionKey(1));
        }

        public async Task<ExistingInvestment> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ExistingInvestment> response = await this._container.ReadItemAsync<ExistingInvestment>(id, new PartitionKey(1));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<object>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<object>(new QueryDefinition(queryString));

            List<object> results = new List<object>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(object obj, string dbGrouping)
        {
            if (dbGrouping == "ExistingInvestment")
            {
                ExistingInvestment investment = new ExistingInvestment();

                investment = (ExistingInvestment)obj;

                await this._container.UpsertItemAsync<ExistingInvestment>(investment, new PartitionKey(investment.PartitionKey));
            }
            else if (dbGrouping == "User")
            {
                CMUser user = new CMUser();

                user = (CMUser)obj;

                await this._container.UpsertItemAsync<CMUser>(user, new PartitionKey(user.PartitionKey));
            }
        }
    }
}
