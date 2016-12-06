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
    }
}