using System;
using System.Net.Http;
using TheBatCoreWebScrapper.Business.Models.Results;

namespace TheBatCoreWebScrapper.Business.Models.Clients
{
    public class BaseScrapperClient
    {
        public BaseScrapperClient()
        {
            
        }

        public BaseScrapperResult GetContent(string url)
        {
            var result = new BaseScrapperResult(){IsSucces = false, BodyContent = ""};
            using (var httpClient = new HttpClient())
            {
                //_httpClient.BaseAddress = new Uri(url);
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    result.IsSucces = true;
                    result.BodyContent = response.Content.ReadAsStringAsync().Result;

                }
            }

            return result;
        }
    }
}
