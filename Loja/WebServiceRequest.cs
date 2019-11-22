using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Loja
{
    public class WebServiceRequest
    {
        public List<Produto> CallWebService(string method, string identifier)
        {
            List<Produto> produtos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44389/web/");
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