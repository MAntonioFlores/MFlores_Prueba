using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Company
    {
        public string company_id { get; set; }
        public string company_name { get; set; }
        public List<Modelo.Company> Companys { get; set; }
    }
}
