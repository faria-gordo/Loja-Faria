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
            Produto produto;
            if (message == null)
            {
                //Validation on nomeProduto
                string partitionKey = nomeProduto.Split('-')[0].Replace("_","-");
                List<Produto> produtos = webShared.CallWebService("web","GetProduct",partitionKey,false);
                produto = (from p in produtos
                                   where p.Nome == nomeProduto.Split('-')[1].Replace("_", " ")
                                   select p).First();
            }
            else
            {
                //Validation on nomeProduto
                List<Produto> produtos = webShared.CallWebService("web","GetProduct",PartitionKeyFormatter(nomeProduto),false);
                produto = (from p in produtos
                           where p.Nome == nomeProduto.Split('-')[0].Replace("_", " ")
                           select p).First();
            }
            return View(produto);
        }
        //public void AddProductToCart(FormCollection form)
        //{
        //    string nomePorduto = form["productName"];
        //    int quantidade = Int32.Parse(form["num"]);
            
        //}
        private string PartitionKeyFormatter(string uneditedPK)
        {
            string editedPK = uneditedPK.Split('-')[1].First() + "-" + uneditedPK.Split('-')[2].Substring(0, 3);
            return editedPK;
        }
    }
}