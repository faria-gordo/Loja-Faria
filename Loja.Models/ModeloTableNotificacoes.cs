using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class ModeloTableNotificacoes : TableEntity
    {
        public ModeloTableNotificacoes() { }
        public ModeloTableNotificacoes(string mensagem, string data)
        {
            PartitionKey = mensagem;
            RowKey = data;
        }
        public string Mensagem => PartitionKey;
        public string Data => RowKey;
    }
}
