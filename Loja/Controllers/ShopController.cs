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
        public ActionResult Index(List<Produto> produtos, int quantidade)
        {
            //Criar varias partials views para criar a pagina de carrinho
            //Uma terá um iterador para a lista produtos
            ViewBag.Produtos = produtos;
            return View();
        }
    }
}