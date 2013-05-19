using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using CacheDeObjetos.Models;

namespace CacheDeObjetos.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        //Exemplo 01
        public ActionResult AdicionarObjeto()
        {

            var cachedCotacao = (Cotacao)HttpContext.Cache["chave1"];

            if (cachedCotacao == null)
            {
                cachedCotacao = new Cotacao()
                {
                    Descricao = "PTBR",
                    Valor = 10.5m,
                    UltimaAtualizacao = DateTime.Now
                };
                HttpContext.Cache.Insert("chave1", cachedCotacao);
                //adiciona utilizando chave valor
                //HttpContext.Cache["chave1"] = cachedCotacao;
            }

            return View(cachedCotacao);
        }

        public ActionResult RemoveObjetoDoCache()
        {
            HttpContext.Cache.Remove("chave1");
            return RedirectToAction("AdicionarObjeto");
        }

        //Exemplo 02
        public ActionResult AdicionarObjetoComDependencia()
        {
            var cachedCotacao = (Cotacao)HttpContext.Cache["chave2"];

            if (cachedCotacao == null)
            {
                ManipularArquivoTxt();

                cachedCotacao = new Cotacao()
                {
                    Descricao = "PTBR",
                    Valor = 13.5m,
                    UltimaAtualizacao = DateTime.Now
                };

                HttpContext.Cache.Insert("chave2", cachedCotacao,
                    new System.Web.Caching.CacheDependency(Server.MapPath(@"~\Content\arquivo.txt"))
                    );
            }

            return View(cachedCotacao);
        }

        private void ManipularArquivoTxt()
        {
            var arquivo = new StreamWriter(Server.MapPath(@"~\Content\arquivo.txt"), true, Encoding.UTF8);
            arquivo.WriteLine("Alteração " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            arquivo.Close();
        }

        public ActionResult AlteraArquivoDeDependencia()
        {
            ManipularArquivoTxt();
            return RedirectToAction("AdicionarObjetoComDependencia");
        }

        //Exemplo 03
        public ActionResult AdicionarObjetoComTempo()
        {
            var cachedCotacao = (Cotacao)HttpContext.Cache["chave3"];

            if (cachedCotacao == null)
            {
                cachedCotacao = new Cotacao()
                {
                    Descricao = "MCSS",
                    Valor = 16.5m,
                    UltimaAtualizacao = DateTime.Now
                };
                HttpContext.Cache.Insert("chave3", cachedCotacao, null, DateTime.Now.AddSeconds(5), 
                    System.Web.Caching.Cache.NoSlidingExpiration);
            }

            return View(cachedCotacao);
        }

        //Exemplo 04
        public ActionResult AdicionarObjetoComTempoBasedoNoUltimoAcesso()
        {
            var cachedCotacao = (Cotacao)HttpContext.Cache["chave4"];

            if (cachedCotacao == null)
            {
                cachedCotacao = new Cotacao()
                {
                    Descricao = "CCF",
                    Valor = 20.5m,
                    UltimaAtualizacao = DateTime.Now
                };
                
                HttpContext.Cache.Insert("chave4", cachedCotacao, null, System.Web.Caching.Cache.NoAbsoluteExpiration,TimeSpan.FromSeconds(5));
            }

            return View(cachedCotacao);
        }
    }
}
