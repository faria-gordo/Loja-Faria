using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Loja.Library
{
    /// <summary>
    /// 
    /// 
    ///         TODO:
    ///         
    ///     Esquecime
    /// </summary>
    public class WebServiceRequestPublic
    {
        private string message;
        public string CallWebService(string controller, string method, string identifier, bool inCart)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://lojaservices.azurewebsites.net/");
                var content = new StringContent(identifier, Encoding.UTF8, "application/json");
                var responseTaskPost = client.PostAsync($"{method}", content);
                var resultpost = responseTaskPost.Result;
                if (resultpost.IsSuccessStatusCode)
                {
                    message = resultpost.Content.ReadAsStringAsync().Result;
                }
                return message;
            }
        }
        public User CallUserWebService(string controller, string method, string identifier, bool inCart)
        {
            User userLogged = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://lojaservices.azurewebsites.net/{controller}/");
                var identifier2 = identifier.ToString();
                var content = new StringContent(identifier, Encoding.UTF8, "application/json");
                var responseTaskPost = client.PostAsync($"{method}", content);
                var resultpost = responseTaskPost.Result;
                if (resultpost.IsSuccessStatusCode)
                {
                    string user = resultpost.Content.ReadAsStringAsync().Result;
                    userLogged = new JavaScriptSerializer().Deserialize<User>(user);
                }
                return userLogged;
            }
        }
    }
}
