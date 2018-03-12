using DirectoryWalker.Services.Interfaces;
using DirectoryWalker.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using DirectoryWalker.Database.Entities;
using System;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHierarchyService hierarchyService;
        private readonly IErrorLogService errorLogService;

        public HomeController(IHierarchyService hierarchyService,
            IErrorLogService errorLogService)
        {
            this.hierarchyService = hierarchyService;
            this.errorLogService = errorLogService;
        }

        public async Task<ActionResult> BrowseHierarchyTree(string enteredPath)
        {
            ViewData["Home"] = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority;
            IEnumerable<TreeNode> childrenOfNode = new List<TreeNode>();

            try
            {
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
                    ViewBag.Message = errorLogService.GetWrongPatternErrorMessage(enteredPath);
                    return View("CustomError");
                }

                // check if current node exists
                var searchedNode = await hierarchyService.GetNodeByCombinedPath(filteredPathes.ToList());
                if (hierarchyService.IsFoundNodeEmpty(searchedNode))
                {
                    ViewBag.Message = errorLogService.GetWrongPathErrorMesage(enteredPath);
                    return View("CustomError");
                }

                // check and returns all child nodes for current node
                childrenOfNode = await hierarchyService.GetChildrenNodes(searchedNode);
            }
            catch (Exception ex)
            {
                ViewBag.Message = errorLogService.GetExceptionMessage(ex);
                return View("CustomError");
            }

            List<LinkToChild> linksToChildren = hierarchyService.GetLinksToChildren(HttpContext.Request.Path, childrenOfNode.Select(node => node.Name)).ToList();

            // data for links on cshtml side
            ViewData["CurrentDirectory"] = enteredPath;
            ViewData["LinksToChildren"] = linksToChildren;

            return View();
        }
    }
}