using Autofac;
using TheBatCoreWebScrapper.Business.Services;
using TheBatCoreWebScrapper.DAL;

namespace TheBatCoreWebScrapper.Core.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static int Order
        {
            get
            {
                return -1000;
            }
        }
        
        public static void Register(ContainerBuilder builder)
        {
            //Register DbContext
            builder.RegisterType<ScrapperContext>().InstancePerLifetimeScope();
            builder.RegisterType<ScrapperService>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        
    }
}