using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class CategoriaServiciosController : Controller
    {
        // GET: CategoriaServicios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadodeCategoriaServicios()
        {
            CategoriaServiciosHandler accesoDatos = new CategoriaServiciosHandler();
            ViewBag.Trayectos = accesoDatos.obtenerTodosLasCategoriaServicios();
            return View();
        }

        [HttpGet]
        public ActionResult consultarCategoriaServicios(int trayectoId = 0)
        {
            CategoriaServiciosHandler accesoDatos = new CategoriaServiciosHandler();
            ViewBag.CategoriaServicios = accesoDatos.obtenerCategoriaServiciosDelTrayecto(trayectoId);
            ViewBag.trayectoId = trayectoId;
            return View();
        }

        public ActionResult agregarCategoriaServicios(int TrayectoId = 0)
        {
            ViewBag.TrayectoId = TrayectoId;
            return View();
        }

        [HttpPost]
        public ActionResult agregarCategoriaServicios(CategoriaServiciosModel CategoriaServicios)
        {
            ViewBag.ExitoAlCrear = false;
            CategoriaServicios.CategoriaId = Convert.ToInt32(Request.Form["CategoriaId"]);
            ViewBag.CategoriaServiciosId = CategoriaServicios.CategoriaId;
            try
            {
                if (ModelState.IsValid)
                {
                    CategoriaServiciosHandler accesoDatos = new CategoriaServiciosHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearCategoriaServicios(CategoriaServicios);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La CategoriaServicios fue agregada con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear la CategoriaServicios";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }


        public ActionResult eliminarCategoriaServicios(int CategoriaId)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    CategoriaServiciosHandler accesoDatos = new CategoriaServiciosHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.eliminarCategoriaServicios(CategoriaId); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La CategoriaServicios fue eliminada con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible eliminar la CategoriaServicios";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }


        public ActionResult editarCategoriaServicios(int CategoriaServiciosId, string CategoriaServicios)
        {
            ViewBag.CategoriaServiciosId = CategoriaServiciosId;
            ViewBag.CategoriaServicios = CategoriaServicios;
            return View();
        }

        [HttpPost]
        public ActionResult editarCategoriaServicios(CategoriaServiciosModel CategoriaServicios)
        {
            ViewBag.ExitoAlCrear = false;
            CategoriaServicios.CategoriaId = Convert.ToInt32(Request.Form["CategoriaServiciosId"]);
            ViewBag.CategoriaServicios = CategoriaServicios.CategoriaId;
            try
            {
                if (ModelState.IsValid)
                {
                    CategoriaServiciosHandler accesoDatos = new CategoriaServiciosHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.editarCategoriaServicios(CategoriaServicios);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La CategoriaServicios fue editado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible editar la CategoriaServicios";
                return View();
            }
        }
    }
}