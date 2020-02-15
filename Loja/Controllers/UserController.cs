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
using System.Net.Mail;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// 
    ///     TODO:
    ///     
    ///     - De momento, ao criar um utilizador que ja existe, a pagina refresca completamente. Deve se fazer ajax assincrono para verificar db.
    /// 
    ///     Lista de smtp clients:
    ///     
    ///     pop.sapo.pt para sapo.pt
    ///     smtp.gmail.com para gmail.com
    /// </summary>
    public class UserController : Controller
    {

        readonly private WebServiceRequestPublic webShared = new WebServiceRequestPublic();
        private string message;
        public ActionResult Login(string message)
        {
            bool allowCP;
            if (Session["allowCP"] is bool)
            {
                allowCP = (bool)Session["allowCP"];
                if (allowCP)
                {
                    ViewBag.Allow = Session["allowCP"];
                }
            }
            if (Session["resetCod"] != null)
            {
                if (Int32.Parse(Session["resetCod"].ToString()) != null)
                {
                    ViewBag.Codigo = Session["resetCod"];
                }
            }
            if (message == null)
            {
                Session["User"] = "entry";
            }
            else
            {
                Session["User"] = message;
            }
            string ver = Session["User"] as String;
            string vere = Session["User"].ToString();
            var vesr = Session["User"];
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
            if (message != "\"Já existe esse email registado.\"")
            {
                if (novoUser != null)
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
                return Redirect(Url.Action("Login", "User"));
            }

        }

        [HttpGet]
        public RedirectResult logOff()
        {
            User user = Session["User"] as User;
            var userJson = new JavaScriptSerializer().Serialize(user);
            string message = webShared.CallWebService("User", "logOffUser", userJson, false);
            if (message != null)
            {
                Session["User"] = null;
                Session["UserAddMessage"] = message;
            }
            else
            {
                Session["UserAddMessage"] = message;
            }
            return Redirect(Url.Action("index", "home"));
        }
        [HttpPost]
        public RedirectResult resetPassword(FormCollection form)
        {
            User user = new User();
            string[] userInfo;
            if (form.Count < 2)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    user.Email = userInfo[0];
                }
            }
            var userJson = new JavaScriptSerializer().Serialize(user);
            message = webShared.CallWebService("User", "isUserRegistered", userJson, false);
            if (message != null)
            {
                DateTime date = DateTime.Now;
                int cod = (date.Month + date.Day + ((date.Year / 100) + ((date.Hour + date.Minute) * 24)) + 366) - date.Millisecond + date.Second;
                if (cod <= 0)
                {
                    cod += 6000;
                }
                else if (cod > 0 && cod <= 999)
                {
                    cod += 4000;
                }
                Session["resetCod"] = cod;
                Session["resetPWEmail"] = user.Email;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("testedev123123123@gmail.com");
                mail.To.Add($"{user.Email}");
                mail.Subject = "Loja-Faria Email de confirmação";
                mail.Body = $"Copia o codigo e confirma o teu email. Codigo: {cod}\n" +
                    "Se fechares a sessão terás que repetir este processo.\n"
                    + "Por favor não respondes a este email.";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("testedev123123123@gmail.com", "123qwe,.-");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            else
            {
                Session["resetCod"] = 0000;
            }
            return Redirect(Url.Action("Login", "User", new { message = message }));
        }
        [HttpPost]
        public RedirectResult allowChangePassword(FormCollection form)
        {
            string[] userInfo;
            if (form.Count < 2)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    int cod = Int32.Parse(userInfo[0]);
                    if (cod == Int32.Parse(Session["resetCod"].ToString()))
                    {
                        bool allowChangePassword = true;
                        Session["allowCP"] = allowChangePassword;
                    }
                }
            }
            return Redirect(Url.Action("Login", "User", new { message = "Permissão cedida para alterar a tua palavra passe" }));
        }
        [HttpPost]
        public RedirectResult changePassword(FormCollection form)
        {
            User newPWUser = new User();
            string[] userInfo;
            message = null;
            if (form.Count < 2)
            {
                for (int i = 0; i < form.Count; i++)
                {
                    userInfo = form.GetValues(form.AllKeys[i]);
                    newPWUser.Email = Session["resetPWEmail"].ToString();
                    //Add encryption
                    using (SHA512 sha = new SHA512Managed())
                    {
                        newPWUser.Password = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(userInfo[0]))).Replace("-", "").ToLower();
                    }
                }
                if (newPWUser != null)
                {
                    var userJson = new JavaScriptSerializer().Serialize(newPWUser);
                    message = webShared.CallWebService("User", "changePassword", userJson, false);
                    if (message != null)
                    {
                        return Redirect(Url.Action("Login", "User", new { message = "Nova Palavra passe registada" }));
                    }
                }
            }
            return Redirect(Url.Action("Login", "User", new { message = "Ocorreu um erro. Tente novamente" }));
        }
    }
}