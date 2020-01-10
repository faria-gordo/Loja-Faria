using Loja.Library;
using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// TODO: 
    ///     -Apos criacao da tabela, aceder a essa tabela e criar um formater para a PK e RK. 
    ///     -Adicionar no Loja.Models o modelo do carrinho
    ///     - Por o metodo WebServiceRequest no Shared
    ///     - Nome da tabela deve ser encaminhado no controller mvc nao aqui. Enviar como parametro em identifier
    /// </summary>
    public class DashController : ApiController
    {
        private readonly Data.Data manager = new Data.Data("LojaFaria");
        private readonly Shared lib = new Shared();
        public IHttpActionResult GetProducts()
        {
            List<Produto> produtos = manager.SelecionarProdutos();
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
                    produtos = manager.SelecionarProdutoPorPartitionKey(response.Split('-')[1] + "-" + response.Split('-')[2]);
                    break;
                case "rowkey":
                    produtos = manager.SelecionarProdutoPorRowKey(response.Split('-')[1]);
                    break;
                case "name":
                    produtos = manager.SelecionarProdutoPorNome(response.Split('-')[1]);
                    break;
                default:
                    return NotFound();
            }
            //chamar servico para informar dashboard

            return Ok(produtos);
        }
        public IHttpActionResult PutProduct(string id)
        {
            //Dashboard
            return Ok();
        }
    }
}
