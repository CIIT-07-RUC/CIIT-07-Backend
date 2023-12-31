﻿using System;
using AutoMapper;
using DataLayer.Models;

namespace WebAPI.Models.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserModel>();
            CreateMap<User, UserListModel>();
            CreateMap<User, UpdateUserModel>();
            CreateMap<User, DeactivateUserAccountModel>();
            CreateMap<User, AddInformationUserModel>();
        }
    }
} 

