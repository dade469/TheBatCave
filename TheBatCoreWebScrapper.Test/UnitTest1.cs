using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using TheBatCoreWebScrapper.DAL;
using TheBatCoreWebScrapper.DAL.Models;

namespace TheBatCoreWebScrapper.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using (var context = new ScrapperContext())
            {
                context.UrlLibraries.Add(new UrlLibrary() {Url = "http://www.cephei.com"});
                context.ScrappingConfigurations.Add(new ScrappingConfiguration(){Interval = 1, UrlLibrary = context.UrlLibraries.First()});
                //context.ScrappingResults.Add(new ScrappingResult{BodyResult = "", UrlLibrary = new UrlLibrary{Url = "http://www.google.com"}});
                context.SaveChanges();
            }
        }
    }
}          