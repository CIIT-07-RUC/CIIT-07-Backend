using System.Security.Claims;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public RatingsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetRatings([FromQuery] int page)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var ratings = _dataService.GetUserRatings(userId);
            
            var helperFunction = new Helpers();
            var pagination = helperFunction.Pagination(ratings, page);
            
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public IActionResult GetRating(string id)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(_dataService.GetUserRating(userId, id));
        }
        
        [HttpPost]
        public IActionResult AddRating(UserRating userRating)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _dataService.AddRating(userId, userRating.TConst, userRating.Rating, userRating.Comment);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(string id)
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            _dataService.DeleteMovieRating(userId, id);
            return Ok();
        }
    }
}
