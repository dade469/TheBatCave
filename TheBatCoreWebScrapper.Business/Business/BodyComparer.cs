using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;
using TheBatCoreWebScrapper.Notifier;

namespace TheBatCoreWebScrapper.Business.Business
{
    public class BodyComparer
    {
        public BodyComparer()
        {
            
        }

        public BodyComparer( ScrappingConfiguration configuration, ScrapperContext context)
        {
            Context = context;
            Configuration = configuration;
            _messageSender = new MessageSender();
        }

        public ScrapperContext Context { get; set; }
        public ScrappingConfiguration Configuration { get; set; }
        public bool EnableCompare { get; set; }
        
        private MessageSender _messageSender { get; set; }

        public void Stop()
        {
            EnableCompare = false;
        }

        public void Start()
        {
            EnableCompare = true;
            Compare();
        }
        
        public async void Compare()
        {

                var scrappingResult = Context.ScrappingResults.Include(item=>item.Configuration.UrlLibrary).Single(item =>
                    item.ScrappingConfigurationId == Configuration.ScrappingResult.ScrappingResultId);

                if (scrappingResult.HasChanged)
                {
                    //send notification
                    _messageSender.SendMessage($"Change found in page {scrappingResult.Configuration.UrlLibrary.Url}");
                    //reset page status
                    scrappingResult.HasChanged = false;
                    scrappingResult.BodyUnchanged = scrappingResult.BodyResult;
                    Context.SaveChanges();
                } 
                
        }
        
    }
}