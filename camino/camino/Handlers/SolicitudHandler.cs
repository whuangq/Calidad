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
    public class SolicitudHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public SolicitudHandler()
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

        public List<Solicitud> obtenerTodaslasSolicitudes()
        {
            List<Solicitud> Solicitudes = new List<Solicitud>();
            //string consulta = "SELECT * FROM Solicitante";
            string consulta = "execute spGetSolicitantes";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Solicitudes.Add(
                    new Solicitud
                    {
                        nombre = Convert.ToString(columna["Nombre"]),
                        apellido = Convert.ToString(columna["Apellido"]),
                        edad = Convert.ToInt32(columna["edad"]),
                        numeroTelefonico = Convert.ToString(columna["tel"]),
                        email = Convert.ToString(columna["Correo"]),
                        genero = Convert.ToString(columna["Sexo"]),
                    });
            }
            return Solicitudes;
        }

        public bool crearSolicitud(Solicitud Solicitud)
        {
            //string consulta = "INSERT INTO Solicitante (correo, nombre, apellido, sexo,edad , tel) " +
            //"VALUES (@correo, @nombre,@apellido,@sexo,edad, @tel) ";

            string consulta = "INSERT INTO Solicitante VALUES (@correo, @nombre,@apellido,@sexo,@edad, @tel) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@correo", Solicitud.email);
            comandoParaConsulta.Parameters.AddWithValue("@nombre", Solicitud.nombre);
            comandoParaConsulta.Parameters.AddWithValue("@apellido", Solicitud.apellido);
            comandoParaConsulta.Parameters.AddWithValue("@sexo", Solicitud.genero);
            comandoParaConsulta.Parameters.AddWithValue("@edad", Solicitud.edad);
            comandoParaConsulta.Parameters.AddWithValue("@tel", Solicitud.numeroTelefonico);
           
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }

}