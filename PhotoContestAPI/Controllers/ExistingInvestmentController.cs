using CryptoManagerAPI.Models;
using CryptoManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;
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
    }
}
