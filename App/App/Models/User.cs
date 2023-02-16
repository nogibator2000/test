
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class User :IEntity
    {
        public const string ADMIN = "admin";
        public const string USER = "user";
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Rights { get; set; }
        public virtual List<Record> Records { get; set; }

    }
}