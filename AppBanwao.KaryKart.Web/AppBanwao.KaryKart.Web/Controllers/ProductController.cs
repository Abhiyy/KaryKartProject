using AppBanwao.KarryKart.Model;
using AppBanwao.KaryKart.Web.Helpers;
using AppBanwao.KaryKart.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBanwao.KaryKart.Web.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        string _productImages = ConfigurationManager.AppSettings["ProductDirectory"].ToString();


        ProductHelper _productHelper = null;

        public ActionResult Index()
        {
            try
            {
                _productHelper = new ProductHelper();

                return View(_productHelper.GetAllProducts());
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public ActionResult Details(Guid id)
        {
            using (_dbContext = new karrykartEntities())
            {
                _productHelper = new ProductHelper();
                return View(_productHelper.GetProduct(id));
            }
            return View();
        }

        public ActionResult Create()
        {
            CreateViewBagForProduct();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var product = new Product()
                    {
                        Active = model.Active,
                        CategoryID = model.CategoryID,
                        CreatedBy = User.Identity.Name,
                        UpdatedBy = User.Identity.Name,
                        Description = model.Description,
                        Name = model.Name,
                        ProductID = Guid.NewGuid(),
                        SubCategoryID = model.SubCategoryID,
                        BrandID = model.BrandID,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now

                    };
                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Product created successfully with name=" + product.ProductID, "Create", "ProductController", User.Identity.Name);
                    return RedirectToAction("AddImageFeatureDetails", "Product", new { id = product.ProductID });
                }

            }
            CreateViewBagForProduct();
            return View();
        }

        public ActionResult AddImageFeatureDetails(Guid id)
        {
            using (_dbContext = new karrykartEntities())
            {
                var product = _dbContext.Products.Find(id);
                return View(new ProductModel() { ProductID = product.ProductID, Name = product.Name });

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImageFeatureDetails(ProductModel model)
        {

            using (_dbContext = new karrykartEntities())
            {
                if (!(String.IsNullOrEmpty(model.Features)))
                {
                    foreach (var featureText in model.Features.Split(';'))
                    {
                        _dbContext.ProductFeatures.Add(new ProductFeature() { Feature = featureText, ProductID = model.ProductID });
                    }
                }

                var lstImages = UploadImage(model);

                foreach (var image in lstImages)
                {
                    if (!String.IsNullOrEmpty(image))
                    {
                        _dbContext.ProductImages.Add(new ProductImage() { ImageID = Guid.NewGuid(), ImageLink = image, ProductID = model.ProductID });
                        _dbContext.SaveChanges();
                    }
                }
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product imgaes and features added successfully with name=" + model.ProductID, "Create", "ProductController", User.Identity.Name);
                return RedirectToAction("AddStockPrice", "Product", new { id = model.ProductID });
            }

            return View(model);
        }

        public ActionResult AddStockPrice(Guid id)
        {
            using (_dbContext = new karrykartEntities())
            {
                var product = _dbContext.Products.Find(id);
                CreateViewBagForStockPrice();
                return View(new ProductStockPriceModel() { ProductID = product.ProductID, Name = product.Name });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStockPrice(ProductStockPriceModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var productSizeMapping = new ProductSizeMapping() { ProductID = model.ProductID, SizeID = model.SizeID, UnitID = model.UnitID, Stock = model.Stock };
                    _dbContext.ProductSizeMappings.Add(productSizeMapping);

                    var productprice = new ProductPrice() {UnitID=model.UnitID, CurrencyID = model.CurrencyID, ProductID = model.ProductID, SizeID = model.SizeID, Price = Convert.ToDecimal(model.Price) };
                    _dbContext.ProductPrices.Add(productprice);

                    var productShipping = new ProductShipping() { UnitID = model.UnitID, ProductID = model.ProductID, SizeID = model.SizeID, Cost = Convert.ToDecimal(model.ShippingCost) };
                    _dbContext.ProductShippings.Add(productShipping);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Product Stock and price added successfully with ID=" + model.Name, "AddStockPrice", "ProductController", User.Identity.Name);
                    return RedirectToAction("Index", "Product");
                }
            }
            CreateViewBagForStockPrice();
            return View();

        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            _dbContext = new karrykartEntities();
            var categories = _dbContext.Categories.Select(x => new { x.CategoryID, x.Name }).ToList();
            _dbContext = null;
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBrands()
        {
            _dbContext = new karrykartEntities();
            var brands = _dbContext.Brands.Select(x => new { x.BrandID, x.Name }).ToList();
            _dbContext = null;
            return Json(brands, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubCategories(int id = -1) // its a GET, not a POST
        {
            _dbContext = new karrykartEntities();
            if (id != -1)
            {
                var subcategories = _dbContext.Subcategories.Where(x => x.CategoryID == id).Select(x => new { x.SCategoryID, x.Name }).ToList();
                _dbContext = null;
                return Json(subcategories, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var subcategories = _dbContext.Subcategories.Select(x => new { x.SCategoryID, x.Name }).ToList();
                _dbContext = null;
                return Json(subcategories, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetSizes(int id=-1) // its a GET, not a POST
        {
            if( id!=-1)
            {
            _dbContext = new karrykartEntities();
            var sizes = _dbContext.Sizes.Where(x => x.SizeTypeID == id).Select(x => new { x.SizeID, x.Name }).ToList();
            _dbContext = null;
            return Json(sizes, JsonRequestBehavior.AllowGet);
            }else{
            _dbContext = new karrykartEntities();
            var sizes = _dbContext.Sizes.Select(x => new { x.SizeID, x.Name }).ToList();
            _dbContext = null;
            return Json(sizes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetSizeTypes()
        {
            _dbContext = new karrykartEntities();

            var sizetypes = _dbContext.SizeTypes.Select(x => new { x.SizeTypeID, x.Name }).ToList();
            _dbContext = null;

            return Json(sizetypes, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GetUnits()
        {
            _dbContext = new karrykartEntities();
            var units = _dbContext.Units.Select(x => new { x.UnitID, x.Name }).ToList();
            _dbContext = null;

            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditBasicProductDetails(ProductDetailsModel model)
        {
            if (model.ProductID != Guid.Empty)
            {
                _dbContext = new karrykartEntities();
                var product = _dbContext.Products.Find(model.ProductID);
                if (product != null)
                {
                    product.Active = model.Active;
                    product.BrandID = model.BrandID;
                    product.CategoryID = model.CategoryID;
                    product.SubCategoryID = model.SubCategoryID;
                    product.Description = model.Description;
                    product.Name = model.Name;
                    product.UpdatedBy = User.Identity.Name;
                    product.UpdatedOn = DateTime.Now;
                    _dbContext.Entry(product).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Basic product details updated successfully with Name=" + model.Name, "EditBasicProductDetails", "ProductController", User.Identity.Name);

                    return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "Basic product details updated successfully." });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RemoveProductFeature(Guid ProductID, int FeatureID)
        {

            _dbContext = new karrykartEntities();

            var feature = _dbContext.ProductFeatures.Where(x => x.FeatureID == FeatureID && x.ProductID == ProductID).FirstOrDefault();

            if (feature != null)
            {
                _dbContext.Entry(feature).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product feature deleted successfully with id=" + FeatureID, "RemoveProductFeature", "ProductController", User.Identity.Name);
                _dbContext = null;
                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "Product feature removed successfully." });
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditProductFeature(Guid ProductID, int FeatureID, string featureText)
        {
            _dbContext = new karrykartEntities();

            var feature = _dbContext.ProductFeatures.Where(f => f.FeatureID == FeatureID && f.ProductID == ProductID).FirstOrDefault();
            if (feature != null)
            {
                feature.Feature = featureText;
                _dbContext.Entry(feature).State = EntityState.Modified;
                _dbContext.SaveChanges();
                _dbContext = null;
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product feature edited successfully with id=" + FeatureID, "EditProductFeature", "ProductController", User.Identity.Name);
                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "Product feature added successfully." });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddProductFeature(Guid ProductID, int FeatureID, string featureText)
        {
            if (FeatureID == -1)
            {
                _dbContext = new karrykartEntities();
                var feature = new ProductFeature() { ProductID = ProductID, Feature = featureText };
                _dbContext.ProductFeatures.Add(feature);
                _dbContext.SaveChanges();
                _dbContext = null;
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product feature added successfully with text=" + featureText, "AddProductFeature", "ProductController", User.Identity.Name);

                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "New product feature details added successfully.", id = feature.FeatureID });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProductImage(Guid ProductID, HttpPostedFileBase file)
        {
            if (file != null)
            {
                _dbContext = new karrykartEntities();
                var productImage = new ProductImage() {ImageID=Guid.NewGuid(), ImageLink = CommonHelper.UploadFile(file, _productImages), ProductID = ProductID };
                _dbContext.ProductImages.Add(productImage);
                _dbContext.SaveChanges();
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product image added successfully for product ID=" + ProductID +", ImageID="+productImage.ImageID, "AddNewProductImage", "ProductController", User.Identity.Name);
                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "New product image details added successfully.", id = productImage.ImageID, ImageLink = productImage.ImageLink });
            }
          
            return View();
        }

        [HttpPost]
        public ActionResult RemoveProductImage(Guid ProductID, Guid ImageID)
        {
            if (ProductID != null)
            {
                _dbContext = new karrykartEntities();
                var productImage = _dbContext.ProductImages.Where(x => x.ProductID == ProductID && x.ImageID == ImageID).FirstOrDefault();
                _dbContext.Entry(productImage).State = EntityState.Deleted;
                _dbContext.SaveChanges();
                _dbContext = null;
                _logger.WriteLog(CommonHelper.MessageType.Success, "Product image deleted successfully.", "RemoveProductImage", "ProductController", User.Identity.Name);

                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "New product feature details added successfully." });
            
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddProductStockPrice(Guid ProductID, int SizeID, string SizeName, int UnitID, int SizeTypeID, int Stock, decimal Cost, decimal Price)
        {
            if (ProductID != null)
            { 
                _dbContext = new karrykartEntities();
               
                var productSizeMapping = _dbContext.ProductSizeMappings.Where(x => x.ProductID == ProductID && x.SizeID==SizeID &&x.UnitID==UnitID).FirstOrDefault();
                if (productSizeMapping == null)
                {
                    productSizeMapping = new ProductSizeMapping();
                    productSizeMapping.ProductID = ProductID;
                    productSizeMapping.SizeID = SizeID;
                    productSizeMapping.Stock = Stock;
                    productSizeMapping.UnitID = UnitID;
                    _dbContext.Entry(productSizeMapping).State = EntityState.Added;
                }
                else {
                    return Json(new { messagetype = ApplicationMessages.Product.ERROR, message = "Product and size mapping details already exists." });
                }

                var price = _dbContext.ProductPrices.Where(x => x.ProductID == ProductID && x.SizeID==SizeID && x.UnitID==UnitID).FirstOrDefault();
                if (price == null)
                {
                    price = new ProductPrice();
                    price.Price = Price;
                    price.SizeID = SizeID;
                    price.ProductID = ProductID;
                    price.CurrencyID = 1;
                    price.UnitID = UnitID;
                    _dbContext.Entry(price).State = EntityState.Added;

                }
                else {
                    return Json(new { messagetype = ApplicationMessages.Product.ERROR, message = "Product's size and Price mapping details already exists." });
                }
                var shipping = _dbContext.ProductShippings.Where(x => x.SizeID == SizeID && x.ProductID == ProductID && x.UnitID==UnitID).FirstOrDefault();
                if (shipping == null)
                {
                    shipping = new ProductShipping();
                    shipping.Cost = Cost;
                    shipping.SizeID = SizeID;
                    shipping.ProductID = ProductID;
                    shipping.UnitID = UnitID;
                    _dbContext.Entry(shipping).State = EntityState.Added;

                }
                else {
                    return Json(new { messagetype = ApplicationMessages.Product.ERROR, message = "Product size and shipping cost mapping details already exists." });
                }

                _dbContext.SaveChanges();

                _logger.WriteLog(CommonHelper.MessageType.Success, "Product stock, price and size mapping details has been added successfully.", "EditProductStockPrice", "ProductController", User.Identity.Name);

                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "Product stock, price and size mapping details has been added successfully." });
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult DeleteProductSizeMapping(Guid ProductID, int SizeID)
        {

            if (ProductID != null)
            {
                _dbContext = new karrykartEntities();
                var productSizeMapping = _dbContext.ProductSizeMappings.Where(x => x.ProductID == ProductID && x.SizeID == SizeID).FirstOrDefault();
                _dbContext.Entry(productSizeMapping).State = EntityState.Deleted;

                var productPrice = _dbContext.ProductPrices.Where(x => x.ProductID == ProductID && x.SizeID == SizeID).FirstOrDefault();
                _dbContext.Entry(productPrice).State = EntityState.Deleted;

                var productSC = _dbContext.ProductShippings.Where(x => x.ProductID == ProductID && x.SizeID==SizeID).FirstOrDefault();
                _dbContext.Entry(productSC).State = EntityState.Deleted;

                _dbContext.SaveChanges();

                _dbContext = null;

                _logger.WriteLog(CommonHelper.MessageType.Success, "Product stock, price and size mapping details has been deleted successfully.", "DeleteProductSizeMapping", "ProductController", User.Identity.Name);

                return Json(new { messagetype = ApplicationMessages.Product.SUCCESS, message = "Product stock, price and size mapping details has been deleted successfully." });
            }

            return View();
        }

        

        void CreateViewBagForProduct()
        {
            _dbContext = new karrykartEntities();
            ViewBag.CategoryID = new SelectList(_dbContext.Categories.ToList(), "CategoryID", "Name");
            ViewBag.SubcategoryID = new SelectList(_dbContext.Subcategories.ToList(), "SCategoryID", "Name");
            ViewBag.BrandID = new SelectList(_dbContext.Brands.ToList(), "BrandID", "Name");

            _dbContext = null;

        }

        void CreateViewBagForStockPrice()
        {
            _dbContext = new karrykartEntities();
            ViewBag.UnitID = new SelectList(_dbContext.Units.ToList(), "UnitID", "Name");
            ViewBag.SizeTypeID = new SelectList(_dbContext.SizeTypes.ToList(), "SizeTypeID", "Name");
            ViewBag.CurrencyID = new SelectList(_dbContext.Currencies.ToList(), "CurrencyID", "Shortform");
            ViewBag.SizeID = new SelectList(_dbContext.Sizes.ToList(), "SizeID", "Name");
            _dbContext = null;

        }

        List<string> UploadImage(ProductModel model)
        {
            List<string> lstImageLink = new List<string>();
            if (model.Image1 != null)
                lstImageLink.Add(CommonHelper.UploadFile(model.Image1, _productImages));

            if (model.Image2 != null)
                lstImageLink.Add(CommonHelper.UploadFile(model.Image2, _productImages));

            if (model.Image3 != null)
                lstImageLink.Add(CommonHelper.UploadFile(model.Image3, _productImages));

            return lstImageLink;
        }
    }

}