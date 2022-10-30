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
    public class ServicioHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public ServicioHandler()
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

        public List<ServicioModel> obtenerTodosLosServicios()
        {
            List<ServicioModel> Servicios = new List<ServicioModel>();
            //string consulta = "SELECT * FROM Servicio";
            string consulta = "execute spGetServicios";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Servicios.Add(
                    new ServicioModel
                    {
                        ServicioId = Convert.ToInt32(columna["ServicioID"]),
                        Categoria = Convert.ToString(columna["Categoria"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                    });
            }
            return Servicios;
        }

        public List<ServicioModel> obtenerServiciosDelTrayecto(int trayectoId)
        {
            List<ServicioModel> Servicios = new List<ServicioModel>();
            //string consulta = $"SELECT * FROM Servicio join Trayecto_Servicio on Servicio.ServicioId=Trayecto_Servicio.Servicioid where Trayecto_Servicio.TrayectoId = {trayectoId}";
            string consulta = "execute spGetServiciosDelTrayecto " + trayectoId.ToString();
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Servicios.Add(
                    new ServicioModel
                    {
                        ServicioId = Convert.ToInt32(columna["ServicioID"]),
                        Categoria = Convert.ToString(columna["Categoria"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                    });
            }
            return Servicios;
        }

        public bool crearServicio(ServicioModel Servicio)
        {
            string consulta = "INSERT INTO Servicio VALUES (@Categoria, @Descripcion, null, null, null) "
            +"INSERT INTO Trayecto_Servicio (TrayectoId, Servicioid) VALUES (@TrayectoId, @@IDENTITY)";


            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Categoria", Servicio.Categoria);
            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", Servicio.Descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@TrayectoId", Servicio.TrayectoId);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool eliminarServicio(int servicioId)
        {
            //string consulta = "INSERT INTO Evaluacion (Caminantecorreo, Servicioid, Calificacion, Date, Comentario, Version) " +
            //"VALUES (@Servicioid, @Calificacion, @Date, @Comentario, @Version) ";

            string consulta = "DELETE FROM Servicio WHERE ServicioId = @ServicioId";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@ServicioId", servicioId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool editarServicio(ServicioModel Servicio)
        {
            string consulta = "UPDATE Servicio SET Categoria=@Categoria, Descripcion=@Descripcion WHERE ServicioId=@ServicioId";


            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Categoria", Servicio.Categoria);
            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", Servicio.Descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@ServicioId", Servicio.ServicioId);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }

}