namespace MAD_Keuzedeel.Models
{
    public class Book
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}