using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite
{
    public class VMCheckout
    {
        public CustomerInformation CustomerInfo { get; set; }
        public List<Product> Products { get; set; }
        public List<SessionProduct> SessionProducts { get; set; }

        public VMCheckout()
        {
            CustomerInfo = new CustomerInformation();
            Products = new List<Product>();
            SessionProducts = new List<SessionProduct>();
        }
    }

    public class CustomerInformation
    {
        [Required (ErrorMessage = "Please enter your name")]
        public string FullName { get; set; }

        [Required (ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your zipcode")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        public CustomerInformation()
        {
            FullName = "";
            Address = "";
            ZipCode = "";
            City = "";
            Country = "";
            Email = "";
            Phone = "";
            Comment = "";
        }
    }
}