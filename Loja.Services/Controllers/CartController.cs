﻿using Loja.Library;
using Loja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Models;
using Newtonsoft.Json;
using System.Text;

namespace Loja.Services.Controllers
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// </summary>
    public class CartController : ApiController
    {
        private readonly Data.Data manager = new Data.Data("Carrinho");
        private readonly Data.Data managerLoja = new Data.Data("LojaFaria");
        private readonly Data.Data managerNoti = new Data.Data("Notificacoes");
        [HttpGet]
        public IHttpActionResult GetCarrinhos()
        {
            List<Carrinho> carrinhos;
            carrinhos = manager.SelecionarCarrinhos();
            return Ok(carrinhos);
        }
        [HttpGet]
        [Route ("Cart/PutCarrinho/{identifier}")]
        public IHttpActionResult PutCarrinho(string identifier)
        {
            string message = "";
            List<Carrinho> carro = new List<Carrinho>();
            foreach (string id in identifier.Split(','))
            {
                Carrinho carrinho = new Carrinho();
                Produto produto = managerLoja.SelecionarProdutoPorRowKeyunico(id.Split('_')[0]);
                carrinho = manager.ProdutoToCarrinho(produto);
                message += manager.AdicionarCarrinho(carrinho);
                if(message == "Novo carrinho adicionado!")
                {
                    managerNoti.AdicionarNotificao(message);
                }
            }
            return Ok();
        }
        public bool Checkout(Carrinho carrinho, string payform, string userInfo)
        {
            //Recebe formulario de pagamento e adiciona carrinho para o dashboard
            return false;
        }
    }
}
