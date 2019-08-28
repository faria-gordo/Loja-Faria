using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    /// <summary>
    /// 
    /// Criar projeto para Modelos
    /// Criar projeto para fazer ligacao a base de dados e criar helpers (select, update, delete, etc) e por todas as conn strings
    /// Fazer merge, ShopController sera ligado a ProductController
    /// 
    /// Criar js para adicionar/retirar/trocar ao carrinho atraves de ajax, recolhendo form no ProductController
    /// Criar js para modificar quantidade
    /// 
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Us()
        {
            return View();
        }
        public ActionResult Store()
        {
            string[] urlParams = HttpContext.Request.Url.ToString().Split('/');
            foreach(string product in urlParams)
            {
                if (product.ToLower() == "bijutaria")//Um dos tipos de artigos
                {
                    //Conectar blob storage, retirar imagens
                    //Conectar table storage e retirar informacao de produtos do tipo bijutaria
                    //lista de urls
                    List<string> imagesPaths = new List<string>();
                    imagesPaths.Add("https://www.google.com/imgres?imgurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2Ffiles%2F2017%2F09%2Ffotolia_117306153-.jpg&imgrefurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2F2017%2F09%2F25%2Fa-abordagem-criativa-para-fotos-de-banco-de-imagens%2F&docid=nxFo0oHSeooNmM&tbnid=8PK0kmzNidGgfM%3A&vet=10ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ..i&w=700&h=467&bih=754&biw=1536&q=imagens&ved=0ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ&iact=mrc&uact=8");
                    ViewBag.ProductName = product.ToLower();
                    ViewBag.ImagesPaths = imagesPaths;
                }
                if(product.ToLower() == "lembrancas")
                {
                    List<string> imagesPaths = new List<string>();
                    imagesPaths.Add("https://www.google.com/imgres?imgurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2Ffiles%2F2017%2F09%2Ffotolia_117306153-.jpg&imgrefurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2F2017%2F09%2F25%2Fa-abordagem-criativa-para-fotos-de-banco-de-imagens%2F&docid=nxFo0oHSeooNmM&tbnid=8PK0kmzNidGgfM%3A&vet=10ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ..i&w=700&h=467&bih=754&biw=1536&q=imagens&ved=0ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ&iact=mrc&uact=8");
                    ViewBag.ProductName = product.ToLower();
                    ViewBag.ImagesPaths = imagesPaths;
                }
                if (product.ToLower() == "religiosos")
                {
                    List<string> imagesPaths = new List<string>();
                    imagesPaths.Add("https://www.google.com/imgres?imgurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2Ffiles%2F2017%2F09%2Ffotolia_117306153-.jpg&imgrefurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2F2017%2F09%2F25%2Fa-abordagem-criativa-para-fotos-de-banco-de-imagens%2F&docid=nxFo0oHSeooNmM&tbnid=8PK0kmzNidGgfM%3A&vet=10ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ..i&w=700&h=467&bih=754&biw=1536&q=imagens&ved=0ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ&iact=mrc&uact=8");
                    ViewBag.ProductName = product.ToLower();
                    ViewBag.ImagesPaths = imagesPaths;
                }
                if (product.ToLower() == "diversos")
                {
                    List<string> imagesPaths = new List<string>();
                    imagesPaths.Add("https://www.google.com/imgres?imgurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2Ffiles%2F2017%2F09%2Ffotolia_117306153-.jpg&imgrefurl=https%3A%2F%2Fblog.fotolia.com%2Fbr%2F2017%2F09%2F25%2Fa-abordagem-criativa-para-fotos-de-banco-de-imagens%2F&docid=nxFo0oHSeooNmM&tbnid=8PK0kmzNidGgfM%3A&vet=10ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ..i&w=700&h=467&bih=754&biw=1536&q=imagens&ved=0ahUKEwjNxdS1hovkAhUKhRoKHQDXDTQQMwhTKAEwAQ&iact=mrc&uact=8");
                    ViewBag.ProductName = product.ToLower();
                    ViewBag.ImagesPaths = imagesPaths;
                }
            }
            return View();
        }
        public ActionResult DataTable()
        {
            //Read excel and display table in the view
            string filePath = "";
            string fileName = "";
            using(var reader = new StreamReader(System.IO.File.OpenRead(filePath + fileName)))
            return View();
        }
    }
}