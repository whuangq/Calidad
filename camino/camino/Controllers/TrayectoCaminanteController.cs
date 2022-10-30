using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class TrayectoCaminanteController : Controller
    {
        // GET: TrayectoCaminante
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnlistadosEnTrayectos()
        {
            TrayectoCaminanteHandler acceso = new TrayectoCaminanteHandler();
            List<TrayectoCaminanteModel> enlistados = acceso.obtenerTodosLosTrayectoCaminantes();
            TrayectoHandler accessoTrayectos = new TrayectoHandler();

            List<TrayectoModel> trayectos = accessoTrayectos.obtenerTodosLosTrayectos();
            ViewBag.trayectos = trayectos;
            ViewBag.enlistados = enlistados; 
            return View();
        }

        public ActionResult Enlistar(string email, int TrayectoTrayectoID)
        {   
            TrayectoCaminanteHandler accesoDatos = new TrayectoCaminanteHandler();

            if (accesoDatos.Buscar(email, TrayectoTrayectoID))
            {
                ViewBag.mensaje = "El usuario ya se encuentra apuntado en ese trayecto";
            }
            else
            {

                TrayectoCaminanteModel modelo = new TrayectoCaminanteModel();
                modelo.CaminanteCorreo = email;
                modelo.TrayectoTrayectoID = TrayectoTrayectoID;
                accesoDatos.crearTrayectoCaminante(modelo);

                ViewBag.mensaje = "Se inserto el usuario: " + email + "En el trayecto de ID: " + TrayectoTrayectoID;

                List<TrayectoCaminanteModel> enlistados = accesoDatos.obtenerTodosLosTrayectoCaminantes();
                ViewBag.enlistados = enlistados;
            }

            return View();
        }

        [HttpPost]
        public ActionResult VerParticipacion(TrayectoCaminanteModel caminante)
        {
            TrayectoCaminanteHandler accesoDatos = new TrayectoCaminanteHandler();
            List<TrayectoCaminanteModel> participantes = accesoDatos.BuscarCaminanteEnTrayectos(caminante.CaminanteCorreo);
            // logica de busqueda del correo
            TrayectoHandler accesoDatosTrayectos = new TrayectoHandler();
            List<TrayectoModel> trayectos = accesoDatosTrayectos.obtenerTodosLosTrayectos();
            
           

            ViewBag.trayectos = trayectos;
            ViewBag.participantes = participantes;

            return View();
        }


    }
}