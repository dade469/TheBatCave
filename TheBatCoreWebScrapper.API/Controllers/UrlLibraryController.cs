using Microsoft.AspNetCore.Mvc;

namespace TheBatCoreWebScrapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlLibraryController : ControllerBase
    {
        [HttpPost]
        public void PostUrl()
        {
        }
        
        [HttpGet]
        public void GetUrlInfo(int ulrId)
        {
        }
        
        [HttpGet]
        public void GetUrlLibrary()
        {
        }
        
        [HttpPut]
        public void PutUrlInfo(int urlId)
        {
        }
        
        [HttpPut]
        public void PutUrlLibrary()
        {
        }
    }
}