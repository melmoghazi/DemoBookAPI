using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using DemoBookAPI.EF.Repositories;
using DemoBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBaseRepository<Author> _baseRepository;

        public AuthorsController(IBaseRepository<Author> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpPost]
        [Route("addauthor")]
        public IActionResult AddBook([FromBody] AddAuthorRequst addAuthorRequst)
        {
            var result = _baseRepository.Add(new Author
            {
                Email = addAuthorRequst.Email,
                AddedDate = DateTime.Now,
                FirstName = addAuthorRequst.FirstName,
                LastName = addAuthorRequst.LastName,
                IsActive = addAuthorRequst.IsActive,
                Phone = addAuthorRequst.Phone,
            });
            var response = new AddAuthorResponse
            {
                AddedDate = result.AddedDate,
                AuthorId = result.AuthorId,
                Email = result.Email,
                FirstName = result.FirstName,
                LastName = result.LastName,
                IsActive = result.IsActive,
                IsDeleted = result.IsDeleted,
                Phone = result.Phone,
            };
            return Ok(response);
        }
    }
}
