using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class UnitModel
    {
        public int UnitID { get; set; }
        [Required]
        [Display(Name="Enter Unit name")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}