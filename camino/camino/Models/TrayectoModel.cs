using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class TrayectoModel
    {
        public int TrayectoID { get; set; }
        public int Inicio { get; set; }
        public int Final { get; set; }
        
        public int AltimetriaMin { get; set; }
        public int AltimetriaMax { get; set; }
        public int Distancia { get; set; }
        public string Descripcion { get; set; }


    }
}