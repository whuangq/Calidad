using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Models;

namespace camino.Controllers
{
    public class CodigoController : Controller
    {
        // GET: Codigo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(camino.Models.Solicitud solicitud)
        {
            return View();
        }

        
    }
}