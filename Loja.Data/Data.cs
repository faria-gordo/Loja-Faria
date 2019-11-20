using Loja.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:      
    /// 
    /// </summary>
    public class Data
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableClient;
        private readonly CloudTable table;
        public Data()
        {
            string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=lojafariastorage;AccountKey=RzxcaNCnheT4HKh7ym8KaML3J1FSXKXUS0HaIHvw7diyood7Ekk9D8ki7szjnfO6X9drbGaZIE6gHlbcXQUWaA==;EndpointSuffix=core.windows.net";
            storageAccount = CloudStorageAccount.Parse(ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("LojaFaria");
            table.CreateIfNotExists();
        }
        //Devolve mensagem de feedback de um helper existente   
        //Array de quantidaes(int) é respetivamente com a lista de produtos

        //ADICIONAR
        public string AdicionarProduto(Produto produto)
        {
            try
            {
                table.Execute(TableOperation.Insert(ModelToModelTable(produto)));
                //Chamar ação de update que existe(ou ira existir) em Home da Loja para avisar á loja que foi adicionado um produto, caso já exista o produto, é so adicionado
                // o numero da quantia de produtos, se for um produto que nao exista na loja, é criado um produto novo chamando uma ação ainda por existir em Home da Loja.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Mensagem do helper";
        }
        public string AdicionarProdutos(List<Produto> produtos)
        {
            try
            {
                foreach (Produto prod in produtos)
                {
                    table.Execute(TableOperation.Insert(ModelToModelTable(prod)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Mensagem do helper";
        }

        //RETIRAR
        public string RetirarProduto(Produto produto)
        {
            //Pegar em todos os id presentes em produtos e apaga los da bd [QUE NAO PASSAM PELO CARRINHO DE COMPRAS] sera feito pela tabelda da bd do website
            try
            {
                table.Execute(TableOperation.Delete(ModelToModelTable(produto)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }
        public string RetirarProdutoPorRowKey(string rowKey)
        {
            return "";
        }
        public string RetirarProdutoPorPartitionKey(string partitionKey)
        {
            return "";
        }
        public string RetirarProdutoPorNome(string nome)
        {
            return "";
        }
        public string RetirarProdutos(List<Produto> produtos)
        {
            //Pegar em todos os id presentes em produtos e apaga los da bd [QUE NAO PASSAM PELO CARRINHO DE COMPRAS] sera feito pela tabelda da bd do website
            try
            {
                foreach (Produto prod in produtos)
                {
                    table.Execute(TableOperation.Delete(ModelToModelTable(prod)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Mensagem do helper";
        }
        public string RetirarProdutosPorPartitionKey(string[] partitionKey)
        {
            return "";
        }

        //SELECIONAR
        public Produto SelecionarProduto(Produto produto)
        {

            return null;
        }
        public Produto SelecionarProdutoPorRowKey(string rowkey)
        {

            return null;
        }
        public Produto SelecionarProdutoPorPartitionKey(string partitionKey)
        {
            return null;
        }
        public Produto SelecionarProdutoPorNome(string nome)
        {

            return null;
        }
        public List<Produto> SelecionarProdutos(List<Produto> produtos)
        {
            return null;
        }
        public List<Produto> SelecionarProdutosPorPartitionKey(string partitionKey)
        {
            if (partitionKey.Split('-')[1] != " ")
            {
                List<Produto> produtos = new List<Produto>();
                try
                {
                    TableQuery<ModeloTable> query = new TableQuery<ModeloTable>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                    List<ModeloTable> resultado = table.ExecuteQuery(query).ToList<ModeloTable>();
                    foreach (var produto in resultado)
                    {
                        produtos.Add(ModelTableToModel(produto));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return produtos;
            }
            else
            {
                string tipoDeProduto = "Pul;Col;Ter";
                List<Produto> produtos = new List<Produto>();
                foreach (string tipo in tipoDeProduto.Split(';'))
                {
                    var PartitionKey = partitionKey.Trim() + tipo.Trim();
                    try
                    {
                        TableQuery<ModeloTable> query = new TableQuery<ModeloTable>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PartitionKey));
                        List<ModeloTable> resultado = table.ExecuteQuery(query).ToList<ModeloTable>();
                        foreach (var produto in resultado)
                        {
                            produtos.Add(ModelTableToModel(produto));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                return produtos;
            }

        }

        //ATUALIZAR
        public Produto AtualizarProduto(Produto produto)
        {
            return null;
        }
        public Produto AtualizarProdutoPorRowKey(string rowkey)
        {

            return null;
        }
        public Produto AtualizarProdutoPorPartitionKey(string partitionKey)
        {
            return null;
        }
        public Produto AtualizarProdutoPorNome(string nome)
        {

            return null;
        }
        public List<Produto> AtualizarProdutos(List<Produto> produtos)
        {
            return null;
        }
        public List<Produto> AtualizarProdutosPorPartitionKey(string partitionKey)
        {
            return null;
        }

        //MAPPINGS
        public Produto ModelTableToModel(ModeloTable modeloTable)
        {
            Produto produto = new Produto()
            {
                Nome = modeloTable.Nome,
                Id = modeloTable.RowKey,
                Descricao = modeloTable.Descricao,
                Tipo = modeloTable.Tipo,
                Preco = modeloTable.Preco,
                Seccao = modeloTable.Seccao,
                Url = modeloTable.Url
            };
            return produto;
        }
        public ModeloTable ModelToModelTable(Produto prod)
        {
            ModeloTable modelo = new ModeloTable()
            {
                PartitionKey = prod.Seccao + "-" + prod.Tipo,
                RowKey = prod.Id,
                Nome = prod.Nome,
                Tipo = prod.Tipo,
                Preco = prod.Preco,
                Descricao = prod.Descricao,
                Url = prod.Url
            };
            return modelo;
        }
    }
}
