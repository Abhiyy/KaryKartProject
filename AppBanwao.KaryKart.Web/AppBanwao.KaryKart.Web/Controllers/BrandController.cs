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
    public class BrandController : BaseController
    {
        //
        // GET: /Brand/

        public ActionResult Index()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Brands.ToList());
            }
            return View();
        }

        public ActionResult Create()
        {
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var brand = new Brand();
                    brand.Name = model.Name;
                    _dbContext.Brands.Add(brand);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Brand add successfully with id=" + brand.BrandID, "Create", "BrandController", User.Identity.Name);
                    Success("The brand is added successfully.", true);
                    return RedirectToAction("Index", "Brand");
                }
            }
            return View();
        }

        public ActionResult Edit(int id=-1)
        {
            using (_dbContext = new karrykartEntities()) {
                var brand = _dbContext.Brands.Find(id);
                return View(new BrandModel() { BrandID=brand.BrandID,Name= brand.Name});
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var brand = _dbContext.Brands.Find(model.BrandID);
                    brand.Name = model.Name;
                    _dbContext.Entry(brand).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "The brand is edited successfully with id=" + brand.BrandID, "Edit", "BrandController", User.Identity.Name);
                    Success("The brand is edit successfully.", true);
                    return RedirectToAction("Index", "Brand");
                }
            }
            return View(model);
        }
    }
}
