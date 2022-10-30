using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadodeServicios()
        {
            ServicioHandler accesoDatos = new ServicioHandler();
            ViewBag.Trayectos = accesoDatos.obtenerTodosLosServicios();
            return View();
        }

        [HttpGet]
        public ActionResult consultarServicios(int trayectoId)
        {
            ServicioHandler accesoDatos = new ServicioHandler();
            ViewBag.Servicios = accesoDatos.obtenerServiciosDelTrayecto(trayectoId);
            ViewBag.trayectoId = trayectoId;
            return View();
        }

        public ActionResult agregarServicio(int TrayectoId)
        {
            ViewBag.TrayectoId = TrayectoId;
            return View();
        }

        [HttpPost]
        public ActionResult agregarServicio(ServicioModel servicio)
        {
            ViewBag.ExitoAlCrear = false;
            servicio.TrayectoId = Convert.ToInt32(Request.Form["TrayectoId"]);
            ViewBag.TrayectoId = servicio.TrayectoId;
            try
            {
                if (ModelState.IsValid)
                {
                    ServicioHandler accesoDatos = new ServicioHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearServicio(servicio);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El servicio fue agregado con éxito con código ";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear el servicio";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }


        public ActionResult eliminarServicio(int servicioId)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ServicioHandler accesoDatos = new ServicioHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.eliminarServicio(servicioId); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El servicio fue eliminado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible eliminar el servicio";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }


        public ActionResult editarServicio(int ServicioId, int TrayectoId)
        {
            ViewBag.ServicioId = ServicioId;
            ViewBag.TrayectoId = TrayectoId;
            return View();
        }

        [HttpPost]
        public ActionResult editarServicio(ServicioModel servicio)
        {
            ViewBag.ExitoAlCrear = false;
            servicio.TrayectoId = Convert.ToInt32(Request.Form["TrayectoId"]);
            ViewBag.TrayectoId = servicio.TrayectoId;
            try
            {
                if (ModelState.IsValid)
                {
                    ServicioHandler accesoDatos = new ServicioHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.editarServicio(servicio);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El servicio fue editado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible editar el servicio";
                return View();
            }
        }
    }
}