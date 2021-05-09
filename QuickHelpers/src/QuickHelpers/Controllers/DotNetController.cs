using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickHelpers.Business.CSharpExecutorService;
using QuickHelpers.Models;

namespace QuickHelpers.Controllers
{
    public class DotNetController : Controller
    {
        private readonly CSharpRuntimeExecutor _parseLogic;

        public DotNetController(CSharpRuntimeExecutor parseLogic)
        {
            _parseLogic = parseLogic;
        }

        public IActionResult Index()
        {
            return View(new DotNetServiceSettings());
        }

        [HttpGet]
        public IActionResult ExecuteCode()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteCode(string code)
        {
            var executeResult = await _parseLogic.ExecuteAsync(code);
            
            // ReSharper disable once MethodHasAsyncOverload
            var retval = JsonConvert.SerializeObject(new CSharpCodeResponse(executeResult));
            return Json(retval);
        }
    }
}