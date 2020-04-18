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
        private string _url { get; set; }
        private Timer _clock { get; set; }
        private int _resultId { get; set; }
        private int _interval { get; set; }
        private BodyComparer _comparer { get; set; }
        private ScrapperContext _context { get; set; }
        
        public int ConfigurationId { get; set; }
        
        public BaseScrapperClient(ScrapperContext context, BodyComparer
            comparer, string url, int resultId, int interval, int configurationId)
        {
            _comparer = comparer;
            _url = url;
            _resultId = resultId;
            _context = context;
            _interval = interval;
            ConfigurationId = configurationId;
            Initialize();
        }

        public void Initialize()
        {
            _clock = new Timer();
            _clock.Interval = _interval;
            _clock.Elapsed += ClockOnElapsed;
        }

        public void StartScrapping()
        {
            _clock.Start();
        }
        
        public void StopScrapping()
        {
            _clock.Stop();
        }

        private void ClockOnElapsed(object sender, ElapsedEventArgs e)
        {
            _clock.Stop();
            GetContent();
            _clock.Start();
        }


        public void GetContent()
        {
            var result = new BaseScrapperResult() {IsSucces = false, BodyContent = ""};
            using (var httpClient = new HttpClient())
            {
                //_httpClient.BaseAddress = new Uri(url);
                var response = httpClient.GetAsync(_url).Result;
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
                    var l = _context.ScrappingResults.Single(item => item.ScrappingResultId == _resultId);
                    l.BodyResult =   result.BodyContent;
                    l.LastUpdate = DateTime.Now;
                    
                    if (l.BodyResult != l.BodyUnchanged)
                    {
                        l.HasChanged = true;
                        _comparer.Compare(_resultId);
                    }
                    else
                    {
                        l.HasChanged = false;
                    }
                    _context.SaveChanges();
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