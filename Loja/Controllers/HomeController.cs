using Loja.Models;
using Loja.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    /// 
    ///     -Criar helper para sumariar os servicos que o web api tem e dispolos aqui.
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
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
            string extraInfo = urlParams[5].ToUpper().First() + "-" + " ";
            List<Produto> produtos = webShared.CallWebService("GetProduct", extraInfo);
            ViewBag.Seccao = urlParams[5]; 
            ViewBag.Produtos = produtos;
            return View();
        }
    }
}