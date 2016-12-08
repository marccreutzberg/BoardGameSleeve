using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
            List<SessionProduct> SessionProducts = (List<SessionProduct>)Session["Products"];
            if (SessionProducts.Count() < 1)
            {
                return RedirectToAction("Products", "Product");
            }

            VMCheckout vm = new VMCheckout();

            if (Session["CustomerInformation"] == null)
            {
                Session["CustomerInformation"] = new CustomerInformation();
            }


            vm.CustomerInfo = (CustomerInformation)Session["CustomerInformation"];
            vm.Products = service.GetProductsBasedOnIds(SessionProducts);
            vm.SessionProducts = SessionProducts;

            return View(vm);
        }

        public ActionResult OrderConfirmation()
        {
            VMCheckout vm = (VMCheckout)TempData["checkout"];
            Session["Products"] = new List<SessionProduct>();
            Session["CustomerInformation"] = new CustomerInformation();
            

            return View(vm);
        }


        [HttpPost]
        public ActionResult CreateSale(VMCheckout vm)
        {
            List<SessionProduct> SessionProducts = (List<SessionProduct>)Session["Products"];
            vm.Products = service.GetProductsBasedOnIds(SessionProducts);
            vm.SessionProducts = SessionProducts;

            if (ModelState.IsValid)
            {
                service.CreateSale(vm);

                TempData["checkout"] = vm;

                return RedirectToAction("OrderConfirmation");
            }

            return View("Checkout", vm);
        }

        [WebMethod]
        public void SaveCheckoutInfo(string fullName, string address, string zip, string city, string country, string email, string phone, string comment)
        {
            CustomerInformation c = (CustomerInformation)Session["CustomerInformation"];
            c.FullName = fullName;
            c.Address = address;
            c.ZipCode = zip;
            c.City = city;
            c.Country = country;
            c.Email = email;
            c.Phone = phone;
            c.Comment = comment;

        }
    }
}