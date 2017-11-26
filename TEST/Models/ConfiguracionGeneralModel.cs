using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class ConfiguracionGeneralModel
    {
        public decimal TasaFinanciamiento { get; set; }
        public decimal PorcentajeEnganche { get; set; }
        public int PlazoMaximo { get; set; }
    }
}