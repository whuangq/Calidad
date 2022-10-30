using camino.Handlers;
using camino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace camino.Controllers
{
    public class TrayectoViewModelController : Controller
    {
        // GET: TrayectoViewModel
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Crear(TrayectoViewModel modelo)
        {
            TrayectoHandler accesoTrayectos = new TrayectoHandler();
            SitioHandler accesoSitios = new SitioHandler();
            modelo.SitioInicio.Latitud = Convert.ToDecimal(modelo.LatitudInicial);
            modelo.SitioInicio.Longitud = Convert.ToDecimal(modelo.LongitudInicial);
            modelo.SitioInicio.Direccion = modelo.SitioInicio.Provincia + ", " + modelo.SitioInicio.Canton + ", " + modelo.SitioInicio.Distrito;
            modelo.SitioFin.Latitud = Convert.ToDecimal(modelo.LatitudFinal);
            modelo.SitioFin.Longitud = Convert.ToDecimal(modelo.LongitudFinal);
            modelo.SitioFin.Direccion = modelo.SitioFin.Provincia + ", " + modelo.SitioFin.Canton + ", " + modelo.SitioFin.Distrito;
            //insertar con los pks de sitios recientemente creados

            bool crearSitioInicio = accesoSitios.crearSitio(modelo.SitioInicio);
            bool crearSitioFinal = accesoSitios.crearSitio(modelo.SitioFin);
            List<SitioModel> Sitios = accesoSitios.obtenerTodosLosSitios();
            int llaveInicio = accesoSitios.buscarPorLatLng(Convert.ToDecimal(modelo.LatitudInicial), Convert.ToDecimal(modelo.LongitudInicial));
            int llaveFin = accesoSitios.buscarPorLatLng(Convert.ToDecimal(modelo.LatitudFinal), Convert.ToDecimal(modelo.LongitudFinal));
            modelo.Trayecto.Distancia = Convert.ToInt32(Convert.ToDouble(modelo.Distancia));
            modelo.Trayecto.Inicio = llaveInicio;
            modelo.Trayecto.Final = llaveFin;

            bool crearTrayecto = accesoTrayectos.crearTrayecto(modelo.Trayecto);

            ViewBag.modelo = modelo;

            return View();
        }

        [HttpPost]
        public ActionResult LlenarFormulario(TrayectoViewModel modelo)
        {
            ViewBag.modelo = modelo;
            return View();
        }
    }
}