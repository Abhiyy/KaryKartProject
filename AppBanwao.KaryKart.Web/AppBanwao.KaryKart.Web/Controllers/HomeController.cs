using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppBanwao.KaryKart.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            CreateViewBag();
            return View();
        }

        void CreateViewBag()
        { 
            using(_dbContext = new KarryKart.Model.karrykartEntities())
            {
                ViewBag.Slider = _dbContext.Sliders.Where(x=>x.Active.Value).OrderBy(x=>x.SlideOrder).ToList();
               
            }
        }

    }
}
