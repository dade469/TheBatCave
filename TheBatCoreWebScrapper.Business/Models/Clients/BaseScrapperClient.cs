using System;
using System.Linq;
using System.Net.Http;
using System.Timers;
using Telegram.Bot.Types.InlineQueryResults;
using TheBatCoreWebScrapper.Business.Business;
using TheBatCoreWebScrapper.Business.Models.Results;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;

namespace TheBatCoreWebScrapper.Business.Models.Clients
{
    public class BaseScrapperClient
    {
        public BaseScrapperClient()
        {
        }

        public BaseScrapperClient(ScrappingConfiguration configuration, ScrapperContext context, BodyComparer
            comparer)
        {
            Comparer = comparer;
            Configuration = configuration;
            Url = configuration.UrlLibrary.Url;
            if (configuration.ScrappingResult != null)
                ResultId = configuration.ScrappingResult.ScrappingResultId;
            
            Context = context;

            Clock = new Timer();
            Clock.Interval = configuration.Interval;
            Clock.Elapsed += ClockOnElapsed;
        }

        public void StartScrapping()
        {
            if (!ResultId.HasValue)
            {
                Context.Add(new ScrappingResult {ScrappingConfigurationId = Configuration.ScrappingConfigurationId});
                Context.SaveChanges();
                ResultId = Context.ScrappingResults
                    .Single(item => item.ScrappingConfigurationId == Configuration.ScrappingConfigurationId)
                    .ScrappingResultId;
            }

            Clock.Start();
        }


        public void StopScrapping()
        {
            Clock.Stop();
        }

        private void ClockOnElapsed(object sender, ElapsedEventArgs e)
        {
            Clock.Stop();
            GetContent();
            Clock.Start();
        }

        public ScrappingConfiguration Configuration { get; set; }
        public string Url { get; set; }
        public Timer Clock { get; set; }
        public int? ResultId { get; set; }
        
        public BodyComparer Comparer { get; set; }

        public ScrapperContext Context { get; set; }

        public void GetContent()
        {
            var result = new BaseScrapperResult() {IsSucces = false, BodyContent = ""};
            using (var httpClient = new HttpClient())
            {
                //_httpClient.BaseAddress = new Uri(url);
                var response = httpClient.GetAsync(Url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result.IsSucces = true;
                    result.BodyContent = response.Content.ReadAsStringAsync().Result;
                }
            }

            //TODO manage error case
            if (result.IsSucces)
            {
                try
                {
                    var l = Context.ScrappingResults.Single(item => item.ScrappingResultId == ResultId);
                    l.BodyResult =   result.BodyContent;
                    if (l.BodyResult != l.BodyUnchanged)
                    {
                        l.HasChanged = true;
                        Comparer.Compare();
                    }
                    else
                    {
                        l.HasChanged = false;
                    }
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }
    }
}