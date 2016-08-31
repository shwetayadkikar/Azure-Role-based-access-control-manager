using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.Owin.Security.OpenIdConnect;
using RBACManager.Filter;
using RBACManager.Models;
using RBACManager.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RBACManager.Controllers
{
    [ActiveDirectoryAuthorization]
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