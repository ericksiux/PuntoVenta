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
    //[Authorize]
    public class ArticulosController : Controller
    {
        public ActionResult Index()
        {
            List<ArticuloModel> model = new List<ArticuloModel>();

            ArticuloDAO ArticuloDAO = new ArticuloDAO();
            var articulos = ArticuloDAO.ObtenerArticulos();

            foreach (var articulo in articulos)
            {
                model.Add(new ArticuloModel { ArticuloID = articulo.ArticuloID,
                    Descripcion = articulo.Descripcion,
                    DescripcionModelo = articulo.DescripcionModelo,
                    Existencia = articulo.Existencia,
                    Precio = articulo.PrecioArticulo
                });
            }

            Session["ArticuloModel"] = model;
            return View(model);
        }

        public ActionResult Nuevo(long ArticuloID)
        {
            ArticuloModel model = new ArticuloModel();
            List<ArticuloModel> ArticulosModel = (List<ArticuloModel>)Session["ArticuloModel"];

            foreach (var articulo in ArticulosModel)
            {
                if (articulo.ArticuloID == ArticuloID)
                {
                    model = articulo;
                    continue;
                }
            }


            return View(model);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarArticulo(ArticuloModel articuloParam)
        {
            try
            {
                ArticuloDAO ArticuloDAO = new ArticuloDAO();

                var articuloDTO = new ArticuloDTO();
                articuloDTO.ArticuloID = articuloParam.ArticuloID;
                articuloDTO.Descripcion = articuloParam.Descripcion;
                articuloDTO.DescripcionModelo = articuloParam.DescripcionModelo;
                articuloDTO.PrecioArticulo = articuloParam.Precio;
                articuloDTO.Existencia = articuloParam.Existencia;

                ArticuloDAO.Guardar(articuloDTO);

                return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}