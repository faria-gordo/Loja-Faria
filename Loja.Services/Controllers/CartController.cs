using Loja.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Models;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    ///     -Irao passar por aqui todos os pedidos relacionados com os carrinhos. 
    ///         Desde o registo de carrinhos, compras, carrinhos cancelados. Estado de compra. 
    ///         
    /// 
    /// </summary>
    public class CartController : ApiController
    {
        private readonly Data.Data manager = new Data.Data("Carrinho");

        //Dashboard
        public IHttpActionResult GetCarrinhos()
        {
            List<Carrinho> carrinhos = new List<Carrinho>();
            carrinhos = manager.SelecionarCarrinhos();
            return Ok();
        }
        //ShopController
        public IHttpActionResult PutCarrinho(Carrinho carrinho)
        {
            string message = manager.AdicionarCarrinho(carrinho);
            return Ok(message);
        }
    }
}
