using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST.Models
{
    public class ClienteModel
    {
        public long ClienteID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }

        public string NombreCompleto {
            get
            {
                return ClienteID.ToString("000") + " - " + Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno;
            }
        }
    }
}