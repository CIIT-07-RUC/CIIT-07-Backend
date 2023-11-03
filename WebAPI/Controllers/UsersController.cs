using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataService _dataservice;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public UsersController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataservice = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _dataservice.GetUsers()
                .Select(x => CreateUserListModel(x));
            if (users == null)
            {
                return NotFound();
            }

            // var usersMapped = _mapper.Map<IEnumerable<UserModel>>(users);
            return Ok(users);
        }


        [HttpPost]
        public IActionResult SignIn(RegisterUserModel model)
        {
            _dataservice.RegisterUser(model.Email, model.Password, model.PasswordConfirmation);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserModel model)
        {
            var userLogin = _dataservice.LoginUser(model.Email, model.Password);

            if (userLogin == false)
            {
                BadRequest();
            }

            var claims = new List<Claim>
        {
                new Claim(ClaimTypes.Name, model.Email)
            };

            var secret = "popeopwqodpodpaosap323";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddDays(4),
               signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new { model.Email, token = jwt });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _dataservice.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(CreateUserModel(user));
        }

        private UserModel CreateUserModel(User user)
        {
            var model = _mapper.Map<UserModel>(user);
            return model;

        }

        private UserListModel CreateUserListModel(User user)
        {
            var model = _mapper.Map<UserListModel>(user);
            return model;

        }
    }
}