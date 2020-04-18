using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TheBatCoreWebScrapper.DAL
{
    public class ScrapperContextFactory: IDesignTimeDbContextFactory<ScrapperContext>
    {
        public ScrapperContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScrapperContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1401;Database=ScrapperDB;User Id=sa;Password=Isis2018;");

            return new ScrapperContext(optionsBuilder.Options);
        }
    }
}