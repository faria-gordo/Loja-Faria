using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Data;
using Loja.Models;
using Loja.Library;
using Newtonsoft.Json;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// 
    /// TODO:
    ///           
    ///         -Haver'a dois metodos para cada Tipo de acao.
    ///         -So ira poder receber identifiers. Fazer um metodo para criar identifiers 'string especifica que contem o rk/pk ou nome
    /// 
    /// </summary>
    public class WebController : ApiController
    {   
        private readonly Data.Data manager = new Data.Data();
        private readonly Shared lib = new Shared();
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult GetProduct(string data)
        {
            // 1 verificar pelo qual se quer obter o produto (RK/PK/Nome)
            // 2 verificar identifier, saber se é PK ou RK ou Nome
            string response = lib.WebServiceRequestFormatData(data);
            Produto produto = new Produto();
            switch (response.Split('-')[0].ToLower())
            {
                case "partitionkey":
                    produto = manager.SelecionarProdutoPorPartitionKey(response.Split('-')[1]);
                    break;
                case "rowkey":
                    produto = manager.SelecionarProdutoPorRowKey(response.Split('-')[1]);
                    break;
                case "name":
                    produto = manager.SelecionarProdutoPorNome(response.Split('-')[1]);
                    break;
                default:
                    return NotFound();
            }
            return Ok(produto);
        }

        //Update
        [HttpPut]
        public IHttpActionResult PutProduct(string data)
        {
            //Get specific product and upload it
            return Ok();
        }
        //ONLY USING THE DASHBOARD (use authentication)

        //Create
        [HttpPost]
        public IHttpActionResult PostProduct(string data)
        {
            //Chamar Loja um método que adicione estes produtos á loja para disposição.
            return Ok();
        }
        [HttpPost]
        public IHttpActionResult PostProducts(string data)
        {
            return Ok();
        }
        //Method only used when the admin deletes the product from the bd using the dashboard
        [HttpDelete]
        public IHttpActionResult DeleteProduct(string data)
        {
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteProducts(string data)
        {
            return Ok();
        }
    }
}
