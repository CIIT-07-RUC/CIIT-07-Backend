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
            Console.WriteLine("ID ID ID {0}", id);
            var user = db.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
            Console.WriteLine("CURR USER {0}", user);
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = db.Users
                .Where(x => x.Email == email)
                .Select(p => new User
                {
                    UserName = p.UserName,
                    LastName = p.LastName,
                    Email = p.Email
                })
                .FirstOrDefault();
            return user;
        }

        public List<User> GetUsers()
        {
            var users = db.Users
                .Select(p => new User
                {
                    UserName = p.UserName,
                    LastName = p.LastName,
                    Email = p.Email
                })
                .ToList();
            return users;
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
            Console.WriteLine("TEST ME UP {0} {1} {2}", id, phone, email);
            var currUser = GetUser(id);
            Console.WriteLine("CURR USER {0}", currUser.Id);

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
                Console.WriteLine("TU SOM phone -- {0} email {1}", currUser.Email, currUser.Phone);
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
    }
}

