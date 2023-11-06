using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/bookmarks")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly IDataService _dataService;

        public BookmarksController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetBookmarks()
        {
            // TODO: use actual ID of the authenticated user
            return Ok(_dataService.GetBookmarks(1));
        }

        [HttpGet("{id}")]
        public IActionResult GetBookmark(int id)
        {
            return Ok(_dataService.GetBookmark(id));
        }

        [HttpPost]
        public IActionResult AddBookmark(UserBookmark userBookmark)
        {
            return Ok(_dataService.AddBookmark(userBookmark));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookmark(int id, UserBookmark userBookmark)
        {
            _dataService.UpdateBookmark(id, userBookmark);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBookmark(int id)
        {
            _dataService.DeleteBookmark(id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllBookmarks()
        {
            // TODO: use actual ID of the authenticated user
            _dataService.DeleteAllBookmarks(1);
            return Ok();
        }
    }
}
