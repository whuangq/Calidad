using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class ListaPreguntasModel
    {
        public List<PreguntaModel> Preguntas { get; set; }

        public ListaPreguntasModel(List<PreguntaModel> list)
        {
            Preguntas = list;
        }
        public ListaPreguntasModel()
        {
        }
    }
}