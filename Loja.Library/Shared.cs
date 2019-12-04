using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Library
{
    /// <summary>
    /// 
    /// Sumário serve para expor informação adicional aos comentários ou para informar todos os bugs na aplicação em causa.
    /// 
    /// TODO:
    ///         - Por aqui o metodo PartitionKeyFormatter
    ///         - Adicionar metodo WebServiceRequestMessage
    ///         - Rever flow, se toda a informação é validade e filtrada corretamente.
    ///         
    /// Erro: 
    ///         - Caso inicial onde é só dado parte da PK, considera-se PK ou vai para o nome? como proceder neste caso?
    /// </summary>
    public class Shared
    {
        //To be called within the services to determine either is RK or PK or ProductName
        //Raw Data - 'info'_'ProductName'_'info'
        public string WebServiceRequestFormatData(string rawData)
        {
            string response;
            if (CompareToAllRowKeys(rawData))
            {
                response = "RowKey-" + rawData;
            }
            else if(CompareToAllPrimaryKeys(rawData))
            {
                response = "PartitionKey-" + rawData;
            }
            //Errado, podem pesquisar por um produto que nao existe nem RK ou PK ou nome
            else if(rawData.Length > 0)
            {
                response = "Name-" + RetrieveName(rawData);
            }
            else
            {
                return "";
            }
            return response;
        }

        public string RetrieveName(string info)
        {
            string name = info.Split('-')[0];
            return name;
        }
        public string RetrieveRowKey(string info)
        {
            string rowKey = "";
            for (int i = 1; i < 26; i++)
            {
                string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                string genericRowKey = i.ToString() + letters[i-1];
                if (info.Split('-').Contains(genericRowKey))
                {
                    rowKey = genericRowKey;
                }
            }
            return rowKey;
        }
        public string RetrievePartitionKey(string info)
        {
            string partitionKey = "";
            for (int s = 1; s < 26; s++)
            {
                //secção?? entao só por as letras que existem em secção
                string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                foreach (string subinfo in info.Split('-'))
                {
                    if (subinfo.Length == 3)
                    {
                        partitionKey = letters[s].ToUpper() + "-" + subinfo.ToUpperInvariant();
                    }
                }
            }
            return partitionKey;
        }
        public bool CompareToAllRowKeys(string info)
        {
            bool val = false;
            for (int i = 1; i < 26; i++)
            {
                string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                string genericRowKey = i.ToString() + letters[i -1];
                if (info.Contains(genericRowKey))
                {
                    val = true;
                }
            }
            return val;
        }
        public bool CompareToAllPrimaryKeys(string info)
        {
            bool val = false;
            for (int s = 1; s < 26; s++)
            {
                //secção?? entao só por as letras que existem em secção
                string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                foreach (string subinfo in info.Split('-'))
                {
                    if (subinfo.Length == 3)
                    {
                        val = true;
                    }
                }
            }
            return val;
        }

    }
}