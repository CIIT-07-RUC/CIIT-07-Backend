using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IDataService _dataService;

        public SearchController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("title/{searchInput}")]
        public IActionResult SearchByTitle(string searchInput)
        {
            var titles = _dataService.SearchByTitle(searchInput);
            return Ok(titles);
        }

        [HttpGet("person/{searchInput}")]
        public IActionResult SearchByPersonName(string searchInput)
        {
            var persons = _dataService.SearchByPersonName(searchInput);
            return Ok(persons);
        }
        [HttpGet]
        public IActionResult SearchTitlebyKeyword(string searchInput) 
        {
            return Ok(_dataService.SearchTitleByKeyword(searchInput));
        }


    }
}
