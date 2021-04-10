using CryptoManagerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoContestAPI.Services;
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

        [HttpPost]
        public async Task<ExistingInvestment> Post([FromForm] IFormFile file, [FromForm] string index, [FromForm] string author, [FromForm] string uuid)
        {
            var fileName = file.FileName;
            var convertedIndex = Convert.ToInt32(index) + 1;

            PhotoData photoDataObject = new PhotoData();

            try
            {
                byte[] fileBytes;
                //byte[] resultData;
                //string byteArrayToString;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();

                    //These two lines compress the photo, but may not need to compress twice since we do it again down below with the Resize() method
                    /*Tinify.Key = "NC86NBC6Qrjhp2GtQxC6k0l8Dbv17NZc";*/ //API Key
                    /*resultData = await Tinify.FromBuffer(fileBytes).ToBuffer();*/ //Compresses image only

                    //byteArrayToString = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }

                var mimeType = file.ContentType;

                var blobStorageService = new BlobStorageService();
                var url = blobStorageService.UploadFileToBlob(fileName, fileBytes, mimeType);
                //var url = blobStorageService.UploadFileToBlob(fileName, resultData, mimeType);

                Tinify.Key = "NC86NBC6Qrjhp2GtQxC6k0l8Dbv17NZc";
                var source = Tinify.FromUrl(url);
                var resized = source.Resize(new
                {
                    //method = "scale",
                    //width = 568

                    method = "cover",
                    width = 156,
                    height = 156
                });
                var resultDataResized = await resized.ToBuffer();
                var url2 = blobStorageService.UploadFileToBlob(fileName, resultDataResized, mimeType);

                MiscCalculations miscCalculations = new MiscCalculations();
                var photoData = new PhotoData
                {
                    Id = convertedIndex.ToString(),
                    UUID = uuid,
                    Author = author,
                    Description = "Test " + convertedIndex.ToString(),
                    ImgUrlHighQuality = url,
                    ImgUrlLowQuality = url2,
                    Votes = 0,
                    SubmitDt = DateTime.Now,
                    ContestWeek = miscCalculations.GetContestWeek(),
                    Partition = 1
                };

                await _cosmosDbService.AddItemAsync(photoData);

                //get the newly created PhotoData object to send back in POST reponse
                photoDataObject = await _cosmosDbService.GetItemAsync(convertedIndex.ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }

            return photoDataObject;
        }
    }
}
