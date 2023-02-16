namespace App.Services
{
    public interface ITokenService
    {
        public string GetToken(string email, string role);

    }
}
