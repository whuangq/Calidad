using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class PreguntaController : Controller
    {
        // GET: Pregunta
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult crearPregunta(int encuestaId)
        {
            ViewBag.encuestaId = encuestaId;
            return View();
        }

        [HttpPost]
        public ActionResult crearPregunta(PreguntaModel pregunta)
        {
            ViewBag.ExitoAlCrear = false;
            pregunta.encuestaId = Convert.ToInt32(Request.Form["encuestaId"]);
            ViewBag.encuestaId = pregunta.encuestaId;
            try
            {
                if (ModelState.IsValid)
                {
                    PreguntaHandler accesoDatos = new PreguntaHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearPregunta(pregunta); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La pregunta fue agregado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible agregar la pregunta";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }

        public ActionResult preguntasdeEncuesta(int encuestaId)
        {
            PreguntaHandler accesoDatos = new PreguntaHandler();
            ViewBag.Preguntas = accesoDatos.obtenerTodasLasPreguntasRespondidas(encuestaId);
            ViewBag.encuestaId = encuestaId;
            return View();
        }

        public ActionResult seleccionarPregunta(int encuestaId)
        {
            PreguntaHandler accesoDatos = new PreguntaHandler();
            ViewBag.Preguntas = accesoDatos.obtenerTodasLasPreguntasDeEncuesta(encuestaId);
            ViewBag.encuestaId = encuestaId;
            return View();
        }

        public ActionResult editarPregunta(int preguntaId, int encuestaId)
        {
            ViewBag.preguntaId = preguntaId;
            ViewBag.encuestaId = encuestaId;
            return View();
        }

        [HttpPost]
        public ActionResult editarPregunta(PreguntaModel pregunta)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                pregunta.id = Convert.ToInt32(Request.Form["preguntaId"]);
                pregunta.encuestaId = Convert.ToInt32(Request.Form["encuestaId"]);
                if (ModelState.IsValid)
                {
                    PreguntaHandler accesoDatos = new PreguntaHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.editarPregunta(pregunta); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "La pregunta fue editada con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible editar la pregunta";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }

        public ActionResult listadodeRespuestas(int preguntaId)
        {
            PreguntaHandler accesoDatos = new PreguntaHandler();
            ViewBag.Respuestas = accesoDatos.obtenerRespuestasdePregunta(preguntaId);
            return View();
        }
    }
}