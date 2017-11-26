using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    [Serializable]
    public class NuevaVentaModel
    {
        public int FoliadorVentaID { get; set; }
        public decimal TasaFinanciamiento { get; set; }
        public decimal PorcentajeEnganche { get; set; }
        public decimal PlazoMaximo { get; set; }
        public long ClienteID { get; set; }
        public decimal TotalAdeudo { get; set; }
        public byte Plazo { get; set; }
    }
}