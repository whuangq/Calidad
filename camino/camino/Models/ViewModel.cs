using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class ViewModel
    {
        public IEnumerable<Solicitud> solicitudes { get; set; }
        public gmail getGmailModel{get;set;}

    }
}