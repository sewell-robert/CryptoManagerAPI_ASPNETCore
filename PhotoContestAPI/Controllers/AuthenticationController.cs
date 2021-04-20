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

        //Logs in user
        [HttpPost]
        [Route("/api/Authentication/Login")]
        public async Task<AuthenticationResponseVM> Post([FromForm] string username, [FromForm] string password)
        {
            AuthenticationResponseVM model = new AuthenticationResponseVM();

            try
            {
                string query = $"SELECT * FROM c where c.userID = '{username}' and c.password = '{password}'";

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

        //Registers user
        [HttpPost]
        [Route("/api/Authentication/Register")]
        public async Task<AuthenticationResponseVM> Post([FromForm] string username, [FromForm] string password, [FromForm] string email)
        {
            AuthenticationResponseVM model = new AuthenticationResponseVM();

            try
            {
                var query = "SELECT * FROM c";
                var allItems = await _cosmosDbService.GetItemsAsync(query);

                var count = allItems.Count();

                CMUser user = new CMUser();
                user.ID = (count + 1).ToString();
                user.DbGrouping = "User";
                user.UserID = username;
                user.Password = password;
                user.Email = email;
                user.EntryDt = DateTime.Today;
                user.ModifyDt = DateTime.Today;
                user.PartitionKey = 1;

                await _cosmosDbService.AddItemAsync(user, user.DbGrouping);

                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
            }

            return model;
        }
    }
}
