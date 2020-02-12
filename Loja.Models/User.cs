using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    //adicionar aqui e no tablemodel para adicionar link de foto para perfil.
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int QuantLogins { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        //public DateTime DataNascimento { get; set; }
        public bool Autenticado { get; set; }
    }
}
