using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
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

        // GET: Product
        public ActionResult SingleProduct(int id)
        {
            return View("SingleProduct");
        }

        public ActionResult Products()
        {
            VMProducts model = new VMProducts();
            model = service.ProductsModel();

            return View(model);
        }

        public ActionResult Size(int id)
        {
            VMSize model = service.SizeModel(id);

            return View("Size", model);
        }

        
    }
}