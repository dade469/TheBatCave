using Autofac;
using TheBatCoreWebScrapper.Business.Models.Clients;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.Business.Services
{
    public class ScrapperService
    {
        private readonly ScrapperContext _context;
        private ILifetimeScope _lifetimeScope { get; set; }
        public ScrapperService(ILifetimeScope scope)
        {
            _lifetimeScope = scope;
        }

        public void InitializeScrapper()
        {
            var context = _lifetimeScope.BeginLifetimeScope().Resolve<ScrapperContext>();
            ScrapperFactory fac = new ScrapperFactory(context);
            fac.StartOperation();
        }


    }
}