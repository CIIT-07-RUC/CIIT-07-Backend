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
            Console.WriteLine("USERLOGIN {0}", userLogin);
            return Ok(userLogin);
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