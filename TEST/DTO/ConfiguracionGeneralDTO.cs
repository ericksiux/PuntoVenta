using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.DTO
{
    public class ConfiguracionGeneralDTO
    {
        public decimal TasaFinanciamiento { get; set; }
        public decimal PorcentajeEnganche { get; set; }
        public decimal PlazoMaximo { get; set; }
    }
}