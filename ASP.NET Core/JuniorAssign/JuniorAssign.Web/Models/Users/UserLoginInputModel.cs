namespace JuniorAssign.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
