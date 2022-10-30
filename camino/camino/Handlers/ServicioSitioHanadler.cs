using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using camino.Models;
using System.Collections.Generic;
using System;

namespace camino.Handlers
{
    public class ServicioSitioHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public ServicioSitioHandler()
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

        public List<ServicioSitioModel> obtenerTodosLosServicioSitios()
        {
            List<ServicioSitioModel> ServicioSitios = new List<ServicioSitioModel>();
            //string consulta = "SELECT * FROM ServicioSitio";
            string consulta = "execute spGetServicioSitio";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                ServicioSitios.Add(
                    new ServicioSitioModel
                    {                 
                        ServicioServicioID = Convert.ToInt32(columna["ServicioServicioID"]),
                        SitioSitioID = Convert.ToInt32(columna["SitioSitioID"]),
                    });
            }
            return ServicioSitios;
        }

        public bool crearServicioSitio(ServicioSitioModel ServicioSitio)
        {
            //string consulta = "INSERT INTO ServicioSitioModel (ServicioServicioID , SitioSitioID) " +
            //"VALUES (@ServicioServicioID, @SitioSitioID) ";

            string consulta = "INSERT INTO SitioCaminante (Caminantecorreo, SitioSitioID) VALUES (@CaminanteCorreo,@SitioSitioID) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@CaminanteCorreo", ServicioSitio.ServicioServicioID);
            comandoParaConsulta.Parameters.AddWithValue("@SitioSitioID", ServicioSitio.SitioSitioID);
       
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }

}