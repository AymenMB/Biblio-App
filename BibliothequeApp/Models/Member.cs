using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace BibliothequeApp.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

      
        public DateTime? SubscriptionEndDate { get; set; }

       
        public virtual ICollection<Borrowing>? Borrowings { get; set; }
    }
}