using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMHome
    {
        public List<Product> TopProducts { get; set; }
        public List<Product> DropDownProducts { get; set; }
        public List<Size> DropDownSizes { get; set; }

        public VMHome() { }
    }
}