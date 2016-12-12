using BoardGameSleeveWebsite.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMProducts
    {
        public Service service = new Service();

        public List<CommonProduct> Products { get; set; }
        
        public VMProducts() { }                

    }

    public class CommonProduct
    {
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<string> Games { get; set; }
        public List<string> Colors { get; set; }
        public string Img { get; set; }

        public CommonProduct() { }
    }
}