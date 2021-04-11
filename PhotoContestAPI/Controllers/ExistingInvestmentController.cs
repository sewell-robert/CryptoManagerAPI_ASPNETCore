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
        public async Task<bool> Post([FromForm] string userID, [FromForm] string assetID, [FromForm] string amountUSD, [FromForm] string quantity, [FromForm] string averagePrice)
        {
            bool isSuccessful = false;

            try
            {
                var investment = new ExistingInvestment
                {
                    ID = "1",
                    UserID = userID,
                    AssetID = assetID,
                    AmountUSD = 1000.00M,
                    Quantity = 10,
                    AveragePrice = 10,
                    EntryDt = DateTime.Now,
                    ModifyDt = DateTime.Now,
                    Partition = 1
                };

                await _cosmosDbService.AddItemAsync(investment);

                //get the newly created PhotoData object to send back in POST reponse
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
