using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework;
using TheBatCoreWebScrapper.Business.Business;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;
// using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheBatCoreWebScrapper.Business.Models.Clients
{
    public class ScrapperFactory
    {
        private List<BaseScrapperClient> _scrappingTask;
        private readonly ScrapperContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public ScrapperFactory(ScrapperContext context, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _context = context;
        }

        public void Initialize()
        {
            var toDoList = _context.ScrappingConfigurations.Include(item => item.ScrappingResult)
                .Include(item => item.UrlLibrary);
            _scrappingTask = new List<BaseScrapperClient>();
            foreach (var scrappingConfiguration in toDoList)
            {
                //Resolving a new context
                var scope = _scopeFactory.CreateScope();
                var taskContext = scope.ServiceProvider.GetService<ScrapperContext>();
                var bodyComparer = new BodyComparer(taskContext);
                var scrapperClient = new BaseScrapperClient(taskContext, bodyComparer,
                    scrappingConfiguration.UrlLibrary.Url, scrappingConfiguration.ScrappingResult.ScrappingResultId,
                    scrappingConfiguration.Interval, scrappingConfiguration.ScrappingConfigurationId);
                if(scrappingConfiguration.ScrappingEnabled)
                    scrapperClient.StartScrapping();
                _scrappingTask.Add(scrapperClient);
            }
        }

        public void StopOperation()
        {
            _scrappingTask.ForEach(item=>item.StopScrapping());
        }
        public void StartOperation()
        {
            _scrappingTask.ForEach(item=>item.StartScrapping());
        }

        public void StopOperation(int configurationId)
        {
            var task = _scrappingTask.SingleOrDefault(item => item.ConfigurationId == configurationId);
            
            if(task!=null)
                task.StopScrapping();
        }
        public void StartOperation(int configurationId)
        {
            var task = _scrappingTask.SingleOrDefault(item => item.ConfigurationId == configurationId);
            
            if(task!=null)
                task.StartScrapping();
        }

        public void AddAndStartOperation(int configurationId)
        {
            var scrappingConfiguration =
                _context.ScrappingConfigurations.Include(item=>item.ScrappingResult).Include(item=>item.UrlLibrary).Single(item => item.ScrappingConfigurationId == configurationId);
            var scope = _scopeFactory.CreateScope();
            var taskContext = scope.ServiceProvider.GetService<ScrapperContext>();
            var bodyComparer = new BodyComparer(taskContext);
            var scrapperClient = new BaseScrapperClient(taskContext, bodyComparer,
                scrappingConfiguration.UrlLibrary.Url, scrappingConfiguration.ScrappingResult.ScrappingResultId,
                scrappingConfiguration.Interval, scrappingConfiguration.ScrappingConfigurationId);
            if(scrappingConfiguration.ScrappingEnabled)
                scrapperClient.StartScrapping();
            _scrappingTask.Add(scrapperClient);
        }

        // public Task StartAsync(CancellationToken cancellationToken)
        // {
        //     StartOperation();
        //     
        //     return Task.CompletedTask;
        // }

        // public Task StopAsync(CancellationToken cancellationToken)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}