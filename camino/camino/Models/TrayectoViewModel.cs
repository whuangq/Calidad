using camino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class TrayectoViewModel
    {
        public TrayectoModel Trayecto { get; set; }
        public SitioModel SitioInicio { get; set; }
        public SitioModel SitioFin { get; set; }

        public string LatitudInicial { get; set; }
        public string LongitudInicial { get; set; }
        public string AltitudInicial { get; set; }

        public string LatitudFinal { get; set; }
        public string LongitudFinal { get; set; }
        public string AltitudFinal { get; set; }

        public string Distancia { get; set; }
        public string Altimetria { get; set; }


    }
}