using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardGameSleeveWebsite.Models;

namespace BoardGameSleeveWebsite.services
{
    public interface IService
    {
        VMHome HomeModel();
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

        public VMGames2 Games()
        {
            List<Game> games = (from game in dbContext.Games orderby game.Name select game).ToList();
            List<Size> sizes = (from size in dbContext.Sizes select size).ToList();
            return new VMGames2(games, sizes);
        }
    }
}