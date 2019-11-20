using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Services.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
        /// 
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
