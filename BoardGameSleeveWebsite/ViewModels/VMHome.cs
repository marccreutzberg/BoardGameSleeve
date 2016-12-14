using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMHome
    {
        public List<Product> TopProducts { get; set; }
        public List<DropDownProduct> DropDownProducts { get; set; }
        public List<Size> DropDownSizes { get; set; }

        public VMHome() { }
    }

    public class DropDownProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public List<string> Games { get; set; }
        public string JsonGames { get; set; }

        public DropDownProduct() { }
    }

}