using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class ModeloTable : TableEntity
    {
        public ModeloTable() { }
        public ModeloTable(string partitionKey, string rowKey, string nome, string tipo,string seccao,string descricao ,double preco, DateTime dataDeAquisicao, DateTime dataDeVenda, string url)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Nome = nome;
            Tipo = tipo;
            Seccao = seccao;
            Descricao = descricao;
            Preco = preco;
            DataDeAquisicao = dataDeAquisicao;
            DataDeVenda = dataDeVenda;
            Url = url;
        }
        public string SeccaoTipo => PartitionKey;
        public string Id => RowKey;
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Seccao { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public DateTime DataDeAquisicao { get; set; }
        public DateTime DataDeVenda { get; set; }
        public string Url { get; set; }
    }
}
