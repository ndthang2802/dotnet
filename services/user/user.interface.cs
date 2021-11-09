using Application.model;
using Application.model.AppData;

namespace Application.services.user
{
    public interface IUserService
    {
        bool Create(RegisterModel user);
        UserModel GetUserByUsername(string username);
    }

}