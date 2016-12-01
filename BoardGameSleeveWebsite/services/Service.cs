using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoardGameSleeveWebsite.Models;

namespace BoardGameSleeveWebsite.services
{
    public interface IService
    {
        Home HomeModel();
    }

    public class Service : IService
    {

        private ModelContext dbContext = new ModelContext();

        public Home HomeModel()
        {
                Home home = new Home();
                home.Products = dbContext.Products.Include("Size").Take(3).ToList();

                return home;
        }

    }
}