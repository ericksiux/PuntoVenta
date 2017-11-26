using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.DTO
{
    public class ArticuloDTO
    {
        public long ArticuloID { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionModelo { get; set; }
        public int Existencia { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioArticulo { get; set; }

        public string DescripcionCompleta    // the Name property
        {
            get
            {
                return ArticuloID.ToString("000") + " - " + Descripcion;
            }
        }
    }
}