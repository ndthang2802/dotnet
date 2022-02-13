using Application.model;

namespace Application.services.user
{
    public interface IUserService
    {
        bool Create(RegisterModel user);
        UserModel GetUserByUsername(string username);
    }

}