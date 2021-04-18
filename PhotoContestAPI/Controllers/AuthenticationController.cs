using CryptoManagerAPI.Classes;
using CryptoManagerAPI.Models;
using CryptoManagerAPI.Services;
using CryptoManagerAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;
        public AuthenticationController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }

        [HttpPost]
        public async Task<AuthenticationResponseVM> Post([FromForm] string username, [FromForm] string password)
        {
            AuthenticationResponseVM model = new AuthenticationResponseVM();

            try
            {
                //var user = await _cosmosDbService.GetItemsAsync($"SELECT * FROM c where c.userID = {username} and c.password = {password}");
                string query = $"SELECT * FROM c where c.userID = '{username}'";

                var results = await _cosmosDbService.GetItemsAsync(query);

                CMUser user = new CMUser();
                foreach (var result in results)
                {
                    string jsonResult = result.ToString();

                    user = JsonConvert.DeserializeObject<CMUser>(jsonResult);

                    break;
                }

                if (user.UserID == username)
                    model.IsUserAuthenticated = true;
                else
                    model.IsUserAuthenticated = false;
            }
            catch (Exception ex)
            {
                model.IsUserAuthenticated = false;
            }

            return model;
        }
    }
}
