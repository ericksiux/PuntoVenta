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
    public class ArticuloDAO
    {
        public List<ArticuloDTO> ObtenerArticulos()
        {
            List<ArticuloDTO> articulos = new List<ArticuloDTO>();

            try
            {
                articulos = ArticuloMapper.ArticuloDStoList(DALHelper.Retrive("Articulos_Obtener"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return articulos;
        }

        public void Guardar(ArticuloDTO articulo)
        {
            try
            {
                IList<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ArticuloID", Value = articulo.ArticuloID });
                parameters.Add(new SqlParameter { ParameterName = "@Descripcion", Value = articulo.Descripcion });
                parameters.Add(new SqlParameter { ParameterName = "@DescripcionModelo", Value = articulo.DescripcionModelo });
                parameters.Add(new SqlParameter { ParameterName = "@Precio", Value = articulo.PrecioArticulo });
                parameters.Add(new SqlParameter { ParameterName = "@Existencia", Value = articulo.Existencia });

                DALHelper.Create("Articulo_Crear", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}