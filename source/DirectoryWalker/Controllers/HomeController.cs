using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DirectoryWalker.Models;
using DirectoryWalker.Database.Repositories.Interfaces;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITreeReadRepository treeReadRepository;

        public HomeController(ITreeReadRepository treeReadRepository)
        {
            this.treeReadRepository = treeReadRepository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpGet]
        //public IActionResult GetString(string article)
        //{
        //    var a = article;

        //    return View();
        //}

        [HttpGet]
        public async Task GetString(string article)
        {
            var a = article;

            var b = this.treeReadRepository.Val;

            var result = await this.treeReadRepository.CheckIfPathExists(new string[] { "Creating Digddital Images", "Resources", "Primary Sources" });

            return;
        }
    }
}
