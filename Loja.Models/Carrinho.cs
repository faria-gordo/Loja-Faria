using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class Carrinho
    {
        public string IdCompra { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Seccao { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        //public DateTime Data { get; set; }
        public string Url { get; set; }
    }
}
