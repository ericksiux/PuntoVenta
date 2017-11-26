using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TEST.DTO;

namespace TEST.DAO.Mapper
{
    public static class ArticuloMapper
    {
        public static List<ArticuloDTO> ArticuloDStoList(DataSet ds)
        {
            List<ArticuloDTO> clientes = new List<ArticuloDTO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var articulo = new ArticuloDTO();
                articulo.ArticuloID = Convert.ToInt64(row["ArticuloID"]);
                articulo.Descripcion = Convert.ToString(row["Descripcion"]);
                articulo.DescripcionModelo = Convert.ToString(row["DescripcionModelo"]);
                articulo.Existencia = Convert.ToInt32(row["Existencia"]);
                articulo.PrecioArticulo = Convert.ToDecimal(row["Precio"]);

                clientes.Add(articulo);
            }

            return clientes;
        }
    }
}