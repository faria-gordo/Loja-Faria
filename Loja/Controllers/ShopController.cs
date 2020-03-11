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
                partitionKey = Request.Form["productName"].Split('-')[1] + "-" + Request.Form["productName"].Split('-')[2];
                quantity = Int32.Parse(Request.Form["quantity"]);
            }
            else
            {
                partitionKey = product;
                productName = product.Split('-')[0];
            }
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
            return RedirectToAction("Index", "Product", new { prodName = productName,prodSecc = partitionKey.Split('-')[0], prodTipo = partitionKey.Split('-')[1], message = "Este artigo foi adicionado ao carrinho" });
        }
        [HttpGet]
        public ActionResult GetShoppingCart(string message)
        {
            if (Session["Produtos"] != null)
            {
                ViewBag.Produtos = Session["Produtos"] as List<Produto>;
                if (message != null)
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

            ///verificar se session produtos tem mais que um produto, se nao, nao mostrar nada.
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
        public RedirectToRouteResult AddShoppingCart()
        {
            User user = Session["User"] as User;
            if (user != null)
            {
                if (user.Autenticado == false)
                {
                    return RedirectToAction("GetShoppingCart", "Shop", new { message = "Ocorreu um erro, saia da sua conta e entre outra vez" });
                }
                else
                {
                    //Call payment service
                    return RedirectToAction("PayShoppingCart");
                }
            }
            else
            {
                return RedirectToAction("GetShoppingCart", "Shop", new { message = "Para comprar produtos tem que ter conta no nosso site" });
            }
        }
        [HttpGet]
        public ActionResult PayShoppingCart()
        {
            List<Produto> produtos = Session["Produtos"] as List<Produto>;
            Carrinho carro = new Carrinho();
            double preco = 0;
            carro.Produtos = produtos;
            carro.IdCompra = Guid.NewGuid().ToString();
            carro.Nome = "";
            if(Session["User"] == null)
            {
                return RedirectToAction("Login","User",new { message = "Não está autenticado, por favor lige á sua conta" });
            }
            carro.Email = (Session["User"] as User).Email;
            carro.Descricao = "";
            foreach(Produto prod in produtos)
            {
                if(prod == produtos.Last())
                {
                    preco += prod.Preco;
                    carro.Preco = preco;
                }
                else
                {
                    preco += prod.Preco;
                }
            }
            ViewBag.Carrinho = carro;
            Session["Carrinho"] = carro;
            return View("PayShoppingCart", carro);
        }
    }
}