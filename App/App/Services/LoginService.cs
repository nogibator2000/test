using App.InputModels;
using App.Models;
using App.Repository;
using App.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Services
{
    public class LoginService:ILoginService
    {
        private IUserRepository userRepository;
        public LoginService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserResponse Login(UserLogin model)
        {
            if (model == null || model.Email == null)
            {
                throw new Exception("модель пустая");
            }
            return GetUser(model.Email);
        }
        public UserResponse GetUser(string email)
        {
            var ur = new UserResponse();
            var user = userRepository.GetByEmail(email);
            if (user != null && user.Email != null && (user.Rights == User.USER || user.Rights == User.ADMIN))
            {
                ur.Email = user.Email;
                ur.Rights = user.Rights;
                ur.Name = user.Name;
                ur.Authenticated = true;
                var ts = new TokenService();
                ur.Token = ts.GetToken(user.Email, user.Rights);
                return ur;
            }
            throw new Exception("ошибка логина");
        }
    }
}
