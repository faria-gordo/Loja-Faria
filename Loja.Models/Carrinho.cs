using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    /// <summary>
    /// 
    /// todo:
    /// 
    ///     -Criar as propriedades do modelo Carrinho para por na nova tabela Carrinho
    /// </summary>
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
        public DateTime DataDeCompra { get; set; }
        public string Url { get; set; }
    }
}
