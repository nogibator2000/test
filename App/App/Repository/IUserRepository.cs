using App.Models;

namespace App.Repository
{
    public interface IUserRepository
    {
        public User? GetByEmail(string email); 
        public void AddUser(string email, string rights, string name);
        void Commit();
    }
}
