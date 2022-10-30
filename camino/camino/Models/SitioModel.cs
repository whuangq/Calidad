using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class SitioModel
    {
        public int SitioID { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string SitioNombre { get; set; }
        public string Descripcion { get; set; }
        public Byte[] FotoUno { get; set; }
        public Byte[] FotoDos { get; set; }
        public Byte[] FotoTres { get; set; }
        public string Direccion { get; set; }

        
        
    }
}