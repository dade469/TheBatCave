using TheBatCoreWebScrapper.Business.Models.Clients;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.Business.Services
{
    public class ScrappingManagerService
    {
        private readonly ScrapperContext _context;
        private readonly ScrapperFactory _scrapperFactory;
        public ScrappingManagerService(ScrapperContext context, ScrapperFactory scrapperFactory)
        {
            _context = context;
            _scrapperFactory = scrapperFactory;
        }

        public void StartScrapping()
        {
            _scrapperFactory.StartOperation();
        }

        public void StopScrapping()
        {
            _scrapperFactory.StopOperation();
        }

    }
}