using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using DemoBookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBaseRepository<Category> _baseRepository;

        public CategoriesController(IBaseRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpPost]
        [Route("addcategory")]
        public IActionResult addcategory(AddCategoryRequest requst)
        {
            var result = _baseRepository.Add(new Category
            {
                Name = requst.Name,
                Description = requst.Description
            });
            return Ok(new AddCategoryResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description
            });
        }
    }
}
