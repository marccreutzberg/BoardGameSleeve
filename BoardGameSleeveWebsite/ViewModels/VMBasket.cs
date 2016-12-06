using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite
{
    public class VMBasket
    {
        public List<SessionProduct> sessionProducts { get; set; }
        public List<Product> products { get; set; }

        public VMBasket()
        {
            sessionProducts = new List<SessionProduct>();
            products = new List<Product>();
        }
    }
}