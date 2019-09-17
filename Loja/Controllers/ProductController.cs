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
        public ActionResult Index(Produto product)
        {
            return View();
        }
    }
}