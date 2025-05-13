using Microsoft.EntityFrameworkCore;
using BibliothequeApp.Models; 

namespace BibliothequeApp.Data
{
    public class LibraryDbContext : DbContext
    {
      
        public DbSet<Book> Books { get; set; } = null!; 
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Borrowing> Borrowings { get; set; } = null!;

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;");
            }
        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Borrowing>()
               .HasOne(b => b.Book) 
               .WithMany(bk => bk.Borrowings) 
               .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrowing>()
               .HasOne(b => b.Member) 
               .WithMany(m => m.Borrowings)
               .HasForeignKey(b => b.MemberId);

          
            modelBuilder.Entity<Member>()
               .HasIndex(m => m.Email)
               .IsUnique();

       
        }
    }
}