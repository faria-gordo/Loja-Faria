using Loja.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        [HttpPost]
        public ActionResult Index(string productName, string quantity)
        {
            //Hold these values!! how?? session variables!! 
            string partitionKey = productName.Split('-')[1] + productName.Split('-')[2];
            Loja.Data.Data manager = new Loja.Data.Data();
            List<Produto> produtosCarrinho = new List<Produto>();
            Produto produto = (from s in manager.Selecionar(partitionKey)
                               where s.Nome == productName.Split('-')[0]
                               select s).First();
            ViewBag.Quantidade = quantity;
            ViewBag.Produto = produto;
            //Criar varias partials views para criar a pagina de carrinho
            //Uma terá um iterador para a lista produtos
            return View();
        }
    }
}