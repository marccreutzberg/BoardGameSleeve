using BoardGameSleeveWebsite.Models;
using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoardGameSleeveWebsite.Controllers
{
    public class ProductController : Controller
    {
        public Service service = new Service();
        
        public ActionResult SingleProduct(int? id)
        {
            VMProductSingle model = service.ProductSingleModel(id.HasValue == true ? id.Value : 1);
            return View("SingleProduct", model);
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}