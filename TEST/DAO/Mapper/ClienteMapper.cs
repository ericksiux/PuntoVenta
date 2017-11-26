using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TEST.DTO;

namespace TEST.DAO.Mapper
{
    public static class ClienteMapper
    {
        public static List<ClienteDTO> ClientoDStoList(DataSet ds)
        {
            List < ClienteDTO > clientes = new List<ClienteDTO>();
          
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var cliente = new ClienteDTO();

                cliente.ClienteID = Convert.ToInt64(row["ClienteID"]);
                cliente.Nombre = Convert.ToString(row["Nombre"]);
                cliente.ApellidoPaterno = Convert.ToString(row["ApellidoPaterno"]);
                cliente.ApellidoMaterno = Convert.ToString(row["ApellidoMaterno"]);
                cliente.RFC = Convert.ToString(row["RFC"]);

                clientes.Add(cliente);
            }

            return clientes;
        }
    }
}