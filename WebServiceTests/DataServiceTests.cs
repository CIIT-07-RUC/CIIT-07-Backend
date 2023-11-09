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
        
        /* Bookmark tests */

        [Fact]
        public void AddBookmark_ValidData()
        {
            var service = new DataService();
            service.RegisterUser("bookmark@fromunittests.com", "test123", "test123");
            var findNewCreatedUser = service.GetUserByEmail("bookmark@fromunittests.com");

            var bookmark = new UserBookmark
            {
                UserId = findNewCreatedUser.Id,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            var addedBookmark = service.AddBookmark(bookmark);
            
            Assert.Equal(findNewCreatedUser.Id, addedBookmark.UserId);
            Assert.Equal("tt13210498", addedBookmark.TConst);
            Assert.Equal("Test comment", addedBookmark.BookmarkComment);
            
            service.DeleteBookmark(addedBookmark.BookmarkId);
            service.RemoveUser(findNewCreatedUser.Id);
        }

        [Fact]
        public void GetBookmarks_ValidData()
        {
            var service = new DataService();
            service.RegisterUser("bookmark@fromunittests.com", "test123", "test123");
            var findNewCreatedUser = service.GetUserByEmail("bookmark@fromunittests.com");

            var bookmark = new UserBookmark
            {
                UserId = findNewCreatedUser.Id,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            service.AddBookmark(bookmark);

            var userBookmarks = service.GetBookmarks(findNewCreatedUser.Id);
            var addedBookmark = userBookmarks[0];
            
            Assert.Single(userBookmarks);
            Assert.Equal(findNewCreatedUser.Id, addedBookmark.UserId);
            Assert.Equal("tt13210498", addedBookmark.TConst);
            Assert.Equal("Test comment", addedBookmark.BookmarkComment);
            
            service.DeleteBookmark(addedBookmark.BookmarkId);
            service.RemoveUser(findNewCreatedUser.Id);
        }

        [Fact]
        public void GetBookmarks_InvalidUserId()
        {
            var service = new DataService();
            var userBookmarks = service.GetBookmarks(999);
            Assert.Empty(userBookmarks);
        }

        [Fact]
        public void GetSingleBookmark_ValidId()
        {
            var service = new DataService();
            
            var bookmark = new UserBookmark
            {
                UserId = 1,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            var addedBookmark = service.AddBookmark(bookmark);

            var userBookmark = service.GetBookmark(addedBookmark.BookmarkId);
            
            Assert.NotNull(userBookmark);
            Assert.Equal(1, userBookmark.UserId);
            Assert.Equal("tt13210498", userBookmark.TConst);
            Assert.Equal("Test comment", userBookmark.BookmarkComment);
            
            service.DeleteBookmark(addedBookmark.BookmarkId);
        }

        [Fact]
        public void GetSingleBookmark_InvalidId()
        {
            var service = new DataService();
            var userBookmark = service.GetBookmark(999);
            Assert.Null(userBookmark);
        }

        [Fact]
        public void UpdateBookmark()
        {
            var service = new DataService();
            
            var bookmark = new UserBookmark
            {
                UserId = 1,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            var addedBookmark = service.AddBookmark(bookmark);
            
            var bookmarkUpdate = new UserBookmark
            {
                UserId = 1,
                TConst = "tt13210498",
                BookmarkComment = "Updated text"
            };
            
            service.UpdateBookmark(addedBookmark.BookmarkId, bookmarkUpdate);

            var userBookmark = service.GetBookmark(addedBookmark.BookmarkId);
            
            Assert.NotNull(userBookmark);
            Assert.Equal(1, userBookmark.UserId);
            Assert.Equal("tt13210498", userBookmark.TConst);
            Assert.Equal("Updated text", userBookmark.BookmarkComment);
            
            service.DeleteBookmark(addedBookmark.BookmarkId);
        }

        [Fact]
        public void DeleteBookmark_ValidId()
        {
            var service = new DataService();
            
            var bookmark = new UserBookmark
            {
                UserId = 1,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            var addedBookmark = service.AddBookmark(bookmark);

            service.DeleteBookmark(addedBookmark.BookmarkId);
            
            var userBookmark = service.GetBookmark(addedBookmark.BookmarkId);
            Assert.Null(userBookmark);
        }

        [Fact]
        public void DeleteBookmarksOfUser()
        {
            var service = new DataService();
            service.RegisterUser("bookmark@fromunittests.com", "test123", "test123");
            var findNewCreatedUser = service.GetUserByEmail("bookmark@fromunittests.com");
            
            var bookmark = new UserBookmark
            {
                UserId = findNewCreatedUser.Id,
                TConst = "tt13210498",
                BookmarkComment = "Test comment"
            };
            var addedBookmark = service.AddBookmark(bookmark);

            service.DeleteAllBookmarks(findNewCreatedUser.Id);
            
            var userBookmarks = service.GetBookmarks(findNewCreatedUser.Id);
            Assert.Empty(userBookmarks);
            
            service.RemoveUser(findNewCreatedUser.Id);
        }
        
        /* Ratings tests */

        [Fact]
        public void AddAndGetRatings_ValidData()
        {
            var service = new DataService();
            service.RegisterUser("rating@fromunittests.com", "test123", "test123");
            var findNewCreatedUser = service.GetUserByEmail("rating@fromunittests.com");

            service.AddRating(findNewCreatedUser.Id, "tt13210498", 2, "Test comment");

            var userRatings = service.GetUserRatings(findNewCreatedUser.Id);
            var addedRating = userRatings[0];
            
            Assert.Single(userRatings);
            Assert.Equal(findNewCreatedUser.Id, addedRating.UserId);
            Assert.Equal("tt13210498", addedRating.TConst);
            Assert.Equal(2, addedRating.Rating);
            Assert.Equal("Test comment", addedRating.Comment);

            service.DeleteMovieRating(findNewCreatedUser.Id, "tt13210498");

            service.RemoveUser(findNewCreatedUser.Id);
        }

        [Fact]
        public void GetRatings_InvalidUserId()
        {
            var service = new DataService();
            var userRatings = service.GetUserRatings(999);
            Assert.Empty(userRatings);
        }
    }
}

