using System;
using Application.model.Authentication;
using Application.entities;
using System.Collections.Generic;
namespace Application.services.user
{
    public interface IUserService
    {
        bool Create(RegisterModel user);
        List<User> GetUserBy();
    }

}