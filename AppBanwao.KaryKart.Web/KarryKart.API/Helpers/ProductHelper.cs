using AppBanwao.KarryKart.Model;
using KarryKart.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarryKart.API.Helpers
{
    public class ProductHelper
    {
        karrykartEntities _context = null;

        public ProductModel GetProductDetail(Guid Id)
        {
            ProductModel objProduct = null;
            _context = new karrykartEntities();
            var product = _context.Products.Find(Id);
            if (product != null)
            {
                objProduct = new ProductModel()
                {
                    Active = product.Active.Value,
                    BrandID = product.BrandID.Value,
                    BrandName = product.Brand.Name,
                    CategoryID = product.CategoryID.Value,
                    CategoryName = product.Category.Name,
                    CreatedBy = product.CreatedBy,
                    CreatedOn = product.CreatedOn.Value,
                    Description = product.Description,
                    Features = GetProductFeatures(product),
                    Images = GetProductImages(product),
                    Name = product.Name,
                    Prices = GetProductPrices(product),
                    ProductID = product.ProductID,
                    ProductSizeMappings = GetProductSizeMapping(_context, product),
                    ShippingDetails = GetShippingDetails(product),
                    SubCategoryID = product.SubCategoryID.Value,
                    SubCategoryName = product.Subcategory.Name,
                    UpdatedBy = product.UpdatedBy,
                    UpdatedOn = product.UpdatedOn.Value
                };
            }
            _context = null;
            return objProduct;
        }


        public List<ProductModel> GetAllProducts(bool ActiveOnly = true)
        {
            _context = new karrykartEntities();
            List<Guid> productIDs = null;
            List<ProductModel> lstProducts = null;
            if (ActiveOnly)
                productIDs = _context.Products.Where(x => x.Active == ActiveOnly).Select(x => x.ProductID).ToList();
            else
                productIDs = _context.Products.Select(x => x.ProductID).ToList();

            if (productIDs.Count > 0)
            {
                lstProducts = new List<ProductModel>();
                foreach (var id in productIDs)
                {
                    lstProducts.Add(GetProductDetail(id));
                }
            }
            productIDs = null;

            return lstProducts;
        }

        private List<ProductSize> GetProductSizeMapping(karrykartEntities context, Product product)
        {
            List<ProductSize> lstProductSizeMapping = new List<ProductSize>(); ProductSize objSizeMapping = null;
            foreach (var productSize in product.ProductSizeMappings)
            {
                objSizeMapping = new ProductSize()
                {
                    SizeID = productSize.SizeID.Value,
                    ProductSizeMappingID = productSize.ProductSizeMappingID,
                    Stock = productSize.Stock.Value,
                    UnitID = productSize.UnitID.Value,

                };
                objSizeMapping.UnitName = context.Units.Where(x => x.UnitID == productSize.UnitID).FirstOrDefault().Name;
                objSizeMapping.SizeName = context.Sizes.Where(x => x.SizeID == productSize.SizeID).FirstOrDefault().Name;
                objSizeMapping.SizeTypeID = context.Sizes.Where(x => x.SizeID == productSize.SizeID).FirstOrDefault().SizeTypeID.Value;
                objSizeMapping.SizeTypeName = context.SizeTypes.Where(x => x.SizeTypeID == objSizeMapping.SizeTypeID).FirstOrDefault().Name;
                lstProductSizeMapping.Add(objSizeMapping);
                objSizeMapping = null;
            }
            return lstProductSizeMapping;
        }
        
        private List<ProductFeatures> GetProductFeatures(Product product)
        {
            List<ProductFeatures> lstFeatures = new List<ProductFeatures>();
            ProductFeatures objFeature = null;
            foreach (var feature in product.ProductFeatures)
            {
                objFeature = new ProductFeatures() { FeatureID = feature.FeatureID, Feature = feature.Feature };
                lstFeatures.Add(objFeature);
                objFeature = null;
            }
            return lstFeatures;
        }

        private List<ProductImages> GetProductImages(Product product)
        {
            List<ProductImages> lstImages = new List<ProductImages>();
            ProductImages objImage = null;

            foreach (var img in product.ProductImages)
            {
                objImage = new ProductImages() { ImageID = img.ImageID,ImageLink=img.ImageLink.Replace("~","../..")};
                lstImages.Add(objImage);
                objImage = null;
            }

            return lstImages;
        }

        private List<ProductShippingDetail> GetShippingDetails(Product product)
        {
            List<ProductShippingDetail> lstShippingDetails = new List<ProductShippingDetail>();
            ProductShippingDetail objShippingDetail = null;

            foreach (var shipping in product.ProductShippings)
            {
                objShippingDetail = new ProductShippingDetail() { ShippingCostID = shipping.ShippingCostID, Cost = shipping.Cost.Value, SizeID = shipping.SizeID.Value };
                lstShippingDetails.Add(objShippingDetail);
                objShippingDetail = null;
            }

            return lstShippingDetails;
        }


        private List<ProductPrices> GetProductPrices(Product product)
        {
            List<ProductPrices> lstProductPrices = new List<ProductPrices>();

            ProductPrices objProductPrice = null;

            foreach (var price in product.ProductPrices)
            {
                objProductPrice = new ProductPrices() { CurrencyID = price.CurrencyID.Value, Price = price.Price.Value, PriceID = price.PriceID, SizeID = price.SizeID.Value };

                lstProductPrices.Add(objProductPrice);

                objProductPrice = null;
            }

            return lstProductPrices;
        }
    }
}