using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TEST.DTO;

namespace TEST.DAO.Mapper
{
    public static class VentaMapper
    {
        public static List<VentaDTO> VentaDStoList(DataSet ds)
        {
            List<VentaDTO> ventas = new List<VentaDTO>();
          
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var venta = new VentaDTO();

                venta.VentaID = Convert.ToInt64(row["VentaID"]);
                venta.FoliadorVentaID = Convert.ToInt32(row["FoliadorVentaID"]);
                venta.ClienteID = Convert.ToInt64(row["ClienteID"]);
                venta.NombreCompleto = Convert.ToString(row["NombreCompleto"]);
                venta.Total = Convert.ToDecimal(row["Total"]);
                venta.Fecha = Convert.ToDateTime(row["Fecha"]);

                ventas.Add(venta);
            }

            return ventas;
        }

        public static int VentaDStoFoliadorVentaID(DataSet ds)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                return Convert.ToInt32(row["FoliadorVentaID"]);
            }

            return 0;
        }
    }
}