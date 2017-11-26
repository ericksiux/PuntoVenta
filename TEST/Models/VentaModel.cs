using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    [Serializable]
    public class VentaModel
    {
        public long VentaID { get; set; }
        public int FoliadorVentaID { get; set; }
        public long ClienteID { get; set; }
        public string NombreCompleto { get; set; }
        public decimal TotalAdeudo { get; set; }
        public DateTime Fecha { get; set; }
    }
}