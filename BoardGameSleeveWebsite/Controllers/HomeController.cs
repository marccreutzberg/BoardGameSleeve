using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.Models;

namespace BoardGameSleeveWebsite.Controllers
{
    public class HomeController : Controller
    {
        public Service service = new Service();

        // GET: Home
        public ActionResult Index()
        {
            Home model = service.HomeModel();

            return View();
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