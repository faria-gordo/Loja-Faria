using Loja.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    ///     Registar nova palavra passe, elimina o correto user mas nao adiciona
    /// 
    /// </summary>
    public class Data
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableClient;
        private readonly CloudTable table;
        public Data(string nomeTabela)
        {
            string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=lojafariastorage;AccountKey=RzxcaNCnheT4HKh7ym8KaML3J1FSXKXUS0HaIHvw7diyood7Ekk9D8ki7szjnfO6X9drbGaZIE6gHlbcXQUWaA==;EndpointSuffix=core.windows.net";
            storageAccount = CloudStorageAccount.Parse(ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(nomeTabela);
            table.CreateIfNotExists();
        }

        //ADICIONAR PRODUTO
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

        //ADICIONAR CARRINHO
        public string AdicionarCarrinho(Carrinho carrinho)
        {
            try
            {
                table.Execute(TableOperation.Insert(CarrinhoToModelTable(carrinho)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Mensagem do helper Carrinho adicionado!! ";
        }


        //RETIRAR PRODUTO
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

        //SELECIONAR PRODUTO
        public List<Produto> SelecionarProduto(Produto produto)
        {

            return null;
        }
        public Produto SelecionarProdutoPorRowKey(string rowkey)
        {
            Produto produto = new Produto();
            try
            {
                TableQuery<ModeloTable> query = new TableQuery<ModeloTable>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowkey));
                List<ModeloTable> resultado = table.ExecuteQuery(query).ToList<ModeloTable>();
                foreach (var prod in resultado)
                {
                    produto = ModelTableToModel(prod);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return produto;
        }
        public List<Produto> SelecionarProdutoPorPartitionKey(string partitionKey)
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
        public List<Produto> SelecionarProdutoPorNome(string name)
        {
            string nome = name + "-";

            if (nome.Split('-')[1] != "")
            {
                List<Produto> produtos = new List<Produto>();
                try
                {
                    TableQuery<ModeloTable> query = new TableQuery<ModeloTable>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, nome));
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
                List<Produto> produtos = new List<Produto>();
                string tipoDeProduto = "Pul;Col;Ter";
                if (name.Length > 2)
                {
                    string seccaoDeProduto = "B,D,R";
                    foreach (string secao in seccaoDeProduto.Split(','))
                    {
                        foreach (string tipo in tipoDeProduto.Split(';'))
                        {
                            var PartitionKey = secao.Trim() + '-' + tipo.Trim();
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
                    }
                }
                else
                {
                    foreach (string tipo in tipoDeProduto.Split(';'))
                    {
                        var PartitionKey = nome.Trim() + tipo.Trim();
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
                }
                return produtos;
            }
        }
        public List<Produto> SelecionarProdutos()
        {

            List<ModeloTable> modelos = table.ExecuteQuery(new TableQuery<ModeloTable>()).ToList();
            List<Produto> produtos = new List<Produto>();
            foreach (ModeloTable modelo in modelos)
            {
                produtos.Add(ModelTableToModel(modelo));
            }
            return produtos;
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

        //SELECIONAR CARRINHO
        public List<Carrinho> SelecionarCarrinhos()
        {
            List<ModeloTableCarrinho> modelos = table.ExecuteQuery(new TableQuery<ModeloTableCarrinho>()).ToList();
            List<Carrinho> carrinhos = new List<Carrinho>();
            foreach (ModeloTableCarrinho modelo in modelos)
            {
                carrinhos.Add(ModelTableToCarrinho(modelo));
            }
            return carrinhos;
        }

        //USER ACTIONS
        public string AdicionarUser(User user)
        {
            try
            {
                TableQuery<ModeloTableUser> query = new TableQuery<ModeloTableUser>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, user.Email));
                List<ModeloTableUser> resultado = table.ExecuteQuery(query).ToList<ModeloTableUser>();

                //Verifica se existe
                if (resultado.Count > 0)
                {
                    return "Já existe esse email registado.";
                }
                else
                {
                    user.Autenticado = true;
                    user.QuantLogins += 1;
                    table.Execute(TableOperation.Insert(UserToModelTableUser(user)));
                }

            }
            catch (Exception ex)
            {
                //Erro conflito
                Console.WriteLine(ex.Message);
                return "Já existe esse email registado.";
            }
            return "Mensagem do helper User adicionado";
        }
        public User LogIn(User user)
        {
            //este user tem so email e password
            try
            {
                TableQuery<ModeloTableUser> query = new TableQuery<ModeloTableUser>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, user.Email));
                List<ModeloTableUser> resultado = table.ExecuteQuery(query).ToList<ModeloTableUser>();

                if (resultado.Count == 1)
                {
                    foreach (var userDB in resultado)
                    {
                        if (userDB.RowKey == user.Email && userDB.PartitionKey == user.Password)
                        {
                            user.Nome = userDB.Nome;
                            user.Apelido = userDB.Apelido;
                            user.QuantLogins = userDB.QuantLogins + 1;
                            user.Autenticado = true;

                            //Atualiza quantidade de logins e se esta autenticado.
                            TableOperation update = TableOperation.Replace(UserToModelTableUser(user));
                            table.Execute(update);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (user.Nome == null && user.Apelido == null)
            {
                user = null;
            }
            return user;
        }
        public string LogOffUser(User user)
        {
            try
            {
                TableOperation retrieve = TableOperation.Retrieve<ModeloTableUser>(user.Password, user.Email);

                TableResult result = table.Execute(retrieve);

                ModeloTableUser loggedOfUser = (ModeloTableUser)result.Result;

                loggedOfUser.Autenticado = false;

                if (result != null)
                {
                    TableOperation update = TableOperation.Replace(loggedOfUser);

                    table.Execute(update);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Saíste da tua conta!";
        }
        public string VerificarUser(User user)
        {
            string message = "Não encontramos o teu email. Tenta novamente";
            try
            {
                TableQuery<ModeloTableUser> query = new TableQuery<ModeloTableUser>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, user.Email));
                List<ModeloTableUser> resultado = table.ExecuteQuery(query).ToList<ModeloTableUser>();

                if (resultado.Count == 1)
                {
                    foreach (var userDB in resultado)
                    {
                        if (userDB.RowKey == user.Email)
                        {
                            return message = "Encontramos o teu email, podes repor a tua palavra passe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return message;
        }
        public string MudarPassword(User user)
        {
            User oldUser = new User();
            User newUser = new User();
            try
            {
                //Procurar palavra passe antiga pelo email
                TableQuery<ModeloTableUser> query = new TableQuery<ModeloTableUser>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, user.Email));
                List<ModeloTableUser> resultado = table.ExecuteQuery(query).ToList<ModeloTableUser>();

                if (resultado.Count == 1)
                {
                    foreach (var userDB in resultado)
                    {
                        if (userDB.RowKey == user.Email)
                        {
                            oldUser.Password = userDB.Password;
                            oldUser.Email = userDB.Email;
                            oldUser.Nome = userDB.Nome;
                            oldUser.Apelido = userDB.Apelido;
                            oldUser.FotoUrl = userDB.FotoUrl;
                            oldUser.Autenticado = userDB.Autenticado;
                            oldUser.QuantLogins = userDB.QuantLogins;
                        }
                    }
                }

                //Com a palavra passe antiga, encontrar na tabela o registo com o email e a passantiga, e eliminar o registo
                TableOperation retrieve = TableOperation.Retrieve<ModeloTableUser>(oldUser.Password, oldUser.Email);
                TableResult result = table.Execute(retrieve);
                ModeloTableUser loggedOfUser = (ModeloTableUser)result.Result;
                if (result != null)
                {
                    TableOperation delete = TableOperation.Delete(loggedOfUser);

                    table.Execute(delete);
                }

                //Inserir mesmo user com palavra passe nova
                //CHEGA AQUI MAS NAO INSERE O USER PRETENDIDO. NAO INSERE NADA
                if (user != null)
                {
                    newUser.Nome = oldUser.Nome;
                    newUser.Password = user.Password;
                    newUser.Email = oldUser.Email;
                    newUser.QuantLogins = oldUser.QuantLogins;
                    newUser.Autenticado = oldUser.Autenticado;
                    newUser.Apelido = oldUser.Apelido;
                    newUser.FotoUrl = oldUser.FotoUrl;
                    TableOperation insert = TableOperation.Insert(UserToModelTableUser(newUser));
                    TableResult res = table.Execute(insert);
                    if(res.HttpStatusCode == 204)//NO CONTENT
                    {
                        return "Nova palavra passe registada";
                    }
                    else
                    {
                        TableOperation insertIfProblem = TableOperation.Insert(UserToModelTableUser(oldUser));
                        TableResult resProb = table.Execute(insertIfProblem);
                        return $"{res.HttpStatusCode}: Ocorreu um erro a trocar a sua palavra passe";
                    }
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Ocorreu um problema ao registar a nova palavra passe";
        }
        public List<User> SelecionarUsers()
        {
            List<ModeloTableUser> modelos = table.ExecuteQuery(new TableQuery<ModeloTableUser>()).ToList();
            List<User> users = new List<User>();
            foreach (ModeloTableUser modelo in modelos)
            {
                users.Add(ModelTableToUser(modelo));
            }
            return users;
        }

        //ATUALIZAR PRODUTO
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
        //TableEntity to Entity
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
        public Carrinho ModelTableToCarrinho(ModeloTableCarrinho modeloCarrinho)
        {
            Carrinho carrinho = new Carrinho()
            {
                IdCompra = modeloCarrinho.IdCompra,
                Nome = modeloCarrinho.Nome,
                Email = modeloCarrinho.PartitionKey,
                Tipo = modeloCarrinho.Tipo,
                Seccao = modeloCarrinho.Seccao,
                Descricao = modeloCarrinho.Descricao,
                Preco = modeloCarrinho.Preco,
                Quantidade = modeloCarrinho.Quantidade,
                //DataDeCompra = modeloCarrinho.DataDeCompra,
                Url = modeloCarrinho.Url
            };
            return carrinho;
        }
        public User ModelTableToUser(ModeloTableUser modeloUser)
        {
            User user = new User()
            {
                Password = modeloUser.Password,
                Email = modeloUser.Email,
                Nome = modeloUser.Nome,
                Apelido = modeloUser.Apelido,
                QuantLogins = modeloUser.QuantLogins,
                Autenticado = modeloUser.Autenticado,
                FotoUrl = modeloUser.FotoUrl
            };
            return user;
        }

        //Entity to TableEntity
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
        public ModeloTableCarrinho CarrinhoToModelTable(Carrinho carrinho)
        {
            ModeloTableCarrinho modelo = new ModeloTableCarrinho()
            {
                PartitionKey = carrinho.Email,
                RowKey = carrinho.IdCompra,
                Nome = carrinho.Nome,
                Tipo = carrinho.Tipo,
                Seccao = carrinho.Seccao,
                Descricao = carrinho.Descricao,
                Preco = carrinho.Preco,
                Quantidade = carrinho.Quantidade,
                //DataDeCompra = carrinho.DataDeCompra,
                Url = carrinho.Url
            };
            return modelo;
        }
        public ModeloTableUser UserToModelTableUser(User user)
        {
            ModeloTableUser modelo = new ModeloTableUser()
            {
                PartitionKey = user.Password,
                RowKey = user.Email,
                Nome = user.Nome,
                Apelido = user.Apelido,
                QuantLogins = user.QuantLogins,
                ETag = "*",
                Autenticado = user.Autenticado,
                FotoUrl = user.FotoUrl
            };
            return modelo;
        }
        public Carrinho ProdutoToCarrinho(Produto produto)
        {
            Carrinho carrinho = new Carrinho()
            {
                IdCompra = Guid.NewGuid().ToString(), //Rk
                Email = "test@gmail.com", //PK
                Nome = "asd",
                Tipo = produto.Tipo,
                Seccao = produto.Seccao,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                //DataDeCompra = produto.DataDeVenda,
                Url = produto.Url
            };
            return carrinho;
        }
        public Produto CarrinhoToProduto(Carrinho carrinho)
        {
            Produto produto = new Produto()
            {
                Id = "",
                Nome = carrinho.Nome,
                //Email = "",
                Tipo = carrinho.Tipo,
                Seccao = carrinho.Seccao,
                Descricao = carrinho.Descricao,
                Preco = carrinho.Preco,
                Quantidade = carrinho.Quantidade,
                //DataDeVenda = carrinho.DataDeCompra,
                Url = carrinho.Url
            };
            return produto;
        }
    }
}
