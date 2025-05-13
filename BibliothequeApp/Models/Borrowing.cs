using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace BibliothequeApp.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; } 

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime DueDate { get; set; } 

        public DateTime? ReturnDate { get; set; } 

     
        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member? Member { get; set; }
    }
}