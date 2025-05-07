using Microsoft.EntityFrameworkCore;
using BibliothequeApp.Models; // Accès aux modèles

namespace BibliothequeApp.Data
{
    public class LibraryDbContext : DbContext
    {
        // Déclarez un DbSet pour chaque entité (table)
        public DbSet<Book> Books { get; set; } = null!; // null! pour indiquer qu'il sera initialisé par EF Core
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Borrowing> Borrowings { get; set; } = null!;

        // Configuration de la connexion à la base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Chaîne de connexion pour SQL Server Express LocalDB
                // La base de données s'appellera "LibraryDB"
                // (LocalDB)\MSSQLLocalDB est le nom de serveur par défaut pour LocalDB
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;");
            }
        }

        // Optionnel : Configuration plus fine des relations (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple : S'assurer que la relation est bien configurée (même si les annotations suffisent souvent)
            modelBuilder.Entity<Borrowing>()
               .HasOne(b => b.Book) // Un emprunt a un livre
               .WithMany(bk => bk.Borrowings) // Un livre peut avoir plusieurs emprunts
               .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrowing>()
               .HasOne(b => b.Member) // Un emprunt a un membre
               .WithMany(m => m.Borrowings) // Un membre peut avoir plusieurs emprunts
               .HasForeignKey(b => b.MemberId);

            // Vous pourriez ajouter des contraintes uniques ici, par ex. sur l'email du membre
            modelBuilder.Entity<Member>()
               .HasIndex(m => m.Email)
               .IsUnique();

            // Vous pourriez aussi ajouter des données initiales (seeding) ici si nécessaire
        }
    }
}