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
    public class PreguntaHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public PreguntaHandler()
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

        public List<PreguntaModel> obtenerTodasLasPreguntas()
        {
            List<PreguntaModel> Preguntas = new List<PreguntaModel>();
            //string consulta = "SELECT * FROM Pregunta";
            string consulta = "execute spGetPreguntas";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Preguntas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["Id"]),
                        texto = Convert.ToString(columna["Texto"]),
                        calificacion = Convert.ToDecimal(columna["Calificacion"]),
                        comentario = Convert.ToString(columna["Comentario"]),
                    });
            }
            return Preguntas;
        }

        public List<PreguntaModel> obtenerPreguntasdeEncuesta(int encuestaId)
        {
            List<PreguntaModel> Preguntas = new List<PreguntaModel>();
            //string consulta = $"SELECT * FROM Pregunta join Pregunta_Encuesta on Pregunta.Id=Pregunta_Encuesta.PreguntaId where Pregunta_Encuesta.EncuestaId = {encuestaId}";
            string consulta = "execute getPreguntasDeEncuestaById "+encuestaId.ToString();

            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Preguntas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["Id"]),
                        texto = Convert.ToString(columna["Texto"]),
                        calificacion = Convert.ToDecimal(columna["Calificacion"]),
                        comentario = Convert.ToString(columna["Comentario"]),
                    });
            }
            return Preguntas;
        }

        public bool crearPregunta(PreguntaModel Pregunta)
        {
            //string consulta = "INSERT INTO Pregunta (Texto, Calificacion, Comentario) " +
            //"VALUES (@Texto, @Calificacion, @Comentario) ";

            string consulta = "INSERT INTO Pregunta (Texto) VALUES (@Texto) " +
            "INSERT INTO Pregunta_Encuesta (PreguntaId, EncuestaId) VALUES (SCOPE_IDENTITY(), @EncuestaId) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@Texto", Pregunta.texto);
            comandoParaConsulta.Parameters.AddWithValue("@EncuestaId", Pregunta.encuestaId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public List<PreguntaModel> obtenerTodasLasPreguntasDeEncuesta(int encuestaId)
        {
            List<PreguntaModel> Preguntas = new List<PreguntaModel>();
            string consulta = $"SELECT Pregunta.Id, Pregunta.texto FROM Pregunta_Encuesta JOIN Pregunta ON Pregunta_Encuesta.PreguntaId = Pregunta.Id WHERE Pregunta_Encuesta.EncuestaId = {encuestaId} GROUP BY Pregunta.Id, Pregunta.texto ";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Preguntas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["Id"]),
                        texto = Convert.ToString(columna["texto"]),
                    });
            }
            return Preguntas;
        }

        public List<PreguntaModel> obtenerTodasLasPreguntasRespondidas(int encuestaId)
        {
            List<PreguntaModel> Preguntas = new List<PreguntaModel>();
            string consulta = $"SELECT Pregunta.Id, Pregunta.texto, avg(calificacion) AS Promedio FROM Pregunta_Encuesta JOIN Respuesta ON Pregunta_Encuesta.PreguntaId = Respuesta.PreguntaId JOIN Pregunta ON Pregunta_Encuesta.PreguntaId = Pregunta.Id WHERE Pregunta_Encuesta.EncuestaId = {encuestaId} GROUP BY Pregunta.Id, Pregunta.texto ";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Preguntas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["Id"]),
                        texto = Convert.ToString(columna["texto"]),
                        calificacion = Convert.ToDecimal(columna["Promedio"]),
                    });
            }
            return Preguntas;
        }

        public bool editarPregunta(PreguntaModel Pregunta)
        {
            //string consulta = "INSERT INTO Pregunta (Texto, Calificacion, Comentario) " +
            //"VALUES (@Texto, @Calificacion, @Comentario) ";

            string consulta = $"UPDATE Pregunta SET Texto = @Texto WHERE Id = {Pregunta.id} " +
            $"DELETE FROM Respuesta WHERE PreguntaID = {Pregunta.id} " +
            $"UPDATE Encuesta SET Version = Version + 1 WHERE EncuestaId = {Pregunta.encuestaId} ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@Texto", Pregunta.texto);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public List<PreguntaModel> obtenerRespuestasdePregunta(int preguntaId)
        {
            List<PreguntaModel> Respuestas = new List<PreguntaModel>();
            string consulta = $"SELECT * FROM Respuesta WHERE Respuesta.PreguntaId = {preguntaId}";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Respuestas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["RespuestaID"]),
                        calificacion = Convert.ToDecimal(columna["Calificacion"]),
                        comentario = Convert.ToString(columna["Comentario"]),
                    });
            }
            return Respuestas;
        }
    }
}