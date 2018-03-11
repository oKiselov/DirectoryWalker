using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DirectoryWalker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult BrowseHierarchyTree(string enteredPath)
        {
            //var a = enteredPath;


            /*
             connection to DB 169.254.43.208:1433
             127.0.0.1:1433
             login adminsql, password adminsql
             */

            return View("Index");
        }
    }
}