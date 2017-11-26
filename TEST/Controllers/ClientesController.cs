using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.DAO;
using TEST.DTO;
using TEST.Models;

namespace TEST.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<ClienteModel> model = new List<ClienteModel>();

            ClienteDAO ClienteDAO = new ClienteDAO();
            var clientes = ClienteDAO.ObtenerClientes();

            foreach (var cliente in clientes)
            {
                model.Add(new ClienteModel { ClienteID = cliente.ClienteID, Nombre = cliente.Nombre, ApellidoPaterno = cliente.ApellidoPaterno, ApellidoMaterno = cliente.ApellidoMaterno, RFC = cliente.RFC });
            }

            Session["ClienteModel"] = model;
            return View(model);
        }

        public ActionResult Nuevo(long ClienteID)
        {
            ClienteModel model = new Models.ClienteModel();
            List<ClienteModel> ClienteModel = (List<ClienteModel>)Session["ClienteModel"];

            foreach (var cliente in ClienteModel)
            {
                if (cliente.ClienteID == ClienteID)
                {
                    model = cliente;
                    continue;
                }
            }


            return View(model);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarCliente(ClienteModel clienteParam)
        {
            try
            {
                ClienteDAO ClienteDAO = new ClienteDAO();

                var clienteDTO = new ClienteDTO();
                clienteDTO.ClienteID = clienteParam.ClienteID;
                clienteDTO.Nombre = clienteParam.Nombre;
                clienteDTO.ApellidoPaterno = clienteParam.ApellidoPaterno;
                clienteDTO.ApellidoMaterno = clienteParam.ApellidoMaterno;
                clienteDTO.RFC = clienteParam.RFC;

                ClienteDAO.Guardar(clienteDTO);

                return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}