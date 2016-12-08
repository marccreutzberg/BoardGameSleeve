using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System.Web.Services;
using Newtonsoft.Json;

namespace BoardGameSleeveWebsite.Controllers
{
    public class AdminController : Controller
    {
        public Service service = new Service();
        // GET: Admin
        public ActionResult index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Product()
        {
            List<Product> products = service.GetAlleProducts();

            return View(products);
        }

        #region Game Things
        public ActionResult Game()
        {
            List<Size> allSizes = service.GetSize();
            string[] allSizesString = allSizes.Select(size => size.Name).ToArray();
            this.ViewData["json_allSizes"] = JsonConvert.SerializeObject(allSizesString);
            this.ViewData["games"] = service.GetAllGames();
            return View();
        }
        public ActionResult CreateGame(string name, List<string> sizeNames)
        {
            string createGameError = service.CreateGame(name, sizeNames);
            return RedirectToAction("Game");
        }

        public ActionResult DeleteGame(int id)
        {
            string deleteGameError = service.DeleteGame(id);
            return this.RedirectToAction("Game");
        }
		public ActionResult EditGame(int gameId, string newName, List<string> sizeNames)
		{
			service.UpdateGame(gameId, newName, sizeNames);
			return RedirectToAction("Game");
		}
        #endregion

        #region Size Things
        public ActionResult Size()
        {
            List<Size> sizes = service.GetSize();

            return View(sizes);
        }

        [WebMethod]
        public void CreateSize(int width, int height, string name, string description)
        {
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
            service.deleteSizeFromId(id);
        }

        public ActionResult EditSize(int id)
        {
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
            service.editSize(width, height, name, description, id);

            return Content("redirect");
        }

        public ActionResult CreateProduct()
        {
            List<Size> size = service.GetSize();
            return View(size);
        }



        
        #endregion
    }
}