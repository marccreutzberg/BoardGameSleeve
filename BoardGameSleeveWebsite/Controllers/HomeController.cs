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
            return View("Index", model);
        }

        public ActionResult Contact()
        {
            VMContactForm vm = new VMContactForm();
            return View(vm);
        }

        public ActionResult About()
        {
            return View("About");
        }

        [HttpPost]
        public ActionResult Contact(VMContactForm vm)
        {
            service.sendContactFormEmail(vm);

            ViewBag.signifier = "Thanks for the message we will contact your as fast as posseble";

            VMContactForm vmNew = new VMContactForm();
            return View(vmNew);
        }
    }
}