using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Us()
        {
            return View();
        }
        public ActionResult Store(string productType)
        {
            ViewBag.ProductType = productType;
            //Colar productType a viewBag e na view() por partials
            return View();
        }
    }
}