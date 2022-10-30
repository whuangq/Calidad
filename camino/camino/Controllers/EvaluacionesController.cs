using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class EvaluacionesController : Controller
    {
        // GET: Evaluaciones
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadodeTrayectosDelCaminante()
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string caminanteId = reqCookies["Correo"].ToString();
            EvaluacionesHandler accesoDatos = new EvaluacionesHandler();
            ViewBag.Evaluaciones = accesoDatos.obtenerLosTrayectosDelCaminante(caminanteId);
            return View();
        }
        public ActionResult listadodeEvaluaciones()
        {
            EvaluacionesHandler accesoDatos = new EvaluacionesHandler();
            ViewBag.Evaluaciones = accesoDatos.obtenerTodasLasEvaluaciones();
            return View();
        }

        public ActionResult listadodeEvaluacionesCaminante(int trayectoId)
        {
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            string caminanteId = reqCookies["Correo"].ToString();
            EvaluacionesHandler accesoDatos = new EvaluacionesHandler();
            ViewBag.Evaluaciones = accesoDatos.obtenerEvaluacionesDelCaminante(trayectoId, caminanteId);
            return View();
        }

        public ActionResult crearEvaluacion(int servicioId)
        {
            ViewBag.servicioId = servicioId;
            return View();
        }

        [HttpPost]
        public ActionResult crearEvaluacion(EvaluacionesModel evaluacion)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    EvaluacionesHandler accesoDatos = new EvaluacionesHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearEvaluacion(evaluacion); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La encuesta fue creada con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear la encuesta";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }
        public ActionResult eliminarEvaluacion(int encuestaId)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    EvaluacionesHandler accesoDatos = new EvaluacionesHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.eliminarEvaluacion(encuestaId); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La encuesta fue eliminada con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible eliminar la enceusta";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }
        public ActionResult seleccionarServicio()
        {
            ServicioHandler accesoDatos = new ServicioHandler();
            ViewBag.Servicios = accesoDatos.obtenerTodosLosServicios();
            return View();
        }
    }
}