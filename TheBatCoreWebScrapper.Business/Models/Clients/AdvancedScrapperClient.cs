using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PuppeteerSharp;
using TheBatCoreWebScrapper.Business.Models.Results;
using TheBatCoreWebScrapper.Notifier;

namespace TheBatCoreWebScrapper.Business.Models.Clients
{
    public class AdvancedScrapperClient
    {
        private MessageSender _messageSender { get; set; }

        public AdvancedScrapperClient()
        {
            _messageSender= new MessageSender();
        }
        public async Task<AdvancedScrapperResult> GetContent(string url)
        {
            try
            {
                var result = new AdvancedScrapperResult{IsSucces = false};
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                var browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    //Timeout = 10000,
                    Headless = true
                });

                using (var page = await browser.NewPageAsync())
                {
                    var response = await page.GoToAsync(url);
                    if (response.Ok)
                    {
                        result.IsSucces = true;

                        result.BodyContent = await page.GetContentAsync();
                        _messageSender.SendMessage("Request correct completed");
                    }
                    else
                    {
                        _messageSender.SendMessage("Request failed");
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                return new AdvancedScrapperResult(){IsSucces = false, BodyContent = e.Message};
            }
            
        }
    }
    

}