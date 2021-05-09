using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickHelpers.Business.ConvertorService;
using QuickHelpers.Business.ConvertorService.Strategies;
using QuickHelpers.Domain.Models.Models.Convertor;
using QuickHelpers.Models;

namespace QuickHelpers.Controllers
{
    public class ConvertorController : Controller
    {
        private readonly Convertor _convertor;

        public ConvertorController(Convertor convertor)
        {
            _convertor = convertor;
        }

        public IActionResult Index()
        {
            return View(new ConvertorServiceSettings());
        }

        [HttpGet]
        public IActionResult PdfToWord()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult UploadPdfToWord()
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file is null)
                return BadRequest();

            using (var fileStream = file.OpenReadStream())
            {
                var request = new PdfToWordRequest(file.FileName, fileStream);
                var retval = _convertor.Convert(new PdfToWordConvertor(request));
            }
            return Json("file uploaded successfully");
        }

        [HttpGet]
        public IActionResult WordToPdf()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult UploadWordToPdf()
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file is null)
                return BadRequest();

            using (var fileStream = file.OpenReadStream())
            {
                var request = new WordToPdfRequest(file.FileName, fileStream);
                var retval =  _convertor.Convert(new WordToPdfConvertor(request));
            }
            
            return Json("file uploaded successfully");
        }
    }
}