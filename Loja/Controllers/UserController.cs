using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Loja.Library;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// 
    ///     TODO:
    ///     
    ///     -QuantLogins nao esta a funcionar pois a incrementacao davariavel e so feita localmente, a tabela nao e atualizada
    ///     - De momento, ao criar um utilizador que ja existe, a pagina refresca completamente. Deve se fazer ajax asincrono para verificar db.
    /// </summary>
    public class UserController : Controller
    {
        readonly private WebServiceRequestPublic webShared = new WebServiceRequestPublic();
        private string message;
        public ActionResult Login(string message)
        {
            if(message == null)
            {
                Session["User"] = "entry";
            }
            else
            {
                Session["User"] = message;
            }
            return View();
        }
        [HttpPost]
        public RedirectResult logInUser(FormCollection form)
        {
            string[] userInfo = new string[2];
            User user = new User();
            if (form.Count < 3)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    if (i == 0)
                    {
                        user.Email = userInfo[i];
                    }
                    else
                    {
                        using (SHA512 sha = new SHA512Managed())
                        {
                            user.Password = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(userInfo[0]))).Replace("-", "").ToLower();
                        }
                    }
                }
            }
            var userJson = new JavaScriptSerializer().Serialize(user);
            User userLogged = webShared.CallUserWebService("User", "logInUser", userJson, false);
            if (userLogged != null)
            {
                Session["User"] = userLogged;
                return Redirect(Url.Action("index", "home"));
            }
            else
            {
                Session["User"] = "unmanaged";
                return Redirect(Url.Action("Login", "User", new { message = "Houve um erro ao entrar no site, tente novamente!" }));
            }
        }
        [HttpPost]
        public RedirectResult AddNewUser(FormCollection form)
        {
            User novoUser = new User();
            string[] userInfo;
            if (form.Count < 5)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    if (i == 0)
                    {
                        novoUser.Nome = userInfo[i];
                    }
                    if (i == 1)
                    {
                        novoUser.Apelido = userInfo[0];
                    }
                    if (i == 2)
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
                    novoUser.FotoUrl = "https://static.fnac-static.com/multimedia/Images/PT/NR/a1/d5/0e/972193/1540-1.jpg";
                }
            }
            var userJson = new JavaScriptSerializer().Serialize(novoUser);
            message = webShared.CallWebService("User", "addUser", userJson, false);
            if(message != "\"Já existe esse email registado.\"")
            {
                if(novoUser != null)
                {
                    novoUser.Autenticado = true;
                    novoUser.QuantLogins = 1;
                    Session["User"] = novoUser;
                    Session["UserAddMessage"] = message;
                    return Redirect(Url.Action("index", "home"));
                }
                else
                {
                    Session["User"] = null;
                    Session["UserAddMessage"] = message;
                    return Redirect(Url.Action("Login", "User"));
                }
            }
            else
            {
                Session["User"] = null;
                Session["UserAddMessage"] = message;
                return Redirect(Url.Action("Login","User"));
            }

        }

        [HttpGet]
        public RedirectResult logOff()
        {
            User user = Session["User"] as User;
            var userJson = new JavaScriptSerializer().Serialize(user);
            string message = webShared.CallWebService("User", "logOffUser", userJson, false);
            if(message != null)
            {
                Session["User"] = null;
                Session["UserAddMessage"] = message;
            }
            else
            {
                Session["UserAddMessage"] = message;
            }
            return Redirect(Url.Action("index","home"));
        }
    }
}