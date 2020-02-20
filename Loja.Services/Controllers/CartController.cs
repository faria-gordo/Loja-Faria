using Loja.Library;
using Loja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Models;
using Newtonsoft.Json;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:      
    /// 
    /// </summary>
    public class CartController : ApiController
    {
        private readonly Data.Data manager = new Data.Data("Carrinho");
        private readonly Data.Data managerLoja = new Data.Data("LojaFaria");
        [HttpGet]
        public IHttpActionResult GetCarrinhos()
        {
            List<Carrinho> carrinhos;
            carrinhos = manager.SelecionarCarrinhos();
            return Ok(carrinhos);
        }
        [HttpGet]
        [Route ("Cart/PutCarrinho/{identifier}")]
        public IHttpActionResult PutCarrinho(string identifier)
        {
            string message = "";
            List<Carrinho> carro = new List<Carrinho>();
            foreach (string id in identifier.Split(','))
            {
                Carrinho carrinho = new Carrinho();
                Produto produto = managerLoja.SelecionarProdutoPorRowKeyunico(id.Split('_')[0]);
                carrinho = manager.ProdutoToCarrinho(produto);
                carrinho.Quantidade = Int32.Parse(id.Split('_')[1]);
                message += manager.AdicionarCarrinho(carrinho);
            }
            return Ok();
        }
    }
}
