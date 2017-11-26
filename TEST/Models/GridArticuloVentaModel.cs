using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    [Serializable]
    public class GridArticuloVentaModel
    {
        public long ArticuloID { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionModelo { get; set; }
        public int Existencia { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }

        public decimal Importe   
        {
            get
            {
                return Cantidad * PrecioVenta;
            }
        }

        public string GUID { get; set; }
    }
}