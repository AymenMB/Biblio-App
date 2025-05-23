# BibliothequeApp - Système de Gestion de Bibliothèque

## 📚 Description

BibliothequeApp est une application Windows Forms développée en C# .NET pour la gestion complète d'une bibliothèque. Cette solution permet de gérer les livres, les membres, ainsi que les emprunts et retours de livres. L'application offre une interface intuitive avec un tableau de bord pour visualiser les informations importantes et faciliter la gestion quotidienne d'une bibliothèque.

## 🎯 Fonctionnalités principales

- Gestion des livres (CRUD complet)
- Gestion des membres (CRUD complet)
- Gestion des emprunts et retours
- Tableau de bord avec indicateurs clés
- Import/export des données en CSV
- Système d'alertes pour les livres en retard

## 🛠️ Technologies utilisées

- C# .NET 8.0
- Windows Forms
- Entity Framework Core
- SQL Server (LocalDB)
- CsvHelper pour l'import/export de données

## 📋 Prérequis

- Visual Studio 2022 ou supérieur
- .NET 8.0 SDK
- SQL Server LocalDB (inclus avec Visual Studio)

## 🚀 Installation et démarrage

1. Cloner le dépôt
2. Ouvrir la solution `BibliothequeApp.sln` dans Visual Studio
3. Restaurer les packages NuGet
4. Exécuter les migrations pour créer la base de données:

```powershell
Update-Database
```

5. Démarrer l'application via Visual Studio (F5)

## 🏛️ Architecture du projet

### Structure des dossiers

```
BibliothequeApp/
├─ Data/             # Accès aux données via EF Core
├─ Models/           # Entités du domaine
├─ Migrations/       # Migrations EF Core
├─ MainForm.cs       # Interface principale de l'application
```

### Modèle de conception

L'application suit un modèle de conception simple basé sur trois couches:

- **Couche présentation**: Formulaires Windows Forms
- **Couche métier**: Logique implémentée dans les formulaires
- **Couche d'accès aux données**: Entity Framework Core avec DbContext

## 📊 Diagrammes

### Diagramme de classe

```mermaid
classDiagram
    class Book {
        +int Id
        +string Title
        +string Author
        +string? ISBN
        +int PublicationYear
        +int CopiesAvailable
        +bool IsAvailable
        +ICollection~Borrowing~ Borrowings
        +bool IsActuallyAvailable
    }

    class Member {
        +int Id
        +string Name
        +string Email
        +DateTime RegistrationDate
        +DateTime? SubscriptionEndDate
        +ICollection~Borrowing~ Borrowings
    }

    class Borrowing {
        +int Id
        +int BookId
        +int MemberId
        +DateTime BorrowDate
        +DateTime DueDate
        +DateTime? ReturnDate
        +Book Book
        +Member Member
    }

    class LibraryDbContext {
        +DbSet~Book~ Books
        +DbSet~Member~ Members
        +DbSet~Borrowing~ Borrowings
        +OnConfiguring()
        +OnModelCreating()
    }

    Book "1" --o "*" Borrowing : has
    Member "1" --o "*" Borrowing : makes
    LibraryDbContext -- Book : manages
    LibraryDbContext -- Member : manages
    LibraryDbContext -- Borrowing : manages
```

### Diagramme d'entités-relations

```mermaid
erDiagram
    BOOK ||--o{ BORROWING : "a été emprunté dans"
    BOOK {
        int Id PK
        string Title
        string Author
        string ISBN
        int PublicationYear
        int CopiesAvailable
        bool IsAvailable
    }

    MEMBER ||--o{ BORROWING : "a réalisé"
    MEMBER {
        int Id PK
        string Name
        string Email UK
        datetime RegistrationDate
        datetime SubscriptionEndDate
    }

    BORROWING {
        int Id PK
        int BookId FK
        int MemberId FK
        datetime BorrowDate
        datetime DueDate
        datetime ReturnDate
    }
```

### Diagramme de séquence pour l'emprunt d'un livre

```mermaid
sequenceDiagram
    actor User as Bibliothécaire
    participant UI as Interface Utilisateur
    participant Context as DbContext
    participant Book as Livre
    participant Borrowing as Emprunt

    User->>UI: Sélectionne membre et livre
    User->>UI: Clique sur "Emprunter"
    UI->>Context: Débute une transaction
    Context->>Book: Vérifie disponibilité
    alt Livre disponible
        Book-->>Context: Exemplaires disponibles > 0
        Context->>Borrowing: Crée un nouvel emprunt
        Context->>Book: Décrémente CopiesAvailable
        Book->>Book: Met à jour IsAvailable
        Context->>Context: Sauvegarde les changements
        Context->>UI: Transaction réussie
        UI-->>User: Confirmation d'emprunt
        UI->>UI: Met à jour l'interface
    else Livre non disponible
        Book-->>Context: Exemplaires disponibles = 0
        Context-->>UI: Erreur: livre non disponible
        UI-->>User: Message d'erreur
    end
```

### Diagramme de séquence pour le retour d'un livre

```mermaid
sequenceDiagram
    actor User as Bibliothécaire
    participant UI as Interface Utilisateur
    participant Context as DbContext
    participant Book as Livre
    participant Borrowing as Emprunt

    User->>UI: Sélectionne un emprunt
    User->>UI: Clique sur "Retourner"
    UI->>Context: Débute une transaction
    Context->>Borrowing: Retrouve l'emprunt avec le livre
    alt Emprunt trouvé
        Borrowing->>Borrowing: Définit ReturnDate = DateTime.Now
        Context->>Book: Incrémente CopiesAvailable
        Book->>Book: Met à jour IsAvailable = true
        Context->>Context: Sauvegarde les changements
        Context->>UI: Transaction réussie
        UI-->>User: Confirmation de retour
        UI->>UI: Met à jour l'interface
    else Emprunt non trouvé
        Context-->>UI: Erreur: emprunt non trouvé
        UI-->>User: Message d'erreur
    end
```

## 📝 Règles de gestion

### Gestion des livres

- Un livre doit avoir un titre et un auteur obligatoirement
- L'ISBN est optionnel mais doit être unique s'il est fourni
- Un livre peut avoir plusieurs exemplaires (CopiesAvailable)
- Un livre est considéré disponible si CopiesAvailable > 0

### Gestion des membres

- Un membre doit avoir un nom et un email obligatoirement
- L'email doit être unique
- La date d'inscription est définie automatiquement à la date actuelle
- La date de fin d'abonnement est optionnelle

### Gestion des emprunts

- Un membre ne peut emprunter que des livres disponibles
- La date de retour prévue est définie lors de l'emprunt
- Un livre avec des emprunts en cours ne peut pas être supprimé
- Un membre avec des emprunts en cours ne peut pas être supprimé
- Lors de l'emprunt, le nombre d'exemplaires disponibles est décrémenté
- Lors du retour, le nombre d'exemplaires disponibles est incrémenté

### Autres fonctionnalités

- Les emprunts en retard sont mis en évidence dans l'interface
- Possibilité d'envoyer des alertes par email pour les retards
- Import/export des données en CSV pour les livres et les membres
- Recherche de livres et de membres par différents critères

## 👥 Contribuer

Les contributions sont les bienvenues ! N'hésitez pas à soumettre des pull requests.

---

**Ce projet est réalisé de la part de Aymen Mabrouk et Mohammed Amine Ben Charrada à Polytechnique Sousse.**
