using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class ArticuloModel
    {
        public long ArticuloID { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionModelo { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
    }
}