using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.DAO;
using TEST.DTO;
using TEST.Models;

namespace TEST.Controllers
{
    //[Authorize]
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            List<VentaModel> model = new List<VentaModel>();

            VentaDAO VentaDAO = new VentaDAO();
            var ventas = VentaDAO.ObtenerVentas();

            foreach(var venta in ventas)
            {
                model.Add(new VentaModel { FoliadorVentaID  = venta.FoliadorVentaID, ClienteID = venta.ClienteID , NombreCompleto = venta.NombreCompleto, TotalAdeudo = venta.Total, Fecha = venta.Fecha } );
            }

            
            return View(model);
        }


        // GET: Nuevo
        public ActionResult Nuevo()
        {
            NuevaVentaModel model = new Models.NuevaVentaModel();

            try
            {
                ConfiguracionGeneralDAO ConfiguracionDAO = new ConfiguracionGeneralDAO();
                var ConfiguracionDTO = ConfiguracionDAO.ObtenerConfiguracionGeneral();

                VentaDAO VentaDAO = new VentaDAO();
                model.FoliadorVentaID = VentaDAO.ObtenerFoliador(); //ToDo obtener Folio

                model.PlazoMaximo = ConfiguracionDTO.PlazoMaximo;
                model.PorcentajeEnganche = ConfiguracionDTO.PorcentajeEnganche;
                model.TasaFinanciamiento = ConfiguracionDTO.TasaFinanciamiento;

                Session["modelGridArticuloVenta"] =  new List<GridArticuloVentaModel>();
                Session["ConfiguracionGeneral"] = ConfiguracionDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(model);
        }
        

        [HttpGet]
        public JsonResult ObtenerCliente()
        {
            try
            {
                ClienteDAO DAO = new ClienteDAO();
                List<ClienteDTO> clientes = DAO.ObtenerClientes();


                return Json(new { Success = 1, Data = clientes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ObtenerArticulos()
        {
            try
            {
                var ConfiguracionGeneral = (ConfiguracionGeneralDTO)Session["ConfiguracionGeneral"];
                

                ArticuloDAO DAO = new ArticuloDAO();
                List<ArticuloDTO> articulos = DAO.ObtenerArticulos();

                foreach(var item in articulos)
                {
                    item.PrecioVenta = item.PrecioArticulo * (1 + (ConfiguracionGeneral.TasaFinanciamiento * ConfiguracionGeneral.PlazoMaximo) / 100);
                }


                return Json(new { Success = 1, Data = articulos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AgregarArticuloVenta(GridArticuloVentaModel articuloParam)
        {
            try
            {
                var model =(List<GridArticuloVentaModel>)Session["modelGridArticuloVenta"];

                ArticuloDAO DAO = new ArticuloDAO();

                bool itemagregado = false;
                foreach (var item in model)
                {
                    if (item.GUID == articuloParam.GUID)
                    {
                        item.Cantidad = articuloParam.Cantidad;
                        itemagregado = true;
                    }
                }

                if (!itemagregado)
                {
                    var ConfiguracionGeneral = (ConfiguracionGeneralDTO)Session["ConfiguracionGeneral"];
                    var articulos = DAO.ObtenerArticulos();

                    foreach (var item in articulos)
                    {
                        item.PrecioVenta = item.PrecioArticulo * (1 + (ConfiguracionGeneral.TasaFinanciamiento * ConfiguracionGeneral.PlazoMaximo) / 100);
                    }

                    foreach (var item in articulos)
                    {
                        if(item.ArticuloID == articuloParam.ArticuloID)
                        {
                            articuloParam.Cantidad = 0;
                            articuloParam.Descripcion = item.Descripcion;
                            articuloParam.DescripcionModelo = item.DescripcionModelo;
                            articuloParam.Existencia = item.Existencia;
                            articuloParam.PrecioVenta = item.PrecioVenta;
                        }
                    }

                    articuloParam.GUID = Guid.NewGuid().ToString();

                    model.Add(articuloParam);
                }

                Session["modelGridArticuloVenta"] = model;
                return RenderView(model, "GridArticulosNuevaVenta");
            }
            catch (Exception ex)
            {
                return RenderError(ex, "ErrorGeneral");
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarArticulo(GridArticuloVentaModel articuloParam)
        {
            try
            {
                var model = (List<GridArticuloVentaModel>)Session["modelGridArticuloVenta"];

                ArticuloDAO DAO = new ArticuloDAO();
                GridArticuloVentaModel itemeliminar = new GridArticuloVentaModel();
                foreach (var item in model)
                {
                    if (item.GUID == articuloParam.GUID)
                    {
                        itemeliminar = item;
                    }
                }

                model.Remove(itemeliminar);

                Session["modelGridArticuloVenta"] = model;
                return RenderView(model, "GridArticulosNuevaVenta");
            }
            catch (Exception ex)
            {
                return RenderError(ex, "ErrorGeneral");
            }
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CalcularAbonoMensual()
        {
            try
            {
                var ConfiguracionGeneral = (ConfiguracionGeneralDTO)Session["ConfiguracionGeneral"];

                var ArticulosVenta = (List<GridArticuloVentaModel>)Session["modelGridArticuloVenta"];
                List<AbonoMensualModel> AbonosLista = new List<AbonoMensualModel>();


                var Importe = 0.00M;
                foreach (var item in ArticulosVenta)
                {
                    Importe = Importe + (item.Cantidad * item.PrecioVenta);
                }

                var Enganche = (ConfiguracionGeneral.PorcentajeEnganche / 100) * Importe;
                var BonificacionEnganche = Enganche * ((ConfiguracionGeneral.TasaFinanciamiento * ConfiguracionGeneral.PlazoMaximo) / 100);
                var TotalAdeudo = Importe - Enganche - BonificacionEnganche;

                var PrecioContado = TotalAdeudo / (1 + ((ConfiguracionGeneral.TasaFinanciamiento * ConfiguracionGeneral.PlazoMaximo) / 100));
                int[] Plazos = new int[4] { 3, 6,9,12 };

                foreach(var plazo in Plazos)
                {
                    AbonoMensualModel detalle = new AbonoMensualModel();
                    detalle.Plazo = plazo;
                    detalle.TotalPagar = PrecioContado * (1 + (ConfiguracionGeneral.TasaFinanciamiento * plazo) / 100);
                    detalle.ImporteAbono = detalle.TotalPagar / plazo;
                    detalle.ImporteAhorra = TotalAdeudo - detalle.TotalPagar;
                    AbonosLista.Add(detalle);
                }

                return RenderView(AbonosLista, "GridDetalleAbono");
            }
            catch (Exception ex)
            {
                return RenderError(ex, "ErrorGeneral");
            }
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarVenta(NuevaVentaModel ventaParam)
        {
            try
            {
                
                VentaDAO ventaDAO = new VentaDAO();

                var ventaDTO = new VentaDTO();
                ventaDTO.FoliadorVentaID = ventaParam.FoliadorVentaID;
                ventaDTO.ClienteID = ventaParam.ClienteID;
                ventaDTO.Total = ventaParam.TotalAdeudo;
                ventaDTO.Plazo = ventaParam.Plazo;

                ventaDAO.Guardar(ventaDTO);

                var ArticulosVenta = (List<GridArticuloVentaModel>)Session["modelGridArticuloVenta"];
                ArticuloDAO DAO = new ArticuloDAO();
                ArticuloDTO articulo = new ArticuloDTO();
                foreach (var item in ArticulosVenta)
                {
                    articulo.ArticuloID = item.ArticuloID;
                    articulo.Existencia = item.Existencia - item.Cantidad;

                    DAO.ActualizarInventario(articulo);
                }

                return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RenderView(List<GridArticuloVentaModel> model, string viewname)
        {
            var view = RenderRazorViewToString(viewname, model);

            var json = Json(new { Success = 1, Data = view }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult RenderView(List<AbonoMensualModel> model, string viewname)
        {
            var view = RenderRazorViewToString(viewname, model);

            var json = Json(new { Success = 1, Data = view }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public JsonResult RenderError(Exception ex, string labelError)
        {
            return Json(new { Success = 0, Data = ex.Data }, JsonRequestBehavior.AllowGet);
        }
    }
}