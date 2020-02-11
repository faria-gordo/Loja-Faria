using Loja.Library;
using Loja.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Loja.Services.Controllers
{
    public class UserController : ApiController
    {
        private readonly Data.Data userManager = new Data.Data("Users");
        private readonly Shared lib = new Shared();
        [HttpPost]
        public IHttpActionResult addUser(string userInfo)
        {
            //verificar se nao existe ja um user com email igual.
            User user = new User();
            JObject jo = new JObject(userInfo);
            JToken jUser = jo["user"];
            user.Nome = (string) jUser["firstname"];
            user.Apelido = (string)jUser["lastname"];
            user.Email = (string)jUser["email"];
            user.Password = (string)jUser["password"];
            user.QuantLogins = 1;
            user.Autenticado = true;
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
        public IHttpActionResult isUserRegistered()
        {
            return Ok();
        }
    }
}