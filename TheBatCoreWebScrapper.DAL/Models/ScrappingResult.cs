namespace TheBatCoreWebScrapper.DAL.Models
{
    /// <summary>
    /// Stores scrapping results
    /// </summary>
    public class ScrappingResult
    {
        public int ScrappingResultId { get; set; }

        public string BodyResult { get; set; }

        public ScrappingConfiguration Configuration { get; set; }
    }
}