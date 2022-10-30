using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace camino.Models
{
    public class Caminante
    {
        [Required(ErrorMessage = "Indique el email del caminante")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Indique el nombre del caminante")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Indique el apellido del caminante")]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Indique el genero del caminante")]
        [Display(Name = "Género")]
        public string genero { get; set; }

        [Required(ErrorMessage = "Indique el numero telefonico del caminante")]
        [Display(Name = "Número telefónico")]
        public string numeroTelefonico { get; set; }

        public int edad { get; set; }

        public int CodigoRegistro { get; set; }

        public string password { get; set; }





    }
}