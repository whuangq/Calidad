using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class MiCuentaController : Controller
    {
        // GET: MiCuenta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MiCuenta()
        {
            MiCuentaHandler accesoDatos = new MiCuentaHandler();
            ViewBag.MiCuenta = accesoDatos.obtenerDatosUsuario();
            return View();
        }
    }
}