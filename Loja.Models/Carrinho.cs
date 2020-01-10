﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Modelos
{
    /// <summary>
    /// 
    /// todo:
    /// 
    ///     -Criar as propriedades do modelo Carrinho para por na nova tabela Carrinho
    /// </summary>
    class Carrinho
    {
        public string Id { get; set; }
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