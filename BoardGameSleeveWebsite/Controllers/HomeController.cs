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
        private IService iService;

        public HomeController(IService iService)
        {
            this.iService = iService;
        }

        // GET: Home
        public ActionResult Index()
        {
            //Home model = iService.HomeModel();

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