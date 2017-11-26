using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TEST.DTO;

namespace TEST.DAO.Mapper
{
    public static class ConfiguracionGeneralMapper
    {
        public static ConfiguracionGeneralDTO ConfiguracionDStoList(DataSet ds)
        {
            ConfiguracionGeneralDTO dto = new ConfiguracionGeneralDTO();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dto.PorcentajeEnganche = Convert.ToDecimal(row["PorcentajeEnganche"]);
                dto.TasaFinanciamiento = Convert.ToDecimal(row["TasaFinanciamiento"]);
                dto.PlazoMaximo = Convert.ToDecimal(row["PlazoMaximo"]);
            }

            return dto;
        }
    }
}