using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace camino.Models
{
    public class Login
    {
        public Credential Credential { get; set; }
        public ChangePassword ChangePassword { get; set; }
        public string pattern = "[a-z0-9A-Z._%+-]+@(\"@\")[a-z0-9A-Z.-]+\\.[a-zA-Z]{2,4}$"; //regex

    }
    
    public class Credential
    {
        [Required(ErrorMessage = "Ingrese su Correo electrónico")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public int Role { get; set; }

        public string Usuario { get; set; }

    }

    public class ChangePassword
    {
        [Required(ErrorMessage = "Vuelva a ingresar su nueva Contraseña")]
        [Display(Name = "Confirmación de Contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


    }
}