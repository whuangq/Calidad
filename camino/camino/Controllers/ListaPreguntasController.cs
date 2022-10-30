using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class ListaPreguntasController : Controller
    {
        // GET: ListaPreguntas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listadodePreguntasenEncuesta(int encuestaId)
        {
            ListaPreguntasHandler accesoDatos = new ListaPreguntasHandler();
            ViewBag.Preguntas = accesoDatos.obtenerPreguntasdeEncuesta(encuestaId);
            List<PreguntaModel> preguntas = new List<PreguntaModel>();
            foreach (var pregunta in ViewBag.Preguntas)
            {
                preguntas.Add(pregunta);
            }
            return View(new ListaPreguntasModel(preguntas));
        }

        [HttpPost]
        public ActionResult listadodePreguntasenEncuesta(ListaPreguntasModel listaPreguntas)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ListaPreguntasHandler accesoDatos = new ListaPreguntasHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearRespuesta(listaPreguntas); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "Encuesta completada";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible completar la encuesta";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }
    }
}