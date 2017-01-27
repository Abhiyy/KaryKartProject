using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBanwao.KaryKart.Web.Models
{
    public class ProductStockPriceModel
    {
        public Guid ProductID { get; set; }

        public string Name { get; set; }

        [Display(Name="Currency")]
        public int CurrencyID { get; set; }
        [Display(Name = "Unit")]
        public int UnitID { get; set; }
        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }
       
        [Required]
        [Display(Name = "Add Stock")]
        public int Stock { get; set; }
        [Required]
        [Display(Name = "Shipping Cost")]
        public double ShippingCost { get; set; }
        [Display(Name = "Select Size Type")]
        public int SizeTypeID { get; set; }
        [Display(Name = "Select Size")]
        public int SizeID { get; set; }


    }
}