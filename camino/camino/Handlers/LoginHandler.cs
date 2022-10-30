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
    public class LoginHandler
    {

        private SqlConnection conexion;
        private string rutaConexion;

        public LoginHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["proyecto"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        public SqlCommand consultarRol(SqlConnection conexion, camino.Models.Login model)
        {
            SqlCommand cd2 = new SqlCommand("select EsAdmin from [dbo].[Caminante] where Contraseña = @Contraseña and correo = @correo", conexion);
            cd2.Parameters.AddWithValue("Contraseña", model.Credential.Password);
            cd2.Parameters.AddWithValue("Correo", model.Credential.Email);

            return cd2;
        }

        public SqlCommand consultarCredenciales(SqlConnection conexion, camino.Models.Login model)
        {
            SqlCommand cd1 = new SqlCommand("select correo,Contraseña from [dbo].[Caminante] where Contraseña = @Contraseña and correo = @correo", conexion);
            cd1.Parameters.AddWithValue("Contraseña", model.Credential.Password);
            cd1.Parameters.AddWithValue("Correo", model.Credential.Email);

            return cd1;
        }

        public SqlCommand consultarUsuario(SqlConnection conexion, camino.Models.Login model)
        {
            SqlCommand cd3 = new SqlCommand("select Nombre,Apellido from [dbo].[Caminante] where Contraseña = @Contraseña and correo = @correo", conexion);
            cd3.Parameters.AddWithValue("Contraseña", model.Credential.Password);
            cd3.Parameters.AddWithValue("Correo", model.Credential.Email);

            return cd3;
        }

        public bool authenticate(camino.Models.Login model)
        {
            bool authenticated = false;
            string rol = "";
            string usuario = "";
            rutaConexion = ConfigurationManager.ConnectionStrings["proyecto"].ToString();
            conexion = new SqlConnection(rutaConexion);
            conexion.Open();
            SqlDataReader reader1;
            reader1 = consultarCredenciales(conexion, model).ExecuteReader();
            if (reader1.Read())
            {
                reader1.Close();
                reader1 = consultarRol(conexion, model).ExecuteReader();
                while (reader1.Read())
                {
                   rol = String.Format("{0}", reader1[0]);
                }
                model.Credential.Role = Convert.ToInt32(rol);

                reader1.Close();
                reader1 = consultarUsuario(conexion, model).ExecuteReader();
                while (reader1.Read())
                {
                    usuario = String.Format("{0}", reader1[0]) + " " +  String.Format("{0}", reader1[1]);
                }
                model.Credential.Usuario = usuario;
                authenticated = true;
                conexion.Close();
            }
            else
            {
                authenticated = false;
                conexion.Close();
                
            }
            return authenticated;
        }
    }
}