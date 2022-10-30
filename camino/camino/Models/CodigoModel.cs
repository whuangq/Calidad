using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace camino.Models
{
    public class CodigoModel
    {
        //int codigo;

        public int Codigo { get; set; }
        public CodigoModel()
        {
            Random Random = new Random();
            this.Codigo = (Random.Next(100000, 999999));

        }
     


  
    }

}   