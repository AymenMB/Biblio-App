using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

namespace BibliothequeApp.Models
{
    public class Book
    {
        [Key] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est requis")]
        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        public string? ISBN { get; set; } 

        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre d'exemplaires doit être positif ou zéro")]
        public int CopiesAvailable { get; set; } = 1; 

        public bool IsAvailable { get; set; } = true; 

    
        public virtual ICollection<Borrowing>? Borrowings { get; set; }
        
        
        [NotMapped]
        public bool IsActuallyAvailable => CopiesAvailable > 0;
    }
}