using DemoBookAPI.Domain;

namespace DemoBookAPI.Models
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public int? AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
