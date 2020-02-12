using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class ModeloTableUser: TableEntity
    {
        public ModeloTableUser() { }
        public ModeloTableUser(string partitionKey, string rowKey, int quantLogins, string nome, string apelido, bool autenticado,string fotoUrl)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            QuantLogins = quantLogins;
            Nome = nome;
            Apelido = apelido;
            Autenticado = autenticado;
            FotoUrl = fotoUrl;
        }

        public string Email => RowKey;
        public string Password => PartitionKey;
        public int QuantLogins { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public bool Autenticado { get; set; }
        public string FotoUrl { get; set; }
    }
}
