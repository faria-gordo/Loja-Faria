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
            //TODO: Aprender a usar session variables, aplicar a lista de produtos como session variable, sendo assim, caso seja adicionado algum produto
            //      ao carrinho de compras ira aparecer na mesma sem apagar o ultimo
            string partitionKey = PartitionKeyFormatter(productName);
            Loja.Data.Data manager = new Loja.Data.Data();
            List<Produto> produtosCarrinho = (from s in manager.Selecionar(partitionKey)
                                              where s.Nome == productName.Split('-')[0]
                                              select s).ToList<Produto>();
            ViewBag.Quantidade = quantity;
            ViewBag.Produtos = produtosCarrinho;
            return View();
        }
        private string PartitionKeyFormatter(string uneditedPK)
        {
            string editedPK = uneditedPK.Split('-')[1].First() + "-" + uneditedPK.Split('-')[2].Substring(0, 3);
            return editedPK;
        }
    }
}