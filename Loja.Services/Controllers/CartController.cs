using Loja.Library;
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
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

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
        [HttpPost]
        public IHttpActionResult PutCarrinho(JObject identifier)
        {
            string message = "";
            Carrinho carrinho = new JavaScriptSerializer().Deserialize<Carrinho>(identifier.ToString());
            message = manager.AdicionarCarrinho(carrinho);
            if (message == "Novo carrinho adicionado!")
            {
                managerNoti.AdicionarNotificao(message);
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
