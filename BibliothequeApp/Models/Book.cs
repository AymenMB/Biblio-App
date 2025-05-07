using System.ComponentModel.DataAnnotations; // Nécessaire pour [Key]
using System.ComponentModel.DataAnnotations.Schema; // Nécessaire pour [NotMapped]

namespace BibliothequeApp.Models
{
    public class Book
    {
        [Key] // Indique que Id est la clé primaire
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")] // Validation simple
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est requis")]
        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        public string? ISBN { get; set; } // Nullable string

        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre d'exemplaires doit être positif ou zéro")]
        public int CopiesAvailable { get; set; } = 1; // Par défaut un seul exemplaire disponible

        public bool IsAvailable { get; set; } = true; // Sera calculé en fonction de CopiesAvailable

        // Propriété de navigation pour les emprunts (relation un-à-plusieurs)
        public virtual ICollection<Borrowing>? Borrowings { get; set; }
        
        // Propriété calculée pour compatibilité arrière
        [NotMapped] // Ne pas stocker cette propriété dans la base de données
        public bool IsActuallyAvailable => CopiesAvailable > 0;
    }
}