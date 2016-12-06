using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardGameSleeveWebsite.ViewModels;

namespace BoardGameSleeveWebsite.services
{
    public interface IService
    {
        VMHome HomeModel();
        VMProducts ProductsModel();
        VMSize SizeModel(int id);
        VMProductSingle ProductSingleModel(int id);
    }

    public class Service : IService
    {
        private ModelContext dbContext = new ModelContext();

        public VMHome HomeModel()
        {
            VMHome home = new VMHome();
            home.Products = dbContext.Products.Include("Size").Take(3).ToList();

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
        public string CreateGame(string name, int sizeId)
        {
            //Returns an empty string if succeeded, otherwise the string will be the error description

            try
            {
                Game newGame = new Game();
                newGame.Name = name;

                var sizes = (from x in dbContext.Sizes
                             where x.ID == sizeId
                             select x).ToList();

                //If there are none of that size
                if (sizes.Count == 0)
                    return "Cant add game. No sizeId of: " + sizeId + " exists";

                newGame.Sizes.Add(sizes.First());
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

        public void deleteSizeFromId(int id)
        {
            Size s = dbContext.Sizes.Where(x => x.ID == id).FirstOrDefault();

            dbContext.Sizes.Remove(s);

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
    }
}