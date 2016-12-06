using BoardGameSleeveWebsite.Models;
using BoardGameSleeveWebsite.services;
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
            VMGames2 vmGames = service.Games();
            return View(vmGames);
        }
    }
}