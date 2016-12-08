﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System.Web.Services;
using Newtonsoft.Json;
using System.IO;

namespace BoardGameSleeveWebsite.Controllers
{
    public class AdminController : Controller
    {
        public Service service = new Service();
        // GET: Admin
        public ActionResult index()
        {
			if (this.HasLoginCredentialsInCookies() == false)
				this.RedirectToAction("Login");

            return View();
        }
		bool HasLoginCredentialsInCookies()
		{
			string[] allKeys = this.Request.Cookies.AllKeys;
			if (!allKeys.Contains("username") || !allKeys.Contains("password"))
				return false;

			string username = this.Request.Cookies["username"].Value;
			string password = this.Request.Cookies["password"].Value;

			if (service.IsLoginCredentialsCorrect(username, password))
				return true;
			return false;
		}
		public ActionResult Login()
        {
			return View("Login");
        }
		public ActionResult TryLogin(string username, string password)
		{
			if (service.IsLoginCredentialsCorrect(username, password) == false)
				return this.RedirectToAction("Login");
			return this.View("index");
		}

        public ActionResult Product()
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");
			List<Product> products = service.GetAlleProducts();

            return View(products);
        }

        #region Game Things
        public ActionResult Game()
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");
			List<Size> allSizes = service.GetSize();
            string[] allSizesString = allSizes.Select(size => size.Name).ToArray();
            this.ViewData["json_allSizes"] = JsonConvert.SerializeObject(allSizesString);
            this.ViewData["games"] = service.GetAllGames();
            return View();
        }
        public ActionResult CreateGame(string name, List<string> sizeNames)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");
			string createGameError = service.CreateGame(name, sizeNames);
            return RedirectToAction("Game");
        }

        public ActionResult DeleteGame(int id)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");
			string deleteGameError = service.DeleteGame(id);
            return this.RedirectToAction("Game");
        }
		public ActionResult EditGame(int gameId, string newName, List<string> sizeNames)
		{
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");
			service.UpdateGame(gameId, newName, sizeNames);
			return RedirectToAction("Game");
		}
        #endregion

        #region Size Things
        public ActionResult Size()
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");

			List<Size> sizes = service.GetSize();

            return View(sizes);
        }

        [WebMethod]
        public void CreateSize(int width, int height, string name, string description)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return;

			Size s = new Size();
            s.Height = height;
            s.Width = width;
            s.Name = name;
            s.SizeDescription = description;

            service.createSize(s);
        }

        [WebMethod]
        public void DeleteSize(int id)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return;

			service.deleteSizeFromId(id);
        }

        public ActionResult EditSize(int id)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");

			Size s = service.GetSize().Where(x => x.ID == id).FirstOrDefault();

            if (s == null)
            {
                return RedirectToAction("Size");
            }

            return View(s);
        }

        [WebMethod]
        public ActionResult EditChosenSize(int width, int height, string name, string description, int id)
        {
			if (this.HasLoginCredentialsInCookies() == false)
				return this.RedirectToAction("Login");

			service.editSize(width, height, name, description, id);

            return Content("redirect");
        }

        public ActionResult CreateProduct()
        {
            VMCreateProduct VMC = new VMCreateProduct();

            List<Game> games = service.getAllGames();

            VMC.Games = games;

            return View(games);
        }

        [WebMethod]
        public ActionResult CreateSingelProduct(string name, string desc, string color, decimal price, int SleeveCountInProduct, int InStock, string[] sizes)
        {
            Product p = new Product();
            p.Name = name;
            p.Description = desc;
            p.Color = color;
            p.Price = price;
            p.SleeveCountInProduct = SleeveCountInProduct;
            p.InStock = InStock;

            List<Size> size = new List<Size>();



            if (size != null)
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    size.Add(service.getSizeFromId(Convert.ToInt32(sizes[i])));
                    p.Size = service.getSizeFromId(Convert.ToInt32(sizes[i]));
                }
            }

            //TODO
            // SKAL OGSÅ SMIDE SIZE MED
            //MEN DER SKAL VÆRE EN MANGE TIL MANGE RELATION I KLASSEN 
            //AKA VI SKAL OPDATERE ENTITY ! :D 
            //

            service.addProduct(p);

            return Content("success");
        }


        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine( Server.MapPath("~/img/test"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("createProduct", "Admin");
        }



        
        #endregion
    }
}