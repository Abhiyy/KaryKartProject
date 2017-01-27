using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class SizeModel
    {
        public int SizeID { get; set; }

        [Required]
        [Display(Name="Enter Size Name")]
        [MaxLength(10)]
        public string SizeName { get; set; }

        
        [Display(Name = "Select Size Type")]
        public int SizeType { get; set; }

        public string SizeTypeName { get; set; }
    }
}