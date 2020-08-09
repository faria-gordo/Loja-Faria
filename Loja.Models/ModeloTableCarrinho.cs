using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class ModeloTableCarrinho: TableEntity
    {
        public ModeloTableCarrinho() { }
        public ModeloTableCarrinho(string partitionKey, string rowKey, string nome, List<Produto> produtos,User user, string descricao, double preco, /*DateTime dataDeCompra,*/string formaPagamento)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Nome = nome;
            Produtos = produtos;
            User = user;
            Descricao = descricao;
            Preco = preco;
            FormaPagamento = formaPagamento;
            //DataDeCompra = dataDeCompra;
        }
        public string Email => PartitionKey;
        public string IdCompra => RowKey; 
        public string Nome { get; set; }
        public List<Produto> Produtos { get; set; }
        public User User { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string FormaPagamento { get; set; }
        //public DateTime DataDeCompra { get; set; }
    }
}

