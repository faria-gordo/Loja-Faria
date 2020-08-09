using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loja.Models
{
    public class Produto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public DateTime DataDeAquisicao { get; set; }
        public DateTime DataDeVenda { get; set; }
        public string Seccao { get; set; }
        public int Quantidade { get; set; }
        //public string NomeImagem { get; set; }
        //public string PathImagem { get; set; }
        public HttpPostedFileBase Imagem { get; set; }
        public string Url { get; set; }
    }
}
