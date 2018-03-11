using DirectoryWalker.Services.Interfaces;
using System.Web.Mvc;

/*
 connection to DB 169.254.43.208:1433
 127.0.0.1:1433
 login adminsql, password adminsql
 */

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHierarchyService hierarchyService;

        public HomeController(IHierarchyService hierarchyService)
        {
            this.hierarchyService = hierarchyService;
        }

        public ActionResult BrowseHierarchyTree(string enteredPath)
        {
            if (string.IsNullOrEmpty(enteredPath))
            {
                //var rootPath = this.hierarchyService.GetRootNode();
                ViewBag.Message = "There are incompatible characters in current path";
                //return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There are incompatible characters in current path" });

                return View("CustomError");
            }

            //// check if the path is not valid
            //var filteredPathes = hierarchyService.GetFilteredNodesNames(enteredPath);
            //if (!filteredPathes.Any())
            //{
            //    return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There are incompatible characters in current path" });
            //}

            //// check if current node exists
            //var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes);
            //if (hierarchyService.IsFoundNodeEmpty(searchedNode))
            //{
            //    return RedirectToAction(nameof(HomeController.CustomError), "Home", new { message = "There is no node with current path" });
            //}

            //// check and returns all child nodes for current node
            //var childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);
            //var linksToChildren = hierarchyService.GetLinksToChildren(HttpContext.Request.Path, childrenOfNode.Select(node => node.Name));

            //// data for links on cshtml side
            //ViewData["CurrentDirectory"] = enteredPath;
            //ViewData["LinksToChildren"] = linksToChildren;

            //return View();


            ViewBag.RootPath = HttpContext.Request.Url.Scheme+"://"+HttpContext.Request.Url.Authority;
            return View("Index");
        }

        public ActionResult CustomError(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}