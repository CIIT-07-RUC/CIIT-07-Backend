using System.Security.Claims;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IDataService _dataService;

        public MovieController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(string id)
        {
            var movie = _dataService.GetMovieById(id);

            if (movie == null)
            {
                return Ok("Not Found Movie");
            }
            return Ok(movie);
        }
    }
}
