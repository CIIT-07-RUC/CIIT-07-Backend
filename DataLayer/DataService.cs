using System;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataLayer
{
    
    public class DataService : IDataService
	{
        Cit07Context db = new();

        
        public DataService()
        {
        }

        public User GetUser(int id)
        {
            var user = db.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = db.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            return user;
        }

        public List<User> GetUsers()
        {
            var users = db.Users
                .Where( x => x.IsAccountDeactivated != true)
                .ToList();
            return users;
        }

        public bool ReactivateAccount(int id, bool reactivateAccount)
        {
            var user = GetUser(id);

            try
            {
                user.IsAccountDeactivated = reactivateAccount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION {0}", ex);
                return false;
            }

        }

        public bool LoginUser(string email, string password)
        {

            var connectionString = "Host=cit.ruc.dk;Database=cit07;Username=cit07;Password=GdSpVBqksHbh";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT public.login_in_user('{email}', '{password}')";

            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                return rdr.GetBoolean(0);
            }

            connection.Close();
            return false;
        }

        public Tuple<bool, string> RegisterUser(string email, string password, string passwordConfirmation)
        {

            var connectionString = "Host=cit.ruc.dk;Database=cit07;Username=cit07;Password=GdSpVBqksHbh";
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT public.register_user('{email}', '{password}', '{passwordConfirmation}')";

            using var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var IsRegistrationSuccessful = false;
                if (rdr.GetString(0) == "OK")
                {
                    IsRegistrationSuccessful = true;
                }
                return Tuple.Create(IsRegistrationSuccessful, rdr.GetString(0));
            }
            connection.Close();
            return Tuple.Create(false, "Authentication was not succesfull");

        }

        public bool RemoveUser(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                db.Remove(user);

                return db.SaveChanges() > 0;
            }
            return false;
        }


        public bool UpdateUserInfo(int id, string phone, string email)
        {
            var currUser = GetUser(id);

            if (currUser == null)
            {
                return false; 
            }

            if (phone != null)
            {
                currUser.Phone = phone;   
            }

            if (email != null)
            {
                currUser.Email = email;
            }

            try
            {
                db.Entry(currUser).State = EntityState.Modified; // Mark the entity as modified
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION EX {0}", ex);
                return false;
            }
        }


        public bool AddUserInfo(int id, string UserName, string FirstName, string LastName, string Phone)
        {
            var currUser = GetUser(id);
            if (currUser == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                currUser.UserName = UserName;
            }

            if (!string.IsNullOrEmpty(FirstName))
            {
                currUser.FirstName = FirstName;
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                currUser.LastName = LastName;
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                currUser.Phone = Phone;
            }

            try
            {
                db.Entry(currUser).State = EntityState.Modified; 
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION EX {0}", ex);
                return false;
            }


        }


        public List<UserBookmark> GetBookmarks(int userId)
        {
            return db.UserBookmarks
                .Where(bookmark => bookmark.UserId == userId)
                .ToList();
        }
        
        public UserBookmark? GetBookmark(int id)
        {
            return db.UserBookmarks.Find(id);
        }
        
        public UserBookmark AddBookmark(UserBookmark userBookmark)
        {
            userBookmark.Timestamp = DateTime.Now;
            var bookmark = db.UserBookmarks.Add(userBookmark).Entity;
            db.SaveChanges();
            return bookmark;
        }

        public void UpdateBookmark(int id, UserBookmark userBookmark)
        {
            var bookmark = db.UserBookmarks.Find(id);

            if (bookmark == null)
            {
                return;
            }

            db.UserBookmarks.Entry(bookmark).CurrentValues.SetValues(userBookmark);
        }

        public void DeleteBookmark(int id)
        {
            var bookmark = db.UserBookmarks.Find(id);

            if (bookmark == null)
            {
                return;
            }
            
            db.UserBookmarks.Remove(bookmark);
            db.SaveChanges();
        }

        public void DeleteAllBookmarks(int userId)
        {
            db.UserBookmarks.RemoveRange(db.UserBookmarks
                .Where(bookmark => bookmark.UserId == userId));
            db.SaveChanges();
        }

        public List<UserRating> GetUserRatings(int userId)
        {
            return db.UserRatings
                .Where(rating => rating.UserId == userId)
                .ToList();
        }

        public UserRating? GetUserRating(int userId, string tconst)
        {
            return db.UserRatings
                .FirstOrDefault(rating => rating.UserId == 1 && rating.TConst == tconst);
        }

        public void AddRating(int userId, string tconst, int? rating, string? comment)
        {
            db.Database.ExecuteSqlRaw("SELECT public.rate_movie({0}::int4, {1}::bpchar, {2}::int4, {3}::bpchar) AS result", userId, tconst, rating, comment);
        }
        public void DeleteMovieRating(int userId, string tconst)
        {
            var rating = db.UserRatings.FirstOrDefault(rating => rating.UserId == userId && rating.TConst == tconst);

            if (rating == null)
            {
                return;
            }
            
            db.UserRatings.Remove(rating);
            db.SaveChanges();
        }

        public void DeleteAllMovieRatings()
        {
            throw new NotImplementedException();
        }

	public TitleExtended? SearchByTitle(string searchInput)
        {
            return db.TitleExtendeds
                  .FirstOrDefault(title => title.PrimaryTitle == searchInput);
                   
        }
	public NameBasic? SearchByPersonName(string searchInput)
        {
            return db.NameBasics
                .FirstOrDefault(name => name.Primaryname == searchInput);
        }

        public List<TitleExtended?> SearchTitlebyKeyword(string searchInput) 
        {
            db.Database.ExecuteSqlRaw("SELECT public.exact_match({0}::text) AS result", searchInput);
        } 
    }
}

