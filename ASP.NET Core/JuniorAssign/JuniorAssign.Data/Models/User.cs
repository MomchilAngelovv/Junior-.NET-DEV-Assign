namespace JuniorAssign.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Contragents = new List<Contragent>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public virtual IList<Contragent> Contragents { get; set; }
    }
}
