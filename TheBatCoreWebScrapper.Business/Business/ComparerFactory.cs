using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.Business.Models.Clients;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.Business.Business
{
    public class ComparerFactory
    {

        public ComparerFactory(ScrapperContext context)
        {
            _context = context;
        }

        private ScrapperContext _context { get; set; }
        
        public void StartOperation()
        {
            var toDoList = _context.ScrappingConfigurations.Include(item => item.ScrappingResult)
                .Include(item => item.UrlLibrary);
            var comparerTask = new List<BodyComparer>();
            foreach (var scrappingConfiguration in toDoList)
            {
                BodyComparer tmp = new BodyComparer(scrappingConfiguration, _context);
                comparerTask.Add(tmp);
            }
            comparerTask.ForEach(item=>item.Start());
        }
    }
}