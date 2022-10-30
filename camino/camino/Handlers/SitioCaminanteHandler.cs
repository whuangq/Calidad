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
    public class SitioCaminanteHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public SitioCaminanteHandler()
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

        public List<SitioCaminanteModel> obtenerTodosLosSitioCaminantes()
        {
            List<SitioCaminanteModel> SitioCaminantees = new List<SitioCaminanteModel>();
            //string consulta = "SELECT * FROM SitioCaminante";
            string consulta = "execute spGetSitioCaminantes";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                SitioCaminantees.Add(
                    new SitioCaminanteModel
                    {                 
                        SitioSitioID = Convert.ToInt32(columna["SitioSitioID"]),
                        CaminanteCorreo = Convert.ToString(columna["Caminantecorreo"]),
                    });
            }
            return SitioCaminantees;
        }

        public bool crearSitioCaminante(SitioCaminanteModel SitioCaminante)
        {
            //string consulta = "INSERT INTO SitioCaminante (correo, nombre, apellido, sexo,edad , tel) " +
            //"VALUES (@SitioCaminanteID, @Inicio,@Final,@AltimetriaMin,edad, @Distancia) ";

            string consulta = "INSERT INTO SitioCaminante (Caminantecorreo, SitioSitioID) VALUES (@CaminanteCorreo,@SitioSitioID) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@CaminanteCorreo", SitioCaminante.CaminanteCorreo);
            comandoParaConsulta.Parameters.AddWithValue("@SitioSitioID", SitioCaminante.SitioSitioID);
       
            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }

}