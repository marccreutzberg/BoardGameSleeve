using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMEditProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int SleeveCountInProduct { get; set; }
        public List<Size> Sizes;
        public string selectedSize { get; set; }
        public string Img { get; set; }

        public VMEditProduct()
        {
            Sizes = new List<Size>();
        }

    }
}