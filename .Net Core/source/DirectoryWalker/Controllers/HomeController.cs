using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DirectoryWalker.Models;
using DirectoryWalker.Services.Interfaces;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHierarchyService hierarchyService;
        private readonly ILogger logger;

        public HomeController(IHierarchyService hierarchyService,
            ILogger<HomeController> logger)
        {
            this.hierarchyService = hierarchyService;
            this.logger = logger;
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
            // check if the path is not valid
            var filteredPathes = hierarchyService.GetFilteredNodesNames(enteredPath);
            if (!filteredPathes.Any())
            {
                return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There are incompatible characters in current path" });
            }

            // check if current node exists
            var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes);
            if (hierarchyService.IsFoundNodeEmpty(searchedNode))
            {
                return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There is no node with current path"});
            }

            // check and returns all child nodes for current node
            var childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);
            var linksToChildren = hierarchyService.GetLinksToChildren(HttpContext.Request.Path, childrenOfNode.Select(node => node.Name));

            // data for links on cshtml side
            ViewData["CurrentDirectory"] = enteredPath;
            ViewData["LinksToChildren"] = linksToChildren;

            return View();
        }

        public IActionResult Error()
        {
            logger.LogError(HttpContext.Request.Path);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CustomError(string message)
        {
            ViewBag.Message = message;
            logger.LogError(message);
            return View();
        }
    }
}