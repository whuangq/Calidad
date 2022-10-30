using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class TrayectoController : Controller
    {
        // GET: Trayecto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadodeTrayectos()
        {
            TrayectoHandler accesoDatos = new TrayectoHandler();
            ViewBag.Trayectos = accesoDatos.obtenerTodosLosTrayectos();
            return View();
        }

        public ActionResult VerTrayecto(int TrayectoId)
        {
            TrayectoHandler accesoTrayecto = new TrayectoHandler();
            SitioHandler accesoDatosSitios = new SitioHandler();
            List<string> sitios = new List<string>();
            TrayectoModel Trayecto = accesoTrayecto.obtenerTrayecto(TrayectoId);
            sitios.Add(accesoDatosSitios.BuscarSitio(Trayecto.Inicio).Direccion);
            sitios.Add(accesoDatosSitios.BuscarSitio(Trayecto.Final).Direccion);
            ViewBag.Sitios = sitios;
            ViewBag.Trayecto = Trayecto;
            return View();
        }

        public ActionResult MostrarTodosEnMapa()
        {
            TrayectoHandler accesoDatos = new TrayectoHandler();
            ViewBag.Trayectos = accesoDatos.obtenerTodosLosTrayectos();
            SitioHandler accesoDatosSitios = new SitioHandler();
            List<SitioModel> sitios = accesoDatosSitios.obtenerTodosLosSitios();
            

            /*
            List<double> serializados = new List<double>();


            foreach (var sitio in sitios)
            {
                serializados.Add(sitio.Latitud);
                serializados.Add(sitio.Longitud);
            }


            ViewBag.serializados = serializados;
            */
            List<string> serializados = new List<string>();

            foreach (var sitio in sitios)
            {
                serializados.Add(Convert.ToString(sitio.Direccion));
                
            }
            ViewBag.serializados = serializados;

            return View();
        }

        public ActionResult eliminarTrayecto(int trayectoID)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    TrayectoHandler accesoDatos = new TrayectoHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.eliminarTrayecto(trayectoID);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ModelState.Clear();
                        ViewBag.Message = "El trayecto con ID: \"" + trayectoID + "\" fue eliminado con éxito";
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible eliminar el trayecto";
                ViewBag.ExitoAlCrear = false;
                return View();
            }
        }

        public ActionResult CrearTrayecto()
        {
            return View();
        }

        
        public ActionResult LLenarFormulario(string p)
        {
            ViewBag.p = p;
            return View();
        }

        public ActionResult editarTrayecto(int TrayectoId)
        {
            ViewBag.TrayectoId = TrayectoId;
            return View();
        }

        [HttpPost]
        public ActionResult editarTrayecto(TrayectoModel Trayecto)
        {
            ViewBag.ExitoAlCrear = false;
            Trayecto.TrayectoID = Convert.ToInt32(Request.Form["TrayectoId"]);
            ViewBag.TrayectoId = Trayecto.TrayectoID;
            try
            {
                if (ModelState.IsValid)
                {
                    TrayectoHandler accesoDatos = new TrayectoHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.editarTrayecto(Trayecto);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El Trayecto fue editado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible editar el Trayecto";
                return View();
            }
        }
    }
}