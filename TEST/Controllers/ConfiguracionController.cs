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
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        public ActionResult Index()
        {
            ConfiguracionGeneralDAO ConfiguracionDAO = new ConfiguracionGeneralDAO();
            var ConfiguracionDTO = ConfiguracionDAO.ObtenerConfiguracionGeneral();

            ConfiguracionGeneralModel model = new ConfiguracionGeneralModel();

            model.PlazoMaximo = ConfiguracionDTO.PlazoMaximo;
            model.PorcentajeEnganche = ConfiguracionDTO.PorcentajeEnganche;
            model.TasaFinanciamiento = ConfiguracionDTO.TasaFinanciamiento;

            return View(model);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarConfiguracion(ConfiguracionGeneralModel configuracionParam)
        {
            try
            {
                ConfiguracionGeneralDAO dao = new ConfiguracionGeneralDAO();

                var DTO = new ConfiguracionGeneralDTO();
                DTO.TasaFinanciamiento = configuracionParam.TasaFinanciamiento;
                DTO.PorcentajeEnganche = configuracionParam.PorcentajeEnganche;
                DTO.PlazoMaximo = configuracionParam.PlazoMaximo;

                dao.Guardar(DTO);

                return Json(new { Success = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Data = ex }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}