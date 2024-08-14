using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.Domain
{
    public class Category
    {
        public Category()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Book> Books { get; set; }
    }
}
