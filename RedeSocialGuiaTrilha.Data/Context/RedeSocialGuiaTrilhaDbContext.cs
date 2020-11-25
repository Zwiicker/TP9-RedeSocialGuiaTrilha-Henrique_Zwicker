using RedeSocialGuiaTrilha.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocialGuiaTrilha.Data.Context
{
    public class RedeSocialGuiaTrilhaDbContext : DbContext
    {
        public RedeSocialGuiaTrilhaDbContext(DbContextOptions<RedeSocialGuiaTrilhaDbContext> options)
            : base(options)
        {

        }

        public DbSet<PostModel> Posts { get; set; }
        public DbSet<UserProfileModel> UserProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjetoBlocoHenriqueZwicker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
