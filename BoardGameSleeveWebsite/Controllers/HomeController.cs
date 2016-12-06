using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.ViewModels;

namespace BoardGameSleeveWebsite.Controllers
{
    public class HomeController : Controller
    {
        public Service service = new Service();

        // GET: Home
        public ActionResult Index()
        {
            VMHome model = service.HomeModel();
            this.ViewData["lol"] = "hello";
            return View("Index", model);
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }

        public ActionResult About()
        {
            return View("About");
        }
    }
}