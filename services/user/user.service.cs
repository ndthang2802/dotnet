using System;
using System.Linq;
using Application.model.Authentication;
using Application.model.AppData;
using Application.entities;
using Application.helpers;
using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
namespace Application.services.user
{
    public class UserService : IUserService
    {
        private DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool Create(RegisterModel user)
        {
            if (_dataContext.Users.Any(u=>u.username == user.username))
            {
                throw new Exception("Username taken");
            }
            if (_dataContext.Users.Any(u=>u.phoneNumber == user.phoneNumber))
            {
                throw new Exception("Phone number taken");
            }

            user.password = BC.HashPassword(user.password);

            Guid guid = Guid.NewGuid();
            
            var db_user = new User();

            db_user.Id = guid.ToString();
            db_user.username = user.username;
            db_user.password = user.password;
            db_user.displayName = user.displayName;
            db_user.phoneNumber = user.phoneNumber;
            db_user.RefreshTokenExpiryTime = DateTime.Now;
            

            _dataContext.Users.Add(db_user);

            var state = _dataContext.SaveChanges();

            if (state > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("st wrong");
            }

        }
        public UserModel GetUserByUsername(string username)
        {
            User user =  _dataContext.Users.FirstOrDefault(u => u.username == username);
            return  new UserModel(user.Id,user.username,user.phoneNumber,user.displayName);
        }

        
    } 

}