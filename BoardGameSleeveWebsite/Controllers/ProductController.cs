using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGameSleeveWebsite.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult SingleProduct(int id)
        {
            return View("SingleProduct");
        }

        public ActionResult Products()
        {
            return View();
        }

        
    }
}