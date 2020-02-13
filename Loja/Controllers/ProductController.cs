using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Loja.Models;

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
        public ActionResult Index(string nomeProduto, string message)
         {
            string partitionKey = nomeProduto.Split('-')[0].Replace("_", "-");
            Produto produto;
            if (message == null)
            {
                //Validation on nomeProduto
                List<Produto> produtos = webShared.CallWebService("web","GetProduct",partitionKey,false);
                produto = (from p in produtos
                                   where p.Nome == nomeProduto.Split('-')[1].Replace("_", " ")
                                   select p).First();
            }
            else
            {
                //Validation on nomeProduto
                List<Produto> produtos = webShared.CallWebService("web","GetProduct",partitionKey,false);
                produto = (from p in produtos
                           where p.Nome == nomeProduto.Split('-')[0].Replace("_", " ")
                           select p).First();
            }
            return View(produto);
        }
    }
}