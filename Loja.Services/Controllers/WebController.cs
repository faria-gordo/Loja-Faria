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
using System.Web.Services;
using System.Web.Script.Services;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// 
    /// TODO:
    ///           
    ///     Modificar GetProduct.
    /// </summary>
    public class WebController : ApiController
    {
        private readonly Data.Data manager = new Data.Data("LojaFaria");
        private readonly Data.Data managerST = new Data.Data("SeccaoTipoProduto");
        [HttpGet]
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IHttpActionResult GetProducts(string data)
        {
            List<Produto> produtos = new List<Produto>();
            List<string> tipos = managerST.VerificarTipos(data);
            if (tipos != null)
            {
                foreach (string tipo in tipos)
                {
                    string partitionKey = data + "-" + tipo;
                    List<Produto> searchedProds = manager.SelecionarProdutoPorPartitionKey(partitionKey);
                    if (searchedProds != null && searchedProds.Count() > 0)
                    {
                        foreach (Produto prod in searchedProds)
                        {
                            produtos.Add(prod);
                        }
                    }
                }
            }
            if (produtos.Count() > 0)
            {
                return Ok(produtos);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IHttpActionResult GetProduct(string data)
        {
            //data is the name of the product
            List<Produto> produtos = manager.SelecionarProdutoPorPartitionKey(data);
            if (produtos.Count() == 0)
            {
                produtos = manager.SelecionarProdutoPorRowKey(data);
                if (produtos.Count() == 0)
                {
                    produtos = manager.SelecionarProdutoPorNome(data);
                }
            }
            if (produtos.Count() > 0)
            {
                return Ok(produtos);
            }
            else
            {
                return NotFound();
            }
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
    }
}
