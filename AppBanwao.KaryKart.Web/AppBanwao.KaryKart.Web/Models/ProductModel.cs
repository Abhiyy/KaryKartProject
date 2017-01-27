using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class ProductModel
    {
        public Guid ProductID { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Product Category")]
        public int CategoryID { get; set; }
        
        [Display(Name = "Sub Category")]
        public int SubCategoryID { get; set; }
        
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        
        [Display(Name = "Active")]
        public bool Active { get; set; }

        
        [Display(Name = "Features")]
        public string Features { get; set; }

        
        [Display(Name = "Image 1")]
        public HttpPostedFileBase Image1 { get; set; }

        [Display(Name = "Image 2")]
        public HttpPostedFileBase Image2 { get; set; }

        [Display(Name = "Image 3")]
        public HttpPostedFileBase Image3 { get; set; }

    }
}