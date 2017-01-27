using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class SizeTypeModel
    {
        public int SizetypeID { get; set; }
        [Required]
        [Display(Name = "Enter Size type")]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}