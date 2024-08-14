namespace DemoBookAPI.Domain
{
    public class Book
    {
        public Book()
        {
            categories = new List<Category>();
        }
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
        public IList<Category> categories { get; set; }
    }
}
