using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGameSleeveWebsite.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Basket()
        {
            return View();
        }
    }
}