using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class BrandModel
    {
        public int BrandID { get; set; }
        [Required]
        [Display(Name = "Brand Name")]
        [MaxLength(100)]
        public string Name { get; set; }


    }
}