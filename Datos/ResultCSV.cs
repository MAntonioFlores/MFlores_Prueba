using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ResultCSV
    {
        public int IdRegistro { get; set; }
        public string Mensaje { get; set; }
        public List<ResultCSV> Errores { get; set; }
    }
}
