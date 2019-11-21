using Loja.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loja.Services.Controllers;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly WebController webService = new WebController();
        const string seccoes = "Bijutaria;Lembrancas;Religiosos;Diversos";
        public ActionResult Index()
        {
            ViewBag.Seccoes = seccoes;
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
        public ActionResult Store()
        {
            string[] urlParams = HttpContext.Request.Url.ToString().Split('/');
            List<Produto> produtos = webService.GetProductsByPartitionKey(urlParams[5].ToUpper().First() + "-" + " ");
            ViewBag.Seccao = urlParams[5]; 
            ViewBag.Produtos = produtos;
            return View();
        }
    }
}