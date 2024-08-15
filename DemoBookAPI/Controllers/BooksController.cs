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

        public BooksController( IBaseRepository<Book> baseRepository,IBaseRepository<BookDetail> bookDetailRepository)
        {
            _bookRepository = baseRepository;
            _bookDetailRepository = bookDetailRepository;
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
        public IActionResult AddBook([FromBody] AddBookRequest request)
        {
            //insert into book table
            var bookResult = _bookRepository.Add(new Book
            {
                Title = request.Title,
                AuthorId = request.AuthorId,
                Summary = request.Summary
            });
            //insert into BookDetails table.
            var bookDetialResult = _bookDetailRepository.Add(new BookDetail
            {
                AddedDate = DateTime.Now,
                BookId = bookResult.BookId,
                ISBN = request.ISBN,
                PublishDate = request.PublishDate
            });
            var response = new AddBookResponse
            {
                BookId = bookResult.BookId,
                AuthorId = bookResult.AuthorId,
                Summary = bookResult.Summary,
                Title = request.Title,
                PublishDate = bookDetialResult.PublishDate,
                AddedDate = request.AddedDate,
                ISBN = bookDetialResult.ISBN
            };
            return Ok(response);
        }
    }
}
