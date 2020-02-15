namespace JuniorAssign.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RepeatPassword { get; set; }
    }
}
