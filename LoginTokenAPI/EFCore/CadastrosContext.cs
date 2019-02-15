using JetBrains.Annotations;
using LoginTokenAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginTokenAPI.EFCore
{
    public class CadastrosContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public CadastrosContext(DbContextOptions<CadastrosContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            // Configure entities ...
        }

    }
}
