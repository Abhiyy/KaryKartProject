using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class CurrencyModel
    {

        public int CurrencyID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Enter currency name")]
        public string CurrencyName { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Enter Short currency name")]
        public string ShortName { get; set; }
        [Display(Name = "Select country")]
        public int CountryID { get; set; }
         [Display(Name = "Country Name")]
        public string CountryName { get; set; }
    }
}