using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.Business.Models.Clients;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;
using TheBatCoreWebScrapper.DTO;

namespace TheBatCoreWebScrapper.Business.Services
{
    public class UrlLibraryService
    {
        private readonly ScrapperContext _context;
        private readonly ScrapperFactory _scrapperFactory;

        public UrlLibraryService(ScrapperContext context, ScrapperFactory scrapperFactory)
        {
            _context = context;
            _scrapperFactory = scrapperFactory;
        }

        public void AddUrlToLibrary(UrlLibraryDTO newElement)
        {
            var dbElement = new UrlLibrary() {Url = newElement.Url};
            var dbElementConfiguration = new ScrappingConfiguration()
                {Interval = newElement.ScrappingInterval, UrlLibrary = dbElement, ScrappingResult = new ScrappingResult()};
            _context.Add(dbElementConfiguration);
            _context.SaveChanges();
            
            //start the new url scrapping
            _scrapperFactory.AddAndStartOperation(dbElementConfiguration.ScrappingConfigurationId);

        }

        public void UpdateUrlConfiguration(UrlLibraryDTO editedElement)
        {
            var dbElemenet = _context.ScrappingConfigurations.Include(item => item.UrlLibrary)
                .Single(item => item.UrlLibrary.UrlLibraryId == editedElement.UrlLibraryId);

            dbElemenet.UrlLibrary.Url = editedElement.Url;
            dbElemenet.Interval = editedElement.ScrappingInterval;
            dbElemenet.ScrappingEnabled = editedElement.ScrappingEnabled;

            _context.SaveChanges();

        }


        public List<UrlLibraryDTO> GetAll()
        {
            return _context.ScrappingConfigurations.Include(item=>item.UrlLibrary)
                .Select(item => new UrlLibraryDTO()
                {
                    Url = item.UrlLibrary.Url, 
                    UrlLibraryId = item.UrlLibrary.UrlLibraryId, 
                    ScrappingInterval = item.Interval, 
                    ScrappingEnabled = item.ScrappingEnabled
                }).ToList();
        }
    }
}