using System;
using DataLayer;
using DataLayer.Models;

namespace Tests
{
	public class DataServiceTests
	{
        /* USER Tests  */

        [Fact]
        public void User_Object_HasAllProperties()
        {
            var user = new User();
            Assert.Equal(0, user.Id);
            Assert.Null(user.Email);
            Assert.Null(user.FirstName);
            Assert.Null(user.LastName);
            Assert.Null(user.Password);
            Assert.Null(user.Phone);

        }

        [Fact]
        public void GetUser_InvalidId_ReturnsNull()
        {
            var service = new DataService();
            var findUser = service.GetUser(0);
            Assert.Null(findUser);
        }

        [Fact]
        public void GetUser_ValidId_ReturnsUserObject()
        {
            var service = new DataService();
            var findUser = service.GetUser(18);
            Assert.Equal("test1@test.com", findUser.Email);
        }

        [Fact]
        public void RegisterUser_ValidData_ReturnTrue()
        {
            var service = new DataService();
            var createUser = service.RegisterUser("newemail@fromunittests.com", "test123", "test123");
            var responseBoolean = createUser.Item1;
            Assert.Equal(true, responseBoolean);

            var findNewCreatedUser = service.GetUserByEmail("newemail@fromunittests.com");
            service.RemoveUser(findNewCreatedUser.Id);
        }

        [Fact]
        public void RegisterUser_InValidData_ReturnFalse()
        {
            var service = new DataService();
            var createUser = service.RegisterUser("newemail@fromunittests.com", "test", "test123");
            var responseBoolean = createUser.Item1;
            Assert.Equal(false, responseBoolean);
        }

        [Fact]
        public void RegisterUser_ValidData_ReturnValidMessage()
        {
            var service = new DataService();
            var createUser = service.RegisterUser("newemail2@fromunittests.com", "test123", "test123");
            var responseMessage = createUser.Item2;
            Assert.Equal("OK", responseMessage);

            var findNewCreatedUser = service.GetUserByEmail("newemail2@fromunittests.com");
            service.RemoveUser(findNewCreatedUser.Id);
        }

        [Fact]
        public void RegisterUser_PasswordsAreNotIdentical_ReturnErrorMessage()
        {
            var service = new DataService();
            var createUser = service.RegisterUser("newemail2@fromunittests.com", "test999", "test123");
            var responseMessage = createUser.Item2;
            Assert.Equal("Password is not valid", responseMessage);
        }

        [Fact]
        public void RegisterUser_EmailAlreadyExists_ReturnErrorMessage()
        {
            var service = new DataService();
            var createUser = service.RegisterUser("test1@test.com", "test123", "test123");
            var responseMessage = createUser.Item2;
            Assert.Equal("Email already exists, please choose other one.", responseMessage);
        }

        [Fact]
        public void LoginUser_ValidData_ReturnTrue()
        {
            var service = new DataService();
            var login = service.LoginUser("test1@test.com", "test123");
            Assert.Equal(true, login);
        }

        [Fact]
        public void LoginUser_InValidData_ReturnFalse()
        {
            var service = new DataService();
            var login = service.LoginUser("test1@test.com", "test1234");
            Assert.Equal(false, login);
        }
    }
}

