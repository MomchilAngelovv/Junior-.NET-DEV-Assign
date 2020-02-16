namespace JuniorAssign.Web.Models.Contragents
{
    using System.ComponentModel.DataAnnotations;

    public class ContragentCreateInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string VatNumber { get; set; }
    }
}
