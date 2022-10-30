using camino.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Models;

namespace camino.Controllers
{
    public class CaminanteController : Controller
    {
        // GET: Caminante
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listadodeCaminantes()
        {
            CaminanteHandler accesoDatos = new CaminanteHandler();
            ViewBag.Caminantes = accesoDatos.obtenerTodoslasCaminantes();
            return View();
        }

        public ActionResult agregarTrayecto(string email, string nombre, string apellido, string genero, int edad, string tel)
        {

            Caminante caminante = new Caminante();
            caminante.email = email;
            caminante.nombre = nombre;
            caminante.apellido = apellido;
            caminante.genero = genero;
            caminante.edad = edad;
            caminante.numeroTelefonico = tel;
            

            ViewBag.Caminante = caminante;

            // traer listado de trayectos
            TrayectoHandler accesoTrayectos = new TrayectoHandler();
            List<TrayectoModel> trayectos = accesoTrayectos.obtenerTodosLosTrayectos();
            ViewBag.Trayectos = trayectos;




            return View();
        }

        public ActionResult BuscarCaminante(string email)
        {
            Caminante usuario = new Caminante();
            CaminanteHandler accesoDatos = new CaminanteHandler();
            usuario = accesoDatos.BuscarCaminante(email);
            if(usuario == null)
            {
                ViewBag.mensaje = "No existe el usuario";

            }
            else
            {
                ViewBag.mensaje = "Usuario encontrado";
                ViewBag.usuario = usuario;

            }



            return View();
        }
    }
}