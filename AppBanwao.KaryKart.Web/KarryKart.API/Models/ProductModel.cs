using AppBanwao.KarryKart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarryKart.API.Models
{

    public class ProductModel
    {

        public Guid ProductID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public bool Active { get; set; }

        public List<ProductFeatures> Features { get; set; }

        public List<ProductImages> Images { get; set; }

        public List<ProductPrices> Prices { get; set; }

        public List<ProductShippingDetail> ShippingDetails { get; set; }

        public List<ProductSize> ProductSizeMappings { get; set; }



    }

    public class ProductFeatures
    {
        public int FeatureID { get; set; }

        public string Feature { get; set; }

    }

    public class ProductImages
    {
        public Guid ImageID { get; set; }

        public string ImageLink { get; set; }
    }

    public class ProductPrices
    {
        public int PriceID { get; set; }

        public int SizeID { get; set; }

        public decimal Price { get; set; }

        public int CurrencyID { get; set; }
    }

    public class ProductShippingDetail
    {
        public int ShippingCostID { get; set; }

        public int SizeID { get; set; }

        public decimal Cost { get; set; }
    }

    public class ProductSize
    {
        public int ProductSizeMappingID { get; set; }

        public int SizeID { get; set; }

        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public int Stock { get; set; }
        public string SizeName { get; set; }
        public string SizeTypeName { get; set; }
        public int SizeTypeID { get; set; }
    }

}