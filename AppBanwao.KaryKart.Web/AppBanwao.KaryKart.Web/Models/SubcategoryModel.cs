using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class SubcategoryModel
    {
        
        public int ScategoryID { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        
        [Display(Name = "Sub Category Name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}