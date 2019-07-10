using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Loja.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime DataDeAdquiricao { get; set; }
        public DateTime DataDeVenda{ get; set; }
    }
}