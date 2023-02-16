using App.Models;
using App.Repository;
using App.Context;

namespace App.Repository
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(MyDBContext context) : base(context) { }

        public User? GetByEmail(string email)
        {
            var user = GetSingle(u => u.Email == email);
                return user;
        }
        public void AddUser(string email, string rights, string name)
        {
            var user = GetSingle(u => u.Email == email);
            if (user == null)
            {
                Add(new User { Email = email, Rights = rights, Name = name });
            }
        }
    }
}
