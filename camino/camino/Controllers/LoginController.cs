using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

using System.Net;
using System.Data.SqlClient;


namespace camino.Controllers
{
    public class loginController : Controller
    {
        public ActionResult CambiarContraseña()
        {
            return View();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(camino.Models.Login model)
        {
            string rol = "Caminante";
            LoginHandler accesoDatos = new LoginHandler();
            if (accesoDatos.authenticate(model))
            {
                if(model.Credential.Role == 1)
                {
                    rol = "Administrador";
                }
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo.Expires = DateTime.Now.AddDays(1);
                userInfo["Login"] = "Successful";
                userInfo["Role"] = rol;
                userInfo["Correo"] = model.Credential.Email;
                userInfo["Usuario"] = model.Credential.Usuario;
                userInfo.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(userInfo);
                return View("~/Views/Home/Index.cshtml");
            } else
            {
                return View("~/Views/Login/LoginFailure.cshtml");
            }
            

        }

        [HttpPost]
        public ActionResult CambiarContraseña(camino.Models.Login model)
        {
            if (ModelState.IsValid)
            {
                if (model.ChangePassword.Password == model.ChangePassword.ConfirmPassword)
                {
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            return View("/Views/Login/CambiarContraseña.cshtml");
        }

        public ActionResult Cerrar()
        {
            if (Request.Cookies["userInfo"] != null)
            {

                HttpCookie myCookie = new HttpCookie("userInfo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);

                /*
                Request.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1d);
                Request.Cookies["userInfo"].Value = null;
                HttpContext.Response.SetCookie(Request.Cookies["userInfo"]);

                Response.Cookies.Add(Request.Cookies["userInfo"]);
                */
                ViewBag.mensaje = "Ha cerrado sesión con exito";

            }

            //HttpContext.Response.Cookies.Remove("userInfo");
            
            return View("~/Views/Home/Index.cshtml");
        }

    }



}