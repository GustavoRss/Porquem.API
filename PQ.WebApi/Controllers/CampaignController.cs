using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Campaign;
using PQ.CoreShared.ModelViews.FileUpload;
using PQ.Manager.Interfaces.Managers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICampaignManager campaignManager;
        private readonly IWebHostEnvironment hostEnvironment;
        public CampaignController(ICampaignManager campaignManager, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            this.campaignManager = campaignManager;
            this.hostEnvironment = hostEnvironment;
            this.configuration = configuration;
        }

        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var request = await campaignManager.GetCampaignAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(request);
            }

        }

        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CampaignWithFileUpload fileObject)
        {

            NewCampaign novaCampanha = JsonConvert.DeserializeObject<NewCampaign>(fileObject.Campaign);
            

            if (fileObject.Wallpaper == null)
            {
                novaCampanha.Wallpaper = null;
            }
            else
            {
                novaCampanha.Wallpaper = UploadBase64Image(fileObject.Wallpaper);
            }
            if (fileObject.Logo == null)
            {
                novaCampanha.Logo = null;
            }
            else
            {
                novaCampanha.Logo = UploadBase64Image(fileObject.Logo);
            }

            Campaign campanhaInserida = await campaignManager.InsertCampaignAsync(novaCampanha);


            //throw new ArgumentException("Esse e-mail já está sendo utilizado");

            return Ok(campanhaInserida);

        }

        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] CampaignWithFileUpload alteraEntidade, Guid id)
        {
            
            UpdateCampaign novaCampanha = JsonConvert.DeserializeObject<UpdateCampaign>(alteraEntidade.Campaign);
            var request = await campaignManager.GetCampaignAsync(id);
            novaCampanha.Id = id;

            if (alteraEntidade.Wallpaper == null)
            {
                novaCampanha.Wallpaper = request.Wallpaper;
            } else
            {
                if (request.Wallpaper != null)
                {
                    DeleteBase64Image(request.Wallpaper);
                   
                }
                novaCampanha.Wallpaper = UploadBase64Image(alteraEntidade.Wallpaper);
            }
            if (alteraEntidade.Logo == null)
            {
                novaCampanha.Logo = request.Logo;
            }
            else
            {
                if (request.Logo != null)
                {
                    DeleteBase64Image(request.Logo);
                }
                novaCampanha.Logo = UploadBase64Image(alteraEntidade.Logo);
            }
            var campanhaAtualizada = await campaignManager.UpdateCampaignAsync(novaCampanha);
            if (campanhaAtualizada == null)
            {
                return NotFound();
            }
            return Ok(campanhaAtualizada);

        }

        [ProducesResponseType(typeof(NewCampaign), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = await campaignManager.GetCampaignAsync(id);

            if (request.Logo != null)
            {
                DeleteBase64Image(request.Logo);
            }
            if (request.Wallpaper != null)
            {
                DeleteBase64Image(request.Wallpaper);
            }
            await campaignManager.DeleteCampaignAsync(id);
            return NoContent();
        }

        /* [NonAction]
         public async Task<byte[]> CreateByteImage(IFormFile file)
         {
             MemoryStream ms = new MemoryStream();
             await file.CopyToAsync(ms);
             var document = ms.ToArray();
             ms.Close();
             ms.Dispose();
             return document;
         }*/

        [NonAction]
        public string SaveImage(IFormFile imageFile, string type)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var documentPath = Path.Combine(hostEnvironment.ContentRootPath, @"Api/Resources/Campaigns", imageName);
            using (Image image = Image.Load(imageFile.OpenReadStream()))
            {
                if (type == "logo" && image.Width > 280)
                {
                    int maxWidth = false ? 280 : Math.Min(280, image.Width);
                    int maxHeight = false ? 280 : Math.Min(280, image.Height);
                    decimal rnd = Math.Min(maxWidth / (decimal)image.Width, maxHeight / (decimal)image.Height);
                    image.Mutate(x => x.Resize((int)Math.Round(image.Width * rnd), (int)Math.Round(image.Height * rnd)));

                } 
                else
                {
                    int maxWidth = false ? 1280 : Math.Min(1280, image.Width);
                    int maxHeight = false ? 1280 : Math.Min(1280, image.Height);
                    decimal rnd = Math.Min(maxWidth / (decimal)image.Width, maxHeight / (decimal)image.Height);
                    image.Mutate(x => x.Resize((int)Math.Round(image.Width * rnd), (int)Math.Round(image.Height * rnd)));
                }

                image.Save(documentPath);
            }

            return imageName;
        }
        [NonAction]
        public string UploadBase64Image(string base64Image) {
            // Gera um nome randomico para imagem

            if(base64Image != "undefined")
            {

            var type = "";
            var clean = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");
            var data = clean.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                        type = ".png";
                        break;
                case "/9J/4":
                        type = ".jpg";
                        break; 
                default:
                    return string.Empty;
            }

            string fileName = Guid.NewGuid().ToString() + type;

            byte[] imageBytes = Convert.FromBase64String(clean);

            var blobClient = new BlobClient(Environment.GetEnvironmentVariable("PQ_CONTAINER"), "pqwebapi", fileName);

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

                return blobClient.Uri.AbsoluteUri;

            } else
            {
                return null;
            }

        }

        [NonAction]
        public bool DeleteBase64Image(string base64Image)
        {
            // Gera um nome randomico para imagem
            base64Image = base64Image.Replace("https://imageswebapi.blob.core.windows.net/pqwebapi/", "");
            BlobClient blobClient = new BlobClient(Environment.GetEnvironmentVariable("PQ_CONTAINER"), "pqwebapi", base64Image);
            try
            {
                blobClient.DeleteIfExistsAsync();
                return true;
            } catch (Exception err)
            {
                return false;
            }

        }


        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Api/Resources/Campaigns", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

    }
}
