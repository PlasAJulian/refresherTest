﻿using System;
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
    }
}
