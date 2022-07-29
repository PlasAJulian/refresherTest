using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace refresherTest.Models
{
    public class employee
    {
        public int ID { get; set; } = 0;
        public string? firstName { get; set; } = "NULL";
        public string lastName { get; set; } = "NULL";
        public string email { get; set; } = "NULL";
        public string? phoneNum { get; set; } = "NULL";
        public DateTime hireDate { get; set; }
        public int jobID { get; set; } = 0;
        public decimal income { get; set; } = 0;
        public string? managereID { get; set; }
        public string? departmentID { get; set; }
    }
}
