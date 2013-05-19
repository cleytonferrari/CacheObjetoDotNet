using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheDeObjetos.Models
{
    public class Cotacao
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
    }
}