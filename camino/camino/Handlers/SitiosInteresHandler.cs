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
    public class SitiosInteresHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public SitiosInteresHandler()
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

        public List<SitiosInteresModel> obtenerSitiosInteres()
        {
            List<SitiosInteresModel> sitios = new List<SitiosInteresModel>();
            //string consulta = "select * from Sitio";
            string consulta = "execute spGetSitios";

            DataTable tablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in tablaResultado.Rows)
            {
                sitios.Add(
                    new SitiosInteresModel
                    {
                        id = Convert.ToInt32(columna["SitioID"]),
                        sitioNombre = Convert.ToString(columna["SitioNombre"]),
                        provincia = Convert.ToString(columna["Provincia"]),
                        canton = Convert.ToString(columna["Canton"]),
                        descripcion = Convert.ToString(columna["Descripcion"])
                    });
            }
            return sitios;
        }

        private byte[] obtenerBytes(HttpPostedFileBase archivo)
        {
            byte[] bytes;
            BinaryReader lector = new BinaryReader(archivo.InputStream); //
            bytes = lector.ReadBytes(archivo.ContentLength);
            return bytes;
        }

        public bool crearSitio(SitiosInteresModel sitio)
        {
            string consulta = "INSERT INTO Sitio (Provincia, Canton, SitioNombre, Descripcion, FotoUno, tipoArchivo) " +
            "VALUES (@Provincia, @Canton, @SitioNombre, @Descripcion, @FotoUno, @tipoArchivo) ";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Provincia", sitio.provincia);
            comandoParaConsulta.Parameters.AddWithValue("@Canton", sitio.canton);
            comandoParaConsulta.Parameters.AddWithValue("@SitioNombre", sitio.sitioNombre);
            comandoParaConsulta.Parameters.AddWithValue("@Descripcion", sitio.descripcion);
            comandoParaConsulta.Parameters.AddWithValue("@FotoUno", obtenerBytes(sitio.FotoUno));
            comandoParaConsulta.Parameters.AddWithValue("@tipoArchivo", sitio.FotoUno.ContentType);


            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
            conexion.Close();

            return exito;
        }

        public Tuple<byte[], string> descargarContenido(int id)
        {
            byte[] bytes;
            string contentType;
            string consulta = "select FotoUno, tipoArchivo from [dbo].[Sitio] where SitioID = @SitioID";
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            comandoParaConsulta.Parameters.AddWithValue("@SitioID", id);
            conexion.Open();
            SqlDataReader lectorDeDatos = comandoParaConsulta.ExecuteReader();
            lectorDeDatos.Read();
            bytes = (byte[])lectorDeDatos["FotoUno"];
            contentType = lectorDeDatos["tipoArchivo"].ToString();
            conexion.Close();
            return new Tuple<byte[], string>(bytes, contentType);
        }

    }
}