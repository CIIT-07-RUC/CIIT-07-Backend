using System;
using DataLayer.Models;

namespace DataLayer
{
	public class DataLayer : IDataService
	{
        Cit07Context db = new();

        public DataLayer()
        {
        }

        public User GetUser(int id)
        {
            var currUser = db.Users.FirstOrDefault(x => x.Id == id);
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(string email, string password, string password2, string phone)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUserInfo(int id)
        {
            throw new NotImplementedException();
        }
    }
}

