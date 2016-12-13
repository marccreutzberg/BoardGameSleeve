using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMEditProduct
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a color")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Please enter a price")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter how many are in stock")]
        [Range(0.01, 999999999, ErrorMessage = "In stock must be greater than 0.00")]
        public int InStock { get; set; }
        [Required(ErrorMessage = "Please enter how many sleeves are in the pack")]
        [Range(0.01, 999999999, ErrorMessage = "Sleevecount must be greater than 0.00")]
        public int SleeveCountInProduct { get; set; }
        public List<Size> Sizes;
        [Required(ErrorMessage = "Please choose a size")]
        public string selectedSize { get; set; }
        public string Img { get; set; }

        public VMEditProduct()
        {
            Sizes = new List<Size>();
        }

    }
}