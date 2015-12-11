using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mantenimiento_001.Models
{
    public class datos
    {
        public int espera { get; set; }
        public int proceso { get; set; }
        public int reparadas { get; set; }
        public int detenidas { get; set; }
        public string tiempoEspera { get; set; }
        public string tiempointervencion { get; set; }
        public string tiempoTotal { get; set; }
        public List<tipeData> porcentaje { get; set; }
    }


}