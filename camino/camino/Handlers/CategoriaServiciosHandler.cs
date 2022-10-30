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
    public class CategoriaServiciosHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public CategoriaServiciosHandler()
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

        public List<CategoriaServiciosModel> obtenerTodosLasCategoriaServicios()
        {
            List<CategoriaServiciosModel> CategoriaServicios = new List<CategoriaServiciosModel>();
            string consulta = "SELECT * FROM Categoria_Servicio";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                CategoriaServicios.Add(
                    new CategoriaServiciosModel
                    {
                        CategoriaId = Convert.ToInt32(columna["CategoriaId"]),
                        Categoria = Convert.ToString(columna["Categoria"]),
                    });
            }
            return CategoriaServicios;
        }

        public List<CategoriaServiciosModel> obtenerCategoriaServiciosDelTrayecto(int trayectoId = 0)
        {
            List<CategoriaServiciosModel> CategoriaServicios = new List<CategoriaServiciosModel>();
            //string consulta = $"SELECT * FROM Servicio join Trayecto_Servicio on Servicio.ServicioId=Trayecto_Servicio.Servicioid where Trayecto_Servicio.TrayectoId = {trayectoId}";
            string consulta = "execute spGetServiciosDelTrayecto " + trayectoId.ToString();
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                CategoriaServicios.Add(
                    new CategoriaServiciosModel
                    {
                        CategoriaId = Convert.ToInt32(columna["CategoriaId"]),
                        Categoria = Convert.ToString(columna["Categoria"]),
                    });
            }
            return CategoriaServicios;
        }

        public bool crearCategoriaServicios(CategoriaServiciosModel CategoriaServicios)
        {
            string consulta = "INSERT INTO Categoria_Servicio VALUES (@Categoria) ";


            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Categoria", CategoriaServicios.Categoria);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool eliminarCategoriaServicios(int CategoriaServiciosId)
        {
            //string consulta = "INSERT INTO Evaluacion (Caminantecorreo, Servicioid, Calificacion, Date, Comentario, Version) " +
            //"VALUES (@Servicioid, @Calificacion, @Date, @Comentario, @Version) ";

            string consulta = "DELETE FROM Categoria_Servicio WHERE CategoriaId = @CategoriaId";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@CategoriaId", CategoriaServiciosId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool editarCategoriaServicios(CategoriaServiciosModel CategoriaServicios)
        {
            string consulta = "UPDATE Categoria_Servicio SET Categoria=@Categoria WHERE CategoriaId=@CategoriaId";


            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Categoria", CategoriaServicios.Categoria);
            comandoParaConsulta.Parameters.AddWithValue("@CategoriaId", CategoriaServicios.CategoriaId);
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }
}