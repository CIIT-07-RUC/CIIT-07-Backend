using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static WebAPI.Controllers.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataService _dataservice;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public UsersController(IDataService dataService, IMapper mapper, IConfiguration configuration)
        {
            _dataservice = dataService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] int page)
        {
            var users = _dataservice.GetUsers();
            if (users == null)
            {
                return NotFound();
            }

            var helperFunction = new Helpers();
            var pagination = helperFunction.Pagination(users, page);

            // var usersMapped = _mapper.Map<IEnumerable<UserModel>>(users);
            return Ok(pagination);
        }


        [HttpPost]
        public IActionResult SignIn(RegisterUserModel model)
        {
            var registerUser = _dataservice.RegisterUser(model.Email, model.Password, model.PasswordConfirmation);

            bool isRegistrationSuccessful = registerUser.Item1;
            string responseMessage = registerUser.Item2;

            if (isRegistrationSuccessful == false)
            {
                return BadRequest(new { isRegistrationSuccessful = isRegistrationSuccessful, responseMessage = responseMessage });
            }

            return Ok(new { isRegistrationSuccessful = isRegistrationSuccessful, responseMessage = responseMessage });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserModel model)
        {
            var userLogin = _dataservice.LoginUser(model.Email, model.Password);
            if (userLogin == false)
            {
                return BadRequest("User does not exist");
            }
            var findUserByEmail = _dataservice.GetUserByEmail(model.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, findUserByEmail.Id.ToString()),
            };

            // TODO: Hide secret 
            var secret = _configuration.GetSection("Authentication:Secret").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.Now.AddDays(4),
               signingCredentials: creds
            );


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            if (findUserByEmail.IsAccountDeactivated)
            {
               _dataservice.ReactivateAccount(findUserByEmail.Id, false);
            }



            return Ok(new {
                model.Email,
                id = findUserByEmail.Id,
                token = jwt
            });
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult UpdateUserInfo(int id, [FromBody] UpdateUserModel updatedCategory)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Int32.Parse(userId) != id)
            {
                return BadRequest();    
            }
            _dataservice.UpdateUserInfo(id, updatedCategory.Phone, updatedCategory.Email);
            return Ok(updatedCategory);
        }

        [Authorize]
        [HttpPost("add-information/{id:int}")]
        public IActionResult PostUserInfo([FromRoute] int id, [FromBody] AddInformationUserModel updatedCategory)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var addUserInformation = _dataservice.AddUserInfo(id, updatedCategory.UserName, updatedCategory.FirstName, updatedCategory.LastName, updatedCategory.Phone);

            if (addUserInformation)
            {
                return Ok("Data were added");
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("deactivate-account")]
        public IActionResult DeactivateAccount(int id, [FromBody] DeactivateUserAccountModel deactivateUserAccountModel )
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currUser = _dataservice.GetUserByEmail(userEmail);

            if (currUser.IsAccountDeactivated)
            {
                return BadRequest("Account is already deactivated.");
            }

            var deactivateAccount = _dataservice.ReactivateAccount(currUser.Id, deactivateUserAccountModel.IsAccountDeactivated);
            if (deactivateAccount)
            {
                return Ok("Success! Account is now deactivated.");
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var currUser = _dataservice.GetUserByEmail(userEmail);

            if (!currUser.IsAdmin)
            {
                return Unauthorized();
            }

            var isUserRemoved = _dataservice.RemoveUser(id);

            if (!isUserRemoved)
            {
                return BadRequest(isUserRemoved);
            }

            return Ok(isUserRemoved);

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

        private DeactivateUserAccountModel CreateDeactivateUserAccountModel(User user)
        {
            var model = _mapper.Map<DeactivateUserAccountModel>(user);
            return model;

        }

        private UpdateUserModel CreateUpdateUserModel(User user)
        {
            var model = _mapper.Map<UpdateUserModel>(user);
            return model;

        }
        private AddInformationUserModel CreateAddInformationUserModel(User user)
        {
            var model = _mapper.Map<AddInformationUserModel>(user);
            return model;
        }



    }
}