using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Loja.Dashboard
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// </summary>
    public class WebServiceRequest
    {
        public List<Carrinho> CallWebService(string controller, string method, string identifier)
        {
            List<Carrinho> carrinho = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:44389/{controller}/");
                //HTTP GET
                var responseTask = client.GetAsync($"{method}/" + $"{identifier}");
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Carrinho>>();
                    readTask.Wait();

                    carrinho = readTask.Result;
                }
            }
            return carrinho;
        }
    }
}