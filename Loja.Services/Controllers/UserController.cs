using Loja.Library;
using Loja.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Loja.Services.Controllers
{
    public class UserController : ApiController
    {
        private readonly Data.Data userManager = new Data.Data("Users");
        [HttpPost]
        public IHttpActionResult logInUser(JObject userInfo)
        {
            User user = new JavaScriptSerializer().Deserialize<User>(userInfo.ToString());
            User userLogged = userManager.LogIn(user);
            if (userLogged != null)
            {
                return Ok(userLogged);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult addUser(JObject userInfo)
        {
            User user = new JavaScriptSerializer().Deserialize<User>(userInfo.ToString());
            string message = userManager.AdicionarUser(user);
            if(message != null)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public IHttpActionResult isUserRegistered(JObject userInfo)
        {
            User user = new JavaScriptSerializer().Deserialize<User>(userInfo.ToString());
            string message = userManager.VerificarUser(user);
            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IHttpActionResult logOffUser(JObject userInfo)
        {
            User user = new JavaScriptSerializer().Deserialize<User>(userInfo.ToString());
            string message = userManager.LogOffUser(user);
            return Ok(message);
        }
        [HttpPost]
        public IHttpActionResult changePassword(JObject userInfo)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string message = userManager.MudarPassword(jss.Deserialize<User>(userInfo.ToString()));
            return Ok(message);
        }
        [HttpGet]
        public IHttpActionResult getAllUsers()
        {
            List<User> users = new List<User>();
            users = userManager.SelecionarUsers();
            return Ok(users);
        }
    }
}