using AppBanwao.KarryKart.Model;
using AppBanwao.KaryKart.Web.Helpers;
using AppBanwao.KaryKart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBanwao.KaryKart.Web.Controllers
{
    public class ProductSettingsController : BaseController
    {
        //
        // GET: /ProductSettings/

        public ActionResult UnitDetails()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Units.ToList());
            }
            return View();
        }

        public ActionResult CreateUnit()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUnit(UnitModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    _dbContext.Units.Add(new Unit() {Name=model.Name });
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Unit created successfully with Name=" + model.Name, "CreateUnit", "ProductSettingsController", User.Identity.Name);
                    Success("A new unit is created successfully.", true);
                    return RedirectToAction("UnitDetails", "ProductSettings");
                }
            }
            return View();
        }

        public ActionResult EditUnit(int id = -1)
        {
            if (id != -1)
                using (_dbContext = new karrykartEntities())
                {
                    var unit = _dbContext.Units.Find(id);
                    if (unit != null)
                    return View(new UnitModel(){UnitID = unit.UnitID,Name=unit.Name});
                }
            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditUnit(UnitModel model)
        {
            if (ModelState.IsValid)
                using (_dbContext = new karrykartEntities())
                {
                    var unit = _dbContext.Units.Find(model.UnitID);
                    if (unit != null)
                    {
                        unit.Name = model.Name;
                        _dbContext.Entry(unit).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                        _logger.WriteLog(CommonHelper.MessageType.Success, "The unit has been edited successfully,unit ID= " + model.UnitID, "EditUnit", "ProductSettingsController", User.Identity.Name);
                        Success("The unit has been edited successfully.", true);
                        return RedirectToAction("UnitDetails", "ProductSettings");
                    }
                }

            return View();
        }

        public ActionResult SizeTypeDetails()
        {
            using (_dbContext = new karrykartEntities()) {
                return View(_dbContext.SizeTypes.ToList());
            }
            return View();
        }

        public ActionResult CreateSizeType()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSizeType(SizeTypeModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities()) {
                    _dbContext.SizeTypes.Add(new SizeType() { Name = model.Name });
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Size type created successfully with Name=" + model.Name, "CreateUnit", "ProductSettingsController", User.Identity.Name);
                    Success("A new size type is created successfully.", true);
                    return RedirectToAction("SizeTypeDetails", "ProductSettings");
                }
            }

            return View();
        }


        public ActionResult EditSizeType(int id = -1)
        {
            if (id != -1)
                using (_dbContext = new karrykartEntities())
                {
                    var sizetype = _dbContext.SizeTypes.Find(id);
                    if (sizetype != null)
                        return View(new SizeTypeModel() { SizetypeID = sizetype.SizeTypeID, Name = sizetype.Name });
                }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditSizeType(SizeTypeModel model)
        {
            if (ModelState.IsValid)
                using (_dbContext = new karrykartEntities())
                {
                    var sizetype = _dbContext.SizeTypes.Find(model.SizetypeID);
                    if (sizetype != null)
                    {
                        sizetype.Name = model.Name;
                        _dbContext.Entry(sizetype).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                        _logger.WriteLog(CommonHelper.MessageType.Success, "The sizetype has been edited successfully,sizetypeID = " + model.SizetypeID, "EditUnit", "ProductSettingsController", User.Identity.Name);
                        Success("The sizetype has been edited successfully.", true);
                        return RedirectToAction("SizeTypeDetails", "ProductSettings");
                    }
                }

            return View();
        }

        public ActionResult SizeDetails()
        {
            using (_dbContext = new karrykartEntities()) {
                var sizes = _dbContext.Sizes.Join(_dbContext.SizeTypes, x => x.SizeTypeID, y => y.SizeTypeID, (x, y) => new { Size = x, SizeType = y });
                List<SizeModel> lstSizes = new List<SizeModel>();
                SizeModel objSize = null;
                foreach (var size in sizes)
                {
                    objSize = new SizeModel(){ SizeID= size.Size.SizeID,
                        SizeName = size.Size.Name, 
                        SizeType= size.Size.SizeTypeID.Value,
                        SizeTypeName=size.SizeType.Name};
                    lstSizes.Add(objSize);
                    objSize = null;
                }
                return View(lstSizes);
            }
            return View();
        }

        public ActionResult CreateSize()
        {
            CreateViewbagForSize();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSize(SizeModel model)
        {
            if (ModelState.IsValid) {
                using (_dbContext = new karrykartEntities())
                {
                    _dbContext.Sizes.Add(new Size() { Name = model.SizeName, SizeTypeID = model.SizeType });
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Size created successfully with Name=" + model.SizeName, "CreateUnit", "ProductSettingsController", User.Identity.Name);
                    Success("A new size is created successfully.", true);
                    return RedirectToAction("SizeDetails", "ProductSettings");
                }
            }
            return View();
            
        }

        public ActionResult CurrencyDetails()
        {
            using (_dbContext = new karrykartEntities()){
                var currencies = new List<CurrencyModel>();
                var lstCurrency = _dbContext.Currencies.Join(_dbContext.Countries,x=>x.CountryID,y=>y.CountryID,(x,y)=> new {Currency =x, Country=y});
            foreach (var currency in lstCurrency)
            {
                currencies.Add(new CurrencyModel() { CountryID = currency.Country.CountryID, CountryName = currency.Country.CountryName, CurrencyID = currency.Currency.CurrencyID, CurrencyName = currency.Currency.Name, ShortName = currency.Currency.Shortform});
            }
            return View(currencies);
            }
            
        }

        public ActionResult CreateCurrency()
        {
            CreateViewbagForCurrency();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCurrency(CurrencyModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    _dbContext.Currencies.Add(new Currency() { CountryID = model.CountryID, Name = model.CurrencyName, Shortform = model.ShortName });
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Currency created successfully with Name=" + model.CurrencyName, "CreateUnit", "ProductSettingsController", User.Identity.Name);
                    Success("A new currency is created successfully.", true);
                    return RedirectToAction("CurrencyDetails", "ProductSettings");
                }
            }
            CreateViewbagForCurrency(model.CountryID);
            return View(model);
        }

        public ActionResult EditCurrency(int id=-1)
        {
            using (_dbContext = new karrykartEntities())
            {
                var currency = _dbContext.Currencies.Find(id);
                if (currency != null)
                { 
                    CreateViewbagForCurrency(currency.CountryID.Value);
                    return View(new CurrencyModel() { CountryID = currency.CountryID.Value, CurrencyID = currency.CurrencyID, CurrencyName = currency.Name, ShortName = currency.Shortform });
                }
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCurrency(CurrencyModel model)
        {
            if(ModelState.IsValid)
            using (_dbContext = new karrykartEntities())
            {
                var currency = _dbContext.Currencies.Find(model.CurrencyID);
                if (currency != null)
                {
                    currency.Name = model.CountryName;
                    currency.CountryID = model.CountryID;
                    currency.Shortform = model.ShortName;
                    _dbContext.Entry(currency).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Currency edited successfully with Name=" + model.CurrencyName, "CreateUnit", "ProductSettingsController", User.Identity.Name);
                    Success("A currency is edited successfully.", true);
                    return RedirectToAction("CurrencyDetails", "ProductSettings");
                }
            }
            CreateViewbagForCurrency(model.CountryID);
            return View(model);
        }

        void CreateViewbagForSize(int id = -1)
        {
            _dbContext = new karrykartEntities();
            ViewBag.SizeType = new SelectList(_dbContext.SizeTypes.ToList(), "SizeTypeID", "Name", id);
            _dbContext = null;
        }

        void CreateViewbagForCurrency(int id = -1)
        {
            _dbContext = new karrykartEntities();
            ViewBag.CountryID = new SelectList(_dbContext.Countries.ToList(), "CountryID", "CountryName", id);
            _dbContext = null;
        
        }
        
    }
}
