using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGameSleeveWebsite.Controllers
{
    public class GamesController : Controller
    {
        public Service service = new Service();

        public ActionResult Index()
        {
            this.ViewData["gameTableJson"] = "Hello";
            VMGames vmGames = service.Games();
            return View(vmGames);
        }
    }
}