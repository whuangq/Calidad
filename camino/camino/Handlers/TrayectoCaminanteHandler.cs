using System.Linq;
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
    public class TrayectoCaminanteHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public TrayectoCaminanteHandler()
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

        public List<TrayectoCaminanteModel> BuscarCaminanteEnTrayectos(string correo)
        {
            List<TrayectoCaminanteModel> TrayectoCaminantes = new List<TrayectoCaminanteModel>();
            //string consulta = "SELECT * FROM Trayecto_Caminante WHERE Caminantecorreo =" + correo;
            string consulta = "execute spGetTrayectoCaminanteByEmail '" + correo + "'";
            //string consulta = "select TrayectoTrayectoID from Trayecto_Caminante where Caminantecorreo ='" + correo + "'";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                TrayectoCaminantes.Add(
                    new TrayectoCaminanteModel
                    {
                        TrayectoTrayectoID = Convert.ToInt32(columna["TrayectoTrayectoID"]),

                        //fotos
                    });
            }
            return TrayectoCaminantes;

        }

        public bool Buscar(string correo, int trayectoId)
        {
            bool resultado = false;
            List<TrayectoCaminanteModel> TrayectoCaminantes = new List<TrayectoCaminanteModel>();
            string consulta = "SELECT * FROM Trayecto_Caminante WHERE Caminantecorreo ='" + correo + "' AND TrayectoTrayectoID =" + trayectoId.ToString();
            //string consulta = "execute spGetTrayectoCaminanteByEmail " + correo;
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                TrayectoCaminantes.Add(
                    new TrayectoCaminanteModel
                    {
                        TrayectoTrayectoID = Convert.ToInt32(columna["TrayectoTrayectoID"]),
                        CaminanteCorreo = Convert.ToString(columna["Caminantecorreo"]),

                        //fotos
                    });
            }
            
            if(TrayectoCaminantes.Count > 0)
            {
                resultado = true; 
            }

            return resultado;

        }

        public List<TrayectoCaminanteModel> obtenerTodosLosTrayectoCaminantes()
        {
            List<TrayectoCaminanteModel> TrayectoCaminantes = new List<TrayectoCaminanteModel>();
            //string consulta = "SELECT * FROM Trayecto_Caminante";
            string consulta = "execute spGetTrayctos";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                TrayectoCaminantes.Add(
                    new TrayectoCaminanteModel
                    {
                        TrayectoTrayectoID = Convert.ToInt32(columna["TrayectoTrayectoID"]),
                        CaminanteCorreo = Convert.ToString(columna["Caminantecorreo"]),
                   
                        //fotos
                    });
            }
            return TrayectoCaminantes;
        }

        public bool crearTrayectoCaminante(TrayectoCaminanteModel TrayectoCaminante)
        {

            string consulta = "INSERT INTO Trayecto_Caminante (TrayectoTrayectoID, Caminantecorreo) VALUES (@TrayectoTrayectoID, @Caminantecorreo) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@TrayectoTrayectoID", TrayectoCaminante.TrayectoTrayectoID);
            comandoParaConsulta.Parameters.AddWithValue("@Caminantecorreo", TrayectoCaminante.CaminanteCorreo);
            

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; 
            conexion.Close();
            return exito;
        }

    }

}