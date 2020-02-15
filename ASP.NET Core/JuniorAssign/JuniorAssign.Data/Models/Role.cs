namespace JuniorAssign.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        public Role()
        {
            this.UserRoles = new List<UserRole>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
