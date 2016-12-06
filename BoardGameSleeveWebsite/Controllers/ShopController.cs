using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace BoardGameSleeveWebsite.Controllers
{
    public class ShopController : Controller
    {
        public Service service = new Service();

        // GET: Shop
        public ActionResult Basket()
        {
            VMBasket vm = new VMBasket();

            List<SessionProduct> sessionProducts = (List<SessionProduct>)Session["Products"];
            List<Product> products = service.GetProductsBasedOnIds(sessionProducts);

            vm.sessionProducts = sessionProducts;
            vm.products = products;

            return View(vm);
        }

        [WebMethod]
        public void UpdateBasketQuantity(int productId, int quantity)
        {
            List<SessionProduct> sessionProducts = (List<SessionProduct>)Session["Products"];
            SessionProduct s = sessionProducts.Where(x => x.productId == productId).FirstOrDefault();
            s.quantity = quantity;

        }

        [WebMethod]
        public void SubtractToQuantityProduct(int productId)
        {
            List<SessionProduct> sessionProducts = (List<SessionProduct>)Session["Products"];
            SessionProduct s = sessionProducts.Where(x => x.productId == productId).FirstOrDefault();
            s.quantity -= 1;
        }

        [WebMethod]
        public void AddToQuantityProduct(int productId)
        {
            List<SessionProduct> sessionProducts = (List<SessionProduct>)Session["Products"];
            SessionProduct s = sessionProducts.Where(x => x.productId == productId).FirstOrDefault();
            s.quantity += 1;
        }

        [WebMethod]
        public void DeleteProductFromSession(int productId)
        {
            List<SessionProduct> sessionProducts = (List<SessionProduct>)Session["Products"];
            SessionProduct s = sessionProducts.Where(x => x.productId == productId).FirstOrDefault();
            sessionProducts.Remove(s);
        }



        public ActionResult Checkout()
        {
            return View();
        }
    }
}