using DemoBookAPI.Domain;

namespace DemoBookAPI.Models
{
    public class AddBookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int? AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public DateTime AddedDate { get; set; }
        public int CategoryId { get; set; }
    }
}
