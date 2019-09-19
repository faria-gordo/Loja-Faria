﻿using Loja.Modelos;
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
    /// TODO: por connectionstring na app.config
    /// 
    /// Adicionar varios tipos Select(), com varios tipos de argumentos
    /// 
    /// </summary>
    public class Data
    {
        readonly CloudStorageAccount storageAccount;
        readonly CloudTableClient tableClient;
        readonly CloudTable table;
        public Data()
        {
            string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=lojafariastorage;AccountKey=RzxcaNCnheT4HKh7ym8KaML3J1FSXKXUS0HaIHvw7diyood7Ekk9D8ki7szjnfO6X9drbGaZIE6gHlbcXQUWaA==;EndpointSuffix=core.windows.net";
            storageAccount = CloudStorageAccount.Parse(ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("LojaFaria");
            table.CreateIfNotExists();
        }
        public bool Adicionar(List<Produto> produto)
        {
            try
            {
                foreach (Produto prod in produto)
                {
                    table.Execute(TableOperation.Insert(ModelToModelTable(prod)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
        public bool Apagar(List<Produto> produtos)
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
            return true;
        }
        public List<Produto> Selecionar(List<Produto> produtos)
        {
            return null;
        }
        public List<Produto> Selecionar(string partitionKey)
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
        //public List<Produto> Selecionar(string seccao, string tipo)
        //{
        //    if (seccao == null || tipo == null)
        //    {
        //        //TODO

        //        //Tipo: pulseiras, braceletes, roupa, colares, brincos, joias, santos, etc...
        //    }
        //    List<Produto> produtos = new List<Produto>();
        //    string partitionKey = seccao + "-" + tipo;
        //    try
        //    {
        //        TableQuery<ModeloTable> query = new TableQuery<ModeloTable>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
        //        List<ModeloTable> resultado = table.ExecuteQuery(query).ToList<ModeloTable>();
        //        foreach (var produto in resultado)
        //        {
        //            produtos.Add(ModelTableToModel(produto));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return produtos;
        //}
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
