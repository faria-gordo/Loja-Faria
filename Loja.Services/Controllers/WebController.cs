using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Data;
using Loja.Models;
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
        [HttpGet]
        public string GetProducts()
        {
            return "All products";
        }
        [HttpGet]
        public string GetProduct(string identifier)
        {
            //verificar identifier, saber se é PK ou RK ou Nome
            return "Specific Product";
        }

        //Update
        [HttpPut]
        public string PutProduct(Produto produto, int quantidade)
        {
            return "";
        }
        [HttpPut]
        public string PutProductByPartitionKey(string partitionKey, int quantidade)
        {
            return "";
        }
        [HttpPut]
        public string PutProductByRowKey(string rowKey, int quantidade)
        {
            return "";
        }
        [HttpPut]
        public string PutProductByName(string name, int quantidade)
        {
            return "";
        }
        [HttpPut]
        public string PutProducts(List<Produto> produtos, int[] quantidades)
        {
            return "";
        }
        [HttpPut]
        public string PutProductsByPartitionKey(string pk, int[] quantidades)
        {
            return "";
        }



        //ONLY USING THE DASHBOARD (use authentication)

        //Create
        [HttpPost]
        public void PostProduct([FromBody]Produto produto)
        {
            //Chamar Loja um método que adicione estes produtos á loja para disposição.
            manager.AdicionarProduto(produto);
        }
        [HttpPost]
        public void PostProducts([FromBody]List<Produto> produtos)
        {
            manager.AdicionarProdutos(produtos);
        }
        //Method only used when the admin deletes the product from the bd using the dashboard
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
