using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.Domain
{
    public class BookDetail
    {
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public DateTime AddedDate { get; set; }

        [Key]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
