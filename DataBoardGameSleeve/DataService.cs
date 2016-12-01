using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoardGameSleeve
{
    public interface IDataService
    {
        void homeProducts();
    }

    public class DataService : IDataService
    {
        public void homeProducts() { }
    }
}
