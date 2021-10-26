using System;
using System.Linq;
using Application.model.Authentication;
using Application.entities;
using Application.helpers;
using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
namespace Application.services.user
{
    public class UserService : IUserService
    {
        private DataContext _dataContex;

        public UserService(DataContext dataContext)
        {
            this._dataContex = dataContext;
        }

        public bool Create(RegisterModel user)
        {
            if (_dataContex.Users.Any(u=>u.username == user.username))
            {
                throw new Exception("Username taken");
            }
            if (_dataContex.Users.Any(u=>u.phoneNumber == user.phoneNumber))
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
            

            _dataContex.Users.Add(db_user);

            var state = _dataContex.SaveChanges();

            if (state > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("st wrong");
            }

        }
        public List<User> GetUserBy()
        {
            return _dataContex.Users.ToList();
        }

        
    } 

}