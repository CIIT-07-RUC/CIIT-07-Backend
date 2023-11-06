using System;
using DataLayer.Models;

namespace DataLayer
{
	public interface IDataService
	{
		public User GetUser(int id);
		public User GetUserByEmail(string email);
        public List<User> GetUsers();
		public bool LoginUser(string email, string password);
		public Tuple<bool, string> RegisterUser(string email, string password, string passwordConfirmation);
		public bool UpdateUserInfo(int id, string Phone, string Email);
		public bool RemoveUser(int id);
		public List<UserBookmark> GetBookmarks(int userId);
		public UserBookmark? GetBookmark(int id);
		public UserBookmark AddBookmark(UserBookmark bookmark);
		public void UpdateBookmark(int id, UserBookmark bookmark);
		public void DeleteBookmark(int id);
		public void DeleteAllBookmarks(int userId);
		public List<UserRating> GetUserRatings(int userId);
		public UserRating? GetUserRating(int userId, string tconst);
		public void AddRating(int userId, string tconst, int? rating, string? comment);
		public void DeleteMovieRating(int userId, string tconst);
		public void DeleteAllMovieRatings();
		

#if haveMoreTime
			// Implement additional functions
			public bool DeactiveUser(int id);
			public bool SetAdminprivilege(bool isAdmin);
#endif
	}
}

