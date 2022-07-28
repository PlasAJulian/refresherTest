using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace refresherTest.Models
{
    public class job
    {
        public int Id { get; set; }
        public string jobTitle { get; set; }
        public decimal? minSalary { get; set; }
        public decimal? maxSalary { get; set; }

    }
}
