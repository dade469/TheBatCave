using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBatCoreWebScrapper.DAL.Models
{
    /// <summary>
    /// Define scrapping configuration for each URL
    /// </summary>
    public class ScrappingConfiguration
    {
        public ScrappingConfiguration()
        {
        }

        public int ScrappingConfigurationId { get; set; }

        public UrlLibrary UrlLibrary { get; set; }

        public int Interval { get; set; }
        
        public bool ScrappingEnabled { get; set; }
        
        [InverseProperty("Configuration")]
        public ScrappingResult ScrappingResult { get; set; }
    }
}
