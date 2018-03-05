using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DirectoryWalker.Models;
using DirectoryWalker.Database.Repositories.Interfaces;
using DirectoryWalker.Services.Interfaces;
using System.Linq;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHierarchyService hierarchyService;

        public HomeController(IHierarchyService hierarchyService)
        {
            this.hierarchyService = hierarchyService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async void BrowseHierarchyTree(string enteredPath)
        {
            var filteredPathes = hierarchyService.GetFilteredNodesNames(enteredPath);
            if (!filteredPathes.Any())
            {
                // return view with error
                var a = 5;
            }

            var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes);
            if (hierarchyService.IsFoundNodeEmpty(searchedNode))
            {
                //return view that no such path 
                var a = 5;
            }

            var childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);

            RedirectToAction("BrowseHierarchyTree");
        }
    }
}