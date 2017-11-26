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
    public class ClienteDAO
    {
        public List<ClienteDTO> ObtenerClientes()
        {
            List<ClienteDTO> clientes = new List<ClienteDTO>();

            try
            {
                clientes = ClienteMapper.ClientoDStoList(DALHelper.Retrive("Cliente_Obtener"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return clientes;
        }

        public void Guardar(ClienteDTO cliente)
        {
            try
            {
                IList<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@ClienteID", Value = cliente.ClienteID });
                parameters.Add(new SqlParameter { ParameterName = "@Nombre", Value = cliente.Nombre });
                parameters.Add(new SqlParameter { ParameterName = "@ApellidoPaterno", Value = cliente.ApellidoPaterno });
                parameters.Add(new SqlParameter { ParameterName = "@ApellidoMaterno", Value = cliente.ApellidoMaterno });
                parameters.Add(new SqlParameter { ParameterName = "@RFC", Value = cliente.RFC });

                DALHelper.Create("Cliente_Crear", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}