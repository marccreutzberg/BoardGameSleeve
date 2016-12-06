using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace BoardGameSleeveWebsite.Controllers
{
    public class ProductController : Controller
    {
        public Service service = new Service();

        // GET: Product
        public ActionResult SingleProduct(int id)
        {
            if (Session["Products"] == null)
            {
                Session["products"] = new List<SessionProduct>();
            }
            VMProductSingle model = service.ProductSingleModel(id.HasValue == true ? id.Value : 1);
            return View("SingleProduct", model);
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

        [WebMethod]
        public void AddToBasket(int productId, int quantity)
        {
            List<SessionProduct> products  = (List<SessionProduct>)Session["Products"];

            SessionProduct product = new SessionProduct(productId, quantity);
            products.Add(product);

        }
    }
}


public class SessionProduct
{
    int productId { get; set; }
    int quantity { get; set; }
    
    public SessionProduct(int ProductId, int Quantity)
    {
        productId = ProductId;
        quantity = Quantity;
    }
}