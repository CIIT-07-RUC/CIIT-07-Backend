using System;
using DataLayer.Models;

namespace DataLayer
{
	public interface IDataService
	{
		public NameBasic GetCastById(string id);
		public TitleExtended GetMovieById(string id);
        public User GetUser(int id);
		public User GetUserByEmail(string email);
        public List<User> GetUsers();
		public bool ReactivateAccount(int id, bool reactivateAccount);
        public bool LoginUser(string email, string password);
		public Tuple<bool, string> RegisterUser(string email, string password, string passwordConfirmation);
		public bool UpdateUserInfo(int id, string Phone, string Email);
		public bool AddUserInfo(int id, string UserName, string FirstName, string LastName, string Phone);
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
		public TitleExtended? SearchByTitle(string searchInput);
        	public NameBasic? SearchByPersonName(string searchInput);
		public List<TitleExtended?> SearchTitleByKeyword(string searchInput);
		
	}
}

