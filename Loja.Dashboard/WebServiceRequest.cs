using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Loja.Dashboard
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// </summary>
    public class WebServiceRequest
    {
        public List<Carrinho> CallCartWebService(string controller, string method, string identifier)
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
        public List<User> CallUserWebService(string controller, string method, string identifier)
        {
            List<User> utilizadores = new List<User>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:44389/{controller}/");
                var responseTaskPost = client.GetAsync($"{method}");
                var resultpost = responseTaskPost.Result;
                if (resultpost.IsSuccessStatusCode)
                {
                    var users = resultpost.Content.ReadAsAsync<List<User>>();
                    users.Wait();

                    utilizadores = users.Result;
                }
                return utilizadores;
            }
        }
    }
}