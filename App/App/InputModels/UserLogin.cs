
using System.ComponentModel.DataAnnotations;

namespace App.InputModels
{
    public class UserLogin
    {
        [Required]
            public string? Email { get; set; }
    }
}