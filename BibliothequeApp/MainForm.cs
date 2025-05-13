using BibliothequeApp.Data;
using BibliothequeApp.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Drawing; 
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace BibliothequeApp
{
    public partial class MainForm : Form
    {

        private readonly LibraryDbContext _context = new LibraryDbContext();

        public MainForm()
        {
            InitializeComponent();
            UpdateChartData();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
      
            dtpDueDate.Value = DateTime.Now.AddDays(14);
            dtpDueDate.MinDate = DateTime.Now.Date; 

      
            await LoadBooksAsync();
            await LoadMembersAsync();
            await LoadBorrowComboBoxesAsync();
            await LoadBorrowingsAsync();
            UpdateDashboard(); 
        }

      
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose(); 
            base.OnFormClosing(e);
        }

        #region Gestion Livres

   
        private async Task LoadBooksAsync(string? searchTerm = null)
        {
            dgvBooks.DataSource = null; 

            IQueryable<Book> query = _context.Books.AsNoTracking();

            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
            
                string likePattern = $"%{searchTerm}%"; 

               
              
                query = query.Where(b => EF.Functions.Like(b.Title, likePattern) ||
                                         EF.Functions.Like(b.Author, likePattern) ||
                                         (b.ISBN != null && EF.Functions.Like(b.ISBN, likePattern)));
            }

            var books = await query.OrderBy(b => b.Title).ToListAsync();

          
            if (!dgvBooks.Columns.Contains("Id")) 
            {
                dgvBooks.Columns.Clear();
                dgvBooks.AutoGenerateColumns = false;

                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Visible = false });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Titre", DataPropertyName = "Title", Width = 250 });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Auteur", DataPropertyName = "Author", Width = 150 });
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn { Name = "ISBN", HeaderText = "ISBN", DataPropertyName = "ISBN", Width = 120 });
                
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "PublicationYear",
                    HeaderText = "Année",
                    DataPropertyName = "PublicationYear",
                    Width = 60,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
                });
                
                dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "CopiesAvailable",
                    HeaderText = "Exemplaires",
                    DataPropertyName = "CopiesAvailable",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
                });
                
                dgvBooks.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsAvailable", HeaderText = "Disponible", DataPropertyName = "IsAvailable", Width = 80 });
            }

            dgvBooks.DataSource = books; 
        }

      
        private void ClearBookFields()
        {
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            txtBookISBN.Clear();
            txtBookYear.Clear();
            numCopiesAvailable.Value = 1; 
            dgvBooks.ClearSelection();
        }

     
        private void btnClearBookFields_Click(object sender, EventArgs e)
        {
            ClearBookFields();
        }

       
        private async void btnSearchBook_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBookTitle.Text.Trim(); 
            await LoadBooksAsync(searchTerm);
        }

   
        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text))
            {
                MessageBox.Show("Le titre et l'auteur sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int year = 0; 
            if (!string.IsNullOrWhiteSpace(txtBookYear.Text) && !int.TryParse(txtBookYear.Text, out year))
            {
                MessageBox.Show("L'année de publication doit être un nombre valide ou vide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            if (year != 0 && (year < 1000 || year > DateTime.Now.Year + 1))
            {
                MessageBox.Show("L'année de publication semble invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
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
                PublicationYear = year,
                CopiesAvailable = (int)numCopiesAvailable.Value,
                IsAvailable = true
            };

            try
            {
              
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
                await LoadBooksAsync();
                await LoadBorrowComboBoxesAsync();
                UpdateDashboard();     
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du livre : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

     
        private async void btnUpdateBook_Click(object sender, EventArgs e)
        {
           
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un livre à mettre à jour.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value;

          
            var bookToUpdate = await _context.Books.FindAsync(bookId);
            if (bookToUpdate == null)
            {
                MessageBox.Show("Livre non trouvé dans la base de données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadBooksAsync();
                return;
            }

           
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text))
            {
                MessageBox.Show("Le titre et l'auteur sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        
            int year = 0;
            if (!string.IsNullOrWhiteSpace(txtBookYear.Text) && !int.TryParse(txtBookYear.Text, out year))
            {
                MessageBox.Show("L'année de publication doit être un nombre valide ou vide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (year != 0 && (year < 1000 || year > DateTime.Now.Year + 1))
            {
                MessageBox.Show("L'année de publication semble invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

       
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

           
            var confirmResult = MessageBox.Show($"Êtes-vous sûr de vouloir mettre à jour le livre '{bookToUpdate.Title}' ?",
                "Confirmation de mise à jour", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                 
                    bookToUpdate.Title = txtBookTitle.Text.Trim();
                    bookToUpdate.Author = txtBookAuthor.Text.Trim();
                    bookToUpdate.ISBN = isbn;
                    bookToUpdate.PublicationYear = year;
                    bookToUpdate.CopiesAvailable = (int)numCopiesAvailable.Value;
                    bookToUpdate.IsAvailable = bookToUpdate.CopiesAvailable > 0;

                    await _context.SaveChangesAsync();
                    MessageBox.Show("Livre mis à jour avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                    await LoadBooksAsync();
                    await LoadBorrowComboBoxesAsync();
                    UpdateDashboard();

                  
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

   
        private async void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un livre a supprimer.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = (int)dgvBooks.SelectedRows[0].Cells["Id"].Value; 

            var bookToDelete = await _context.Books.FindAsync(bookId);

            if (bookToDelete == null)
            {
                MessageBox.Show("Livre non trouve (peut-etre deja supprimé).", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadBooksAsync(); 
                return;
            }

          
            bool isCurrentlyBorrowed = await _context.Borrowings.AnyAsync(b => b.BookId == bookId && b.ReturnDate == null);
            if (isCurrentlyBorrowed)
            {
                MessageBox.Show($"Le livre '{bookToDelete.Title}' est actuellement emprunté et ne peut pas étre supprimé.", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"étes-vous sur de vouloir supprimer le livre '{bookToDelete.Title}' ?\nCeci supprimera aussi son historique d'emprunts.",
                                               "Confirmation de suppression",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
             
                    var relatedBorrowings = _context.Borrowings.Where(b => b.BookId == bookId);
                    if (await relatedBorrowings.AnyAsync())
                    {
                        _context.Borrowings.RemoveRange(relatedBorrowings);
                    }

                    _context.Books.Remove(bookToDelete);
                    await _context.SaveChangesAsync(); 

                    MessageBox.Show("Livre supprim� avec succ�s !", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBookFields();
                    await LoadBooksAsync();
                    await LoadBorrowComboBoxesAsync(); 
                    UpdateDashboard();      
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

        //  Export Livres (CSV)
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

                     
                        var culture = CultureInfo.GetCultureInfo("fr-FR");
                        var config = new CsvConfiguration(culture);

                        using (var writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8)) 
                        using (var csv = new CsvWriter(writer, config)) 
                        {
                            writer.Write('\uFEFF'); 
                            await csv.WriteRecordsAsync(booksToExport);
                        }

                        MessageBox.Show($"Liste des livres exportée avec succés vers:\n{saveFileDialog.FileName}", "Export Termin�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'export des livres : {ex.Message}", "Erreur d'Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
      
            if (dgvBooks.SelectedRows.Count > 0 && !dgvBooks.SelectedRows[0].IsNewRow)
            {
                DataGridViewRow row = dgvBooks.SelectedRows[0];

              
                txtBookTitle.Text = row.Cells["Title"].Value?.ToString() ?? string.Empty;
                txtBookAuthor.Text = row.Cells["Author"].Value?.ToString() ?? string.Empty;
                txtBookISBN.Text = row.Cells["ISBN"].Value?.ToString() ?? string.Empty;

              
                if (row.Cells["PublicationYear"].Value != null && row.Cells["PublicationYear"].Value != DBNull.Value)
                {
                    int year = Convert.ToInt32(row.Cells["PublicationYear"].Value);
                    txtBookYear.Text = year > 0 ? year.ToString() : string.Empty;
                }
                else
                {
                    txtBookYear.Text = string.Empty;
                }

                if (row.DataGridView.Columns.Contains("CopiesAvailable") &&
                    row.Cells["CopiesAvailable"].Value != null &&
                    row.Cells["CopiesAvailable"].Value != DBNull.Value)
                {
                    int copiesValue = Convert.ToInt32(row.Cells["CopiesAvailable"].Value);
                    numCopiesAvailable.Value = copiesValue;
                }
                else
                {
                    numCopiesAvailable.Value = 1; 
                }
            }
        }

        #endregion

        #region Gestion Membres

      
        private async Task LoadMembersAsync(string? searchTerm = null)
        {
            dgvMembers.DataSource = null;

            IQueryable<Member> query = _context.Members.AsNoTracking();

         
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string likePattern = $"%{searchTerm}%";

            
                query = query.Where(m => EF.Functions.Like(m.Name, likePattern) ||
                                         EF.Functions.Like(m.Email, likePattern));
            }

            var members = await query.OrderBy(m => m.Name).ToListAsync();

           
            if (!dgvMembers.Columns.Contains("Id"))
            {
                dgvMembers.Columns.Clear();
                dgvMembers.AutoGenerateColumns = false;

                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", DataPropertyName = "Id", Visible = false });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Nom", DataPropertyName = "Name", Width = 200 });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 250 });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "RegistrationDate",
                    HeaderText = "Inscription",
                    DataPropertyName = "RegistrationDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
                });
                dgvMembers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SubscriptionEndDate",
                    HeaderText = "Fin Abon.",
                    DataPropertyName = "SubscriptionEndDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d", NullValue = "N/A" }
                });
            }

            dgvMembers.DataSource = members;
        }

         
        private void ClearMemberFields()
        {
            txtMemberName.Clear();
            txtMemberEmail.Clear();
            dtpSubscriptionEnd.Checked = false; 
            dtpSubscriptionEnd.Value = DateTime.Now.AddYears(1); 
            dgvMembers.ClearSelection();
        }

      
        private void btnClearMemberFields_Click(object sender, EventArgs e)
        {
            ClearMemberFields();
        }

       
        private async void btnSearchMember_Click(object sender, EventArgs e)
        {
            string searchTerm = txtMemberName.Text.Trim(); 
            await LoadMembersAsync(searchTerm);
        }

      
        private async void btnAddMember_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberName.Text) || string.IsNullOrWhiteSpace(txtMemberEmail.Text))
            {
                MessageBox.Show("Le nom et l'email sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            string emailToAdd = txtMemberEmail.Text.Trim();
            try { var _ = new MailAddress(emailToAdd); }
            catch
            {
                MessageBox.Show("Le format de l'email est invalide.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            bool emailExists = await _context.Members.AnyAsync(m => m.Email.ToLower() == emailToAdd.ToLower());
            if (emailExists)
            {
                MessageBox.Show("Cet email est déjà utilisé par un autre membre.", "Email dupliqué", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newMember = new Member
            {
                Name = txtMemberName.Text.Trim(),
                Email = emailToAdd,
                RegistrationDate = DateTime.Now,
                SubscriptionEndDate = dtpSubscriptionEnd.Checked ? dtpSubscriptionEnd.Value.Date : null
            };

            try
            {
                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                MessageBox.Show("Membre inscrit avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearMemberFields();
                await LoadMembersAsync();
                await LoadBorrowComboBoxesAsync();
                UpdateDashboard();
            }
            catch (DbUpdateException dbEx)
            {
                var sqlEx = dbEx.InnerException as Microsoft.Data.SqlClient.SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    MessageBox.Show("Erreur : L'email fourni existe déjà (conflit détecté lors de la sauvegarde).", "Erreur de Base de Données", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erreur base de données lors de l'inscription : {dbEx.InnerException?.Message ?? dbEx.Message}", "Erreur BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur inattendue lors de l'inscription : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private async void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un membre a supprimer.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId = (int)dgvMembers.SelectedRows[0].Cells["Id"].Value;
            var memberToDelete = await _context.Members.FindAsync(memberId);

            if (memberToDelete == null)
            {
                MessageBox.Show("Membre non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadMembersAsync();
                return;
            }

          
            bool hasActiveBorrowings = await _context.Borrowings.AnyAsync(b => b.MemberId == memberId && b.ReturnDate == null);
            if (hasActiveBorrowings)
            {
                MessageBox.Show($"Le membre '{memberToDelete.Name}' a des livres actuellement empruntés et ne peut pas étre supprimé.", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show($"étes-vous sur de vouloir supprimer le membre '{memberToDelete.Name}' ({memberToDelete.Email}) ?\nCeci supprimera aussi son historique d'emprunts.",
                                               "Confirmation de suppression",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                 
                    var relatedBorrowings = _context.Borrowings.Where(b => b.MemberId == memberId);
                    if (await relatedBorrowings.AnyAsync())
                    {
                        _context.Borrowings.RemoveRange(relatedBorrowings);
                    }

                    _context.Members.Remove(memberToDelete);
                    await _context.SaveChangesAsync();

                    MessageBox.Show("Membre supprimé avec succés !", "Succés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearMemberFields(); 
                    await LoadMembersAsync();
                    await LoadBorrowComboBoxesAsync();
                    UpdateDashboard();
                }
                catch (DbUpdateException dbEx) { MessageBox.Show($"Erreur BD suppression membre: {dbEx.InnerException?.Message ?? dbEx.Message}", "Erreur BD", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch (Exception ex) { MessageBox.Show($"Erreur suppression membre: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

       
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
                        var culture = CultureInfo.GetCultureInfo("fr-FR");
                        var config = new CsvConfiguration(culture);

                        using (var writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                        using (var csv = new CsvWriter(writer, config))
                        {
                            writer.Write('\uFEFF'); 
                            await csv.WriteRecordsAsync(membersToExport);
                        }
                        MessageBox.Show($"Liste des membres exportée avec succés vers:\n{saveFileDialog.FileName}", "Export Terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show($"Erreur lors de l'export des membres : {ex.Message}", "Erreur d'Export", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

        // --- DANS MainForm.cs ---

 
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
                    var errors = new List<string>(); 

                    var culture = CultureInfo.GetCultureInfo("fr-FR");
                    CsvReader csv = null; 

                    var config = new CsvConfiguration(culture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                  
                     
                        BadDataFound = args =>
                        {
                           
                            if (csv != null)
                            {
                                errors.Add($"Donnée invalide ligne {csv.Context.Parser.Row}: {args.RawRecord}");
                            }
                            else
                            {
                                errors.Add($"Donnée invalide (contexte lecteur non dispo): {args.RawRecord}");
                            }
                        },
                        TrimOptions = TrimOptions.Trim,
                        IgnoreBlankLines = true,
                    };

                    try
                    {
                        List<Book> booksToAdd = new List<Book>();
                        using (var reader = new StreamReader(openFileDialog.FileName, System.Text.Encoding.UTF8, true))
                      
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

                             
                                if (record == null || string.IsNullOrWhiteSpace(record.Title) || string.IsNullOrWhiteSpace(record.Author))
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} sautée (Titre/Auteur requis): {csv.Context.Parser.RawRecord}");
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
                                    errors.Add($"Ligne {csv.Context.Parser.Row} sautée (Doublon probable): {trimmedTitle}, {trimmedAuthor}, ISBN:{trimmedIsbn ?? "N/A"}");
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
                        } 

                       
                        if (booksToAdd.Any())
                        {
                            _context.Books.AddRange(booksToAdd);
                            await _context.SaveChangesAsync();
                            importedCount = booksToAdd.Count;
                        }

                        string message = $"{importedCount} livre(s) importé(s).\n{skippedCount} ligne(s) sautée(s) ou erronée(s).";
                        if (errors.Any())
                        {
                            message += "\n\nPremiers détails:\n" + string.Join("\n", errors.Take(10));
                            if (errors.Count > 10) message += "\n...";
                        }
                        MessageBox.Show(message, "Import Terminé", MessageBoxButtons.OK, skippedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                        await LoadBooksAsync();
                        await LoadBorrowComboBoxesAsync();
                        UpdateDashboard();

                    }
                    catch (HeaderValidationException hvex)
                    {
                        MessageBox.Show($"Erreur d'en-téte CSV : {hvex.Message}\nAssurez-vous que les colonnes existent (au moins Title, Author). Les noms doivent correspondre (insensible é la casse). Vérifiez le séparateur (attendu: ';').", "Erreur Format CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'import : {ex.Message}\n{(ex.InnerException != null ? "Détail: " + ex.InnerException.Message : "")}", "Erreur d'Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

       
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
                    var errors = new List<string>(); 

                    var culture = CultureInfo.GetCultureInfo("fr-FR");
                    CsvReader csv = null; 

                    var config = new CsvConfiguration(culture)
                    {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                       
                        BadDataFound = args =>
                        {
                            if (csv != null)
                            {
                                errors.Add($"Donnée invalide ligne {csv.Context.Parser.Row}: {args.RawRecord}");
                            }
                            else
                            {
                                errors.Add($"Donnée invalide (contexte lecteur non dispo): {args.RawRecord}");
                            }
                        },
                        TrimOptions = TrimOptions.Trim,
                        IgnoreBlankLines = true
                    };

                    try
                    {
                        List<Member> membersToAdd = new List<Member>();
                        using (var reader = new StreamReader(openFileDialog.FileName, System.Text.Encoding.UTF8, true))
                    
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

                              
                                if (record == null || string.IsNullOrWhiteSpace(record.Name) || string.IsNullOrWhiteSpace(record.Email))
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} sautée (Nom/Email requis): {csv.Context.Parser.RawRecord}");
                                    skippedCount++;
                                    continue;
                                }

                                string emailToImport = record.Email.Trim();
                                try { var _ = new MailAddress(emailToImport); }
                                catch
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} sautée (Email invalide): {record.Name}, {emailToImport}");
                                    skippedCount++;
                                    continue;
                                }

                                bool exists = await _context.Members.AnyAsync(m => m.Email.ToLower() == emailToImport.ToLower());
                                if (!exists) { exists = membersToAdd.Any(m => m.Email.ToLower() == emailToImport.ToLower()); }

                                if (exists)
                                {
                                    errors.Add($"Ligne {csv.Context.Parser.Row} sautée (Email déja existant): {record.Name}, {emailToImport}");
                                    skippedCount++;
                                    continue;
                                }

                                var newMember = new Member { Name = record.Name.Trim(), Email = emailToImport, RegistrationDate = record.RegistrationDate != default ? record.RegistrationDate : DateTime.Now, SubscriptionEndDate = record.SubscriptionEndDate };
                                membersToAdd.Add(newMember);

                            }
                        } 

                      
                        if (membersToAdd.Any())
                        {
                            _context.Members.AddRange(membersToAdd);
                            await _context.SaveChangesAsync();
                            importedCount = membersToAdd.Count;
                        }

                        string message = $"{importedCount} membre(s) importé(s).\n{skippedCount} ligne(s) sautée(s) ou erronée(s).";
                        if (errors.Any()) { message += "\n\nPremiers détails:\n" + string.Join("\n", errors.Take(10)); if (errors.Count > 10) message += "\n..."; }
                        MessageBox.Show(message, "Import Terminé", MessageBoxButtons.OK, skippedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                        await LoadMembersAsync();
                        await LoadBorrowComboBoxesAsync();
                        UpdateDashboard();
                    }
                    catch (HeaderValidationException hvex) { MessageBox.Show($"Erreur d'en-téte CSV : {hvex.Message}\nColonnes attendues: Name, Email, [RegistrationDate], [SubscriptionEndDate]. Varifiez saparateur (';' attendu).", "Erreur Format CSV", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    catch (Exception ex) { MessageBox.Show($"Erreur lors de l'import membres : {ex.Message}\n{(ex.InnerException != null ? "Datail: " + ex.InnerException.Message : "")}", "Erreur d'Import", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { this.Cursor = Cursors.Default; }
                }
            }
        }

      
        private void dgvMembers_SelectionChanged(object sender, EventArgs e)
        {
        
            if (dgvMembers.SelectedRows.Count > 0 && !dgvMembers.SelectedRows[0].IsNewRow)
            {
                DataGridViewRow row = dgvMembers.SelectedRows[0];                
                txtMemberName.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                txtMemberEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;

                
                if (row.Cells["SubscriptionEndDate"].Value != null && row.Cells["SubscriptionEndDate"].Value != DBNull.Value)
                {
                    if (row.Cells["SubscriptionEndDate"].Value is DateTime date)
                    {
                        dtpSubscriptionEnd.Value = date;
                        dtpSubscriptionEnd.Checked = true;
                    }
                }
                else
                {
                    dtpSubscriptionEnd.Checked = false;
                    dtpSubscriptionEnd.Value = DateTime.Now.AddYears(1);
                }
            }
        }

     
        private async void btnUpdateMember_Click(object sender, EventArgs e)
        {
          
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un membre à mettre à jour.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            int memberId = (int)dgvMembers.SelectedRows[0].Cells["Id"].Value;

           
            if (string.IsNullOrWhiteSpace(txtMemberName.Text) || string.IsNullOrWhiteSpace(txtMemberEmail.Text))
            {
                MessageBox.Show("Le nom et l'email sont requis.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
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

          
            var memberToUpdate = await _context.Members.FindAsync(memberId);
            if (memberToUpdate == null)
            {
                MessageBox.Show("Membre non trouvé dans la base de données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadMembersAsync();
                return;
            }

        
            if (email != memberToUpdate.Email)
            {
                bool emailExists = await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower() && m.Id != memberId);
                if (emailExists)
                {
                    MessageBox.Show("Cet email est déjà utilisé par un autre membre.", "Email dupliqué", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            
            var confirmResult = MessageBox.Show($"Êtes-vous sûr de vouloir mettre à jour les informations de '{memberToUpdate.Name}' ?",
                                              "Confirmation de mise à jour",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                
                    memberToUpdate.Name = txtMemberName.Text.Trim();
                    memberToUpdate.Email = email;
                    memberToUpdate.SubscriptionEndDate = dtpSubscriptionEnd.Checked ? dtpSubscriptionEnd.Value.Date : null;

                    await _context.SaveChangesAsync();
                    MessageBox.Show("Membre mis à jour avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  
                    await LoadMembersAsync();
                    await LoadBorrowComboBoxesAsync();
                    UpdateDashboard();

                   
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

       


        #endregion

        #region Gestion Emprunts

       
        private async Task LoadBorrowComboBoxesAsync()
        {
           
            var members = await _context.Members.AsNoTracking().OrderBy(m => m.Name).ToListAsync();
            var selectedMemberId = cmbBorrowMember.SelectedValue;
            cmbBorrowMember.DataSource = members;
            cmbBorrowMember.DisplayMember = "Name";
            cmbBorrowMember.ValueMember = "Id";
           
            if (selectedMemberId != null && members.Any(m => m.Id == (int)selectedMemberId)) { cmbBorrowMember.SelectedValue = selectedMemberId; } else { cmbBorrowMember.SelectedIndex = -1; }


           
            var availableBooks = await _context.Books.AsNoTracking()
                                        .Where(b => b.CopiesAvailable > 0) 
                                        .OrderBy(b => b.Title)
                                        .ToListAsync();
            var selectedBookId = cmbBorrowBook.SelectedValue;
            cmbBorrowBook.DataSource = availableBooks;
            cmbBorrowBook.DisplayMember = "Title"; 
            cmbBorrowBook.ValueMember = "Id";
            if (selectedBookId != null && availableBooks.Any(b => b.Id == (int)selectedBookId)) { cmbBorrowBook.SelectedValue = selectedBookId; } else { cmbBorrowBook.SelectedIndex = -1; }
        }

       
        private async Task LoadBorrowingsAsync()
        {
            dgvBorrowings.DataSource = null;

            var currentBorrowings = await _context.Borrowings.AsNoTracking()
                                            .Include(b => b.Book)   
                                            .Include(b => b.Member) 
                                            .Where(b => b.ReturnDate == null)
                                            .OrderBy(b => b.DueDate) 
                                            .Select(b => new 
                                            {
                                                b.Id,
                                                MemberName = b.Member != null ? b.Member.Name : "Membre Supprima",
                                                BookTitle = b.Book != null ? b.Book.Title : "Livre Supprima",
                                                BorrowDate = b.BorrowDate,
                                                DueDate = b.DueDate,
                                                IsOverdue = b.DueDate.Date < DateTime.Now.Date 
                                            })
                                            .ToListAsync();

         
            if (!dgvBorrowings.Columns.Contains("Id"))
            {
                dgvBorrowings.Columns.Clear();
                dgvBorrowings.AutoGenerateColumns = false;

                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID Emprunt", DataPropertyName = "Id", Visible = false });
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Membre", DataPropertyName = "MemberName", Width = 180 });
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Livre", DataPropertyName = "BookTitle", Width = 250 });
            
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "BorrowDate",
                    HeaderText = "Emprunté le",
                    DataPropertyName = "BorrowDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
                });
              
                dgvBorrowings.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DueDate",
                    HeaderText = "Retour Prévu",
                    DataPropertyName = "DueDate",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
                });
                dgvBorrowings.Columns.Add(new DataGridViewCheckBoxColumn { Name = "IsOverdue", HeaderText = "En Retard", DataPropertyName = "IsOverdue", Width = 80 });
            }

            dgvBorrowings.DataSource = currentBorrowings;

            
            foreach (DataGridViewRow row in dgvBorrowings.Rows)
            {
                if (row.Cells["IsOverdue"]?.Value is bool isOverdue && isOverdue)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.Black; 
                }
                else
                {
                  
                    row.DefaultCellStyle.BackColor = dgvBorrowings.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvBorrowings.DefaultCellStyle.ForeColor;
                }
            }
        }

       
        private async void btnBorrow_Click(object sender, EventArgs e)
        {
            if (cmbBorrowMember.SelectedValue == null || cmbBorrowBook.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner un membre ET un livre.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberId = (int)cmbBorrowMember.SelectedValue;
            int bookId = (int)cmbBorrowBook.SelectedValue;
            DateTime dueDate = dtpDueDate.Value.Date; 

            // Vérification de l'abonnement expiré
            var member = await _context.Members.FindAsync(memberId);
            if (member != null && member.SubscriptionEndDate.HasValue && member.SubscriptionEndDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show($"Impossible d'emprunter: L'abonnement de {member.Name} est expiré depuis le {member.SubscriptionEndDate.Value.ToShortDateString()}.", 
                                "Abonnement expiré", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            if (dueDate < DateTime.Now.Date)
            {
                MessageBox.Show("La date de retour prévue ne peut pas être dans le passé.", "Date invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
              
                var bookToBorrow = await _context.Books.FindAsync(bookId);

                if (bookToBorrow == null)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show("Erreur: Livre non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadBorrowComboBoxesAsync(); 
                    return;
                }

                if (bookToBorrow.CopiesAvailable <= 0)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show("Ce livre n'a plus d'exemplaires disponibles.", "Livre indisponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    await LoadBorrowComboBoxesAsync();
                    return;
                }

              

               
                var newBorrowing = new Borrowing
                {
                    MemberId = memberId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now,
                    DueDate = dueDate
                };
                _context.Borrowings.Add(newBorrowing);
                await _context.SaveChangesAsync(); 

              
                bookToBorrow.CopiesAvailable--;

         
                bookToBorrow.IsAvailable = bookToBorrow.CopiesAvailable > 0;

                await _context.SaveChangesAsync();

                
                await transaction.CommitAsync();

                MessageBox.Show("Livre emprunté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                cmbBorrowMember.SelectedIndex = -1; 
                cmbBorrowBook.SelectedIndex = -1;
                dtpDueDate.Value = DateTime.Now.AddDays(14); 

                await LoadBorrowComboBoxesAsync(); 
                await LoadBorrowingsAsync();       
                await LoadBooksAsync();            
                UpdateDashboard();                
            }
            catch (Exception ex)
            {
             
                try { await transaction.RollbackAsync(); } catch (Exception rollbackEx) { Console.WriteLine($"Rollback failed: {rollbackEx.Message}"); }

                var innerEx = ex.InnerException;
                MessageBox.Show($"Une erreur est survenue lors de l'emprunt: {ex.Message}" + (innerEx != null ? $"\nDétail: {innerEx.Message}" : ""),
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    
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
                
                var borrowingToEnd = await _context.Borrowings
                                                .Include(b => b.Book) 
                                                .FirstOrDefaultAsync(b => b.Id == borrowingId);

                if (borrowingToEnd == null)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show("Emprunt non trouvé (peut-être déjà retourné ou supprimé).", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await LoadBorrowingsAsync();
                    return;
                }

                if (borrowingToEnd.ReturnDate != null)
                { 
                    await transaction.RollbackAsync();
                    MessageBox.Show("Ce retour a déjà été enregistré.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

           
                borrowingToEnd.ReturnDate = DateTime.Now;
                await _context.SaveChangesAsync(); 

              
                if (borrowingToEnd.Book != null)
                {
                   
                    borrowingToEnd.Book.CopiesAvailable++;

                    
                    borrowingToEnd.Book.IsAvailable = true; 

                    await _context.SaveChangesAsync(); 
                }
                else
                {
                   
                    MessageBox.Show("Avertissement: Le livre associé n'a pas été trouvé. Seul l'emprunt est marqué comme retourné.", "Livre Manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

               
                await transaction.CommitAsync();

                MessageBox.Show("Retour enregistré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

              
                await LoadBorrowingsAsync();       
                await LoadBorrowComboBoxesAsync(); 
                await LoadBooksAsync();         
                UpdateDashboard();           
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

      
        private void UpdateDashboard()
        {
           
            if (!this.IsHandleCreated) return; 

      
            try
            {
             
                int totalBooks = _context.Books.Count();
                int totalMembers = _context.Members.Count();
                int borrowedBooks = _context.Borrowings.Count(b => b.ReturnDate == null);
                int overdueBooks = _context.Borrowings.Count(b => b.ReturnDate == null && b.DueDate.Date < DateTime.Now.Date);

                
                if (lblTotalBooksCount.IsHandleCreated) lblTotalBooksCount.Text = totalBooks.ToString();
                if (lblTotalMembersCount.IsHandleCreated) lblTotalMembersCount.Text = totalMembers.ToString();
                if (lblBorrowedBooksCount.IsHandleCreated) lblBorrowedBooksCount.Text = borrowedBooks.ToString();
                if (lblOverdueBooksCount.IsHandleCreated) lblOverdueBooksCount.Text = overdueBooks.ToString();

            
                UpdateChartData();
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException invOpEx) when (invOpEx.Message.Contains("disposed")) {  }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur mise a jour TdB: {ex.Message}"); 
            }
        }

      
        private void UpdateChartData()
        {
            
            chartStatistics.Series["Livres"].Points.Clear();
            chartStatistics.Series["Emprunts"].Points.Clear();
            chartStatistics.Series["En retard"].Points.Clear();

           
      
            string[] months = new string[] { "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre" };

            Random rand = new Random();

          
            for (int i = 0; i < months.Length; i++)
            {
         
                int booksValue = rand.Next(5, 20);
                int borrowingsValue = rand.Next(3, 15);
                int overdueValue = rand.Next(0, 5);

                // Ajouter les données au graphique
                chartStatistics.Series["Livres"].Points.AddXY(months[i], booksValue);
                chartStatistics.Series["Emprunts"].Points.AddXY(months[i], borrowingsValue);
                chartStatistics.Series["En retard"].Points.AddXY(months[i], overdueValue);
            }
        }

       
        private void btnRefreshDashboard_Click(object sender, EventArgs e)
        {
            UpdateDashboard(); 
        }


      
        private async void btnSendOverdueAlerts_Click(object sender, EventArgs e)
        {
        
            string smtpServer = "smtp.gmail.com";          
            int smtpPort = 587;                           
            bool enableSsl = true;                      
            string smtpUser = "projetds4@gmail.com"; 
            string smtpPass = "heynzvforbpysohs";         
            string fromEmail = smtpUser;                 
            string fromName = "Bibliothéque Municipale XYZ"; 
                                                             // --- Fin Configuration ---

          
            if (smtpUser.Contains("votre.adresse") || smtpPass.Contains("xxxxxxxx"))
            {
                MessageBox.Show("Veuillez configurer les paramétres SMTP dans le code (btnSendOverdueAlerts_Click) avant d'envoyer des emails.", "Configuration Requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnSendOverdueAlerts.Enabled = false;

            int emailsSent = 0;
            int errors = 0;
            var errorDetails = new List<string>();

            try
            {
                
                var overdueBorrowings = await _context.Borrowings.AsNoTracking()
                                                .Include(b => b.Member)
                                                .Include(b => b.Book)
                                                .Where(b => b.ReturnDate == null && b.DueDate.Date < DateTime.Now.Date)
                                                .ToListAsync();

                if (!overdueBorrowings.Any())
                {
                    MessageBox.Show("Aucun emprunt en retard a signaler.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

              
                using SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = enableSsl,
                    Timeout = 30000 
                };

               
                foreach (var borrowing in overdueBorrowings)
                {
              
                    if (string.IsNullOrWhiteSpace(borrowing.Member?.Email) || borrowing.Book == null)
                    {
                        errors++;
                        errorDetails.Add($"Emprunt ID {borrowing.Id}: Données membre/livre ou email manquantes.");
                        continue;
                    }

                    string toEmail = borrowing.Member.Email;
                    string memberName = borrowing.Member.Name ?? "Cher Membre"; 
                    string bookTitle = borrowing.Book.Title ?? "Titre Inconnu";
                    TimeSpan overdueDays = DateTime.Now.Date - borrowing.DueDate.Date; 

                    string subject = $"Rappel: Retour de livre en retard - {bookTitle}";
                    string body = $@"
Cher(ére) {memberName},

Nous vous rappelons que le livre ""{bookTitle}"", emprunté le {borrowing.BorrowDate:dd/MM/yyyy}, devait étre retourné le {borrowing.DueDate:dd/MM/yyyy}.
Il est maintenant en retard de {overdueDays.Days} jour(s).

Merci de bien vouloir le rapporter a la bibliothéque dés que possible.

Cordialement,
L'équipe de la {fromName}";

                   
                    using MailMessage mailMessage = new MailMessage()
                    {
                        From = new MailAddress(fromEmail, fromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false,
                    };
                    mailMessage.To.Add(toEmail);

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        emailsSent++;
                   
                    }
                    catch (SmtpException smtpEx)
                    {
                        errors++;
                        errorDetails.Add($"Erreur SMTP pour {toEmail}: {smtpEx.StatusCode} - {smtpEx.Message}");
                   
                        if (smtpEx.StatusCode == SmtpStatusCode.MustIssueStartTlsFirst ||
                            smtpEx.StatusCode == SmtpStatusCode.MailboxUnavailable || 
                            smtpEx.Message.Contains("5.7.8") || 
                            smtpEx.Message.Contains("5.5.1"))   
                        {
                            MessageBox.Show($"Erreur SMTP critique ({smtpEx.StatusCode}) pour {toEmail}. Vérifiez la configuration SMTP et l'adresse.\nMessage: {smtpEx.Message}\nL'envoi des autres emails est interrompu.", "Erreur Email Critique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            goto EndSendProcess; 
                        }
                    }
                    catch (Exception mailEx)
                    {
                        errors++;
                        errorDetails.Add($"Erreur envoi pour {toEmail}: {mailEx.Message}");
                    }
                   
                }
            } 
            catch (Exception ex) 
            {
                errors++;
                errorDetails.Add($"Erreur générale avant envoi: {ex.Message}");
            }

        EndSendProcess: 

            this.Cursor = Cursors.Default;
            btnSendOverdueAlerts.Enabled = true; 

           
            string summary = $"{emailsSent} alerte(s) envoyée(s).\n{errors} erreur(s) rencontrée(s).";
            if (errorDetails.Any())
            {
                summary += "\n\nDétails des premiéres erreurs:\n" + string.Join("\n", errorDetails.Take(5));
                if (errorDetails.Count > 5) summary += "\n...";
                
            }
            MessageBox.Show(summary, "Résultat Envoi Alertes", MessageBoxButtons.OK, errors > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }


        #endregion

        private void numCopiesAvailable_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblBookISBN_Click(object sender, EventArgs e)
        {

        }

        private void cmbBorrowBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalBooksCount_Click(object sender, EventArgs e)
        {

        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblSubscriptionEnd_Click(object sender, EventArgs e)
        {
           
        }
    }
}