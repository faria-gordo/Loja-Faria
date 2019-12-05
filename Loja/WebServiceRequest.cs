using Loja.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Loja
{
    /// <summary>
    /// 
    /// TODO:
    ///     - usar feedback message usando o helper de mensagens a criar.
    ///     - Por esta class no shared
    /// </summary>
    public class WebServiceRequest
    {
        public List<Produto> CallWebService(string controller, string method, string identifier, bool inCart)
        {
            List<Produto> produtos = null;
            using (var client = new HttpClient())
            {
                    client.BaseAddress = new Uri($"http://localhost:44389/{controller}/");
                    //HTTP GET
                    var responseTask = client.GetAsync($"{method}/" + $"{identifier}");
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Produto>>();
                        readTask.Wait();

                        produtos = readTask.Result;
                    }
            }
            return produtos;
        }
    }
}