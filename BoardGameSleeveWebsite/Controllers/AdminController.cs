using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System.Web.Services;

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
            return View("CreateProduct");
        }

        public ActionResult Size()
        {
            List<Size> sizes = service.GetSize();

            return View(sizes);
        }

        #region Game Things
        public ActionResult Game()
        {
            return View();
        }
        public ActionResult CreateGame(string name, int sizeId)
        {
            string createGameError = service.CreateGame(name, sizeId);
            return Content(createGameError);
        }
        public ActionResult DeleteGame(int id)
        {
            string deleteGameError = service.DeleteGame(id);
            return Content(deleteGameError);
        }
        #endregion

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
			//ON MERGE, USE YOUR OWN!
			throw new System.ArgumentException("ASDDSASD");
            //Size s = dbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();

            //if (s == null)
            //{
            //    return RedirectToAction("Size");
            //}

            //return View(s);
        }

        [WebMethod]
        public ActionResult EditChosenSize(int width, int height, string name, string description, int id)
        {
            service.editSize(width, height, name, description, id);

            return Content("redirect");
        }

        public ActionResult CreateGame()
        {
            return View("CreateGame");
        }
    }
}