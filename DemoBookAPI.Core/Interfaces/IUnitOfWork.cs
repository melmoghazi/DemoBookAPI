using DemoBookAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Book> Books { get; }
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<CategoryBooks> CategoryBooks { get; }
        int Complete();
    }
}
