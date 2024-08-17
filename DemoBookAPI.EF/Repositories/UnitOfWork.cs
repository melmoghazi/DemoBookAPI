using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBookAPI.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoBookAPIContext _dbContext;
        public IBaseRepository<Author> Authors { get; private set; }
        public IBaseRepository<Book> Books { get; private set; }
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<CategoryBooks> CategoryBooks { get; private set; }

        public UnitOfWork(DemoBookAPIContext dbContext)
        {
            _dbContext = dbContext;
            Authors = new BaseRepository<Author>(_dbContext);
            Books = new BaseRepository<Book>(_dbContext);
            Categories = new BaseRepository<Category>(_dbContext);
            CategoryBooks = new BaseRepository<CategoryBooks>(_dbContext);
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
