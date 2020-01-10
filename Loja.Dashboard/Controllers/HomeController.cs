﻿using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Dashboard.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO: 
    /// </summary>
    public class HomeController : Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tabelas()
        {
            List<Carrinho> carrinhos = webShared.CallWebService("Cart","GetCarrinhos","");
            ViewBag.AllCarts = carrinhos;
            return View();
        }
        public ActionResult Graficos()
        {
            return View();
        }
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }
    }
}