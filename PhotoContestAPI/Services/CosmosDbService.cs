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

        public async Task AddItemAsync(ExistingInvestment investment)
        {
            ContainerProperties properties = await _container.ReadContainerAsync();
            var path = properties.PartitionKeyPath;

            await this._container.CreateItemAsync<ExistingInvestment>(investment, new PartitionKey(investment.Partition));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<ExistingInvestment>(id, new PartitionKey());
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

        public async Task<IEnumerable<ExistingInvestment>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<ExistingInvestment>(new QueryDefinition(queryString));
            List<ExistingInvestment> results = new List<ExistingInvestment>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(ExistingInvestment investment)
        {
            await this._container.UpsertItemAsync<ExistingInvestment>(investment, new PartitionKey(investment.Partition));
        }
    }
}
