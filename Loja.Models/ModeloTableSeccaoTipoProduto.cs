using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models
{
    public class ModeloTableSeccaoTipoProduto : TableEntity
    {
        public ModeloTableSeccaoTipoProduto() { }
        public ModeloTableSeccaoTipoProduto(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public string Seccao => PartitionKey;
        public string Tipo => RowKey;

    }
}
