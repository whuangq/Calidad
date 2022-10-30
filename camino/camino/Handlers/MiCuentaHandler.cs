using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using camino.Models;

namespace camino.Handlers
{
    public class MiCuentaHandler
    {

        private SqlConnection conexion;
        private string rutaConexion;

        public MiCuentaHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["proyecto"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        private DataTable crearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public List<MiCuentaModel> obtenerDatosUsuario()
        {
            List<MiCuentaModel> datosUsuario = new List<MiCuentaModel>();
            HttpCookie reqCookie = HttpContext.Current.Request.Cookies["userInfo"];
            string correo = reqCookie["Correo"].ToString();
            string consulta = $"SELECT * from caminante where correo = '{correo}'";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                datosUsuario.Add(
                    new MiCuentaModel
                    {
                        Nombre = Convert.ToString(columna["Nombre"]),
                        Apellido = Convert.ToString(columna["Apellido"]),
                        Sexo = Convert.ToString(columna["Sexo"]),
                        Edad = Convert.ToInt32(columna["Edad"]),
                        Telefono = Convert.ToString(columna["Tel"]),
                    });
            }
            return datosUsuario;
        }

        public bool llenarInformacion(camino.Models.Login model)
        {
            bool success = false;


            return success;
        }

    }



}