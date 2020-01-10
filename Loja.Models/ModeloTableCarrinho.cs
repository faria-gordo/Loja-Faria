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
        public ModeloTableCarrinho(string partitionKey, string rowKey, string nome, string tipo, string seccao, string descricao, double preco,int quantidade, DateTime dataDeCompra, string url)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Nome = nome;
            Tipo = tipo;
            Seccao = seccao;
            Descricao = descricao;
            Preco = preco;
            Quantidade = quantidade;
            //DataDeCompra = dataDeCompra;
            Url = url;
        }
        public string Email => PartitionKey;
        public string IdCompra => RowKey;
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Seccao { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        //public DateTime DataDeCompra { get; set; }
        public string Url { get; set; }
    }
}

