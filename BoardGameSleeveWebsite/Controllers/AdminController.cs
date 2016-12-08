using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardGameSleeveWebsite.services;
using BoardGameSleeveWebsite.ViewModels;
using System.Web.Services;
using System.IO;

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

        public ActionResult CreateGame()
        {
            return View("CreateGame");
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





    }
}