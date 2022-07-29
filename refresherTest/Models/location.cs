using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace refresherTest.Models
{
    public class location
    {
        public int ID { get; set; }
        public string? address { get; set; }
        public string? postalCode { get; set; }
        public string city { get; set; }
        public string? state { get; set; }
        public string countryID { get; set; }
    }
}
