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
    public class CaminanteHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;

        public CaminanteHandler()
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

        public List<Caminante> obtenerTodoslasCaminantes()
        {
            List<Caminante> Caminantes = new List<Caminante>();
            //string consulta = "SELECT * FROM Caminante";
            string consulta = "execute spGetTodosCaminantes";
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Caminantes.Add(
                    new Caminante
                    {
                        nombre = Convert.ToString(columna["Nombre"]),
                        apellido = Convert.ToString(columna["Apellido"]),
                        edad = Convert.ToInt32(columna["edad"]),
                        numeroTelefonico = Convert.ToString(columna["tel"]),
                        email = Convert.ToString(columna["Correo"]),
                        genero = Convert.ToString(columna["Sexo"]),
                    });
            }
            return Caminantes;
        }

        public Caminante BuscarCaminante(string correo)
        {
            List<Caminante> Caminantes = new List<Caminante>();
            //string consulta = "SELECT * FROM Caminante WHERE email = " + correo;
            string consulta = "execute spBuscarCaminanteByEmail " + correo;
            DataTable TablaResultado = crearTablaConsulta(consulta);

            foreach (DataRow columna in TablaResultado.Rows)
            {
                Caminantes.Add(
                    new Caminante
                    {
                        nombre = Convert.ToString(columna["Nombre"]),
                        apellido = Convert.ToString(columna["Apellido"]),
                        edad = Convert.ToInt32(columna["edad"]),
                        numeroTelefonico = Convert.ToString(columna["tel"]),
                        email = Convert.ToString(columna["Correo"]),
                        genero = Convert.ToString(columna["Sexo"]),
                    });
            }
            return Caminantes[0];

        }

        public bool crearCaminante(Caminante Caminante)
        {
            //string consulta = "INSERT INTO Caminante (correo, nombre, apellido, sexo,edad , tel) " +
            //"VALUES (@correo, @nombre,@apellido,@sexo,edad, @tel) ";

            string consulta = "INSERT INTO Caminante VALUES (@correo, @nombre,@apellido,@contraseña,@sexo,@edad, @CodigoRegistro,@tel) ";
            // buscar en la lista de obtener todos los caminantes para ver si el correo esta
            List<Caminante> caminantes = new List<Caminante>();
            caminantes = this.obtenerTodoslasCaminantes();
            bool existe = false;
            foreach(var caminante in caminantes)
            {
                if (caminante.email.Equals(Caminante.email))
                {
                    existe = true;
                    break;
                }
            }
            if (existe)
            {
                Console.Write("El caminante ya existe en la tabla");
                return false;
            }
            else
            {
                SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion);
                SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta);
                comandoParaConsulta.Parameters.AddWithValue("@Correo", Caminante.email);
                comandoParaConsulta.Parameters.AddWithValue("@Nombre", Caminante.nombre);
                comandoParaConsulta.Parameters.AddWithValue("@Apellido", Caminante.apellido);
                comandoParaConsulta.Parameters.AddWithValue("@Contraseña", Caminante.password);
                comandoParaConsulta.Parameters.AddWithValue("@Sexo", Caminante.genero);
                comandoParaConsulta.Parameters.AddWithValue("@Edad", Caminante.edad);
                comandoParaConsulta.Parameters.AddWithValue("@CodigoRegistro", Caminante.CodigoRegistro);
                comandoParaConsulta.Parameters.AddWithValue("@Tel", Caminante.numeroTelefonico);

                conexion.Open();
                bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1; // indica que se agregO una tupla (cuando es mayor o igual que 1)
                conexion.Close();
                return exito;
            }
        }
    }

}