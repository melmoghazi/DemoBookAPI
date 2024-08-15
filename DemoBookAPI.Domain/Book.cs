namespace DemoBookAPI.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public decimal Serial { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }

        //one author only foreach book
        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        //one book one bookdetail
        public BookDetail BookDetail { get; set; }
        // many books many categories
        public List<Category> Categories { get; } = new();
        public List<CategoryBooks> CategoryBooks { get; } = new();
    }
}
