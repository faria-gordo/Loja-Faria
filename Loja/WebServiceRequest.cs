using Loja.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Loja
{
    /// <summary>
    /// 
    ///  Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    /// 
    ///     //De momento este helper e usado para chamar servicos de carrinhos, produtos, users, etc. Generalizar metodo para facilitar uso.
    ///     //Por todos os callers dos servicos em Shared. 
    ///     
    /// </summary>
    public class WebServiceRequest
    {
        public List<Produto> CallWebService(string controller, string method, string identifier, bool inCart)
        {
            List<Produto> produtos = null;
            if (inCart)
            {
                List<Produto> carrinho = JsonConvert.DeserializeObject<List<Produto>>(identifier);
                identifier = "";
                int max = carrinho.Count;
                foreach (Produto prod in carrinho)
                {
                    if(carrinho.IndexOf(prod) == max - 1)
                    {
                        identifier += prod.Id + "_" + prod.Quantidade;
                    }
                    else
                    {
                        identifier += prod.Id + "_" + prod.Quantidade + ",";
                    }
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:44389/");
                var responseTask = client.GetAsync($"{controller}/{method}/{identifier}");
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Produto>>();
                    readTask.Wait();
                    produtos = readTask.Result;
                    return produtos;
                }
                else
                {
                    return null;
                }
                
            }

        }
    }
}