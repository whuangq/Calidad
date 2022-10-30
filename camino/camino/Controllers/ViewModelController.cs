using camino.Handlers;
using camino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace camino.Controllers
{
    public class ViewModelController : Controller
    {
        // GET: ViewModel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexViewModel(Models.gmail estructura, Models.Solicitud persona)
        {
            ViewBag.Message = "Vista para tener solicitud y correo juntos";
            ViewModel modelo = new ViewModel();
            SolicitudHandler accesoDatos = new SolicitudHandler();
            modelo.solicitudes = accesoDatos.obtenerTodaslasSolicitudes();
            modelo.getGmailModel = new gmail();


            return View(modelo);
        }
    }
}