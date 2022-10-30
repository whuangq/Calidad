using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class TrayectoCaminanteModel
    {
        public int TrayectoTrayectoID { get; set; }

        [Required(ErrorMessage = "Indique el email del solicitante")]
        [Display(Name = "Email")]
        public string CaminanteCorreo { get; set; }
    }
}