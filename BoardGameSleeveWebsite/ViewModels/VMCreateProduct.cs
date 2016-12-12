using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMCreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int SleeveCountInProduct { get; set; }
        public List<Size> Size;
        public string selectedSize { get; set; }

        public VMCreateProduct()
        {
            Size = new List<Size>();
        }
    }
}