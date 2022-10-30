using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class EvaluacionesModel
    {
        public int TrayectoID { get; set; }
        public string Descripcion { get; set; }

        public int EncuestaID { get; set; }
        public int ServicioID { get; set; }
        public decimal Calificacion { get; set; }
        public string Date { get; set; }
        public string Comentario { get; set; }
        public int Version { get; set; }
    }
}