﻿using Base.Interfaces;
using Base.Models;
using Base.Services;
using BaseApi.Extensions;
using BaseApi.Services;

namespace BaoCust.Services
{
    public class MyBaseUserService : IBaseUserSvc
    {
        //get base user info
        public BaseUserDto GetData()
        {
            return _Http.GetSession().Get<BaseUserDto>(_Fun.FidBaseUser)!;   //extension method
        }
    }
}
