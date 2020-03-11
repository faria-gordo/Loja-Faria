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
        public List<Produto> Produtos { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public User User { get; set; }
        public string Pay { get; set; }
        //public DateTime Data { get; set; }
    }
}
