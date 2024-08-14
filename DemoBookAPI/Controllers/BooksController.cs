using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [Route("getbookslist")]
        public IActionResult GetBooksList(int pageNumber, int pageSize)
        {
            return Ok();
        }
    }
}
