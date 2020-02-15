namespace JuniorAssign.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Contragent
    {
        public Contragent()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string VatNumber { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
