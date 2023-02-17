using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.FileUpload;
using PQ.CoreShared.ModelViews.Usuario;
using PQ.Manager.Interfaces;
using PQ.Manager.Validator;
using SerilogTimings;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PQ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PhilanthropicEntityController : ControllerBase
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IPhilanthropicEntityManager philanthropicEntityManager;
        private readonly ILogger<PhilanthropicEntityController> logger;
        private readonly IConfiguration configuration;
        public PhilanthropicEntityController(IPhilanthropicEntityManager philanthropicEntityManager, ILogger<PhilanthropicEntityController> logger, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            this.philanthropicEntityManager = philanthropicEntityManager;
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
            this.configuration = configuration;
        }

        /*
        [HttpGet]
        [ProducesResponseType(typeof(ViewPhilanthropicEntity),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await philanthropicEntityManager.GetEntitiesAsync());
        }*/


        /// <summary>
        /// Retorna uma entidade consultada pelo id.
        /// </summary>
        /// <param name="id" example="123">Id da entidade.</param>
        /// <returns></returns>
        /// 
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD, AN")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request = await philanthropicEntityManager.GetEntityAsync(id);
            if (request == null)
            {
                return NotFound();
            } else
            {
                return Ok(request);
            }
          
        }

        /// <summary>
        /// Insere uma nova entidade
        /// </summary>
        /// <param name="fileObject"></param>
        /// 
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] EntityWithFileUpload fileObject)
        {
            logger.LogInformation("Objeto recebido {@novaEntidade}");

            NewPhilanthropicEntity novaEntidade = new NewPhilanthropicEntity();
            novaEntidade = JsonConvert.DeserializeObject<NewPhilanthropicEntity>(fileObject.PhilanthropicEntity);
            PhilanthropicEntity entidadeInserida = new PhilanthropicEntity();

            using (Operation.Time("Tempo de adição de uma nova PhilanthropicEntity"))
            {
                List<RoleView> roles = new List<RoleView>();
                RoleView role = new RoleView();
                role.Id = 2;
                roles.Add(role);

                logger.LogInformation("Foi requisitada a inserção de um nova PhilanthropicEntity.");
                HttpResponseMessage response = await new HttpClient().PostAsync("https://pqwebapi.azurewebsites.net/api/User",
                new JsonContent(new
                {
                    email = novaEntidade.Email,
                    password = novaEntidade.Password,
                    roles = roles
                }));

                if (response.IsSuccessStatusCode)
                {
                    var myInstance = JsonConvert.DeserializeObject<UserView>(
                    await response.Content.ReadAsStringAsync());
                    novaEntidade.UserId = myInstance.Id;
                    novaEntidade.Status = "AN";
                    novaEntidade.Email = null;
                    novaEntidade.Documents.DocumentPath = SaveImage(fileObject.File, "document");
                    novaEntidade.Documents.DocumentData = await CreateByteImage(fileObject.File);

                    entidadeInserida = await philanthropicEntityManager.InsertEntityAsync(novaEntidade);
                } else
                {
                    Console.Write(response.Content.ReadAsStringAsync().Result);
                    throw new ArgumentException("Esse e-mail já está sendo utilizado");
                }

            }
           
            return CreatedAtAction(nameof(Get), new { id = entidadeInserida.Id}, entidadeInserida);
            
        }

        /// <summary>
        /// Altera uma entidade
        /// </summary>
        /// 
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AT, AD")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] EntityWithFileUpload alteraEntidade, int id)
        {
            UpdatePhilanthropicEntity novaEntidade = new UpdatePhilanthropicEntity();
            novaEntidade = JsonConvert.DeserializeObject<UpdatePhilanthropicEntity>(alteraEntidade.PhilanthropicEntity);
            novaEntidade.Id = id;
            novaEntidade.Address.PhilanthropicEntityId = id;
            var request = await philanthropicEntityManager.GetEntityAsync(id);
            if (alteraEntidade.FileBlob == null)
            {
                novaEntidade.Logo = null;
            } 
            else
            {
                if (request.Logo != null)
                {
                    DeleteBase64Image(request.Logo);
                }
                
                novaEntidade.Logo = UploadBase64Image(alteraEntidade.FileBlob);
            }
            var entidadeAtualizada = await philanthropicEntityManager.UpdateEntityAsync(novaEntidade);
            if(entidadeAtualizada == null)
            {
                return NotFound();
            }
            return Ok(entidadeAtualizada);
           
        }

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="id" example="123"></param>
        /// <remarks>Ao excluir uma entidade a mesma será removida permanentemente da base</remarks>
        /// 
        [ProducesResponseType(typeof(ViewPhilanthropicEntity), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "AD")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await philanthropicEntityManager.DeleteEntityAsync(id);
            return NoContent();
        }

        [NonAction]
        public string SaveImage(IFormFile imageFile, string type)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var documentPath = Path.Combine(hostEnvironment.ContentRootPath, @"Api/Resources/PhilanthropicEntities", imageName);
            /*if (type == "logo")
            {
                using (Image image = Image.Load(imageFile.OpenReadStream()))
                {
                    if(image.Width > 280)
                    {
                    int maxWidth = false ? 280 : Math.Min(280, image.Width);
                    int maxHeight = false ? 280 : Math.Min(280, image.Height);
                    decimal rnd = Math.Min(maxWidth / (decimal)image.Width, maxHeight / (decimal)image.Height);
                    image.Mutate(x => x.Resize((int)Math.Round(image.Width * rnd), (int)Math.Round(image.Height * rnd)));
                    image.Save(documentPath);
                    } else
                    {
                       image.Save(documentPath);
                    }
                }
            }*/

            return imageName;
        }

        [NonAction]
        public string UploadBase64Image(string base64Image)
        {
            // Gera um nome randomico para imagem

            if (base64Image != "undefined")
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

            }
            else
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
            }
            catch (Exception err)
            {
                return false;
            }

        }

        [NonAction]
        public async Task<byte[]> CreateByteImage(IFormFile documentFile)
        {
            MemoryStream ms = new MemoryStream();
            await documentFile.CopyToAsync(ms);
            var document = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return document;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Api/Resources/PhilanthropicEntities", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        public class JsonContent : StringContent
        {
            public JsonContent(object obj) :
                base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            { }
        }
    }
}
