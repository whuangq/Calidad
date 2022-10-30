using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using camino.Handlers;
using camino.Models;

namespace camino.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListadodeProveedores()
        {
            ProveedorHandler accesoDatos = new ProveedorHandler();
            ViewBag.Proveedores = accesoDatos.ObtenerTodosLosProveedores();
            return View();
        }

        public ActionResult CrearProveedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearProveedor(ProveedorModel proveedor)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ProveedorHandler accesoDatos = new ProveedorHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.CrearProveedor(proveedor);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El Poveedor fue creado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear al proveedor";
                return View();
            }
        }

        public ActionResult EditarProveedor(int proveedorId)
        {
            ViewBag.proveedorId = proveedorId;
            return View();
        }

        [HttpPost]
        public ActionResult EditarProveedor(ProveedorModel proveedor)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                int proveedorId = Convert.ToInt32(Request.Form["proveedorId"]);
                if (ModelState.IsValid)
                {
                    ProveedorHandler accesoDatos = new ProveedorHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.EditarProveedor(proveedor, proveedorId);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El proveedor fue editado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible editar al proveedor";
                return View();
            }
        }

        public ActionResult EliminarProveedor(int proveedorId)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    ProveedorHandler accesoDatos = new ProveedorHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.EliminarProveedor(proveedorId);
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El proveedor fue eliminado con éxito";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible eliminar al proveedor";
                return View();
            }
        }
    }
}