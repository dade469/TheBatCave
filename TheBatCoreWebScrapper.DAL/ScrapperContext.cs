using System;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.DAL.Models;
using System.Collections.Generic;

namespace TheBatCoreWebScrapper.DAL
{
    public class ScrapperContext : DbContext
    {
        public DbSet<ScrappingResult> ScrappingResults { get; set; }
        public DbSet<UrlLibrary> UrlLibraries { get; set; }
        public DbSet<ScrappingConfiguration> ScrappingConfigurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1401;Database=ScrapperDB;User Id=sa;Password=Isis2018;");
        }
    }
}