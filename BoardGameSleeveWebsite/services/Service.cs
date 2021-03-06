﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardGameSleeveWebsite.ViewModels;
using System.Net.Mail;

namespace BoardGameSleeveWebsite.services
{
    public class Service
    {
        private ModelContext dbContext = new ModelContext();

        public VMHome HomeModel()
        {
            VMHome home = new VMHome();
            home.TopProducts = new List<Product>();
            home.DropDownSizes = dbContext.Sizes.ToList();
            home.DropDownProducts = new List<DropDownProduct>();

            foreach (var p in dbContext.Products)
            {
                DropDownProduct d = new DropDownProduct();
                d.Id = p.ID;
                d.Name = p.Name;
                d.Img = p.Img;
                d.Games = new List<string>();

                foreach (var g in p.Size.Games)
                {
                    d.Games.Add(g.Name);
                }
                home.DropDownProducts.Add(d);



            }


            var products = from p in dbContext.Products
                           join s in dbContext.Sales on p.ID equals s.ProductID
                           where p.ID == s.ProductID
                           group p by new
                           {
                               Id = p.ID,
                               Name = p.Name,
                               Height = p.Size.Height,
                               Width = p.Size.Width,
                               Img = p.Img,
                               Color = p.Color,
                               Price = p.Price,
                           } into gro

                           select new
                           {
                               Id = gro.Key.Id,
                               Name = gro.Key.Name,
                               Height = gro.Key.Height,
                               Width = gro.Key.Width,
                               Img = gro.Key.Img,
                               TotalSales = dbContext.Sales.Where(y => y.ProductID == gro.Key.Id).Sum(x => x.Quantity),
                               Color = gro.Key.Color,
                               Price = gro.Key.Price,
                           };

            foreach (var p in products.OrderByDescending(x => x.TotalSales).Take(3))
            {
                Product product = new Product();
                product.ID = (int)p.Id;
                product.Name = (string)p.Name;
                product.Color = (string)p.Color;
                product.Size = new Size();
                product.Size.Height = (int)p.Height;
                product.Size.Width = (int)p.Width;
                product.Img = (string)p.Img;
                product.Price = (decimal)p.Price;

                home.TopProducts.Add(product);
            }

            return home;
        }

        public VMProducts ProductsModel()
        {
            VMProducts vm = new VMProducts();

            vm.Products = new List<CommonProduct>();

            foreach (var p in dbContext.Products.Include("Size").GroupBy(x => new { x.Size.ID, x.Size.Name, x.Size.Width, x.Size.Height }))
            {
                CommonProduct c = new CommonProduct();
                c.SizeID = p.Key.ID;
                c.SizeName = p.Key.Name;
                c.Width = (int)p.Key.Width;
                c.Height = (int)p.Key.Height;
                c.Colors = new List<string>();
                c.Img = dbContext.Products.Where(y => y.SizeID == p.Key.ID).FirstOrDefault().Img;

                foreach (var s in dbContext.Products.Include("Size").Where(y => y.Size.Name == c.SizeName))
                {

                    c.Colors.Add(s.Color);

                    foreach (var g in s.Size.Games)
                    {
                        c.Games = new List<string>();

                        c.Games.Add(g.Name);
                    }

                }


                vm.Products.Add(c);
            }


            return vm;
        }

        public VMSize SizeModel(int id)
        {
            VMSize vm = new VMSize();
            vm.Products = new List<Product>();

            vm.Products = dbContext.Products.Include("Size").Where(x => x.SizeID == id).ToList();


            return vm;
        }
        public VMProductSingle ProductSingleModel(int id)
        {
            VMProductSingle productModel = new VMProductSingle();
            var x = from product in dbContext.Products
                    where product.ID == id
                    select product;

            if (x.Count() == 1)
                productModel.Product = x.First();
            return productModel;
        }

        public VMGames Games()
        {
            List<Game> games = (from game in dbContext.Games orderby game.Name select game).ToList();
            List<Size> sizes = (from size in dbContext.Sizes select size).ToList();
            return new VMGames(games, sizes);
        }

        public List<Size> GetSize()
        {
            List<Size> Sizes = dbContext.Sizes.ToList();
            return Sizes;
        }

        public void createSize(Size size)
        {
            dbContext.Sizes.Add(size);
            dbContext.SaveChanges();
        }
        public string CreateGame(string name, List<string> sizeNames)
        {
            //Returns an empty string if succeeded, otherwise the string will be the error description

            try
            {
                Game newGame = new Game();
                newGame.Name = name;

                IQueryable<Size> query = null;
                int sizesCount = dbContext.Sizes.Count();
                query = dbContext.Sizes.Where(x => sizeNames.Contains(x.Name));

                if (query.Count() != sizeNames.Count)
                    return "Cant add game. Some of the sizeIds doesn't exist";

                List<Size> sizes = query.ToList();
                foreach (Size size in sizes)
                    newGame.Sizes.Add(size);

                dbContext.Games.Add(newGame);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        public string DeleteGame(int id)
        {
            try
            {
                var removingGame = (from x in dbContext.Games
                                    where x.ID == id
                                    select x).ToList();
                if (removingGame.Count == 0)
                    return "Game with the id: " + id + " doesn't exist";
                dbContext.Games.Remove(removingGame.First());
                dbContext.SaveChanges();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Game> GetAllGames()
        {
            return (from x in this.dbContext.Games
                    select x).ToList();
        }
        public void UpdateGame(int gameId, string gameName, List<string> sizeNames)
        {
            Game game = (from x in this.dbContext.Games
                         where x.ID == gameId
                         select x).FirstOrDefault();
            if (game == null)
                return;
            game.Name = gameName;
            game.Sizes.Clear();

            IQueryable<Size> query = dbContext.Sizes.Where(x => sizeNames.Contains(x.Name));

            List<Size> sizes = query.ToList();
            foreach (Size size in sizes)
                game.Sizes.Add(size);

            dbContext.SaveChanges();
        }

        public void deleteSizeFromId(int id)
        {
            Size s = dbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();

            dbContext.Sizes.Remove(s);

            dbContext.SaveChanges();
        }

        public void deleteProductFromID(int id)
        {
            Product p = dbContext.Products.Where(x => x.ID == id).FirstOrDefault();
            dbContext.Products.Remove(p);

            dbContext.SaveChanges();
        }


        public void editSize(int width, int height, string name, string description, int id)
        {
            Size s = dbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();

            s.Width = width;
            s.Height = height;
            s.Name = name;
            s.SizeDescription = description;

            dbContext.SaveChanges();
        }

        public Size getSizeFromId(int id)
        {
            return dbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<Product> GetProductsBasedOnIds(List<SessionProduct> sessionProducts)
        {
            List<Product> products = new List<Product>();

            if (sessionProducts != null)
            {
                foreach (var s in sessionProducts)
                {
                    Product p = dbContext.Products.Include("Size").Where(x => x.ID == s.productId).FirstOrDefault();
                    products.Add(p);
                }
            }


            return products;
        }

        public List<Product> GetAlleProducts()
        {
            return dbContext.Products.ToList();
        }
        public bool IsLoginCredentialsCorrect(string username, string password)
        {
            var admin = (from x in this.dbContext.Admins
                         where x.Username == username
                         select x).FirstOrDefault();
            if (admin == null)
                return false;

            if (admin.Password == password)
                return true;
            return false;
        }

        public void CreateSale(VMCheckout vm)
        {
            Invoice i = new Invoice();
            i.Date = DateTime.Now;
            i.Comment = vm.CustomerInfo.Comment;

            Customer c = new Customer();
            c.Name = vm.CustomerInfo.FullName;
            c.Address = vm.CustomerInfo.Address;
            c.Country = vm.CustomerInfo.Country;
            c.Email = vm.CustomerInfo.Email;
            c.Phone = vm.CustomerInfo.Phone;
            c.Zip = vm.CustomerInfo.ZipCode;
            c.Invoice = i;


            foreach (var p in vm.SessionProducts)
            {
                Sale s = new Sale();
                s.Product = dbContext.Products.Where(x => x.ID == p.productId).FirstOrDefault();
                s.Quantity = p.quantity;

                i.Sales.Add(s);
            }

            dbContext.Invoices.Add(i);
            dbContext.Customers.Add(c);

            dbContext.SaveChanges();

        }

        public List<Game> getAllGames()
        {
            return dbContext.Games.ToList();
        }

        public void CreateProduct(Product produt)
        {
            dbContext.Products.Add(produt);
            dbContext.SaveChanges();
        }


        public void EditProduct(int ID, string Name, string Description, string Color, decimal Price, int InStock, int SleeveCountInProduct, Size size, string Img)
        {
            Product p = dbContext.Products.Where(x => x.ID == ID).FirstOrDefault();
            p.Name = Name;
            p.Description = Description;
            p.Color = Color;
            p.Price = Price;
            p.InStock = InStock;
            p.SleeveCountInProduct = SleeveCountInProduct;
            p.Size = size;
            p.Img = Img;

            dbContext.SaveChanges();

        }

        public Product getProductFromID(int id)
        {
            return dbContext.Products.Where(x => x.ID == id).FirstOrDefault();
        }

        public List<Game> GetGamesOfProduct(int productId)
        {
            return dbContext.Products.Where(x => x.ID == productId).Select(y => y.Size).SelectMany(z => z.Games).ToList();
        }

        public void sendContactFormEmail(VMContactForm VM)
        {

            // Using Google's SMTP 
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("Creutzberq@gmail.com", "ztwxercx4321");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(VM.Email);
            msg.To.Add(new MailAddress("Creutzberq@gmail.com"));

            msg.Subject = "Board Game Sleeve - Contact FOrm";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><strong>Name: </strong> "  + VM.Name + "<br /><strong>Email: </strong> " + VM.Email + "<br /> <strong>Message: </strong> " +VM.Message + "</body></html>");

            // Afsender mailen
            try
            {
                //most be on a secure connection aka a online server
                client.Send(msg);
            }
            catch (Exception)
            {

                
            }
  



        }
    }
}