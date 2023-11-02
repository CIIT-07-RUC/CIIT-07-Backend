using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataService _dataservice;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public ProductsController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataservice = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dataservice.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}