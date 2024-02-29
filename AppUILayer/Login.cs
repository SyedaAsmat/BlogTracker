using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APPUILayer
{
    public class Login
    {
        [Key]
        [EmailAddress]
        [Required]
        public string? Email_Id { get; set; }
        [PasswordPropertyText]
        [Required]
        public string? Password { get; set; }
    }
}
