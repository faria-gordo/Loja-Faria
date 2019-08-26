using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Data
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class Data
    {
        private string StorageAccountKey = "";
        private string StorageAccountName = "";
        //Ou por na app config
        public Data()
        {
            //Connection string e codigo para preparar chamada para cloud storage
        }
        //PK: Nome;Tipo RK: Id
        //Recebe a lista de produtos caso queira mais que um produto diferente, retirar elemento da bd mesmo. BdCount = BdCount - 1 !! Quantidades de produtos sempre atualizada, atualizar apartir da aqui 
        // a tabela da bd do website de imediato
        public List<Produto> Selecionar(List<Produto> produtos)
        {
            return null;
        }
        public List<Produto> Apagar(List<Produto> produtos)
        {
            //Pegar em todos os id presentes em produtos e apaga los da bd [QUE NAO PASSAM PELO CARRINHO DE COMPRAS] sera feito pela tabelda da bd do website
            return null;
        }
        public List<Produto> Adicionar(List<Produto> produto)
        {
            //Ira haver uma secçao para modificar a bd diretamente do web site. Caso queira adicionar registo de produtos na bd, sera feita por aqui
            //Caso cliente desejar entregar produto e ja estaja no carrinho, chamar este metodo
            return null;
        }
    }
}
