using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Loja.Controllers
{
    public class UserController: Controller
    {
        readonly private WebServiceRequest webShared = new WebServiceRequest();
        private string urlContext;
        public ActionResult Login()
        {
            urlContext = HttpContext.Request.Url.ToString();
            return View();
        }
        [HttpPost]
        public string logInUser(FormCollection form)
        {
            string[] userInfo = new string[2];
            string email;
            string password;
            if(form.Count < 3)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    if (i == 0) 
                    {
                        email = userInfo[i];
                    }
                    else
                    {
                        using (SHA512 sha = new SHA512Managed())
                        {
                            password = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(userInfo[0]))).Replace("-", "").ToLower();
                        }
                    }
                }
            }
            //Verificar se existe
            //Chamar Loja.Service
            return "feedback";
        }
        [HttpPost]
        public string AddNewUser(FormCollection form)
        {
            User novoUser = new User();
            string[] userInfo = new string[4];
            if (form.Count < 5)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    if (i == 0)
                    {
                        novoUser.Nome = userInfo[i];
                    }
                    if(i == 1)
                    {
                        novoUser.Apelido = userInfo[0];
                    }
                    if(i == 2)
                    {
                        novoUser.Email = userInfo[0];
                    }
                    else
                    {
                        using (SHA512 sha = new SHA512Managed())
                        {
                            novoUser.Password = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(userInfo[0]))).Replace("-", "").ToLower();
                        }
                    }
                }
            }
            var userJson = new JavaScriptSerializer().Serialize(novoUser);
            webShared.CallWebService("User", "addUser", userJson, false);
            return "feedback";
        }
        public RedirectResult LoggedIn()
        {
            return Redirect(urlContext);
        }
    }
}