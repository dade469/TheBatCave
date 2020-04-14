using TheBatCoreWebScrapper.Business.Models.Clients;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.Business.Services
{
    public class ScrapperService
    {
        private readonly ScrapperContext _context;
        
        public ScrapperService(ScrapperContext context)
        {
            _context = context;
        }

        public void InitializeScrapper()
        {
            ScrapperFactory fac = new ScrapperFactory(_context);
            fac.StartOperation();
        }


    }
}