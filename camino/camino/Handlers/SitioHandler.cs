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
    public class SitioHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public SitioHandler()
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

       
        public int buscarPorLatLng(decimal lat, decimal lng)
        {
            List<SitioModel> Sitios = new List<SitioModel>();
            string consulta = "SELECT * FROM Sitio";
            DataTable TablaResultado = crearTablaConsulta(consulta);
            int llave = -1;
            foreach (DataRow columna in TablaResultado.Rows)
            {
                Sitios.Add(
                    new SitioModel
                    {
                        SitioID = Convert.ToInt32(columna["SitioID"]),
                        Latitud = Convert.ToDecimal(columna["Latitud"]),
                        Longitud = Convert.ToDecimal(columna["Longitud"]),
                        Provincia = Convert.ToString(columna["Provincia"]),
                        Canton = Convert.ToString(columna["Canton"]),
                        Distrito = Convert.ToString(columna["Distrito"]),
                        SitioNombre = Convert.ToString(columna["SitioNombre"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                        //FotoUno = Convert.ToByte[
                        Direccion = Convert.ToString(columna["direccion"]),
                        //fotos
                    });
            }

            for (int i = 0; i < Sitios.Count; i++)
            {
                if(Sitios[i].Latitud.CompareTo(lat) == 0 && Sitios[i].Longitud.CompareTo(lng) ==0 )
                {
                    llave = Sitios[i].SitioID;
                }
            }
            return llave;
        }
        public List<SitioModel> obtenerTodosLosSitios()
        {
            List<SitioModel> Sitios = new List<SitioModel>();
            string consulta = "SELECT * FROM Sitio";
           
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Sitios.Add(
                    new SitioModel
                    {
                        Latitud = Convert.ToDecimal(columna["Latitud"]),
                        Longitud = Convert.ToDecimal(columna["Longitud"]),
                        Provincia = Convert.ToString(columna["Provincia"]),
                        Canton = Convert.ToString(columna["Canton"]),
                        Distrito = Convert.ToString(columna["Distrito"]),
                        SitioNombre = Convert.ToString(columna["SitioNombre"]),
                        Descripcion = Convert.ToString(columna["Descripcion"]),
                        //FotoUno = Convert.ToByte[
                        Direccion = Convert.ToString(columna["direccion"]),
                        //fotos
                    });
            }
            return Sitios;
        }

        public SitioModel BuscarSitio(int id)
        {
            SitioModel Sitio = new SitioModel();
            string consulta = "SELECT * FROM Sitio where sitioid =" + id.ToString();

            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Sitio.Latitud = Convert.ToDecimal(columna["Latitud"]);
                Sitio.Longitud = Convert.ToDecimal(columna["Longitud"]);
                Sitio.Provincia = Convert.ToString(columna["Provincia"]);
                Sitio.Canton = Convert.ToString(columna["Canton"]);
                Sitio.Distrito = Convert.ToString(columna["Distrito"]);
                Sitio.SitioNombre = Convert.ToString(columna["SitioNombre"]);
                Sitio.Descripcion = Convert.ToString(columna["Descripcion"]);
                Sitio.Direccion = Convert.ToString(columna["direccion"]);
            }
            return Sitio;
        }

        public bool crearSitio(SitioModel Sitio)
        {
            //string consulta = "INSERT INTO Sitio (correo, nombre, apellido, SitioNombre,edad , tel) " +
            //"VALUES (@SitioID, @Latitud,@Longitud,@Provincia,edad, @Distrito) ";

            string consulta = "INSERT INTO Sitio (Latitud, Longitud, Provincia, Canton, Distrito, SitioNombre, Descripcion) VALUES (@Latitud,@Longitud,@Provincia,@Canton, @Distrito,@SitioNombre, @Descripcion) ";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Latitud", Sitio.Latitud);
            comandoParaConsulta.Parameters.AddWithValue("@Longitud", Sitio.Longitud);
            comandoParaConsulta.Parameters.AddWithValue("@Provincia", Sitio.Provincia);
            comandoParaConsulta.Parameters.AddWithValue("@Canton", Sitio.Canton);
            comandoParaConsulta.Parameters.AddWithValue("@Distrito", Sitio.Distrito);
            comandoParaConsulta.Parameters.AddWithValue("@SitioNombre", Sitio.SitioNombre);
            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", Sitio.Descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@Direccion", Sitio.Direccion);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();
            return exito;
        }
    }

}