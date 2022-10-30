using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace camino.Models
{
    public class Solicitud
    {
        [Required(ErrorMessage = "Indique el email del solicitante")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Indique el nombre del solicitante")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Indique el apellido del solicitante")]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Indique el genero del solicitante")]
        [Display(Name = "Género")]
        public string genero { get; set; }

        [Required(ErrorMessage = "Indique el numero telefonico del solicitante")]
        [Display(Name = "Número telefónico")]
        public string numeroTelefonico { get; set; }

        public int edad { get; set; }





    }
}