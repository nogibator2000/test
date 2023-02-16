
namespace App.ResponseModels
{
    public class UserResponse
    {
        public string? Rights { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Token { get; set; }
        public bool Authenticated { get; set; }
    }
}