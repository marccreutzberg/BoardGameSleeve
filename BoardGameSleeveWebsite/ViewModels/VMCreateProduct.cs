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
        public double Price { get; set; }
        public int InStock { get; set; }
        public int SleeveCountInProduct { get; set; }
        public List<Game> Games;
        public List<Size> Size;

        public int[] SelectedGames { get; set; }
        public int[] SelectedSizes { get; set; }

        public VMCreateProduct()
        {
            Games = new List<Game>();
            Size = new List<Size>();
        }
    }
}