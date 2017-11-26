using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TEST.DAO.Mapper;
using TEST.DTO;

namespace TEST.DAO
{
    public class VentaDAO
    {
        public int ObtenerFoliador()
        {
            int FoliadorVentaID = 0;

            try
            {
                FoliadorVentaID = VentaMapper.VentaDStoFoliadorVentaID(DALHelper.Retrive("Venta_ObtenerFoliador"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return FoliadorVentaID;
        }

        public List<VentaDTO> ObtenerVentas()
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            try
            {
                ventas = VentaMapper.VentaDStoList(DALHelper.Retrive("Venta_Obtener"));
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ventas;
        }

        public void Guardar(VentaDTO venta)
        {
            try
            {
                IList<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@FoliadorVentaID", Value = venta .FoliadorVentaID});
                parameters.Add(new SqlParameter { ParameterName = "@ClienteID", Value = venta.ClienteID });
                parameters.Add(new SqlParameter { ParameterName = "@Total", Value = venta.Total });
                parameters.Add(new SqlParameter { ParameterName = "@Plazo", Value = venta.Plazo });

                DALHelper.Create("Venta_Crear", parameters);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}