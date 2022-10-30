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
    public class ListaPreguntasHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public ListaPreguntasHandler()
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
            string consulta = "SELECT * FROM Pregunta";
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
            string consulta = $"SELECT * FROM Pregunta join Pregunta_Encuesta on Pregunta.Id=Pregunta_Encuesta.PreguntaId where Pregunta_Encuesta.EncuestaId = {encuestaId}";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Preguntas.Add(
                    new PreguntaModel
                    {
                        id = Convert.ToInt32(columna["Id"]),
                        texto = Convert.ToString(columna["Texto"]),
                    });
            }
            return Preguntas;
        }

        public bool crearPregunta(PreguntaModel Preguntas)
        {
            //string consulta = "INSERT INTO Pregunta (Texto, Calificacion, Comentario) " +
            //"VALUES (@Texto, @Calificacion, @Comentario) ";

            string consulta = "INSERT INTO Pregunta (Texto, Calificacion, Comentario) VALUES (@Texto, @Calificacion, @Comentario) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Texto", Preguntas.texto);
            comandoParaConsulta.Parameters.AddWithValue("@Calificacion", Preguntas.calificacion);
            comandoParaConsulta.Parameters.AddWithValue("@Comentario", Preguntas.comentario);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
        public bool crearRespuesta(ListaPreguntasModel listaPreguntas)
        {
            //string consulta = $"UPDATE Pregunta SET calificacion=@Calificacion,comentario=@comentario WHERE id = {listaPreguntas.Preguntas[i].id}";

            SqlCommand comandoParaConsulta = new SqlCommand();

            bool exito = false;
            for (int i = 0; i < listaPreguntas.Preguntas.Count(); i++)
            {
                string consulta = "INSERT INTO Respuesta (Calificacion, Comentario, Caminantecorreo, PreguntaId) VALUES (@Calificacion,@Comentario,@Caminantecorreo,@PreguntaId)";
                
                comandoParaConsulta = new SqlCommand(consulta, conexion);
                SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

                comandoParaConsulta.Parameters.AddWithValue("@Calificacion", listaPreguntas.Preguntas[i].calificacion);
                comandoParaConsulta.Parameters.AddWithValue("@Comentario", listaPreguntas.Preguntas[i].comentario);
                comandoParaConsulta.Parameters.AddWithValue("@PreguntaID", listaPreguntas.Preguntas[i].id);
                comandoParaConsulta.Parameters.AddWithValue("@Caminantecorreo", listaPreguntas.Preguntas[i].correo);

                conexion.Open();
                exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
                conexion.Close();
            }

            
            return exito;
        }
    }
}