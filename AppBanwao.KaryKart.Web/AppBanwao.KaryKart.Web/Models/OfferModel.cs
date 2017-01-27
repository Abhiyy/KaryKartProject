using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class OfferModel
    {
        public int OfferID { get; set; }

        
        [Display(Name = "Active")]
        public bool Active { get; set; }

        public string ImageLink { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string OfferName { get; set; }

        [Required]
        [Display(Name = "Select Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}