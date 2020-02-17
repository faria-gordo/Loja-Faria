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
        private readonly Shared lib = new Shared();
        public IHttpActionResult GetProducts()
        {
            List<Produto> produtos = lojaManager.SelecionarProdutos();
            return Ok(produtos);
        }
        [HttpGet]
        public IHttpActionResult GetProduct(string id)
        {
            // 1 verificar pelo qual se quer obter o produto (RK/PK/Nome)
            // 2 verificar identifier, saber se é PK ou RK ou Nome
            // 3 caso nao haja PK ou RK ou Name, devolver todos, caso do dashboard (se identifier == "")
            string response = lib.WebServiceRequestFormatData(id);
            List<Produto> produtos = new List<Produto>();
            switch (response.Split('-')[0].ToLower())
            {
                case "partitionkey":
                    produtos = lojaManager.SelecionarProdutoPorPartitionKey(response.Split('-')[1] + "-" + response.Split('-')[2]);
                    break;
                case "rowkey":
                    produtos.Add(lojaManager.SelecionarProdutoPorRowKey(response.Split('-')[1]));
                    break;
                case "name":
                    produtos = lojaManager.SelecionarProdutoPorNome(response.Split('-')[1]);
                    break;
                default:
                    return NotFound();
            }
            //chamar servico para informar dashboard

            return Ok(produtos);
        }
        [HttpPost]
        public IHttpActionResult addProduct(JObject json)
        {
            Produto produtoNovo = new JavaScriptSerializer().Deserialize<Produto>(json.ToString());
            string message = lojaManager.AdicionarProduto(produtoNovo);
            if(message != null)
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
