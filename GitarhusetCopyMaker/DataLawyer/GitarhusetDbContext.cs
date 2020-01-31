using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitarhusetCopyMaker.DataLawyer
{
    public class GitarhusetDbContext : DbContext
    {
        private const string connectionString = @"Server=.\;Database=Gitarhuset;Trusted_Connection=True;"; 
        public GitarhusetDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
