using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPG.Models.Entities;

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
        public DbSet<ElectionVoter> ElectionVoters { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectionVoter>()
                .HasKey(t => new { t.ElectionId, t.VoterId });
            modelBuilder.Entity<ElectionVoter>()
                .HasOne(t => t.Election)
                .WithMany(e => e.ElectionVoters)
                .HasForeignKey(t => t.ElectionId);
            modelBuilder.Entity<ElectionVoter>()
                .HasOne(t => t.Voter)
                .WithMany(e => e.ElectionVoters)
                .HasForeignKey(t => t.VoterId);
        }
    }
}
