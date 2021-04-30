using CryptoManagerAPI.Models;
using CryptoManagerAPI.Services;
using CryptoManagerAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistingInvestmentController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public ExistingInvestmentController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }

        [HttpGet("{username}")]
        public async Task<List<ExistingInvestmentResponseVM>> Get(string username)
        {
            List<ExistingInvestmentResponseVM> vmList = new List<ExistingInvestmentResponseVM>();

            try
            {
                username = "'" + username + "'";
                var query = $"SELECT * FROM c where c.userID = {username} and c.dbGrouping = 'ExistingInvestment'";
                var allItems = await _cosmosDbService.GetItemsAsync(query);

                ExistingInvestmentResponseVM vm = new ExistingInvestmentResponseVM();
                foreach (var item in allItems)
                {
                    string jsonResult = item.ToString();

                    vm = JsonConvert.DeserializeObject<ExistingInvestmentResponseVM>(jsonResult);

                    vmList.Add(vm);
                }

                return vmList;
            }
            catch (Exception ex)
            {
                return vmList;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromForm] string userID, [FromForm] string assetName, [FromForm] string assetSymbol, [FromForm] string amountUSD, [FromForm] string averagePrice)
        {
            bool isSuccessful = false;

            var query = "SELECT * FROM c";
            var allItems = await _cosmosDbService.GetItemsAsync(query);

            var count = allItems.Count();

            var amount = Convert.ToDecimal(amountUSD);
            var price = Convert.ToDecimal(averagePrice);
            var quantity = amount / price;

            try
            {
                var investment = new ExistingInvestment
                {
                    ID = (count + 1).ToString(),
                    DbGrouping = "ExistingInvestment",
                    UserID = userID,
                    AssetName = assetName,
                    AssetSym = assetSymbol,
                    AmountUSD = amountUSD,
                    AveragePrice = averagePrice,
                    Quantity = quantity.ToString(),
                    EntryDt = DateTime.Now,
                    ModifyDt = DateTime.Now,
                    PartitionKey = 1
                };

                await _cosmosDbService.AddItemAsync(investment, "ExistingInvestment");

                isSuccessful = true;
            }
            catch (System.Exception ex)
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }

        // DELETE /api/ExistingInvestments/id
        [HttpDelete("{id}")]
        public async Task<bool> DeleteInvestment(string id)
        {
            try
            {
                await _cosmosDbService.DeleteItemAsync(id);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
