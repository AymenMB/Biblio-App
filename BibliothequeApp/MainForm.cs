using BibliothequeApp.Data;
using BibliothequeApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens; // Pas n�cessaire ici, peut �tre supprim� si ajout� pr�c�demment
using System;
using System.Collections.Generic;
using System.Drawing; // Ajout pour Color
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BibliothequeApp
{
    public partial class MainForm : Form
    {
        // Instance unique du DbContext pour ce formulaire
        private readonly LibraryDbContext _context = new LibraryDbContext();

        public MainForm()
        {
            InitializeComponent();
        }

        // M�thode appel�e au chargement initial du formulaire
        private async void MainForm_Load(object sender, EventArgs e)
        {
            // D�finir une date par d�faut pour le DateTimePicker (ex: 2 semaines � partir d'aujourd'hui)
            dtpDueDate.Value = DateTime.Now.AddDays(14);
            dtpDueDate.MinDate = DateTime.Now.Date; // Emp�cher s�lection date pass�e

            // Charger toutes les donn�es initiales
            await LoadBooksAsync();
            await LoadMembersAsync();
            await LoadBorrowComboBoxesAsync(); // Charger apr�s livres/membres
            await LoadBorrowingsAsync();
            UpdateDashboard(); // Mettre � jour le tableau de bord
        }

        // Assurez-vous de lib�rer le contexte lorsque le formulaire est ferm�
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose(); // Tr�s important pour lib�rer les ressources DB
            base.OnFormClosing(e);
        }

        #region Gestion Livres

        // Charger les livres dans le DataGridView dgvBooks
        private async Task LoadBooksAsync(string? searchTerm = null)
        {
            dgvBooks.DataSource = null; // D�binder temporairement

            IQueryable<Book> query = _context.Books.AsNoTracking(); // AsNoTracking pour la lecture

            // DANS LoadBooksAsync - CODE CORRIG� � utiliser
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Pas besoin de ToLowerInvariant ici
                string likePattern = $"%{searchTerm}%"; // Cr�e le pattern pour SQL LIKE '%termerecherche%'

                // Utiliser EF.Functions.Like qui se traduit en SQL LIKE
                // La sensibilit� � la casse d�pendra de la collation de votre base de donn�es
                query = query.Where(b => EF.Functions.Like(b.Title, likePattern) ||
                                         EF.Functions.Like(b.Author, likePattern) ||
                                         (b.ISBN != null && EF.Functions.Like(b.ISBN, likePattern)));
            }

            var books = await query.OrderBy(b => b.Title).ToListAsync();

            // Configuration manuelle des colonnes (meilleur contr�le et ordre)
            if (!dgvBooks.Columns.Contains("Id")) // Configurer seulement si non d�j� fait
            {
                dgvBooks.Columns.Clear();
                dgvBooks.AutoGenerateColumns = false;

                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Visible = false });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Titre", DataPropertyName = "Title", Width = 250 });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Auteur", DataPropertyName = "Author", Width = 150 });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "ISBN", HeaderText = "ISBN", DataPropertyName = "ISBN", Width = 120 });
                // Appliquer l'alignement pour l'ann�e via DefaultCellStyle
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "PublicationYear",
                    HeaderText = "Ann�e",
                    DataPropertyName = "PublicationYear",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
                });
                dgvBooks.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsAvailable", HeaderText = "Disponible", DataPropertyName = "IsAvailable", Width = 80 });
            }

            dgvBooks.DataSource = books; // Relier les donn�es
        }

        // Effacer les champs de saisie du livre
        private void ClearBookFields()
        {
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            txtBookISBN.Clear();
            txtBookYear.Clear();
            numCopiesAvailable.Value = 1; // Réinitialiser le nombre d'exemplaires à 1
            dgvBooks.ClearSelection(); // Désélectionner la ligne dans la grille
        }

        // Bouton Effacer Champs (Livres)
        private void btnClearBookFields_Click(object sender, EventArgs e)
        {
            ClearBookFields();
        }

        // Bouton Rechercher (Livres)
        private async void btnSearchBook_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBookTitle.Text.Trim(); // Utiliser le champ titre pour rechercher
            await LoadBooksAsync(searchTerm);
        }

        // Bouton Ajouter Livre
        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text))
            {
                MessageBox.Show("Le titre et l'auteur sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int year = 0; // Valeur par défaut si vide
            if (!string.IsNullOrWhiteSpace(txtBookYear.Text) && !int.TryParse(txtBookYear.Text, out year))
            {
                MessageBox.Show("L'année de publication doit être un nombre valide ou vide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validation année plus stricte si désiré (ex: > 1500 et <= année actuelle+1)
            if (year != 0 && (year < 1000 || year > DateTime.Now.Year + 1))
            {
                MessageBox.Show("L'année de publication semble invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier si l'ISBN existe déjà (uniquement s'il est fourni)
            if (!string.IsNullOrWhiteSpace(txtBookISBN.Text))
            {
                string isbn = txtBookISBN.Text.Trim();
                bool isbnExists = await _context.Books.AnyAsync(b => b.ISBN == isbn);
                if (isbnExists)
                {
                    MessageBox.Show("Un livre avec cet ISBN existe déjà dans la base de données.", 
                        "ISBN dupliqué", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var newBook = new Book
            {
                Title = txtBookTitle.Text.Trim(),
                Author = txtBookAuthor.Text.Trim(),
                ISBN = string.IsNullOrWhiteSpace(txtBookISBN.Text) ? null : txtBookISBN.Text.Trim(),
                PublicationYear = year, // Stocke 0 si vide/invalide mais accepté
                CopiesAvailable = (int)numCopiesAvailable.Value, // Utiliser la valeur du NumericUpDown
                IsAvailable = true // Un nouveau livre est toujours disponible
            };

            try
            {
                // Vérifier si un livre très similaire existe déjà (optionnel)
                bool maybeExists = await _context.Books.AnyAsync(b => b.Title == newBook.Title && b.Author == newBook.Author);
                if (maybeExists)
                {
                    var confirm = MessageBox.Show("Un livre avec le même titre et auteur existe déjà. Voulez-vous l'ajouter quand même ?",
                                                   "Doublon Potentiel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.No) return;
                }

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                MessageBox.Show("Livre ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearBookFields();
                await LoadBooksAsync(); // Recharger la liste
                await LoadBorrowComboBoxesAsync(); // Mettre à jour la liste des livres empruntables
                UpdateDashboard();      // Mettre à jour le TdB
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du livre : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Pour débug: Console.WriteLine(ex.InnerException?.Message);
            }
        }

        // Bouton Mettre à Jour Livre
        private async void btnUpdateBook_Click(object sender, EventArgs e)
        {
            // Vérifier qu'une ligne est sélectionnée
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un livre à mettre à jour.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Récupérer l'ID du livre sélectionné
            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;

            // Récupérer le livre à mettre à jour depuis la base de données
            var bookToUpdate = await _context.Books.FindAsync(bookId);
            if (bookToUpdate == null)
            {
                MessageBox.Show("Livre non trouvé dans la base de données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadBooksAsync(); // Recharger la liste des livres
                return;
            }

            // Vérifier que les champs obligatoires sont remplis
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text))
            {
                MessageBox.Show("Le titre et l'auteur sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Valider l'année (si fournie)
            int year = 0;
            if (!string.IsNullOrWhiteSpace(txtBookYear.Text) && !int.TryParse(txtBookYear.Text, out year))
            {
                MessageBox.Show("L'année de publication doit être un nombre valide ou vide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Validation stricte de l'année
            if (year != 0 && (year < 1000 || year > DateTime.Now.Year + 1))
            {
                MessageBox.Show("L'année de publication semble invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier si l'ISBN est unique (s'il est modifié)
            string isbn = string.IsNullOrWhiteSpace(txtBookISBN.Text) ? null : txtBookISBN.Text.Trim();
            if (isbn != null && isbn != bookToUpdate.ISBN)
            {
                bool isbnExists = await _context.Books.AnyAsync(b => b.ISBN == isbn && b.Id != bookId);
                if (isbnExists)
                {
                    MessageBox.Show("Un autre livre avec cet ISBN existe déjà dans la base de données.", 
                        "ISBN dupliqué", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Confirmer la mise à jour
            var confirmResult = MessageBox.Show($"Êtes-vous sûr de vouloir mettre à jour le livre '{bookToUpdate.Title}' ?",
                "Confirmation de mise à jour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Mettre à jour les propriétés du livre
                    bookToUpdate.Title = txtBookTitle.Text.Trim();
                    bookToUpdate.Author = txtBookAuthor.Text.Trim();
                    bookToUpdate.ISBN = isbn;
                    bookToUpdate.PublicationYear = year;
                    bookToUpdate.CopiesAvailable = (int)numCopiesAvailable.Value;
                    bookToUpdate.IsAvailable = bookToUpdate.CopiesAvailable > 0;

                    await _context.SaveChangesAsync();
                    MessageBox.Show("Livre mis à jour avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Recharger les données
                    await LoadBooksAsync();
                    await LoadBorrowComboBoxesAsync(); // Mettre à jour la liste des livres empruntables
                    UpdateDashboard();
                    
                    // Sélectionner la ligne mise à jour dans le DataGridView
                    foreach (DataGridViewRow row in dgvBooks.Rows)
                    {
                        if ((int)row.Cells["Id"].Value == bookId)
                        {
                            row.Selected = true;
                            dgvBooks.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise à jour du livre : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Bouton Supprimer Livre
        private async void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez s�lectionner un livre � supprimer.", "S�lection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value; // R�cup�re l'ID cach�

            var bookToDelete = await _context.Books.FindAsync(bookId);

            if (bookToDelete == null)
            {
                MessageBox.Show("Livre non trouv� (peut-�tre d�j� supprim�).", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadBooksAsync(); // Recharger pour �tre s�r
                return;
            }

            // V�rifier s'il est emprunt� ACTUELLEMENT
            bool isCurrentlyBorrowed = await _context.Borrowings.AnyAsync(b => b.BookId == bookId && b.ReturnDate == null);
            if (isCurrentlyBorrowed)
            {
                MessageBox.Show($"Le livre '{bookToDelete.Title}' est actuellement emprunt� et ne peut pas �tre supprim�.", "Op�ration impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"�tes-vous s�r de vouloir supprimer le livre '{bookToDelete.Title}' ?\nCeci supprimera aussi son historique d'emprunts.",
                                               "Confirmation de suppression",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Supprimer d'abord les enregistrements d'emprunts li�s (pass�s)
                    // Note: Si la cl� �trang�re dans la BD a ON DELETE CASCADE, ce n'est pas strictement n�cessaire.
                    // Mais le faire explicitement est plus s�r et ind�pendant de la config BD.
                    var relatedBorrowings = _context.Borrowings.Where(b => b.BookId == bookId);
                    if (await relatedBorrowings.AnyAsync())
                    {
                        _context.Borrowings.RemoveRange(relatedBorrowings);
                    }

                    _context.Books.Remove(bookToDelete);
                    await _context.SaveChangesAsync(); // Sauvegarde la suppression du livre et des emprunts li�s

                    MessageBox.Show("Livre supprim� avec succ�s !", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBookFields(); // Effacer les champs car le livre s�lectionn� n'existe plus
                    await LoadBooksAsync(); // Recharger la liste des livres
                    await LoadBorrowComboBoxesAsync(); // Mettre � jour la liste des livres empruntables
                    UpdateDashboard();      // Mettre � jour le TdB
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"Erreur de base de donn�es lors de la suppression : {dbEx.InnerException?.Message ?? dbEx.Message}", "Erreur BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur inattendue lors de la suppression : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Bouton Export Livres (CSV)
        private async void btnExportBooks_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*";
                saveFileDialog.Title = "Exporter la liste des livres";
                saveFileDialog.FileName = $"Export_Livres_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        var booksToExport = await _context.Books.AsNoTracking().OrderBy(b => b.Title).ToListAsync();

                        // Utiliser UTF8 avec BOM pour meilleure compatibilit� Excel FR
                        // Utiliser la culture fr-FR pour que les bool�ens soient VRAI/FAUX et le s�parateur ';'
                        var culture = CultureInfo.GetCultureInfo("fr-FR");
                        var config = new CsvConfiguration(culture); // Appliquer la culture

                        using (var writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8)) // UTF8 pour l'encodage
                        using (var csv = new CsvWriter(writer, config)) // Utiliser la config avec culture FR
                        {
                            writer.Write('\uFEFF'); // Write UTF-8 BOM
                            await csv.WriteRecordsAsync(booksToExport);
                        }

                        MessageBox.Show($"Liste des livres export�e avec succ�s vers:\n{saveFileDialog.FileName}", "Export Termin�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'export des livres : {ex.Message}", "Erreur d'Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        // Gestionnaire d'événement pour remplir les champs quand un livre est sélectionné dans le DataGridView
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            // Vérifier qu'une ligne est sélectionnée et qu'elle contient des données
            if (dgvBooks.SelectedRows.Count > 0 && !dgvBooks.SelectedRows[0].IsNewRow)
            {
                DataGridViewRow row = dgvBooks.SelectedRows[0];
                
                // Remplir les champs avec les données du livre sélectionné
                txtBookTitle.Text = row.Cells["Title"].Value?.ToString() ?? string.Empty;
                txtBookAuthor.Text = row.Cells["Author"].Value?.ToString() ?? string.Empty;
                txtBookISBN.Text = row.Cells["ISBN"].Value?.ToString() ?? string.Empty;
                
                // Pour l'année, vérifier si c'est une valeur valide
                if (row.Cells["PublicationYear"].Value != null && row.Cells["PublicationYear"].Value != DBNull.Value)
                {
                    int year = Convert.ToInt32(row.Cells["PublicationYear"].Value);
                    txtBookYear.Text = year > 0 ? year.ToString() : string.Empty;
                }
                else
                {
                    txtBookYear.Text = string.Empty;
                }
                
                // Pour le nombre d'exemplaires, vérifier si la colonne CopiesAvailable existe
                // C'est une bonne pratique de vérifier car les colonnes peuvent changer
                if (row.DataGridView.Columns.Contains("CopiesAvailable") && 
                    row.Cells["CopiesAvailable"].Value != null && 
                    row.Cells["CopiesAvailable"].Value != DBNull.Value)
                {
                    numCopiesAvailable.Value = Math.Max(1, Convert.ToInt32(row.Cells["CopiesAvailable"].Value));
                }
                else
                {
                    numCopiesAvailable.Value = 1; // Valeur par défaut
                }
            }
        }

        #endregion

        #region Gestion Membres

        // Charger les membres dans le DataGridView dgvMembers
        private async Task LoadMembersAsync(string? searchTerm = null)
        {
            dgvMembers.DataSource = null;

            IQueryable<Member> query = _context.Members.AsNoTracking();

            // Dans LoadMembersAsync:
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Pas besoin de mettre searchTerm en minuscules ici
                string likePattern = $"%{searchTerm}%"; // Cr�e le pattern pour SQL LIKE

                // Utilisez EF.Functions.Like
                query = query.Where(m => EF.Functions.Like(m.Name, likePattern) ||
                                         EF.Functions.Like(m.Email, likePattern));
            }

            var members = await query.OrderBy(m => m.Name).ToListAsync();

            // Configurer colonnes si n�cessaire
            if (!dgvMembers.Columns.Contains("Id"))
            {
                dgvMembers.Columns.Clear();
                dgvMembers.AutoGenerateColumns = false;

                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Visible = false });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Nom", DataPropertyName = "Name", Width = 200 });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 250 });
                // CORRIG�: Appliquer Format via DefaultCellStyle
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "RegistrationDate",
                    HeaderText = "Inscription",
                    DataPropertyName = "RegistrationDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" } // Format date courte
                });
                // CORRIG�: Appliquer Format et NullValue via DefaultCellStyle
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SubscriptionEndDate",
                    HeaderText = "Fin Abon.",
                    DataPropertyName = "SubscriptionEndDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d", NullValue = "N/A" } // G�rer null et formater date
                });
            }

            dgvMembers.DataSource = members;
        }

        // Effacer les champs de saisie membre
        private void ClearMemberFields()
        {
            txtMemberName.Clear();
            txtMemberEmail.Clear();
            dgvMembers.ClearSelection(); // Désélectionner la ligne dans la grille
            // Si vous avez d'autres champs comme dtpSubscriptionEnd ou un champ pour la date d'abonnement
            // vous pourriez aussi les réinitialiser ici
        }

        // Bouton Effacer Champs (Membres)
        private void btnClearMemberFields_Click(object sender, EventArgs e)
        {
            ClearMemberFields();
        }

        // Bouton Rechercher (Membres)
        private async void btnSearchMember_Click(object sender, EventArgs e)
        {
            string searchTerm = txtMemberName.Text.Trim(); // Utiliser nom pour rechercher
            await LoadMembersAsync(searchTerm);
        }

        // Bouton Inscrire Membre
        private async void btnAddMember_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberName.Text) || string.IsNullOrWhiteSpace(txtMemberEmail.Text))
            {
                MessageBox.Show("Le nom et l'email sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation Email simple (format)
            string emailToAdd = txtMemberEmail.Text.Trim();
            try { var _ = new MailAddress(emailToAdd); }
            catch
            {
                MessageBox.Show("Le format de l'email est invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // V�rifier unicit� email (insensible � la casse)
            bool emailExists = await _context.Members.AnyAsync(m => m.Email.ToLower() == emailToAdd.ToLower());
            if (emailExists)
            {
                MessageBox.Show("Cet email est d�j� utilis� par un autre membre.", "Email dupliqu�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newMember = new Member
            {
                Name = txtMemberName.Text.Trim(),
                Email = emailToAdd, // Stocker avec la casse d'origine ou normalis� ? Ici origine.
                RegistrationDate = DateTime.Now
                // SubscriptionEndDate = ... // G�rer si DateTimePicker ajout�
            };

            try
            {
                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                MessageBox.Show("Membre inscrit avec succ�s !", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearMemberFields();
                await LoadMembersAsync();
                await LoadBorrowComboBoxesAsync(); // Mettre � jour la liste des membres emprunteurs
                UpdateDashboard();
            }
            catch (DbUpdateException dbEx) // G�rer erreur BD (peut arriver si check unicit� a �chou� entre temps)
            {
                var sqlEx = dbEx.InnerException as Microsoft.Data.SqlClient.SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601)) // Erreur contrainte unique SQL Server
                {
                    MessageBox.Show("Erreur : L'email fourni existe d�j� (conflit d�tect� lors de la sauvegarde).", "Erreur de Base de Donn�es", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erreur base de donn�es lors de l'inscription : {dbEx.InnerException?.Message ?? dbEx.Message}", "Erreur BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue lors de l'inscription : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bouton Supprimer Membre
        private async void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez s�lectionner un membre � supprimer.", "S�lection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId = (int)dgvMembers.SelectedRows[0].Cells["Id"].Value;
            var memberToDelete = await _context.Members.FindAsync(memberId);

            if (memberToDelete == null)
            {
                MessageBox.Show("Membre non trouv�.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadMembersAsync();
                return;
            }

            // V�rifier emprunts actifs
            bool hasActiveBorrowings = await _context.Borrowings.AnyAsync(b => b.MemberId == memberId && b.ReturnDate == null);
            if (hasActiveBorrowings)
            {
                MessageBox.Show($"Le membre '{memberToDelete.Name}' a des livres actuellement emprunt�s et ne peut pas �tre supprim�.", "Op�ration impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"�tes-vous s�r de vouloir supprimer le membre '{memberToDelete.Name}' ({memberToDelete.Email}) ?\nCeci supprimera aussi son historique d'emprunts.",
                                               "Confirmation de suppression",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Supprimer les emprunts pass�s li�s
                    var relatedBorrowings = _context.Borrowings.Where(b => b.MemberId == memberId);
                    if (await relatedBorrowings.AnyAsync())
                    {
                        _context.Borrowings.RemoveRange(relatedBorrowings);
                    }

                    _context.Members.Remove(memberToDelete);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Membre supprim� avec succ�s !", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearMemberFields(); // Effacer car membre s�lectionn� n'existe plus
                    await LoadMembersAsync();
                    await LoadBorrowComboBoxesAsync(); // Mettre � jour liste emprunteurs
                    UpdateDashboard();
                }
                catch (DbUpdateException dbEx) { MessageBox.Show($"Erreur BD suppression membre: {dbEx.InnerException?.Message ?? dbEx.Message}", "Erreur BD", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Erreur suppression membre: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // Bouton Export Membres (CSV) - Similaire � Export Livres
        private async void btnExportMembers_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*";
                saveFileDialog.Title = "Exporter la liste des membres";
                saveFileDialog.FileName = $"Export_Membres_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        var membersToExport = await _context.Members.AsNoTracking().OrderBy(m => m.Name).ToListAsync();
                        var culture = CultureInfo.GetCultureInfo("fr-FR"); // Pour s�parateur ';' et format dates/bool
                        var config = new CsvConfiguration(culture);

                        using (var writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                        using (var csv = new CsvWriter(writer, config))
                        {
                            writer.Write('\uFEFF'); // BOM
                            await csv.WriteRecordsAsync(membersToExport);
                        }
                        MessageBox.Show($"Liste des membres export�e avec succ�s vers:\n{saveFileDialog.FileName}", "Export Termin�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show($"Erreur lors de l'export des membres : {ex.Message}", "Erreur d'Export", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        // --- DANS MainForm.cs ---

        // Bouton Import Livres (CSV) - CORRIG� v3
        private async void btnImportBooks_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*";
                openFileDialog.Title = "Importer une liste de livres depuis un CSV";

                if (openFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    this.Cursor = Cursors.WaitCursor;
                    int importedCount = 0;
                    int skippedCount = 0;
                    var errors = new List<string>(); // D�clar� avant config

                    var culture = CultureInfo.GetCultureInfo("fr-FR");
                    CsvReader csv = null; // D�clarer en dehors pour �tre capturable par la lambda ci-dessous

                    var config = new CsvConfiguration(culture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                        // *** CORRECTION FINALE : Utiliser csv.Context.Parser.Row ***
                        // 'args' ici est de type BadDataFoundArgs
                        BadDataFound = args => {
                            // V�rifier si csv a �t� initialis� (s�curit�, m�me si peu probable ici)
                            if (csv != null)
                            {
                                errors.Add($"Donn�e invalide ligne {csv.Context.Parser.Row}: {args.RawRecord}");
                            }
                            else
                            {
                                errors.Add($"Donn�e invalide (contexte lecteur non dispo): {args.RawRecord}");
                            }
                        },
                        TrimOptions = TrimOptions.Trim,
                        IgnoreBlankLines = true,
                    };

                    try
                    {
                        List<Book> booksToAdd = new List<Book>();
                        using (var reader = new StreamReader(openFileDialog.FileName, System.Text.Encoding.UTF8, true))
                        // Initialiser la variable 'csv' ici
                        using (csv = new CsvReader(reader, config))
                        {
                            await csv.ReadAsync();
                            csv.ReadHeader();

                            while (await csv.ReadAsync())
                            {
                                Book record = null;
                                try
                                {
                                    record = csv.GetRecord<Book>();
                                }
                                catch (Exception readEx)
                                {
                                    errors.Add($"Erreur lecture ligne {csv.Context.Parser.Row}: {readEx.Message}");
                                    skippedCount++;
                                    continue;
                                }

                                // ... (reste de la validation et logique d'ajout comme avant) ...
                                if (record == null || string.IsNullOrWhiteSpace(record.Title) || string.IsNullOrWhiteSpace(record.Author))
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} saut�e (Titre/Auteur requis): {csv.Context.Parser.RawRecord}");
                                    skippedCount++;
                                    continue;
                                }

                                bool exists = false;
                                var trimmedIsbn = string.IsNullOrWhiteSpace(record.ISBN) ? null : record.ISBN.Trim();
                                var trimmedTitle = record.Title.Trim();
                                var trimmedAuthor = record.Author.Trim();

                                if (!string.IsNullOrWhiteSpace(trimmedIsbn))
                                {
                                    exists = await _context.Books.AnyAsync(b => b.ISBN == trimmedIsbn);
                                }
                                if (!exists)
                                {
                                    exists = await _context.Books.AnyAsync(b => b.Title == trimmedTitle && b.Author == trimmedAuthor);
                                }
                                if (!exists)
                                {
                                    exists = booksToAdd.Any(b => (b.ISBN != null && b.ISBN == trimmedIsbn) || (b.Title == trimmedTitle && b.Author == trimmedAuthor));
                                }

                                if (exists)
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} saut�e (Doublon probable): {trimmedTitle}, {trimmedAuthor}, ISBN:{trimmedIsbn ?? "N/A"}");
                                    skippedCount++;
                                    continue;
                                }

                                var newBook = new Book
                                {
                                    Title = trimmedTitle,
                                    Author = trimmedAuthor,
                                    ISBN = trimmedIsbn,
                                    PublicationYear = record.PublicationYear,
                                    IsAvailable = record.IsAvailable
                                };
                                booksToAdd.Add(newBook);
                            }
                        } // Fin using CsvReader et StreamReader

                        // ... (reste de la sauvegarde et affichage des messages) ...
                        if (booksToAdd.Any())
                        {
                            _context.Books.AddRange(booksToAdd);
                            await _context.SaveChangesAsync();
                            importedCount = booksToAdd.Count;
                        }

                        string message = $"{importedCount} livre(s) import�(s).\n{skippedCount} ligne(s) saut�e(s) ou erron�e(s).";
                        if (errors.Any())
                        {
                            message += "\n\nPremiers d�tails:\n" + string.Join("\n", errors.Take(10));
                            if (errors.Count > 10) message += "\n...";
                        }
                        MessageBox.Show(message, "Import Termin�", MessageBoxButtons.OK, skippedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                        await LoadBooksAsync();
                        await LoadBorrowComboBoxesAsync();
                        UpdateDashboard();

                    }
                    catch (HeaderValidationException hvex)
                    {
                        MessageBox.Show($"Erreur d'en-t�te CSV : {hvex.Message}\nAssurez-vous que les colonnes existent (au moins Title, Author). Les noms doivent correspondre (insensible � la casse). V�rifiez le s�parateur (attendu: ';').", "Erreur Format CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'import : {ex.Message}\n{(ex.InnerException != null ? "D�tail: " + ex.InnerException.Message : "")}", "Erreur d'Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        // Bouton Import Membres (CSV) - CORRIG� v3
        private async void btnImportMembers_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*";
                openFileDialog.Title = "Importer une liste de membres depuis un CSV";

                if (openFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    this.Cursor = Cursors.WaitCursor;
                    int importedCount = 0;
                    int skippedCount = 0;
                    var errors = new List<string>(); // D�clar� avant config

                    var culture = CultureInfo.GetCultureInfo("fr-FR");
                    CsvReader csv = null; // D�clarer en dehors pour capture

                    var config = new CsvConfiguration(culture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                        // *** CORRECTION FINALE : Utiliser csv.Context.Parser.Row ***
                        BadDataFound = args => {
                            if (csv != null)
                            {
                                errors.Add($"Donn�e invalide ligne {csv.Context.Parser.Row}: {args.RawRecord}");
                            }
                            else
                            {
                                errors.Add($"Donn�e invalide (contexte lecteur non dispo): {args.RawRecord}");
                            }
                        },
                        TrimOptions = TrimOptions.Trim,
                        IgnoreBlankLines = true
                    };

                    try
                    {
                        List<Member> membersToAdd = new List<Member>();
                        using (var reader = new StreamReader(openFileDialog.FileName, System.Text.Encoding.UTF8, true))
                        // Initialiser la variable 'csv' ici
                        using (csv = new CsvReader(reader, config))
                        {
                            await csv.ReadAsync();
                            csv.ReadHeader();

                            while (await csv.ReadAsync())
                            {
                                Member record = null;
                                try { record = csv.GetRecord<Member>(); }
                                catch (Exception readEx)
                                {
                                    errors.Add($"Erreur lecture ligne {csv.Context.Parser.Row}: {readEx.Message}");
                                    skippedCount++;
                                    continue;
                                }

                                // ... (reste de la validation et logique d'ajout comme avant) ...
                                if (record == null || string.IsNullOrWhiteSpace(record.Name) || string.IsNullOrWhiteSpace(record.Email))
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} saut�e (Nom/Email requis): {csv.Context.Parser.RawRecord}");
                                    skippedCount++;
                                    continue;
                                }

                                string emailToImport = record.Email.Trim();
                                try { var _ = new MailAddress(emailToImport); }
                                catch
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} saut�e (Email invalide): {record.Name}, {emailToImport}");
                                    skippedCount++;
                                    continue;
                                }

                                bool exists = await _context.Members.AnyAsync(m => m.Email.ToLower() == emailToImport.ToLower());
                                if (!exists) { exists = membersToAdd.Any(m => m.Email.ToLower() == emailToImport.ToLower()); }

                                if (exists)
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} saut�e (Email d�j� existant): {record.Name}, {emailToImport}");
                                    skippedCount++;
                                    continue;
                                }

                                var newMember = new Member { Name = record.Name.Trim(), Email = emailToImport, RegistrationDate = record.RegistrationDate != default ? record.RegistrationDate : DateTime.Now, SubscriptionEndDate = record.SubscriptionEndDate };
                                membersToAdd.Add(newMember);

                            } // Fin while
                        } // Fin using

                        // ... (reste de la sauvegarde et affichage des messages) ...
                        if (membersToAdd.Any())
                        {
                            _context.Members.AddRange(membersToAdd);
                            await _context.SaveChangesAsync();
                            importedCount = membersToAdd.Count;
                        }

                        string message = $"{importedCount} membre(s) import�(s).\n{skippedCount} ligne(s) saut�e(s) ou erron�e(s).";
                        if (errors.Any()) { message += "\n\nPremiers d�tails:\n" + string.Join("\n", errors.Take(10)); if (errors.Count > 10) message += "\n..."; }
                        MessageBox.Show(message, "Import Termin�", MessageBoxButtons.OK, skippedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                        await LoadMembersAsync();
                        await LoadBorrowComboBoxesAsync();
                        UpdateDashboard();
                    }
                    catch (HeaderValidationException hvex) { MessageBox.Show($"Erreur d'en-t�te CSV : {hvex.Message}\nColonnes attendues: Name, Email, [RegistrationDate], [SubscriptionEndDate]. V�rifiez s�parateur (';' attendu).", "Erreur Format CSV", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    catch (Exception ex) { MessageBox.Show($"Erreur lors de l'import membres : {ex.Message}\n{(ex.InnerException != null ? "D�tail: " + ex.InnerException.Message : "")}", "Erreur d'Import", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        // Gestionnaire d'événement pour remplir les champs quand un membre est sélectionné dans le DataGridView
        private void dgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            // Vérifier qu'une ligne est sélectionnée et contient des données
            if (dgvMembers.SelectedRows.Count > 0 && !dgvMembers.SelectedRows[0].IsNewRow)
            {
                DataGridViewRow row = dgvMembers.SelectedRows[0];
                
                // Remplir les champs avec les données du membre sélectionné
                txtMemberName.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                txtMemberEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
                
                // Si vous avez d'autres champs comme SubscriptionEndDate, vous pourriez les remplir ici
                // Par exemple:
                // if (row.Cells["SubscriptionEndDate"].Value is DateTime date)
                // {
                //     dtpSubscriptionEnd.Value = date;
                // }
            }
        }

        // Bouton Mettre à Jour Membre
        private async void btnUpdateMember_Click(object sender, EventArgs e)
        {
            // Vérifier qu'une ligne est sélectionnée
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un membre à mettre à jour.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Récupérer l'ID du membre sélectionné
            int memberId = (int)dgvMembers.SelectedRows[0].Cells["Id"].Value;

            // Récupérer le membre à mettre à jour depuis la base de données
            var memberToUpdate = await _context.Members.FindAsync(memberId);
            if (memberToUpdate == null)
            {
                MessageBox.Show("Membre non trouvé dans la base de données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadMembersAsync(); // Recharger la liste des membres
                return;
            }

            // Vérifier que les champs obligatoires sont remplis
            if (string.IsNullOrWhiteSpace(txtMemberName.Text) || string.IsNullOrWhiteSpace(txtMemberEmail.Text))
            {
                MessageBox.Show("Le nom et l'email sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation Email simple (format)
            string email = txtMemberEmail.Text.Trim();
            try 
            { 
                var _ = new MailAddress(email); 
            }
            catch
            {
                MessageBox.Show("Le format de l'email est invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier si l'email est unique (s'il est modifié)
            if (email != memberToUpdate.Email)
            {
                bool emailExists = await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower() && m.Id != memberId);
                if (emailExists)
                {
                    MessageBox.Show("Cet email est déjà utilisé par un autre membre.", 
                        "Email dupliqué", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Confirmer la mise à jour
            var confirmResult = MessageBox.Show($"Êtes-vous sûr de vouloir mettre à jour les informations de '{memberToUpdate.Name}' ?",
                "Confirmation de mise à jour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Mettre à jour les propriétés du membre
                    memberToUpdate.Name = txtMemberName.Text.Trim();
                    memberToUpdate.Email = email;
                    
                    // Si vous avez d'autres champs comme SubscriptionEndDate, vous pourriez les mettre à jour ici
                    // memberToUpdate.SubscriptionEndDate = dtpSubscriptionEnd.Value;

                    await _context.SaveChangesAsync();
                    MessageBox.Show("Membre mis à jour avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Recharger les données
                    await LoadMembersAsync();
                    await LoadBorrowComboBoxesAsync(); // Mettre à jour la liste des membres emprunteurs
                    UpdateDashboard();
                    
                    // Sélectionner la ligne mise à jour dans le DataGridView
                    foreach (DataGridViewRow row in dgvMembers.Rows)
                    {
                        if ((int)row.Cells["Id"].Value == memberId)
                        {
                            row.Selected = true;
                            dgvMembers.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la mise à jour du membre : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- FIN DES M�THODES CORRIG�ES ---


        #endregion

        #region Gestion Emprunts

        // Charger les ComboBox pour les nouveaux emprunts
        private async Task LoadBorrowComboBoxesAsync()
        {
            // Charger les membres actifs (ou tous les membres)
            var members = await _context.Members.AsNoTracking().OrderBy(m => m.Name).ToListAsync();
            var selectedMemberId = cmbBorrowMember.SelectedValue;
            cmbBorrowMember.DataSource = members;
            cmbBorrowMember.DisplayMember = "Name";
            cmbBorrowMember.ValueMember = "Id";
            // Restaurer la sélection si possible après rechargement
            if (selectedMemberId != null && members.Any(m => m.Id == (int)selectedMemberId)) { cmbBorrowMember.SelectedValue = selectedMemberId; } else { cmbBorrowMember.SelectedIndex = -1; }


            // Charger les livres disponibles (ceux qui ont au moins un exemplaire disponible)
            var availableBooks = await _context.Books.AsNoTracking()
                                        .Where(b => b.CopiesAvailable > 0) // Modification ici
                                        .OrderBy(b => b.Title)
                                        .ToListAsync();
            var selectedBookId = cmbBorrowBook.SelectedValue;
            cmbBorrowBook.DataSource = availableBooks;
            cmbBorrowBook.DisplayMember = "Title"; // Afficher titre (et auteur si voulu via une propriété calculée dans Book)
            cmbBorrowBook.ValueMember = "Id";
            if (selectedBookId != null && availableBooks.Any(b => b.Id == (int)selectedBookId)) { cmbBorrowBook.SelectedValue = selectedBookId; } else { cmbBorrowBook.SelectedIndex = -1; }
        }

        // Charger les emprunts en cours dans dgvBorrowings
        private async Task LoadBorrowingsAsync()
        {
            dgvBorrowings.DataSource = null;

            var currentBorrowings = await _context.Borrowings.AsNoTracking()
                                            .Include(b => b.Book)   // Jointure
                                            .Include(b => b.Member) // Jointure
                                            .Where(b => b.ReturnDate == null) // Actifs
                                            .OrderBy(b => b.DueDate) // Trier par date retour
                                            .Select(b => new // Projection anonyme pour affichage
                                            {
                                                b.Id,
                                                MemberName = b.Member != null ? b.Member.Name : "Membre Supprim�",
                                                BookTitle = b.Book != null ? b.Book.Title : "Livre Supprim�",
                                                BorrowDate = b.BorrowDate,
                                                DueDate = b.DueDate,
                                                IsOverdue = b.DueDate.Date < DateTime.Now.Date // Comparer juste les dates
                                            })
                                            .ToListAsync();

            // Configurer colonnes si n�cessaire
            if (!dgvBorrowings.Columns.Contains("Id"))
            {
                dgvBorrowings.Columns.Clear();
                dgvBorrowings.AutoGenerateColumns = false;

                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID Emprunt", DataPropertyName = "Id", Visible = false });
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Membre", DataPropertyName = "MemberName", Width = 180 });
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Livre", DataPropertyName = "BookTitle", Width = 250 });
                // CORRIG�: Appliquer Format via DefaultCellStyle
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "BorrowDate",
                    HeaderText = "Emprunt� le",
                    DataPropertyName = "BorrowDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
                });
                // CORRIG�: Appliquer Format via DefaultCellStyle
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DueDate",
                    HeaderText = "Retour Pr�vu",
                    DataPropertyName = "DueDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
                });
                dgvBorrowings.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsOverdue", HeaderText = "En Retard", DataPropertyName = "IsOverdue", Width = 80 });
            }

            dgvBorrowings.DataSource = currentBorrowings;

            // Appliquer style pour retard (apr�s avoir li� la source)
            foreach (DataGridViewRow row in dgvBorrowings.Rows)
            {
                if (row.Cells["IsOverdue"]?.Value is bool isOverdue && isOverdue)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.Black; // Assurer lisibilit�
                }
                else
                {
                    // R�tablir les couleurs par d�faut si la ligne est recycl�e
                    row.DefaultCellStyle.BackColor = dgvBorrowings.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvBorrowings.DefaultCellStyle.ForeColor;
                }
            }
        }

        // Bouton Emprunter
        private async void btnBorrow_Click(object sender, EventArgs e)
        {
            if (cmbBorrowMember.SelectedValue == null || cmbBorrowBook.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner un membre ET un livre.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId = (int)cmbBorrowMember.SelectedValue;
            int bookId = (int)cmbBorrowBook.SelectedValue;
            DateTime dueDate = dtpDueDate.Value.Date; // Juste la date

            // Validation date (déjà faite par MinDate mais double check)
            if (dueDate < DateTime.Now.Date)
            {
                MessageBox.Show("La date de retour prévue ne peut pas être dans le passé.", "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Début de la transaction (si une des opérations échoue, tout est annulé)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Retrouver le livre et vérifier qu'il a des exemplaires disponibles
                var bookToBorrow = await _context.Books.FindAsync(bookId);

                if (bookToBorrow == null)
                { // Sécurité
                    await transaction.RollbackAsync();
                    MessageBox.Show("Erreur: Livre non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadBorrowComboBoxesAsync(); // Recharger pour refléter la réalité
                    return;
                }

                if (bookToBorrow.CopiesAvailable <= 0)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show("Ce livre n'a plus d'exemplaires disponibles.", "Livre indisponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await LoadBorrowComboBoxesAsync(); // Recharger
                    return;
                }

                // Optionnel: Vérifier abonnement membre, nombre max d'emprunts, etc. ici
                // var member = await _context.Members.FindAsync(memberId);
                // if (member.SubscriptionEndDate < DateTime.Now) { /* Refuser emprunt */ }

                // 2. Créer l'emprunt
                var newBorrowing = new Borrowing
                {
                    MemberId = memberId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now,
                    DueDate = dueDate
                };
                _context.Borrowings.Add(newBorrowing);
                await _context.SaveChangesAsync(); // Sauvegarder l'emprunt pour obtenir son ID si besoin

                // 3. Mettre à jour le livre (décrémenter le nombre d'exemplaires disponibles)
                bookToBorrow.CopiesAvailable--;
                
                // Mettre à jour l'état de disponibilité basé sur le nombre d'exemplaires restants
                bookToBorrow.IsAvailable = bookToBorrow.CopiesAvailable > 0;
                
                await _context.SaveChangesAsync(); // Sauvegarder la modif du livre

                // 4. Valider la transaction si tout s'est bien passé
                await transaction.CommitAsync();

                MessageBox.Show("Livre emprunté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Réinitialiser/Recharger
                cmbBorrowMember.SelectedIndex = -1; // Désélectionner
                cmbBorrowBook.SelectedIndex = -1;
                dtpDueDate.Value = DateTime.Now.AddDays(14); // Réinitialiser date picker

                await LoadBorrowComboBoxesAsync(); // Met à jour liste livres dispos
                await LoadBorrowingsAsync();       // Met à jour grille emprunts
                await LoadBooksAsync();            // Met à jour grille livres (statut dispo)
                UpdateDashboard();                 // Met à jour TdB
            }
            catch (Exception ex)
            {
                // Essayer d'annuler la transaction en cas d'erreur
                try { await transaction.RollbackAsync(); } catch (Exception rollbackEx) { Console.WriteLine($"Rollback failed: {rollbackEx.Message}"); }

                var innerEx = ex.InnerException;
                MessageBox.Show($"Une erreur est survenue lors de l'emprunt: {ex.Message}" + (innerEx != null ? $"\nDétail: {innerEx.Message}" : ""),
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Bouton Retourner Livre
        private async void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (dgvBorrowings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner l'emprunt correspondant au livre retourné.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowingId = (int)dgvBorrowings.SelectedRows[0].Cells["Id"].Value;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Trouver l'emprunt *et* inclure le livre pour le mettre à jour
                var borrowingToEnd = await _context.Borrowings
                                                .Include(b => b.Book) // IMPORTANT pour maj livre
                                                .FirstOrDefaultAsync(b => b.Id == borrowingId);

                if (borrowingToEnd == null)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show("Emprunt non trouvé (peut-être déjà retourné ou supprimé).", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadBorrowingsAsync(); // Recharger
                    return;
                }

                if (borrowingToEnd.ReturnDate != null)
                { // Déjà retourné
                    await transaction.RollbackAsync(); // Annuler transaction car rien à faire
                    MessageBox.Show("Ce retour a déjà été enregistré.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Pas d'erreur, juste une info
                }

                // 2. Mettre à jour l'emprunt
                borrowingToEnd.ReturnDate = DateTime.Now;
                await _context.SaveChangesAsync(); // Sauvegarder la date de retour

                // 3. Mettre à jour le livre (s'il existe encore)
                if (borrowingToEnd.Book != null)
                {
                    // Incrémenter le nombre d'exemplaires disponibles
                    borrowingToEnd.Book.CopiesAvailable++;
                    
                    // Mettre à jour l'état de disponibilité
                    borrowingToEnd.Book.IsAvailable = true; // Si au moins 1 exemplaire est rendu, le livre est disponible
                    
                    await _context.SaveChangesAsync(); // Sauvegarder les modifications
                }
                else
                {
                    // Cas rare où le livre aurait été supprimé alors qu'il était emprunté
                    MessageBox.Show("Avertissement: Le livre associé n'a pas été trouvé. Seul l'emprunt est marqué comme retourné.", "Livre Manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 4. Commiter la transaction
                await transaction.CommitAsync();

                MessageBox.Show("Retour enregistré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Recharger
                await LoadBorrowingsAsync();       // L'emprunt disparaît de la grille
                await LoadBorrowComboBoxesAsync(); // Le livre redevient dispo dans la combobox
                await LoadBooksAsync();            // La grille des livres est mise à jour
                UpdateDashboard();                 // Le TdB est mis à jour
            }
            catch (Exception ex)
            {
                try { await transaction.RollbackAsync(); } catch (Exception rbEx) { Console.WriteLine($"Rollback failed: {rbEx.Message}"); }
                var innerEx = ex.InnerException;
                MessageBox.Show($"Une erreur est survenue lors du retour: {ex.Message}" + (innerEx != null ? $"\nDétail: {innerEx.Message}" : ""),
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region Tableau de Bord et Alertes

        // Mettre � jour les compteurs du tableau de bord
        private void UpdateDashboard()
        {
            // Utiliser InvokeRequired/Invoke si appel� depuis un autre thread, mais MainForm_Load et les clics sont sur le thread UI
            if (!this.IsHandleCreated) return; // Ne rien faire si le formulaire n'est pas encore affich�

            // Utiliser try-catch car le contexte pourrait �tre en cours de fermeture/disposition
            try
            {
                // Utiliser CountAsync serait mieux pour de tr�s grosses tables, mais synchrone est ok ici.
                int totalBooks = _context.Books.Count();
                int totalMembers = _context.Members.Count();
                int borrowedBooks = _context.Borrowings.Count(b => b.ReturnDate == null);
                int overdueBooks = _context.Borrowings.Count(b => b.ReturnDate == null && b.DueDate.Date < DateTime.Now.Date);

                // Mettre � jour les labels (le check IsHandleCreated est une s�curit� suppl�mentaire)
                if (lblTotalBooksCount.IsHandleCreated) lblTotalBooksCount.Text = totalBooks.ToString();
                if (lblTotalMembersCount.IsHandleCreated) lblTotalMembersCount.Text = totalMembers.ToString();
                if (lblBorrowedBooksCount.IsHandleCreated) lblBorrowedBooksCount.Text = borrowedBooks.ToString();
                if (lblOverdueBooksCount.IsHandleCreated) lblOverdueBooksCount.Text = overdueBooks.ToString();
            }
            catch (ObjectDisposedException) { /* Ignorer si contexte ferm� pendant l'op�ration */ }
            catch (InvalidOperationException invOpEx) when (invOpEx.Message.Contains("disposed")) { /* Ignorer aussi */ }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur mise � jour TdB: {ex.Message}"); // Log discret pour debug
            }
        }


        // Bouton Rafra�chir TdB
        private void btnRefreshDashboard_Click(object sender, EventArgs e)
        {
            UpdateDashboard(); // Appelle simplement la m�thode de mise � jour
        }


        // Bouton Envoyer Alertes Email pour Retards
        private async void btnSendOverdueAlerts_Click(object sender, EventArgs e)
        {
            // --- Configuration SMTP (� METTRE DANS appsettings.json ou user secrets en production!) ---
            // !! REMPLACEZ PAR VOS VRAIS PARAMETRES !!
            string smtpServer = "smtp.gmail.com";           // Ex: smtp.gmail.com, smtp.office365.com
            int smtpPort = 587;                             // Ex: 587 (TLS), 465 (SSL), 25 (non s�curis�)
            bool enableSsl = true;                          // true pour port 587 ou 465
            string smtpUser = "projetds4@gmail.com"; // Votre login email
            string smtpPass = "heynzvforbpysohs";            // !! Utilisez un MOT DE PASSE D'APPLICATION pour Gmail/O365 si 2FA !!
            string fromEmail = smtpUser;                    // Peut �tre identique ou une autre adresse autoris�e
            string fromName = "Biblioth�que Municipale XYZ"; // Nom qui s'affichera comme exp�diteur
                                                             // --- Fin Configuration ---

            // V�rification rapide de la config (optionnel)
            if (smtpUser.Contains("votre.adresse") || smtpPass.Contains("xxxxxxxx"))
            {
                MessageBox.Show("Veuillez configurer les param�tres SMTP dans le code (btnSendOverdueAlerts_Click) avant d'envoyer des emails.", "Configuration Requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnSendOverdueAlerts.Enabled = false; // D�sactiver pendant l'envoi

            int emailsSent = 0;
            int errors = 0;
            var errorDetails = new List<string>();

            try
            {
                // 1. R�cup�rer les emprunts en retard AVEC membre et livre (AsNoTracking car lecture seule)
                var overdueBorrowings = await _context.Borrowings.AsNoTracking()
                                                .Include(b => b.Member)
                                                .Include(b => b.Book)
                                                .Where(b => b.ReturnDate == null && b.DueDate.Date < DateTime.Now.Date)
                                                .ToListAsync();

                if (!overdueBorrowings.Any())
                {
                    MessageBox.Show("Aucun emprunt en retard � signaler.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Sortir si rien � faire
                }

                // 2. Configurer le client SMTP une seule fois
                using SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = enableSsl,
                    Timeout = 30000 // Augmenter timeout � 30 sec si connexions lentes
                };

                // 3. Boucler et envoyer
                foreach (var borrowing in overdueBorrowings)
                {
                    // V�rifier si on a les infos n�cessaires
                    if (string.IsNullOrWhiteSpace(borrowing.Member?.Email) || borrowing.Book == null)
                    {
                        errors++;
                        errorDetails.Add($"Emprunt ID {borrowing.Id}: Donn�es membre/livre ou email manquantes.");
                        continue;
                    }

                    string toEmail = borrowing.Member.Email;
                    string memberName = borrowing.Member.Name ?? "Cher Membre"; // G�rer nom null
                    string bookTitle = borrowing.Book.Title ?? "Titre Inconnu";
                    TimeSpan overdueDays = DateTime.Now.Date - borrowing.DueDate.Date; // Calculer jours de retard

                    string subject = $"Rappel: Retour de livre en retard - {bookTitle}";
                    string body = $@"
Cher(�re) {memberName},

Nous vous rappelons que le livre ""{bookTitle}"", emprunt� le {borrowing.BorrowDate:dd/MM/yyyy}, devait �tre retourn� le {borrowing.DueDate:dd/MM/yyyy}.
Il est maintenant en retard de {overdueDays.Days} jour(s).

Merci de bien vouloir le rapporter � la biblioth�que d�s que possible.

Cordialement,
L'�quipe de la {fromName}";

                    // Cr�er le message
                    using MailMessage mailMessage = new MailMessage()
                    {
                        From = new MailAddress(fromEmail, fromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false, // Mettre � true si vous formatez en HTML
                    };
                    mailMessage.To.Add(toEmail); // Ajouter le destinataire

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage); // Envoi asynchrone
                        emailsSent++;
                        // Optionnel: Logguer l'envoi ou marquer l'emprunt comme notifi� (ajouter champ bool/date � Borrowing)
                        // await Task.Delay(500); // Petite pause pour ne pas surcharger le serveur SMTP (optionnel)
                    }
                    catch (SmtpException smtpEx)
                    {
                        errors++;
                        errorDetails.Add($"Erreur SMTP pour {toEmail}: {smtpEx.StatusCode} - {smtpEx.Message}");
                        // Si erreur d'authentification, inutile de continuer ?
                        if (smtpEx.StatusCode == SmtpStatusCode.MustIssueStartTlsFirst || // Souvent li� � EnableSsl incorrect
                            smtpEx.StatusCode == SmtpStatusCode.MailboxUnavailable || // Adresse invalide ?
                            smtpEx.Message.Contains("5.7.8") || // Gmail: login/pass incorrect ou acc�s moins s�curis� requis
                            smtpEx.Message.Contains("5.5.1"))   // Authentification incorrecte
                        {
                            MessageBox.Show($"Erreur SMTP critique ({smtpEx.StatusCode}) pour {toEmail}. V�rifiez la configuration SMTP et l'adresse.\nMessage: {smtpEx.Message}\nL'envoi des autres emails est interrompu.", "Erreur Email Critique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            goto EndSendProcess; // Sortir de la boucle et du try
                        }
                    }
                    catch (Exception mailEx)
                    {
                        errors++;
                        errorDetails.Add($"Erreur envoi pour {toEmail}: {mailEx.Message}");
                    }
                    // Pas besoin de mailMessage.Dispose() explicitement avec using
                } // Fin foreach
            } // Fin try global
            catch (Exception ex) // Erreur avant la boucle (ex: r�cup�ration des emprunts, config client SMTP)
            {
                errors++;
                errorDetails.Add($"Erreur g�n�rale avant envoi: {ex.Message}");
            }

        EndSendProcess: // Label pour sortir en cas d'erreur critique

            this.Cursor = Cursors.Default;
            btnSendOverdueAlerts.Enabled = true; // R�activer le bouton

            // 4. Afficher le r�sum�
            string summary = $"{emailsSent} alerte(s) envoy�e(s).\n{errors} erreur(s) rencontr�e(s).";
            if (errorDetails.Any())
            {
                summary += "\n\nD�tails des premi�res erreurs:\n" + string.Join("\n", errorDetails.Take(5));
                if (errorDetails.Count > 5) summary += "\n...";
                // Optionnel : Proposer d'afficher toutes les erreurs dans un fichier log ou une autre fen�tre
            }
            MessageBox.Show(summary, "R�sultat Envoi Alertes", MessageBoxButtons.OK, errors > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }


        #endregion
    }
}