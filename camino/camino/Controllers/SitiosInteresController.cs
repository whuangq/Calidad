using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Models;
using camino.Handlers;

namespace camino.Controllers
{
    public class SitiosInteresController : Controller
    {
        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(camino.Models.SitiosInteresModel sitiosInteres)
        {
            return View();
        }

        public ActionResult listadodeSitios()
        {
            SitiosInteresHandler accesoDatos = new SitiosInteresHandler();
            ViewBag.sitios = accesoDatos.obtenerSitiosInteres();
            return View();
        }

       
        public ActionResult crearSitio(SitiosInteresModel sitio)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SitiosInteresHandler accesoDatos = new SitiosInteresHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearSitio(sitio); //recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El sitio" + " " + sitio.sitioNombre + " fue creado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear el sitio";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }

        [HttpGet]
        public FileResult accederArchivo(int identificador)
        {
            SitiosInteresHandler accesoDatos = new SitiosInteresHandler();
            var tupla = accesoDatos.descargarContenido(identificador);
            return File(tupla.Item1, tupla.Item2);
        }


    }
}