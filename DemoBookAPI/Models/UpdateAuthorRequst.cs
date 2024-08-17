namespace DemoBookAPI.Models
{
    public class UpdateAuthorRequst
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
