using Asp.Versioning;
using DemoBookAPI.Core.Consts;
using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using DemoBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Controllers.V3
{
    [ApiController]
    [Route("[controller]/v{version:apiVersion}")]
    [ApiVersion("3.0")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetAuthors")]
        public async Task<IActionResult> GetAuthors(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                    return BadRequest("PageNumber and PageSize must be grater than 0.");
                pageNumber = pageNumber >= 1 ? pageNumber - 1 : pageNumber;
                var authorsList = await _unitOfWork.Authors.FindAllAsync(a => a.IsActive, pageSize, pageNumber, a => a.AuthorId, OrderBy.Ascending);
                var responseList = authorsList.Select(a => new AddAuthorResponse
                {
                    AuthorId = a.AuthorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    IsActive = a.IsActive,
                    IsDeleted = a.IsDeleted,
                    Phone = a.Phone
                });

                return Ok(responseList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int authorId)
        {
            try
            {
                if (authorId <= 0)
                    return BadRequest("Author Id is required");
                var authorDB = await _unitOfWork.Authors.GetByIdAsync(authorId);
                if (authorDB == null)
                    return BadRequest($"The author with id = {authorId} is not exist.");
                var response = new AddAuthorResponse
                {
                    AddedDate = authorDB.AddedDate,
                    AuthorId = authorDB.AuthorId,
                    Email = authorDB.Email,
                    FirstName = authorDB.FirstName,
                    LastName = authorDB.LastName,
                    IsActive = authorDB.IsActive,
                    IsDeleted = authorDB.IsDeleted,
                    Phone = authorDB.Phone
                };
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAuthorByName")]
        public async Task<IActionResult> GetAuthorByName(string authorName, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorName))
                    return BadRequest("Author Name is required.");
                if (pageNumber <= 0 || pageSize <= 0)
                    return BadRequest("PageNumber and PageSize must be grater than 0.");
                pageNumber = pageNumber >= 1 ? pageNumber - 1 : pageNumber;

                var authorsList = await _unitOfWork
                                    .Authors
                                    .FindAllAsync(a => a.FirstName.ToLower().Contains(authorName.ToLower())
                                    || a.LastName.ToLower().Contains(authorName.ToLower())
                                    , pageSize, pageNumber, a => a.AuthorId, OrderBy.Ascending);
                if (authorsList == null || authorsList.Count() == 0)
                    return BadRequest("No result found.");
                var responseList = authorsList.Select(a => new AddAuthorResponse
                {
                    AuthorId = a.AuthorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    IsActive = a.IsActive,
                    IsDeleted = a.IsDeleted,
                    Phone = a.Phone
                });

                return Ok(responseList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequst addAuthorRequst)
        {
            try
            {
                var result = await _unitOfWork.Authors.Add(new Author
                {
                    Email = addAuthorRequst.Email,
                    AddedDate = DateTime.Now,
                    FirstName = addAuthorRequst.FirstName,
                    LastName = addAuthorRequst.LastName,
                    IsActive = addAuthorRequst.IsActive,
                    Phone = addAuthorRequst.Phone,
                });
                _unitOfWork.Complete();
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
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequst request)
        {
            try
            {
                var authorDB = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
                if (authorDB == null)
                    return BadRequest("The author not exists.");
                authorDB.FirstName = request.FirstName;
                authorDB.LastName = request.LastName;
                authorDB.IsActive = request.IsActive;
                _unitOfWork.Complete();

                var response = new AddAuthorResponse
                {
                    AddedDate = authorDB.AddedDate,
                    AuthorId = authorDB.AuthorId,
                    Email = authorDB.Email,
                    FirstName = authorDB.FirstName,
                    LastName = authorDB.LastName,
                    IsActive = authorDB.IsActive,
                    IsDeleted = authorDB.IsDeleted,
                    Phone = authorDB.Phone,
                };

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPatch]
        [Route("UpdateAuthorEmailAndPhone")]
        public async Task<IActionResult> UpdateAuthorEmailAndPhone(UpdateAuthorEmailAndPhoneRequst request)
        {
            try
            {
                var authorDB = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
                if (authorDB == null)
                    return BadRequest("The author not exists.");
                authorDB.Email = request.Email;
                authorDB.Phone = request.Phone;
                _unitOfWork.Complete();

                var response = new AddAuthorResponse
                {
                    AddedDate = authorDB.AddedDate,
                    AuthorId = authorDB.AuthorId,
                    Email = authorDB.Email,
                    FirstName = authorDB.FirstName,
                    LastName = authorDB.LastName,
                    IsActive = authorDB.IsActive,
                    IsDeleted = authorDB.IsDeleted,
                    Phone = authorDB.Phone,
                };

                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int authorId)
        {
            try
            {
                if (authorId <= 0)
                    return BadRequest("Author Id is required");
                var authorDB = await _unitOfWork.Authors.GetByIdAsync(authorId);
                _unitOfWork.Authors.Delete(authorDB);
                _unitOfWork.Complete();
                return Ok($"Author with Id = {authorId} deleted successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
