using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;


namespace camino.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(camino.Models.Solicitud solicitud)
        {
            return View();
        }

        public ActionResult listadodeSolicitudes()
        {
            SolicitudHandler accesoDatos = new SolicitudHandler();
            ViewBag.Solicitudes = accesoDatos.obtenerTodaslasSolicitudes();
            return View();
        }

        public ActionResult crearSolicitud()
        {
            return View();
        }

        [HttpPost]
        public ActionResult crearSolicitud(Solicitud Solicitud)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SolicitudHandler accesoDatos = new SolicitudHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearSolicitud(Solicitud); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La Solicitud" + " " + Solicitud.nombre + " " + Solicitud.apellido + " fue agregado con éxito :)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible agregar la Solicitud";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }
    }
   

}