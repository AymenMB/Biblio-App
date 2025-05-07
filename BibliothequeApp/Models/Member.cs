using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Pour ICollection

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

        // Abonnement ? On pourrait ajouter une date de fin d'abonnement, un type, etc.
        public DateTime? SubscriptionEndDate { get; set; } // Nullable DateTime

        // Propriété de navigation pour les emprunts
        public virtual ICollection<Borrowing>? Borrowings { get; set; }
    }
}