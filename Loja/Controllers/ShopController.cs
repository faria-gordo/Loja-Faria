using Loja.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    ///         
    ///     --associar carrinho e user pelo email, associar nome do carrinho a uma propriedade a acrescentar ao user.
    /// 
    /// </summary>
    public class ShopController : Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
        // GET: Shop
        [HttpPost]
        public ActionResult Index(string product)
        {
            string productName = null;
            string partitionKey = null;
            int? quantity = null;
            if (product == null)
            {
                productName = Request.Form["productName"].Split('-')[0];
                partitionKey = PartitionKeyFormatter(Request.Form["productName"]);
                quantity = Int32.Parse(Request.Form["quantity"]);
            }
            else
            {
                partitionKey = PartitionKeyFormatter(product);
                productName = product.Split('-')[0];
            }
            Loja.Data.Data manager = new Loja.Data.Data("LojaFaria");

            Produto prod = (from s in webShared.CallWebService("web", "GetProduct", partitionKey, false)
                            where s.Nome == productName.Split('-')[0]
                            select s).FirstOrDefault();
            if (product != null)
            {
                prod.Quantidade = Int32.Parse(product.Split('-')[3]);
            }
            else
            {
                prod.Quantidade = quantity.GetValueOrDefault(1);
            }
            List<Produto> produtosNoCarrinho = new List<Produto>();
            if (Session["Produtos"] == null)
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
            return RedirectToAction("Index", "Product", new { nomeProduto = productName, message = "Este artigo foi adicionado ao carrinho" });
        }
        [HttpGet]
        public ActionResult GetShoppingCart(string message)
        {
            if (Session["Produtos"] != null)
            {
                ViewBag.Produtos = Session["Produtos"] as List<Produto>;
                if(message != null)
                {
                    ViewBag.Message = message;
                }
                else
                {
                    ViewBag.Message = "";
                }
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
            foreach (Produto prod in lista)
            {
                if (prod.Id == urlParams[5])
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
            if (carrinho != null)
            {
                carrinho.RemoveAll(x => x.Id != null);
                Session["Produtos"] = carrinho;
            }
            return RedirectToAction("GetShoppingCart");
        }
        //AUTHENTICATION
        [HttpGet]
        public ActionResult AddShoppingCart()
        {
            //verificar email deste user e ver se esta autenticado
            if (Session["User"] != null)
            {
                User user = Session["User"] as User;
                if(user.Autenticado == false)
                {
                    return RedirectToAction("GetShoppingCart", "Shop", new { message = "Ocorreu um erro, saia da sua conta e entre outra vez" });
                }
                else
                {
                    List<Produto> carrinho = Session["Produtos"] as List<Produto>;
                    //Call payment service
                    webShared.CallWebService("Cart", "PutCarrinho", JsonConvert.SerializeObject(carrinho), true);
                    Session["Produtos"] = null;
                    return RedirectToAction("GetShoppingCart", "Shop", new { message = "Carrinho pronto pagamento" });
                }
            }
            else
            {
                return RedirectToAction("GetShoppingCart", "Shop", new { message = "Para comprar produtos tem que ter conta no nosso site" });
            }
        }
        // Ainda por desenvolver
        [HttpPost]
        public ActionResult PayShoppingCart(FormCollection carrinho)
        {
            //chamar servico externo de Loja.Services chamado Cart
            return RedirectToAction("");
        }
        private string PartitionKeyFormatter(string uneditedPK)
        {
            string editedPK = uneditedPK.Split('-')[1].First() + "-" + uneditedPK.Split('-')[2].Substring(0, 3);
            return editedPK;
        }
    }
}