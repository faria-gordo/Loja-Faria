using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    public class ShopController : Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
        // GET: Shop
        [HttpPost]
        public ActionResult Index(string productName)
        {
            string partitionKey = PartitionKeyFormatter(productName);
            Loja.Data.Data manager = new Loja.Data.Data();

            Produto prod = (from s in webShared.CallWebService("GetProductsByPartitionKey",partitionKey)
                                   where s.Nome == productName.Split('-')[0]
                                   select s).FirstOrDefault();
            List<Produto> produtosNoCarrinho = new List<Produto>();
            if(Session["Produtos"] == null)
            {
                produtosNoCarrinho = new List<Produto>();
                produtosNoCarrinho.Add(prod);
            }
            else
            {
                produtosNoCarrinho = (List<Produto>)Session["Produtos"];
                if (produtosNoCarrinho.All(x => x.Id != prod.Id))
                {
                    produtosNoCarrinho.Add(prod);
                }
            }
            Session["Produtos"] = produtosNoCarrinho;
            return RedirectToAction("Index","Product", new { nomeProduto = productName, message = "Este artigo foi adicionado ao carrinho" });
        }
        [HttpGet]
        public ActionResult GetShoppingCart()
        {
            if(Session["Produtos"] != null)
            {
                ViewBag.Produtos = Session["Produtos"] as List<Produto>;
            }
            return View();
        }
        [HttpGet]
        public ActionResult RemoveShoppingProduct()
        {
            string[] urlParams = HttpContext.Request.Url.ToString().Split('/');

            ///verificao se session produtos tem mais que um produto, se nao, nao mostrar nada.
            List<Produto> lista = Session["Produtos"] as List<Produto>;
            Produto prodRemover = new Produto();
            foreach(Produto prod in lista)
            {
                if(prod.Id == urlParams[5])
                {
                    prodRemover = prod;
                }
            }
            lista.Remove(prodRemover);
            Session["Produtos"] = lista;
            return RedirectToAction("GetShoppingCart");
        }
        [HttpGet]
        public ActionResult CleanShoppingCart()
        {
            List<Produto> carrinho = Session["Produtos"] as List<Produto>;
            if(carrinho != null)
            {
                carrinho.RemoveAll(x => x.Id != null);
                Session["Produtos"] = carrinho;
            }
            return RedirectToAction("GetShoppingCart");
        }
        [HttpPost]
        public ActionResult PayShoppingCart(FormCollection carrinho)
        {
            //chamar servico externo de Loja.Services
            return RedirectToAction("");
        }
        private string PartitionKeyFormatter(string uneditedPK)
        {
            string editedPK = uneditedPK.Split('-')[1].First() + "-" + uneditedPK.Split('-')[2].Substring(0, 3);
            return editedPK;
        }
    }
}