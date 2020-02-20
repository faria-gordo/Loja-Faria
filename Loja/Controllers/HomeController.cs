using Loja.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    /// 
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
            //Fix this, not usual to use HttpContext to retrieve sent info, grab it in the arg of this action, change the route, or use the right arg name
            string[] urlParams = HttpContext.Request.Url.ToString().Split('/');
            string extraInfo = urlParams[5];
            List<Produto> produtos = webShared.CallWebService("web", "GetProducts", extraInfo, false);
            ViewBag.Seccao = urlParams[5];
            ViewBag.Produtos = produtos;
            return View();
        }
    }
}