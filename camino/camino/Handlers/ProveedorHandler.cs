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
    public class ProveedorHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public ProveedorHandler()
        {
            rutaConexion = ConfigurationManager.ConnectionStrings["proyecto"].ToString();
            conexion = new SqlConnection(rutaConexion);
        }

        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }

        public List<ProveedorModel> ObtenerTodosLosProveedores()
        {
            List<ProveedorModel> Proveedor = new List<ProveedorModel>();
            string consulta = "SELECT * FROM Proveedor";
            DataTable TablaResultado = CrearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Proveedor.Add(
                    new ProveedorModel
                    {
                        Cedula = Convert.ToInt32(columna["Cedula"]),
                        Nombre = Convert.ToString(columna["Nombre"]),
                        Telefono = Convert.ToInt32(columna["NumTelefono"]),
                    });
            }
            return Proveedor;
        }

        public bool CrearProveedor(ProveedorModel Proveedor)
        {
            string consulta = "SET IDENTITY_INSERT Proveedor ON "+
                "INSERT INTO Proveedor (Cedula, Nombre, NumTelefono) VALUES (@Cedula, @Nombre, @NumTelefono)";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Cedula", Proveedor.Cedula);
            comandoParaConsulta.Parameters.AddWithValue("@Nombre", Proveedor.Nombre);
            comandoParaConsulta.Parameters.AddWithValue("@NumTelefono", Proveedor.Telefono);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }

        public bool EditarProveedor(ProveedorModel Proveedor,int proveedorId)
        {
            string consulta = $"DELETE FROM Proveedor WHERE Cedula = {proveedorId} " +
                "SET IDENTITY_INSERT Proveedor ON " +
                "INSERT INTO Proveedor (Cedula, Nombre, NumTelefono) VALUES (@Cedula, @Nombre, @NumTelefono)";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Cedula", Proveedor.Cedula);
            comandoParaConsulta.Parameters.AddWithValue("@Nombre", Proveedor.Nombre);
            comandoParaConsulta.Parameters.AddWithValue("@NumTelefono", Proveedor.Telefono);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }

        public bool EliminarProveedor(int proveedorId)
        {
            string consulta = "DELETE FROM Proveedor WHERE Cedula = @Cedula";

            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
            SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);

            comandoParaConsulta.Parameters.AddWithValue("@Cedula", proveedorId);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }
    }
}
