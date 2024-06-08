using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Charger
    {
        public string id {  get; set; }
        public decimal amount { get; set; }
        public string status {  get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Modelo.Company Company { get; set; }
        public List<Modelo.Charger> Chargers { get; set; }
    }
}
