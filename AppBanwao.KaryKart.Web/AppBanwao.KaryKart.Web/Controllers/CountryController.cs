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
    public class CountryController : BaseController
    {
        //
        // GET: /Country/

        public ActionResult Index()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Countries.ToList());
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    _dbContext.Countries.Add(new Country() { CountryName = model.Name });
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "New country successfully with Name=" + model.Name, "Create", "CountryController", User.Identity.Name);
                    Success("A new country is added successfully.", true);
                    return RedirectToAction("Index", "Country");
                }
            }
            return View();
        }
    }
}
