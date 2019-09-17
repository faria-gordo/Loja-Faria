using Loja.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Criar projeto para Modelos
    /// Criar projeto para fazer ligacao a base de dados e criar helpers (select, update, delete, etc) e por todas as conn strings
    /// 
    /// Criar js para adicionar/retirar/trocar ao carrinho atraves de ajax, recolhendo form no ProductController
    /// Criar js para modificar quantidade
    /// 
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        string seccoes = "Bijutaria;Lembrancas;Religiosos;Diversos";
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
            string[] urlParams = HttpContext.Request.Url.ToString().Split('/');
            Data.Data manager = new Data.Data();
            List<Produto> produtos = manager.Selecionar(urlParams[5].ToUpper().First() + "-" + " ");
            ViewBag.Seccao = urlParams[5]; 
            ViewBag.Produtos = produtos;
            return View();
        }
        public ActionResult DataTable()
        {
            List<Produto> produtosAllType = new List<Produto>();
            Data.Data manager = new Data.Data();
            foreach (string seccao in seccoes.Split(';'))
            {
                produtosAllType.AddRange(manager.Selecionar(seccao));
            }
            ViewBag.TodosOsProdutos = produtosAllType;
            return View();
        }
        public void UpdateDataTable(Produto produto)
        {   
            Data.Data manager = new Data.Data();

        }
    }
}