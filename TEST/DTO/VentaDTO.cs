using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.DTO
{
    public class VentaDTO
    {
        public long VentaID { get; set; }
        public int FoliadorVentaID { get; set; }
        public long ClienteID { get; set; }
        public string NombreCompleto { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public Byte Plazo { get; set; }
    }
}