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
    public class PostController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
          //  var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post");

            var response = await _httpClient.GetAsync("http://localhost:51973/api/post");

            var contentString = await response.Content.ReadAsStringAsync();

            var postContent = JsonConvert.DeserializeObject<List<Core.Models.PostModel>>(contentString);

            return View(postContent);
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
          //  var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post/" + id);

            var response = await _httpClient.GetAsync("http://localhost:51973/api/post/" + id);


            var contentString = await response.Content.ReadAsStringAsync();

            var postContent = JsonConvert.DeserializeObject<Core.Models.PostModel>(contentString);

            return View(postContent);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                postModel.UserName = User.Identity.Name;
                postModel.PublishDateTime = DateTime.Now;
                postModel.Id = Guid.NewGuid();
                postModel.Picture = Upload(postModel.PictureFile);

                var postModelJson = JsonConvert.SerializeObject(postModel);

                var content = new StringContent(postModelJson, Encoding.UTF8, "application/json");

              //  var response = await _httpClient.PostAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post", content);

               var response = await _httpClient.PostAsync("http://localhost:51973/api/post", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(postModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
          //  var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post/" + id);

            var response = await _httpClient.GetAsync("http://localhost:51973/api/post/" + id);

            var contentString = await response.Content.ReadAsStringAsync();

            var postContent = JsonConvert.DeserializeObject<Core.Models.PostModel>(contentString);

            return View(postContent);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Models.PostModel postModel)
        {
            if (id != postModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                postModel.UserName = User.Identity.Name;
                postModel.PublishDateTime = DateTime.Now;
                postModel.Id = id;
                postModel.Picture = Upload(postModel.PictureFile);

                var postModelJson = JsonConvert.SerializeObject(postModel);

                var content = new StringContent(postModelJson, Encoding.UTF8, "application/json");

               // var response = await _httpClient.PostAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post/" + id, content);

               var response = await _httpClient.PostAsync("http://localhost:51973/api/post/" + id, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(postModel);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //var response = await _httpClient.GetAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post/" + id);

            var response = await _httpClient.GetAsync("http://localhost:51973/api/post/" + id);

            var contentString = await response.Content.ReadAsStringAsync();

            var postContent = JsonConvert.DeserializeObject<Core.Models.PostModel>(contentString);

            return View(postContent);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                //var response = await _httpClient.DeleteAsync("https://redesocialguiatrilhaapipb.azurewebsites.net/api/post/" + id);

                var response = await _httpClient.DeleteAsync("http://localhost:51973/api/post/" + id);

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

            var container = blobClient.GetContainerReference("guiaposts");

            container.CreateIfNotExists();

            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());

            blob.UploadFromStream(reader);

            var fileCloudAddress = blob.Uri.ToString();

            return fileCloudAddress;
        }
    }
}
