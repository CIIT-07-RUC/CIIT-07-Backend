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

        [HttpGet("{searchInput}")]
        public IActionResult SearchByTitle(string searchInput)
        {
            return Ok(_dataService.SearchByPersonName(searchInput));
        }

        [HttpGet("{searchInput}")]
        public IActionResult SearchByPersonName(string searchInput)
        {
            
            return Ok(_dataService.SearchByTitle(searchInput));
        }
        [HttpGet]
        public IActionResult SearchTitlebyKeyword(string searchInput) 
        {
            return Ok(_dataService.SearchTitleByKeyword(searchInput));
        }


    }
}
