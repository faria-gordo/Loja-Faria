using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loja.Modelos;

namespace Loja.Controllers
{
    public class ProductController : Controller
    {
        //Em vez de string ira haver o modelo de product com url, descricao, todos os campos do excel
        [HttpGet]
        public ActionResult Index(string nomeProduto)
         {
            Data.Data dataManager = new Data.Data();
            //Validation on nomeProduto
            string partitionKey = nomeProduto.Split('-')[0].Replace("_","-");
            List<Produto> produtos = dataManager.Selecionar(partitionKey);
            Produto produto = (from p in produtos
                               where p.Nome == nomeProduto.Split('-')[1].Replace("_"," ")
                               select p).First();
            return View(produto);
        }
    }
}