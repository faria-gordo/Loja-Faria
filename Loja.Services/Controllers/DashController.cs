using Loja.Library;
using Loja.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO: 
    /// </summary>
    public class DashController : ApiController
    {
        private readonly Data.Data lojaManager = new Data.Data("LojaFaria");
        private readonly Data.Data carrinhoManager = new Data.Data("Carrinho");
        public IHttpActionResult GetProducts()
        {
            List<Produto> produtos = lojaManager.SelecionarProdutos();
            return Ok(produtos);
        }
        [HttpGet]
        public IHttpActionResult GetProduct(string id)
        {
            //caso produtos seja null, levar NotFound
            List<Produto> produtos;
            produtos = lojaManager.SelecionarProdutoPorPartitionKey(id);
            if(produtos == null)
            {
                produtos = lojaManager.SelecionarProdutoPorRowKey(id);
                if (produtos == null)
                {
                    produtos = lojaManager.SelecionarProdutoPorNome(id);
                }
            }
            return Ok(produtos);
        }
        [HttpPost]
        public IHttpActionResult addProduct(JObject json)
        {
            Produto produtoNovo = new JavaScriptSerializer().Deserialize<Produto>(json.ToString());
            string message = lojaManager.AdicionarProduto(produtoNovo);
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult addType(JObject json)
        {
            SeccaoTipoProduto tipo  = new JavaScriptSerializer().Deserialize<SeccaoTipoProduto>(json.ToString());
            string message = lojaManager.AdicionarTipo(tipo);
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult addSection(JObject json)
        {
            SeccaoTipoProduto seccao = new JavaScriptSerializer().Deserialize<SeccaoTipoProduto>(json.ToString());
            if(seccao.Tipo == null && seccao.Tipo == "")
            {
                return BadRequest();
            }
            string message = lojaManager.AdicionarSeccao(seccao);
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
