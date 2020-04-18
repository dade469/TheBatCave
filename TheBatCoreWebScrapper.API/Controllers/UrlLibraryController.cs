using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TheBatCoreWebScrapper.Business.Services;
using TheBatCoreWebScrapper.DTO;

namespace TheBatCoreWebScrapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlLibraryController : ControllerBase
    {
        private readonly UrlLibraryService _urlLibraryService;
        public UrlLibraryController(UrlLibraryService urlLibraryService)
        {
            _urlLibraryService = urlLibraryService;
        }
        [HttpPost]
        [ActionName("Momdsds")]

        public void PostUrl(UrlLibraryDTO urlLibraryDTO)
        {
            _urlLibraryService.AddUrlToLibrary(urlLibraryDTO);
        }

        [HttpGet]
        [ActionName("rere")]

        public List<UrlLibraryDTO> GetAllUrls()
        {
            return _urlLibraryService.GetAll();
        }
        
        [HttpPut]
        public void PutUrl(UrlLibraryDTO urlLibraryDTO)
        {
             _urlLibraryService.UpdateUrlConfiguration(urlLibraryDTO);
        }
    }
}