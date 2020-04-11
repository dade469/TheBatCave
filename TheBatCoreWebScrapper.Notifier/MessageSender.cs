using System;
using System.Net;
using System.Net.Http;

namespace TheBatCoreWebScrapper.Notifier
{
    public class MessageSender
    {
        
        

        public void SendMessage(string message)
        {
            using (var httpClient = new HttpClient())
            {
                var res = httpClient.GetAsync(
                    $"https://api.telegram.org/bot{Constants.BOT_TOKEN}/sendMessage?chat_id={Constants.CHANNEL_ID}&text={message}"
                ).Result;
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    /* done, go check your channel */
                }
                else
                {
                    /* something went wrong */
                }
            }
        }

    }
}