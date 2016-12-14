using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardGameSleeveWebsite.ViewModels
{
    public class VMContactForm
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Message")]
        public string Message { get; set; }

        public VMContactForm()
        {

        }

    }
}