using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace camino.Models
{
    public class SitiosInteresModel
    {
        public int id { get; set; }

        
        [Display(Name = "Ingrese el nombre de la provincia")]
        public string provincia { get; set; }

        
        [Display(Name = "Ingrese el nombre del canton")]
        public string canton { get; set; }

        
        [Display(Name = "Ingrese el nombre del sitio")]
        public string sitioNombre { get; set; }

        
        [Display(Name = "Ingrese la descripcion")]
        public string  descripcion { get; set; }

        public string tipoArchivo { get; set; }

       
        [Display(Name = "Ingrese una imagen")]
        public HttpPostedFileBase FotoUno { get; set; }




        public HttpPostedFileBase archivoFotoDos { get; set; }

        public HttpPostedFileBase archivoFotoTres { get; set; }

        public int Latitud { get; set; }

        public int Longitud { get; set; }

        public int Distrito { get; set; }

    }

}   