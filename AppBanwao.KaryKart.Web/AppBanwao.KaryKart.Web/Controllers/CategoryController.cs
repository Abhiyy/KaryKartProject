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
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Categories.ToList());
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = model.Name;
                using (_dbContext = new karrykartEntities())
                {
                    _dbContext.Categories.Add(category);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Category created successfully with categoryID=" + category.CategoryID, "Create", "CategoryController", User.Identity.Name);
                    Success("Category is created successfully.", true);
                    return RedirectToAction("Index", "Category");
                }


            }
            return View();
        }

        public ActionResult Createsubcategory(int id = -1)
        {
            using (_dbContext = new karrykartEntities())
            {
              
            var category = _dbContext.Categories.Find(id);
                if(category!=null)
                {
                var subcategoryModel =  new SubcategoryModel();
                    subcategoryModel.CategoryName = category.Name;
                    subcategoryModel.CategoryID = category.CategoryID;

                    return View(subcategoryModel);
                }

            }
            return View();
        }

        public ActionResult Subcategory(int id = -1)
        {
            using (_dbContext = new karrykartEntities())
            {
                ViewBag.CategoryID = id;
                ViewBag.Category = _dbContext.Categories.Find(id).Name;
            return View(_dbContext.Subcategories.Where(x=>x.CategoryID == id).ToList());
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createsubcategory(SubcategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var Subcategory = new Subcategory();
                    Subcategory.CategoryID = model.CategoryID;
                    Subcategory.Name = model.Name;

                    _dbContext.Subcategories.Add(Subcategory);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success,"New sub category added successfully with id="+Subcategory.SCategoryID,"CreateSubcategory","CategoryController",User.Identity.Name);
                    Success("Subcategory added successfully");
                    return RedirectToAction("Subcategory", "Category", new { id = model.CategoryID });
                }
            }
            return View(model);
        }

        public ActionResult Edit(int id = -1)
        {
            using (_dbContext = new karrykartEntities())
            {
                var category = _dbContext.Categories.Find(id);

                return View(new CategoryModel {CategoryID=category.CategoryID,Name=category.Name });
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext =new karrykartEntities())
                {
                    var category = _dbContext.Categories.Find(model.CategoryID);
                    category.Name = model.Name;
                    _dbContext.Entry(category).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "The category has been edited successfully,category ID= " + model.CategoryID, "Edit", "CategoryController", User.Identity.Name);
                    Success("The category has been edited successfully.", true);
                    return RedirectToAction("Index", "Category");
                }
            }
            return View();
        }

        public ActionResult EditSubCategory(int id = -1)
        {
            using (_dbContext = new karrykartEntities())
            {
                var scategory = _dbContext.Subcategories.Find(id);

                return View(new SubcategoryModel {CategoryID=scategory.CategoryID,Name =scategory.Name,ScategoryID=scategory.SCategoryID });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(SubcategoryModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var scategory = _dbContext.Subcategories.Find(model.ScategoryID);
                    scategory.Name = model.Name;
                    _dbContext.Entry(scategory).State = System.Data.Entity.EntityState.Modified;
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "The sub category has been edited successfully, subcategory ID= " + model.ScategoryID, "EditSubCategory", "CategoryController", User.Identity.Name);
                    Success("The subcategory has been edited successfully.", true);
                    return RedirectToAction("SubCategory", "Category", new { id = model.CategoryID });
                }
            }

            return View();
        }
    }
}
