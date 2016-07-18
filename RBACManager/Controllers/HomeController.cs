using RBACManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RBACManager.Controllers
{
    public class HomeController : Controller
    {
        AzureResourceManager _azureResourceManager;

        public HomeController()
        {
            _azureResourceManager = new AzureResourceManager();
        }

        public ActionResult Index()
        {
        
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}