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
    public class EvaluacionesHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public EvaluacionesHandler()
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

        public List<TrayectoModel> obtenerLosTrayectosDelCaminante(string caminanteId)
        {
            List<TrayectoModel> Trayectoes = new List<TrayectoModel>();
            string consulta = $"SELECT * FROM Trayecto JOIN Trayecto_Caminante ON Trayecto.trayectoId = Trayecto_Caminante.TrayectoTrayectoId WHERE caminantecorreo = '{caminanteId}'";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Trayectoes.Add(
                    new TrayectoModel
                    {
                        TrayectoID = Convert.ToInt32(columna["TrayectoID"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                    });
            }
            return Trayectoes;
        }

        public List<EvaluacionesModel> obtenerTodasLasEvaluaciones()
        {
            List<EvaluacionesModel> Evaluaciones = new List<EvaluacionesModel>();
            string consulta = "SELECT * FROM Encuesta";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Evaluaciones.Add(
                    new EvaluacionesModel
                    {
                        EncuestaID = Convert.ToInt32(columna["EncuestaID"]),
                        ServicioID = Convert.ToInt32(columna["ServicioID"]),
                        Calificacion = Convert.ToDecimal(columna["Calificacion"]),
                        Date = Convert.ToString(columna["Date"]),
                        Comentario = Convert.ToString(columna["Comentario"]),
                        Version = Convert.ToInt32(columna["Version"]),
                    });
            }
            return Evaluaciones;
        }

        public List<EvaluacionesModel> obtenerEvaluacionesDelCaminante(int trayectoId, string caminanteId)
        {
            List<EvaluacionesModel> Evaluaciones = new List<EvaluacionesModel>();
            string consulta = $"SELECT * FROM Encuesta JOIN Encuesta_Caminante ON Encuesta.encuestaId = Encuesta_Caminante.EncuestaEncuestaId JOIN Trayecto_Servicio ON Encuesta.servicioid = Trayecto_Servicio.servicioid WHERE Encuesta_Caminante.CaminanteCorreo = '{caminanteId}' AND trayectoId = {trayectoId}";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Evaluaciones.Add(
                    new EvaluacionesModel
                    {
                        EncuestaID = Convert.ToInt32(columna["EncuestaID"]),
                        ServicioID = Convert.ToInt32(columna["ServicioID"]),
                        Calificacion = Convert.ToDecimal(columna["Calificacion"]),
                        Date = Convert.ToString(columna["Date"]),
                        Comentario = Convert.ToString(columna["Comentario"]),
                        Version = Convert.ToInt32(columna["Version"]),
                    });
            }
            return Evaluaciones;
        }

        public bool crearEvaluacion(EvaluacionesModel Evaluacion)
        {
            //string consulta = "INSERT INTO Evaluacion (Caminantecorreo, Servicioid, Calificacion, Date, Comentario, Version) " +
            //"VALUES (@Servicioid, @Calificacion, @Date, @Comentario, @Version) ";

            string consulta = "INSERT INTO Encuesta (Servicioid, Calificacion, Date, Comentario, Version) VALUES (@Servicioid, 0, getdate(), @Comentario, 1)";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Servicioid", Evaluacion.ServicioID);
            comandoParaConsulta.Parameters.AddWithValue("@Comentario", Evaluacion.Comentario);
            
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool eliminarEvaluacion(int encuestaId)
        {
            //string consulta = "INSERT INTO Evaluacion (Caminantecorreo, Servicioid, Calificacion, Date, Comentario, Version) " +
            //"VALUES (@Servicioid, @Calificacion, @Date, @Comentario, @Version) ";

            //string consulta = "DELETE p FROM Pregunta AS p JOIN Pregunta_Encuesta AS pe ON p.Id = pe.PreguntaId WHERE pe.EncuestaId = @EncuestaId " +
            string consulta = "DELETE FROM Encuesta WHERE EncuestaId = @EncuestaId";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@EncuestaId", encuestaId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }
}