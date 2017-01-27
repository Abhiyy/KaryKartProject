using AppBanwao.KarryKart.Model;
using AppBanwao.KaryKart.Web.Helpers;
using AppBanwao.KaryKart.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBanwao.KaryKart.Web.Controllers
{
    public class OfferController :BaseController
    {
        string _offerDirectory = ConfigurationManager.AppSettings["OfferDirectory"].ToString();
        //
        // GET: /Offer/

        public ActionResult Index()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Offers.ToList());
            }
            
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new KarryKart.Model.karrykartEntities())
                {
                    var offer = new Offer();
                    offer.Active = model.Active;
                    offer.ImageLink = CommonHelper.UploadFile(model.Image, _offerDirectory);
                    offer.OfferName = model.OfferName;
                    _dbContext.Offers.Add(offer);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "Offer image uploaded successfully with ID= " + offer.OfferID, "Create()", "OfferController", User.Identity.Name);
                    Success("New offer has been added successfully.", true);
                    return RedirectToAction("Index", "Offer");
                }
            }
            else { 
            
            }
            return View();
        }
    }
}
