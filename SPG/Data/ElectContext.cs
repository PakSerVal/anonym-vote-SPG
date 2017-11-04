using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPG.Models.Db;

namespace SPG.Data
{
    public class ElectContext : DbContext
    {
        public ElectContext(DbContextOptions<ElectContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
