using App.InputModels;
using App.Repository;
using App.ResponseModels;

namespace App.Services
{
    public interface ILoginService
    {
        public UserResponse Login(UserLogin model);
        public UserResponse GetUser(string email);


    }
}
