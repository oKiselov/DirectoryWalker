using DirectoryWalker.Services.Interfaces;
using DirectoryWalker.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHierarchyService hierarchyService;

        public HomeController(IHierarchyService hierarchyService)
        {
            this.hierarchyService = hierarchyService;
        }

        public async Task<ActionResult> BrowseHierarchyTree(string enteredPath)
        {
            ViewData["Home"] = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority;
            // checking for default url 
            if (string.IsNullOrEmpty(enteredPath))
            {
                ViewBag.RootPath = this.hierarchyService.GetRootNode();
                return View("Index");
            }

            // check if the path is not valid
            var filteredPathes = hierarchyService.GetFilteredNodesNames(enteredPath);
            if (!filteredPathes.Any())
            {
                ViewBag.Message = "There are incompatible characters in current path";
                return View("CustomError");
            }

            // check if current node exists
            var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes.ToList());
            if (hierarchyService.IsFoundNodeEmpty(searchedNode))
            {
                ViewBag.Message = "There is no node with current path";
                return View("CustomError");
            }

            // check and returns all child nodes for current node
            var childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);
            List<LinkToChild> linksToChildren = hierarchyService.GetLinksToChildren(HttpContext.Request.Path, childrenOfNode.Select(node => node.Name)).ToList();

            // data for links on cshtml side
            ViewData["CurrentDirectory"] = enteredPath;
            ViewData["LinksToChildren"] = linksToChildren;

            return View();
        }
    }
}