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
    public class ConfiguracionGeneralDAO
    {
        public ConfiguracionGeneralDTO ObtenerConfiguracionGeneral()
        {
            ConfiguracionGeneralDTO config = new ConfiguracionGeneralDTO();

            try
            {
                config = ConfiguracionGeneralMapper.ConfiguracionDStoList(DALHelper.Retrive("ConfiguracionGeneral_Obtener"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return config;
        }

        public void Guardar(ConfiguracionGeneralDTO configuracion)
        {
            try
            {
                IList<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@TasaFinanciamiento", Value = configuracion.TasaFinanciamiento });
                parameters.Add(new SqlParameter { ParameterName = "@PorcentajeEnganche", Value = configuracion.PorcentajeEnganche });
                parameters.Add(new SqlParameter { ParameterName = "@PlazoMaximo", Value = configuracion.PlazoMaximo });

                DALHelper.Create("ConfiguracionGeneral_Crear", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}