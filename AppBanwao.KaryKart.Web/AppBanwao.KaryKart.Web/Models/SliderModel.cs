using AppBanwao.KarryKart.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class SliderModel
    {
        public int SliderID { get; set; }
        [Required]
        [Display(Name="Slide Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Offer heading")]
        public string OfferHeading { get; set; }
        [Required]
        [Display(Name = "Offer text")]
        public string OfferText { get; set; }
        
        public string Imagelink { get; set; }
       
        [Display(Name = "Select Image")]
        public HttpPostedFileBase Image { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
        public int Order { get; set; }
    }
}