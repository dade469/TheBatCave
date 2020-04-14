using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TheBatCoreWebScrapper.DAL.Models
{
    /// <summary>
    /// Stores scrapping results
    /// </summary>
    public class ScrappingResult
    {
        public int ScrappingResultId { get; set; }

        public string BodyResult { get; set; }

        public string BodyUnchanged { get; set; }
        
        [ForeignKey(nameof(Configuration))]
        public int ScrappingConfigurationId { get; set; }

        public ScrappingConfiguration Configuration { get; set; }

        public bool HasChanged { get; set; }
    }
}