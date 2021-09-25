using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinalCachorroVacinas.Models
{
    public class PostalAddress
    {
        public int Id { get; set; }
        public string AddressCountry { get; set; }
        public string AddressLocality { get; set; }
        public string AddressRegion { get; set; }
        public string PostalCode { get; set; }
    }
}
