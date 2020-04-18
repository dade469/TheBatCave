using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;
using TheBatCoreWebScrapper.Notifier;

namespace TheBatCoreWebScrapper.Business.Business
{
    public class BodyComparer
    {
        public BodyComparer(ScrapperContext context)
        {
            Context = context;
            // Configuration = configuration;
            _messageSender = new MessageSender();
        }

        public ScrapperContext Context { get; set; }
        private MessageSender _messageSender { get; set; }

        // public async Task Compare()
        // {
        //
        //         var scrappingResult = Context.ScrappingResults.Include(item=>item.Configuration.UrlLibrary).Single(item =>
        //             item.ScrappingConfigurationId == Configuration.ScrappingResult.ScrappingResultId);
        //
        //         if (scrappingResult.HasChanged)
        //         {
        //             //send notification
        //             _messageSender.SendMessage($"Change found in page {scrappingResult.Configuration.UrlLibrary.Url}");
        //             //reset page status
        //             scrappingResult.HasChanged = false;
        //             scrappingResult.BodyUnchanged = scrappingResult.BodyResult;
        //             Context.SaveChanges();
        //         } 
        //         
        // }
        
        public async Task Compare(int resultId)
        {

            var scrappingResult = Context.ScrappingResults.Include(item=>item.Configuration.UrlLibrary).Single(item =>
                item.ScrappingConfigurationId == resultId);

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