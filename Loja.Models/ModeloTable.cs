using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loja.Models
{
    public class ModeloTable : TableEntity
    {
        public ModeloTable() { }
        public ModeloTable(string partitionKey, string rowKey, string nome, string tipo,string seccao,string descricao ,double preco, DateTime dataDeAquisicao, DateTime dataDeVenda, /*string nomeImagem,string pathImagem,*/HttpPostedFileBase imagem, string url)
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
            //NomeImagem = nomeImagem;
            //PathImagem = pathImagem;
            Imagem = imagem;
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
        //public string NomeImagem { get; set; }
        //public string PathImagem { get; set; }
        public HttpPostedFileBase Imagem { get; set; }
        public string Url { get; set; }
    }
}
