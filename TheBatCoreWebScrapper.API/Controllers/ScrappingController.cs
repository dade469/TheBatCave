using Microsoft.AspNetCore.Mvc;
using TheBatCoreWebScrapper.Business.Services;

namespace TheBatCoreWebScrapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrappingController : ControllerBase
    {
        private readonly ScrappingManagerService _scrappingManagerService;
        public ScrappingController(ScrappingManagerService scrappingManagerService)
        {
            _scrappingManagerService = scrappingManagerService;
        }

        [HttpPost]
        [ActionName("Mom")]
        public void StopScrapper()
        {
            _scrappingManagerService.StopScrapping();
        }
        
        [HttpGet]
        [ActionName("dsds")]

        public void StartScrapper()
        {
            _scrappingManagerService.StartScrapping();
        }
        
    }
}