using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Data;
using Loja.Models;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// 
    /// TODO:
    ///           
    /// 
    /// </summary>
    public class WebController : ApiController
    {
        private readonly Data.Data manager = new Data.Data();
        private List<Produto> produtos;
        public string GetProduct(Produto Produto)
        {
            return "";
        }
        public Produto GetProductByPartitionKey(string pk)
        {


            return new Produto();
        }
        public string GetProductByRowKey(string rk)
        {
            manager.SelecionarProdutoPorRowKey(rk);
            return "value";
        }
        public Produto GetProductByName(string name)
        {
            return new Produto();
        }
        public string GetProducts(List<Produto> produtos)
        {
            return null;
        }
        public List<Produto> GetProductsByPartitionKey(string pk)
        {
            produtos = manager.SelecionarProdutosPorPartitionKey(pk);
            return produtos;
        }
        //Update
        public string UpdateProduct(Produto produto, int quantidade)
        {
            return "";
        }
        public string UpdateProductByPartitionKey(string partitionKey, int quantidade)
        {
            return "";
        }
        public string UpdateProductByRowKey(string rowKey, int quantidade)
        {
            return "";
        }
        public string UpdateProductByName(string name, int quantidade)
        {
            return "";
        }
        public string UpdateProducts(List<Produto> produtos, int[] quantidades)
        {
            return "";
        }
        public string UpdateProductsByPartitionKey(string pk, int[] quantidades)
        {
            return "";
        }



        //ONLY USING THE DASHBOARD (use authentication)

        //Create
        [HttpPost]
        public void AddProduct([FromBody]Produto produto)
        {
            //Chamar Loja um método que adicione estes produtos á loja para disposição.
            manager.AdicionarProduto(produto);
        }
        [HttpPost]
        public void AddProducts([FromBody]List<Produto> produtos)
        {
            manager.AdicionarProdutos(produtos);
        }
        //Method only used when the admin deletes the product from the bd using the dashboard
        public void Delete(int id)
        {
        }
    }
}
