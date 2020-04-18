using System;

namespace TheBatCoreWebScrapper.DTO
{
    public class UrlLibraryDTO
    {
        public UrlLibraryDTO()
        {
        }

        public string Url { get; set; }
        public int UrlLibraryId { get; set; }
        
        public int ScrappingInterval { get; set; }
        public bool ScrappingEnabled { get; set; }
        
    }
}