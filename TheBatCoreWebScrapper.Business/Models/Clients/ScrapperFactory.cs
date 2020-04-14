using System.Collections.Generic;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TheBatCoreWebScrapper.Business.Business;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;

namespace TheBatCoreWebScrapper.Business.Models.Clients
{
    public class ScrapperFactory
    {
        public ScrapperFactory()
        {
            
        }

        public ScrapperFactory(ScrapperContext context)
        {
            _context = context;
            
        }
        
        private ScrapperContext _context { get; set; }

        public void StartOperation()
        {
            var toDoList = _context.ScrappingConfigurations.Include(item => item.ScrappingResult)
                .Include(item => item.UrlLibrary);
            var scrappingTask = new List<BaseScrapperClient>();
            foreach (var scrappingConfiguration in toDoList)
            {
                BodyComparer comparer = new BodyComparer(scrappingConfiguration, _context);
                var scrapperClient = new BaseScrapperClient(scrappingConfiguration, _context,comparer);
                scrappingTask.Add(scrapperClient);
            }
            scrappingTask.ForEach(item=>item.StartScrapping());
        }
    }
}