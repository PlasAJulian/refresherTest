using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using refresherTest.Models;

namespace refresherTest.Data
{
    public class refresherTestContext : DbContext
    {
        internal object jobList;

        public refresherTestContext (DbContextOptions<refresherTestContext> options)
            : base(options)
        {
        }

        public DbSet<refresherTest.Models.job> job { get; set; }

        public DbSet<refresherTest.Models.country> country { get; set; }

        public DbSet<refresherTest.Models.department> department { get; set; }

        public DbSet<refresherTest.Models.dependent> dependent { get; set; }

        public DbSet<refresherTest.Models.employee> employee { get; set; }

        public DbSet<refresherTest.Models.location> location { get; set; }

        public DbSet<refresherTest.Models.region> region { get; set; }
    }
}
