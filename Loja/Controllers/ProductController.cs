using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Loja.Models;
using Newtonsoft.Json.Linq;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO: 
    /// 
    /// </summary>
    public class ProductController : Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
        [HttpGet]
        public ActionResult Index(string prodName, string prodSecc, string prodTipo, string message)
        {
            Produto produto = new Produto();
            string partitionKey = prodSecc + "-" + prodTipo;
            //Validation on nomeProduto
            List<Produto> produtos = webShared.CallWebService("web", "GetProduct", partitionKey, false);
            if(produtos.Count() > 0)
            {
                produto = (from p in produtos
                                   where p.Nome == prodName
                                   select p).First();
            }
            else
            {
                produto = null;
            }
            if(message != null)
            {
                ViewBag.Message = message;
            }
            return View(produto);
        }
    }
}