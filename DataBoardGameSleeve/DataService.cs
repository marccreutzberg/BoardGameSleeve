using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoardGameSleeve
{
    public interface IDataService
    {
        List<Product> homeProducts();
    }

    public class DataService : IDataService
    {

        public List<Product> homeProducts()
        {
            using (ModelContext context = new ModelContext())
            {

                return context.Products.Take(3).ToList();
            }
        }


    }
}
