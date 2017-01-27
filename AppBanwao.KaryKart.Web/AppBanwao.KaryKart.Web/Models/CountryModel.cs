using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Models
{
    public class CountryModel
    {
        public int CountryID { get; set; }

        [Required]
        [Display]
        public string Name { get; set; }
    }
}