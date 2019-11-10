using System;
using System.IO;
using System.Threading.Tasks;
using ComputerVision.AppServices;
using ComputerVision.AppServices.TransportModels.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VisionApiDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IVisionService _visionService;

        public HomeController(IVisionService visionService, IHostingEnvironment environment)
        {
            _visionService = visionService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(IFormFile uploadedFile)
        {
            // Getting file path
            var fileExtension = Path.GetExtension(uploadedFile.FileName);
            var newFileName = Guid.NewGuid() + fileExtension;
            var filePath = string.Format("{0}\\images\\{1}", _environment.WebRootPath, newFileName);

            using (FileStream fs = System.IO.File.Create(filePath))
            {
                uploadedFile.CopyTo(fs);
                fs.Flush();
            }

            var uploadImageRequestModel = new UploadImageRequest
            {
                Body = uploadedFile.OpenReadStream()
            };

            var response = await _visionService.MakeAnalysisRequestAsync(uploadImageRequestModel);
            response.Metadata.FileName = newFileName;
            return View(response);

        }
    }
}
