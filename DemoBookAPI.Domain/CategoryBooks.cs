using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.Domain
{
    public class CategoryBooks
    {
        public int CategoryId { get; set; }
        public int BookId { get; set; }
        public Category Category { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
