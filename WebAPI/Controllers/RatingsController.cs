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
            // TODO: use actual ID of the authenticated user
            var ratings = _dataService.GetUserRatings(1);
            
            var helperFunction = new Helpers();
            var pagination = helperFunction.Pagination(ratings, page);
            
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public IActionResult GetRating(string id)
        {
            // TODO: use actual ID of the authenticated user
            return Ok(_dataService.GetUserRating(1, id));
        }
        
        [HttpPost]
        public IActionResult AddRating(UserRating userRating)
        {
            // TODO: use actual ID of the authenticated user
            _dataService.AddRating(1, userRating.TConst, userRating.Rating, userRating.Comment);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(string id)
        {
            // TODO: use actual ID of the authenticated user
            _dataService.DeleteMovieRating(1, id);
            return Ok();
        }
    }
}
