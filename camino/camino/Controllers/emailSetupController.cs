using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Models;
using camino.Handlers;

using System.Net;
using System.Net.Mail;


namespace camino.Controllers
{
    public class emailSetupController : Controller
    {
        // GET: emailSetup
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(camino.Models.gmail model)
        {
            MailMessage correo = new MailMessage("elcaminocrpi@gmail.com", model.To);
            correo.Subject = model.Subject;
            correo.Body = model.Body;
            correo.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential networkCred = new NetworkCredential("elcaminocrpi@gmail.com", "nciksqcrzsnsbvzw");
            //smtp.UseDefaultCredentials = false;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCred;
            smtp.Send(correo);
            ViewBag.Message = "Se envio un correo!";
            

            return View();
        }

        [HttpGet]
        public ActionResult envioCodigoSolicitud(string email, string nombre, string apellido, string genero, int edad, string telefono)
        {
            MailMessage correo = new MailMessage("elcaminocrpi@gmail.com", email);
            correo.Subject = "Codigo de Activación";
            camino.Models.CodigoModel code = new CodigoModel();
       

            correo.Body = "Su codigo de registro es: " + code.Codigo ;
            correo.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential networkCred = new NetworkCredential("elcaminocrpi@gmail.com", "nciksqcrzsnsbvzw");
            //smtp.UseDefaultCredentials = false;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCred;
            smtp.Send(correo);
            ViewBag.Message = "Se envio el codigo de registro: " + code.Codigo + " al usuario " + email;

            CaminanteHandler acceso = new CaminanteHandler();
            Caminante persona = new Caminante();
            persona.email = email;
            persona.nombre = nombre;
            persona.apellido = apellido;
            persona.genero = "m"; // hardcoded m
            persona.edad = edad;
            persona.numeroTelefonico = telefono;
            persona.CodigoRegistro = code.Codigo;
            persona.password = Convert.ToString(code.Codigo);
            acceso.crearCaminante(persona);







            return View();
        }
    }
}