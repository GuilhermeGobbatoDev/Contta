using System;
using System.Collections.Generic;
using System.Text;

namespace Contta.Models
{
    public class Despesas
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public int Id { get; set; }
    }

}
