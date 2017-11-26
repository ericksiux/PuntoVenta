using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    [Serializable]
    public class AbonoMensualModel
    {
        public int Plazo { get; set; }
        public decimal PrecioContado { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal ImporteAbono { get; set; }
        public decimal ImporteAhorra { get; set; }
    }
}