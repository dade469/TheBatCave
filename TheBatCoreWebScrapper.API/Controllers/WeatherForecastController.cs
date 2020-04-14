using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheBatCoreWebScrapper.Business.Services;
using TheBatCoreWebScrapper.Core.Infrastructure;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ScrapperContext _context;
        
        public ScrapperService _scrapperService { get; set; }
        

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ScrapperService scrapperService)
        {
            _logger = logger;
            _scrapperService = scrapperService;
        }

        [HttpGet]
        public void Get()
        {
            
           _scrapperService.InitializeScrapper();
        }
    }
}
