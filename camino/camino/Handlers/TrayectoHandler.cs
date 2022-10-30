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
    public class TrayectoHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public TrayectoHandler()
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

        public List<TrayectoModel> obtenerTodosLosTrayectos()
        {
            List<TrayectoModel> Trayectoes = new List<TrayectoModel>();
            //string consulta = "SELECT * FROM Trayecto";
            string consulta = "execute spGetTrayectos";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Trayectoes.Add(
                    new TrayectoModel
                    {   
                        TrayectoID = Convert.ToInt32(columna["TrayectoID"]),
                        Inicio = Convert.ToInt32(columna["Inicio"]),
                        Final = Convert.ToInt32(columna["Final"]),
                        AltimetriaMin = Convert.ToInt32(columna["AltimetriaMin"]),
                        AltimetriaMax = Convert.ToInt32(columna["AltimetriaMax"]),
                        Distancia = Convert.ToInt32(columna["Distancia"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                    });
            }
            return Trayectoes;
        }

        public TrayectoModel obtenerTrayecto(int id)
        {
            TrayectoModel Trayecto = new TrayectoModel();
            //string consulta = "SELECT * FROM Trayecto";
            string consulta = "select * from trayecto where TrayectoID=" + id;

            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Trayecto.TrayectoID = Convert.ToInt32(columna["TrayectoID"]);
                Trayecto.Inicio = Convert.ToInt32(columna["Inicio"]);
                Trayecto.Final = Convert.ToInt32(columna["Final"]);
                Trayecto.AltimetriaMin = Convert.ToInt32(columna["AltimetriaMin"]);
                Trayecto.AltimetriaMax = Convert.ToInt32(columna["AltimetriaMax"]);
                Trayecto.Distancia = Convert.ToInt32(columna["Distancia"]);
                Trayecto.Descripcion = Convert.ToString(columna["Descripcion"]);
            }
            return Trayecto;
        }


        public bool crearTrayecto(TrayectoModel Trayecto)
        {
            //string consulta = "INSERT INTO Trayecto (correo, nombre, apellido, sexo,edad , tel) " +
            //"VALUES (@TrayectoID, @Inicio,@Final,@AltimetriaMin,edad, @Distancia) ";

            string consulta = "INSERT INTO Trayecto (Inicio, Final, AltimetriaMin, AltimetriaMax, Distancia, Descripcion) VALUES (@Inicio,@Final,@AltimetriaMin,@AltimetriaMax, @Distancia, @Descripcion) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            
            comandoParaConsulta.Parameters.AddWithValue("@Inicio", Trayecto.Inicio);
            comandoParaConsulta.Parameters.AddWithValue("@Final", Trayecto.Final);
            comandoParaConsulta.Parameters.AddWithValue("@AltimetriaMin", Trayecto.AltimetriaMin);
            comandoParaConsulta.Parameters.AddWithValue("@AltimetriaMax", Trayecto.AltimetriaMax);
            comandoParaConsulta.Parameters.AddWithValue("@Distancia", Trayecto.Distancia);
            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", Trayecto.Descripcion);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool eliminarTrayecto(int TrayectoId)
        {
            string consulta = "DELETE FROM Trayecto WHERE TrayectoId = @TrayectoId";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@TrayectoId", TrayectoId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }

        public bool editarTrayecto(TrayectoModel Trayecto)
        {
            string consulta = "UPDATE Trayecto SET Inicio=@Inicio, Descripcion=@Descripcion, Final=@Final WHERE TrayectoID=@TrayectoID";


            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", Trayecto.Descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@Inicio", Trayecto.Inicio);
            comandoParaConsulta.Parameters.AddWithValue("@Final", Trayecto.Final);
            comandoParaConsulta.Parameters.AddWithValue("@TrayectoID", Trayecto.TrayectoID);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }


}