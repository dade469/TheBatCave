using Autofac;
using TheBatCoreWebScrapper.Business.Business;
using TheBatCoreWebScrapper.Business.Models.Clients;
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
            //builder.RegisterType<ScrapperContext>().InstancePerLifetimeScope();
            //builder.RegisterType<ScrapperService>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            // builder.RegisterType<ScrapperFactory>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<UrlLibraryService>().InstancePerLifetimeScope();
            builder.RegisterType<ScrappingManagerService>().InstancePerLifetimeScope();
            builder.RegisterType<ScrapperFactory>().SingleInstance();

            // builder.RegisterType<BodyComparer>().InstancePerLifetimeScope();
            
        }

        
    }
}