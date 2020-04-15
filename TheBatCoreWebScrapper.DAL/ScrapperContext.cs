using System;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.DAL.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TheBatCoreWebScrapper.DAL
{
    public class ScrapperContext : DbContext
    {
        public ScrapperContext(DbContextOptions<ScrapperContext> options)
            : base(options)
        {
        }

        public DbSet<ScrappingResult> ScrappingResults { get; set; }
        public DbSet<UrlLibrary> UrlLibraries { get; set; }
        public DbSet<ScrappingConfiguration> ScrappingConfigurations { get; set; }
        
    }
}