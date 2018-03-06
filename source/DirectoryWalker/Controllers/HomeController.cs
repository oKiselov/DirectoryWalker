using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DirectoryWalker.Models;
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
            var rootPath = this.hierarchyService.GetRootNode();

            ViewBag.RootPath = rootPath;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BrowseHierarchyTree(string enteredPath)
        {
            var filteredPathes = hierarchyService.GetFilteredNodesNames(enteredPath);
            if (!filteredPathes.Any())
            {
                return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There are incompatible characters in current path" });
            }

            var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes);
            if (hierarchyService.IsFoundNodeEmpty(searchedNode))
            {
                return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There is no node with current path"});
            }

            var childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);
            var linksToChildren = hierarchyService.GetLinksToChildren(enteredPath, childrenOfNode.Select(node => node.Name));

            ViewBag.CurrentDirectory = enteredPath;
            ViewBag.LinksToChildren = linksToChildren;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CustomError(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}