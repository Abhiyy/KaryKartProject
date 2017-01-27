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
    public class SliderController : BaseController
    {
        //
        // GET: /Slider/
        string _sliderDirectory = ConfigurationManager.AppSettings["SliderDirectory"].ToString();

       
        public ActionResult Index()
        {
            using (_dbContext = new karrykartEntities())
            {
                return View(_dbContext.Sliders.ToList());
            }
            return View();
        }

        public ActionResult Upload()
        { 
        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(SliderModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new KarryKart.Model.karrykartEntities())
                {
                    var slide = new Slider();
                    slide.Name = model.Name;
                    slide.Active = model.Active;
                    slide.Offer = model.OfferText;
                    slide.OfferHeading = model.OfferHeading;
                    slide.ImageLink = CommonHelper.UploadFile(model.Image, _sliderDirectory);
                    slide.SlideOrder = _dbContext.Sliders.Count() > 0 ? _dbContext.Sliders.OrderByDescending(x => x.SliderID).First().SlideOrder + 1 : 1;

                    _dbContext.Sliders.Add(slide);
                    _dbContext.SaveChanges();
                    _logger.WriteLog(CommonHelper.MessageType.Success, "The slider image added successfully with id: " + slide.SliderID.ToString(), "Upload()", "SliderController", User.Identity.Name);
                    Success("The image was added as slide successfully.");
                    return RedirectToAction("Index", "Slider");
                }
            }
            else {
                _logger.WriteLog(CommonHelper.MessageType.Error, "Validation Error" , "Upload()", "SliderController", User.Identity.Name);

            }
            return View();
        }

        public ActionResult Edit(int id=-1)
        {
            if (id != -1)
            {
                using (_dbContext = new karrykartEntities()) 
                {
                    var objSlide = _dbContext.Sliders.Find(id);
                    SliderModel slide = new SliderModel()
                    {
                        Active = objSlide.Active.Value,
                        Imagelink = objSlide.ImageLink,
                        Name = objSlide.Name,
                        OfferHeading = objSlide.OfferHeading,
                        OfferText = objSlide.Offer,
                        SliderID = objSlide.SliderID,
                       Order = objSlide.SlideOrder.Value
                    };

                    return View(slide);
                }
            }
            else { 
            
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SliderModel model)
        {
            if (ModelState.IsValid)
            {
                using (_dbContext = new karrykartEntities())
                {
                    var slide = _dbContext.Sliders.Find(model.SliderID);
                    if (slide != null)
                    {
                        slide.Active = model.Active;
                        slide.Name = model.Name;
                        slide.Offer = model.OfferText;
                        slide.OfferHeading = model.OfferHeading;
                        slide.ImageLink = model.Image.ContentLength > 0 ?CommonHelper.UploadFile(model.Image,_sliderDirectory):model.Imagelink;
                        _dbContext.Entry(slide).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                        _logger.WriteLog(CommonHelper.MessageType.Success, "The image has been edited successfully,Slider ID= " + model.SliderID, "Edit", "SliderController", User.Identity.Name);
                        Success("The image has been edited successfully, with ID = " + slide.SliderID, true);
                    }
                    else { 
                    
                    }
                }
            }
            return View();
        }
    }
}
