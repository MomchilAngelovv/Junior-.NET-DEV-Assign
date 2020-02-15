namespace JuniorAssign.Api.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateUserInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
