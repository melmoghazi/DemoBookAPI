using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using DemoBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IBaseRepository<BookDetail> _bookDetailRepository;
        private readonly IBaseRepository<CategoryBooks> _categoryBooksRepository;

        public BooksController( IBaseRepository<Book> baseRepository,IBaseRepository<BookDetail> bookDetailRepository
            ,IBaseRepository<CategoryBooks> categoryBooksRepository)
        {
            _bookRepository = baseRepository;
            _bookDetailRepository = bookDetailRepository;
            _categoryBooksRepository = categoryBooksRepository;
        }

        [HttpGet]
        [Route("getbookslist")]
        public async Task<IActionResult> GetBooksList(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        /// <summary>
        /// Add to Book table and BookDetails table.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addbook")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest request)
        {
            //insert into book table
            var bookResult = await _bookRepository.Add(new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                Summary = request.Summary
            });
            //insert into BookDetails table.
            var bookDetialResult = await _bookDetailRepository.Add(new BookDetail
            {
                AddedDate = DateTime.Now,
                BookId = bookResult.BookId,
                ISBN = request.ISBN,
                PublishDate = request.PublishDate
            });
            //insert into linked table CategoryBooks
            if (request.CategoryId > 0)
            {
                var cbResult = _categoryBooksRepository.Add(new CategoryBooks
                {
                    BookId = bookResult.BookId,
                    CategoryId = request.CategoryId
                });
            }
            var response = new AddBookResponse
            {
                BookId = bookResult.BookId,
                AuthorId = bookResult.AuthorId,
                Summary = bookResult.Summary,
                Title = request.Title,
                PublishDate = bookDetialResult.PublishDate,
                AddedDate = bookDetialResult.AddedDate,
                ISBN = bookDetialResult.ISBN,
                CategoryId = request.CategoryId
            };
            return Ok(response);
        }
    }
}
