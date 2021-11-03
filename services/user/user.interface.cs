using System;
using Application.model.Authentication;
using Application.model.AppData;

using Application.entities;
using System.Collections.Generic;
namespace Application.services.user
{
    public interface IUserService
    {
        bool Create(RegisterModel user);
        UserModel GetUserByUsername(string username);
    }

}