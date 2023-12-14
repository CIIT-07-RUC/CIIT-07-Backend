using System.Security.Claims;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/cast")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly IDataService _dataService;

        public CastController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public IActionResult GetCast(string id)
        {
            var cast = _dataService.GetCastById(id);

            if (cast == null)
            {
                return NotFound();
            }
            return Ok(cast);
        }
    }
}
