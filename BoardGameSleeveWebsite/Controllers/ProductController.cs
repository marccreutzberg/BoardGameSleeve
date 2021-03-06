﻿using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using Newtonsoft.Json;
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
        public ActionResult SingleProduct(int? id)
        {
            if (Session["Products"] == null)
            {
                Session["products"] = new List<SessionProduct>();
            }


            if (id.HasValue == false)
            {
                //Goto back to products page
                return RedirectToAction("Products");
            }
            else
            {
                VMProductSingle model = service.ProductSingleModel(id.Value);
                return View("SingleProduct", model);
            }
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

            if(products.Exists(x => x.productId == productId)){
                SessionProduct s = products.Where(x => x.productId == productId).FirstOrDefault();
                s.quantity += quantity;
            }

            else
            {
                SessionProduct product = new SessionProduct(productId, quantity);
                products.Add(product);
            }
            

        }

        [WebMethod]
        public object GetGamesOfProduct(int productId)
        {
            List<string> Games = new List<string>();
            foreach(var g in service.GetGamesOfProduct(productId))
            {
                Games.Add(g.Name);
            }


            return JsonConvert.SerializeObject(Games); ;
        }
    }
}


public class SessionProduct
{
    public int productId { get; set; }
    public int quantity { get; set; }

    public SessionProduct(int ProductId, int Quantity)
    {
        productId = ProductId;
        quantity = Quantity;
    }
}