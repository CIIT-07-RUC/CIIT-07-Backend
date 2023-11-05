﻿using System;
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

		#if haveMoreTime
			// Implement additional functions
			public bool DeactiveUser(int id);
			public bool SetAdminprivilege(bool isAdmin);
		#endif
    }
}

