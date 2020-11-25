using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocialGuiaTrilha.Web.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserProfileController()
        {
            _httpClient = new HttpClient();
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            //var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile");
            
            var response = await _httpClient.GetAsync("http://localhost:51973/api/userprofile");

            var contentString = await response.Content.ReadAsStringAsync();

            var userProfileContent = JsonConvert.DeserializeObject<List<Core.Models.UserProfileModel>>(contentString);

            return View(userProfileContent);
        }

        // GET: UserProfile/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
           // var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile/" + id);

            var response = await _httpClient.GetAsync("http://localhost:51973/api/userprofile/" + id);


            var contentString = await response.Content.ReadAsStringAsync();

            var userProfileContent = JsonConvert.DeserializeObject<Core.Models.UserProfileModel>(contentString);

            return View(userProfileContent);
        }

        // GET: UserProfile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.UserProfileModel userProfileModel)
        {
            if (ModelState.IsValid)
            {
                userProfileModel.UserName = User.Identity.Name;
                userProfileModel.Id = Guid.NewGuid();
                userProfileModel.Avatar = Upload(userProfileModel.AvatarFile);

                var postModelJson = JsonConvert.SerializeObject(userProfileModel);

                var content = new StringContent(postModelJson, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile", content);

                var response = await _httpClient.PostAsync("http://localhost:51973/api/userprofile", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(userProfileModel);
        }

        // GET: UserProfile/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile/" + id);

            var response = await _httpClient.GetAsync("http://localhost:51973/api/userprofile/" + id);

            var contentString = await response.Content.ReadAsStringAsync();

            var userProfileContent = JsonConvert.DeserializeObject<Core.Models.UserProfileModel>(contentString);

            return View(userProfileContent);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Models.UserProfileModel userProfileModel)
        {
            if (id != userProfileModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                userProfileModel.UserName = User.Identity.Name;
                userProfileModel.Id = id;
                userProfileModel.Avatar = Upload(userProfileModel.AvatarFile);

                var postModelJson = JsonConvert.SerializeObject(userProfileModel);

                var content = new StringContent(postModelJson, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile/" + id, content);

                var response = await _httpClient.PostAsync("http://localhost:51973/api/userprofile/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(userProfileModel);
        }

        // GET: UserProfile/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile/" + id);
           
            var response = await _httpClient.GetAsync("http://localhost:51973/api/userprofile/" + id);

            var contentString = await response.Content.ReadAsStringAsync();

            var userProfileContent = JsonConvert.DeserializeObject<Core.Models.UserProfileModel>(contentString);

            return View(userProfileContent);
        }

        // POST: UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                //var response = await _httpClient.DeleteAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile/" + id);

                var response = await _httpClient.DeleteAsync("http://localhost:51973/api/userprofile/" + id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string Upload(IFormFile pictureFile)
        {
            var reader = pictureFile.OpenReadStream();

            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=websampleapistorage;AccountKey=Wo6W2IXge0G5G3h72Kk+N767UvaLUlMC4G6z9d6hsyGRHUawuTCSVf0icOeo5UFxdnQUGqOLaHp931geiguOqg==;EndpointSuffix=core.windows.net");

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("guiaprofiles");

            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());

            blob.UploadFromStream(reader);

            var fileCloudAddress = blob.Uri.ToString();

            return fileCloudAddress;
        }
    }
}
