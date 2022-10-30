using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace camino.Models
{
    public class PreguntaModel
    {
        public int encuestaId { get; set; }

        public int id { get; set; }

        public string texto { get; set; }

        public decimal calificacion { get; set; }
        
        public string comentario { get; set; }

        public string correo { get; set; }
    }
}