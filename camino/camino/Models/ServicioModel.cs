using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class ServicioModel
    {
        public int ServicioId { get; set; }
        public int TrayectoId { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        //fotos
    }
}