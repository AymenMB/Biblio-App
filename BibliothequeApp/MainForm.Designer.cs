// ATTENTION : Ce fichier est normalement généré et géré par le concepteur Windows Forms.
// Modifier manuellement ce fichier peut entraîner un comportement inattendu
// et les modifications pourraient être perdues si le formulaire est rouvert dans le concepteur.
namespace BibliothequeApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            // Dispose du DbContext ici aussi par sécurité, bien qu'il soit préférable dans OnFormClosing
            if (disposing && (_context != null))
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.groupBoxBooks = new System.Windows.Forms.GroupBox();
            this.numCopiesAvailable = new System.Windows.Forms.NumericUpDown();
            this.lblCopiesAvailable = new System.Windows.Forms.Label();
            this.btnImportBooks = new System.Windows.Forms.Button();
            this.btnExportBooks = new System.Windows.Forms.Button();
            this.btnClearBookFields = new System.Windows.Forms.Button();
            this.btnSearchBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnUpdateBook = new System.Windows.Forms.Button();
            this.txtBookYear = new System.Windows.Forms.TextBox();
            this.lblBookYear = new System.Windows.Forms.Label();
            this.txtBookISBN = new System.Windows.Forms.TextBox();
            this.lblBookISBN = new System.Windows.Forms.Label();
            this.txtBookAuthor = new System.Windows.Forms.TextBox();
            this.lblBookAuthor = new System.Windows.Forms.Label();
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.tabMembers = new System.Windows.Forms.TabPage();
            this.groupBoxMembers = new System.Windows.Forms.GroupBox();
            this.btnImportMembers = new System.Windows.Forms.Button();
            this.btnExportMembers = new System.Windows.Forms.Button();
            this.btnClearMemberFields = new System.Windows.Forms.Button();
            this.btnSearchMember = new System.Windows.Forms.Button();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnUpdateMember = new System.Windows.Forms.Button();
            this.txtMemberEmail = new System.Windows.Forms.TextBox();
            this.lblMemberEmail = new System.Windows.Forms.Label();
            this.txtMemberName = new System.Windows.Forms.TextBox();
            this.lblMemberName = new System.Windows.Forms.Label();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.tabBorrows = new System.Windows.Forms.TabPage();
            this.groupBoxCurrentBorrowings = new System.Windows.Forms.GroupBox();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.dgvBorrowings = new System.Windows.Forms.DataGridView();
            this.groupBoxNewBorrowing = new System.Windows.Forms.GroupBox();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.cmbBorrowBook = new System.Windows.Forms.ComboBox();
            this.lblBorrowBook = new System.Windows.Forms.Label();
            this.cmbBorrowMember = new System.Windows.Forms.ComboBox();
            this.lblBorrowMember = new System.Windows.Forms.Label();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.btnSendOverdueAlerts = new System.Windows.Forms.Button();
            this.btnRefreshDashboard = new System.Windows.Forms.Button();
            this.lblOverdueBooksCount = new System.Windows.Forms.Label();
            this.lblBorrowedBooksCount = new System.Windows.Forms.Label();
            this.lblTotalMembersCount = new System.Windows.Forms.Label();
            this.lblTotalBooksCount = new System.Windows.Forms.Label();
            this.lblOverdueBooksPrompt = new System.Windows.Forms.Label();
            this.lblBorrowedBooksPrompt = new System.Windows.Forms.Label();
            this.lblTotalMembersPrompt = new System.Windows.Forms.Label();
            this.lblTotalBooksPrompt = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabBooks.SuspendLayout();
            this.groupBoxBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCopiesAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.tabMembers.SuspendLayout();
            this.groupBoxMembers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.tabBorrows.SuspendLayout();
            this.groupBoxCurrentBorrowings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowings)).BeginInit();
            this.groupBoxNewBorrowing.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControlMain
            //
            this.tabControlMain.Controls.Add(this.tabBooks);
            this.tabControlMain.Controls.Add(this.tabMembers);
            this.tabControlMain.Controls.Add(this.tabBorrows);
            this.tabControlMain.Controls.Add(this.tabDashboard);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(784, 561); // Taille exemple
            this.tabControlMain.TabIndex = 0;
            //
            // tabBooks
            //
            this.tabBooks.Controls.Add(this.groupBoxBooks);
            this.tabBooks.Controls.Add(this.dgvBooks);
            this.tabBooks.Location = new System.Drawing.Point(4, 24); // Ajusté pour .NET 6+ style
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooks.Size = new System.Drawing.Size(776, 533);
            this.tabBooks.TabIndex = 0;
            this.tabBooks.Text = "Livres";
            this.tabBooks.UseVisualStyleBackColor = true;
            //
            // groupBoxBooks
            //
            this.groupBoxBooks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBooks.Controls.Add(this.numCopiesAvailable);
            this.groupBoxBooks.Controls.Add(this.lblCopiesAvailable);
            this.groupBoxBooks.Controls.Add(this.btnImportBooks);
            this.groupBoxBooks.Controls.Add(this.btnExportBooks);
            this.groupBoxBooks.Controls.Add(this.btnClearBookFields);
            this.groupBoxBooks.Controls.Add(this.btnSearchBook);
            this.groupBoxBooks.Controls.Add(this.btnDeleteBook);
            this.groupBoxBooks.Controls.Add(this.btnAddBook);
            this.groupBoxBooks.Controls.Add(this.btnUpdateBook);
            this.groupBoxBooks.Controls.Add(this.txtBookYear);
            this.groupBoxBooks.Controls.Add(this.lblBookYear);
            this.groupBoxBooks.Controls.Add(this.txtBookISBN);
            this.groupBoxBooks.Controls.Add(this.lblBookISBN);
            this.groupBoxBooks.Controls.Add(this.txtBookAuthor);
            this.groupBoxBooks.Controls.Add(this.lblBookAuthor);
            this.groupBoxBooks.Controls.Add(this.txtBookTitle);
            this.groupBoxBooks.Controls.Add(this.lblBookTitle);
            this.groupBoxBooks.Location = new System.Drawing.Point(8, 6);
            this.groupBoxBooks.Name = "groupBoxBooks";
            this.groupBoxBooks.Size = new System.Drawing.Size(760, 160); // Taille exemple
            this.groupBoxBooks.TabIndex = 1;
            this.groupBoxBooks.TabStop = false;
            this.groupBoxBooks.Text = "Détails / Recherche Livre";
            //
            // numCopiesAvailable
            //
            this.numCopiesAvailable.Location = new System.Drawing.Point(650, 58);
            this.numCopiesAvailable.Name = "numCopiesAvailable";
            this.numCopiesAvailable.Size = new System.Drawing.Size(80, 23);
            this.numCopiesAvailable.TabIndex = 15;
            this.numCopiesAvailable.Minimum = 1;
            this.numCopiesAvailable.Maximum = 999;
            this.numCopiesAvailable.Value = 1;
            //
            // lblCopiesAvailable
            //
            this.lblCopiesAvailable.AutoSize = true;
            this.lblCopiesAvailable.Location = new System.Drawing.Point(580, 61);
            this.lblCopiesAvailable.Name = "lblCopiesAvailable";
            this.lblCopiesAvailable.Size = new System.Drawing.Size(70, 15);
            this.lblCopiesAvailable.TabIndex = 14;
            this.lblCopiesAvailable.Text = "Exemplaires:";
            //
            // btnImportBooks
            //
            this.btnImportBooks.Location = new System.Drawing.Point(590, 120);
            this.btnImportBooks.Name = "btnImportBooks";
            this.btnImportBooks.Size = new System.Drawing.Size(100, 23);
            this.btnImportBooks.TabIndex = 13;
            this.btnImportBooks.Text = "Importer (CSV)";
            this.btnImportBooks.UseVisualStyleBackColor = true;
            this.btnImportBooks.Click += new System.EventHandler(this.btnImportBooks_Click);
            //
            // btnExportBooks
            //
            this.btnExportBooks.Location = new System.Drawing.Point(480, 120);
            this.btnExportBooks.Name = "btnExportBooks";
            this.btnExportBooks.Size = new System.Drawing.Size(100, 23);
            this.btnExportBooks.TabIndex = 12;
            this.btnExportBooks.Text = "Exporter (CSV)";
            this.btnExportBooks.UseVisualStyleBackColor = true;
            this.btnExportBooks.Click += new System.EventHandler(this.btnExportBooks_Click);
            //
            // btnClearBookFields
            //
            this.btnClearBookFields.Location = new System.Drawing.Point(370, 120);
            this.btnClearBookFields.Name = "btnClearBookFields";
            this.btnClearBookFields.Size = new System.Drawing.Size(100, 23);
            this.btnClearBookFields.TabIndex = 11;
            this.btnClearBookFields.Text = "Effacer Champs";
            this.btnClearBookFields.UseVisualStyleBackColor = true;
            this.btnClearBookFields.Click += new System.EventHandler(this.btnClearBookFields_Click);
            //
            // btnSearchBook
            //
            this.btnSearchBook.Location = new System.Drawing.Point(260, 120);
            this.btnSearchBook.Name = "btnSearchBook";
            this.btnSearchBook.Size = new System.Drawing.Size(100, 23);
            this.btnSearchBook.TabIndex = 10;
            this.btnSearchBook.Text = "Rechercher";
            this.btnSearchBook.UseVisualStyleBackColor = true;
            this.btnSearchBook.Click += new System.EventHandler(this.btnSearchBook_Click);
            //
            // btnDeleteBook
            //
            this.btnDeleteBook.Location = new System.Drawing.Point(150, 120);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteBook.TabIndex = 9;
            this.btnDeleteBook.Text = "Supprimer Sel.";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            //
            // btnAddBook
            //
            this.btnAddBook.Location = new System.Drawing.Point(40, 120);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(100, 23);
            this.btnAddBook.TabIndex = 8;
            this.btnAddBook.Text = "Ajouter";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            //
            // btnUpdateBook
            //
            this.btnUpdateBook.Location = new System.Drawing.Point(40, 150);
            this.btnUpdateBook.Name = "btnUpdateBook";
            this.btnUpdateBook.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateBook.TabIndex = 9;
            this.btnUpdateBook.Text = "Mettre à Jour";
            this.btnUpdateBook.UseVisualStyleBackColor = true;
            this.btnUpdateBook.Click += new System.EventHandler(this.btnUpdateBook_Click);
            //
            // txtBookYear
            //
            this.txtBookYear.Location = new System.Drawing.Point(470, 58);
            this.txtBookYear.Name = "txtBookYear";
            this.txtBookYear.Size = new System.Drawing.Size(100, 23); // Taille standard
            this.txtBookYear.TabIndex = 7;
            //
            // lblBookYear
            //
            this.lblBookYear.AutoSize = true;
            this.lblBookYear.Location = new System.Drawing.Point(400, 61);
            this.lblBookYear.Name = "lblBookYear";
            this.lblBookYear.Size = new System.Drawing.Size(43, 15); // Taille standard
            this.lblBookYear.TabIndex = 6;
            this.lblBookYear.Text = "Année:";
            //
            // txtBookISBN
            //
            this.txtBookISBN.Location = new System.Drawing.Point(470, 28);
            this.txtBookISBN.Name = "txtBookISBN";
            this.txtBookISBN.Size = new System.Drawing.Size(180, 23);
            this.txtBookISBN.TabIndex = 5;
            //
            // lblBookISBN
            //
            this.lblBookISBN.AutoSize = true;
            this.lblBookISBN.Location = new System.Drawing.Point(400, 31);
            this.lblBookISBN.Name = "lblBookISBN";
            this.lblBookISBN.Size = new System.Drawing.Size(35, 15);
            this.lblBookISBN.TabIndex = 4;
            this.lblBookISBN.Text = "ISBN:";
            //
            // txtBookAuthor
            //
            this.txtBookAuthor.Location = new System.Drawing.Point(90, 58);
            this.txtBookAuthor.Name = "txtBookAuthor";
            this.txtBookAuthor.Size = new System.Drawing.Size(280, 23);
            this.txtBookAuthor.TabIndex = 3;
            //
            // lblBookAuthor
            //
            this.lblBookAuthor.AutoSize = true;
            this.lblBookAuthor.Location = new System.Drawing.Point(15, 61);
            this.lblBookAuthor.Name = "lblBookAuthor";
            this.lblBookAuthor.Size = new System.Drawing.Size(47, 15);
            this.lblBookAuthor.TabIndex = 2;
            this.lblBookAuthor.Text = "Auteur:";
            //
            // txtBookTitle
            //
            this.txtBookTitle.Location = new System.Drawing.Point(90, 28);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(280, 23);
            this.txtBookTitle.TabIndex = 1;
            // txtBookTitle sert aussi pour la recherche
            //
            // lblBookTitle
            //
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Location = new System.Drawing.Point(15, 31);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(69, 15);
            this.lblBookTitle.TabIndex = 0;
            this.lblBookTitle.Text = "Titre / Rech:";
            //
            // dgvBooks
            //
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(8, 172);
            this.dgvBooks.MultiSelect = false;
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.RowTemplate.Height = 25;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(760, 355); // Prend l'espace restant
            this.dgvBooks.TabIndex = 0;
            // Ajout de l'événement SelectionChanged pour remplir automatiquement les champs
            this.dgvBooks.SelectionChanged += new System.EventHandler(this.dgvBooks_SelectionChanged);
            //
            // tabMembers
            //
            this.tabMembers.Controls.Add(this.groupBoxMembers);
            this.tabMembers.Controls.Add(this.dgvMembers);
            this.tabMembers.Location = new System.Drawing.Point(4, 24);
            this.tabMembers.Name = "tabMembers";
            this.tabMembers.Padding = new System.Windows.Forms.Padding(3);
            this.tabMembers.Size = new System.Drawing.Size(776, 533);
            this.tabMembers.TabIndex = 1;
            this.tabMembers.Text = "Membres";
            this.tabMembers.UseVisualStyleBackColor = true;
            //
            // groupBoxMembers
            //
            this.groupBoxMembers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMembers.Controls.Add(this.btnImportMembers);
            this.groupBoxMembers.Controls.Add(this.btnExportMembers);
            this.groupBoxMembers.Controls.Add(this.btnClearMemberFields);
            this.groupBoxMembers.Controls.Add(this.btnSearchMember);
            this.groupBoxMembers.Controls.Add(this.btnDeleteMember);
            this.groupBoxMembers.Controls.Add(this.btnAddMember);
            this.groupBoxMembers.Controls.Add(this.btnUpdateMember);
            this.groupBoxMembers.Controls.Add(this.txtMemberEmail);
            this.groupBoxMembers.Controls.Add(this.lblMemberEmail);
            this.groupBoxMembers.Controls.Add(this.txtMemberName);
            this.groupBoxMembers.Controls.Add(this.lblMemberName);
            this.groupBoxMembers.Location = new System.Drawing.Point(8, 6);
            this.groupBoxMembers.Name = "groupBoxMembers";
            this.groupBoxMembers.Size = new System.Drawing.Size(760, 130); // Taille exemple
            this.groupBoxMembers.TabIndex = 3;
            this.groupBoxMembers.TabStop = false;
            this.groupBoxMembers.Text = "Détails / Recherche Membre";
            //
            // btnImportMembers
            //
            this.btnImportMembers.Location = new System.Drawing.Point(590, 90);
            this.btnImportMembers.Name = "btnImportMembers";
            this.btnImportMembers.Size = new System.Drawing.Size(100, 23);
            this.btnImportMembers.TabIndex = 9;
            this.btnImportMembers.Text = "Importer (CSV)";
            this.btnImportMembers.UseVisualStyleBackColor = true;
            this.btnImportMembers.Click += new System.EventHandler(this.btnImportMembers_Click);
            //
            // btnExportMembers
            //
            this.btnExportMembers.Location = new System.Drawing.Point(480, 90);
            this.btnExportMembers.Name = "btnExportMembers";
            this.btnExportMembers.Size = new System.Drawing.Size(100, 23);
            this.btnExportMembers.TabIndex = 8;
            this.btnExportMembers.Text = "Exporter (CSV)";
            this.btnExportMembers.UseVisualStyleBackColor = true;
            this.btnExportMembers.Click += new System.EventHandler(this.btnExportMembers_Click);
            //
            // btnClearMemberFields
            //
            this.btnClearMemberFields.Location = new System.Drawing.Point(370, 90);
            this.btnClearMemberFields.Name = "btnClearMemberFields";
            this.btnClearMemberFields.Size = new System.Drawing.Size(100, 23);
            this.btnClearMemberFields.TabIndex = 7;
            this.btnClearMemberFields.Text = "Effacer Champs";
            this.btnClearMemberFields.UseVisualStyleBackColor = true;
            this.btnClearMemberFields.Click += new System.EventHandler(this.btnClearMemberFields_Click);
            //
            // btnSearchMember
            //
            this.btnSearchMember.Location = new System.Drawing.Point(260, 90);
            this.btnSearchMember.Name = "btnSearchMember";
            this.btnSearchMember.Size = new System.Drawing.Size(100, 23);
            this.btnSearchMember.TabIndex = 6;
            this.btnSearchMember.Text = "Rechercher";
            this.btnSearchMember.UseVisualStyleBackColor = true;
            this.btnSearchMember.Click += new System.EventHandler(this.btnSearchMember_Click);
            //
            // btnDeleteMember
            //
            this.btnDeleteMember.Location = new System.Drawing.Point(150, 90);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteMember.TabIndex = 5;
            this.btnDeleteMember.Text = "Supprimer Sel.";
            this.btnDeleteMember.UseVisualStyleBackColor = true;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            //
            // btnAddMember
            //
            this.btnAddMember.Location = new System.Drawing.Point(40, 90);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(100, 23);
            this.btnAddMember.TabIndex = 4;
            this.btnAddMember.Text = "Ajouter";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            //
            // btnUpdateMember
            //
            this.btnUpdateMember.Location = new System.Drawing.Point(40, 120);
            this.btnUpdateMember.Name = "btnUpdateMember";
            this.btnUpdateMember.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateMember.TabIndex = 10;
            this.btnUpdateMember.Text = "Mettre à Jour";
            this.btnUpdateMember.UseVisualStyleBackColor = true;
            this.btnUpdateMember.Click += new System.EventHandler(this.btnUpdateMember_Click);
            //
            // txtMemberEmail
            //
            this.txtMemberEmail.Location = new System.Drawing.Point(90, 58);
            this.txtMemberEmail.Name = "txtMemberEmail";
            this.txtMemberEmail.Size = new System.Drawing.Size(380, 23);
            this.txtMemberEmail.TabIndex = 3;
            //
            // lblMemberEmail
            //
            this.lblMemberEmail.AutoSize = true;
            this.lblMemberEmail.Location = new System.Drawing.Point(15, 61);
            this.lblMemberEmail.Name = "lblMemberEmail";
            this.lblMemberEmail.Size = new System.Drawing.Size(39, 15);
            this.lblMemberEmail.TabIndex = 2;
            this.lblMemberEmail.Text = "Email:";
            //
            // txtMemberName
            //
            this.txtMemberName.Location = new System.Drawing.Point(90, 28);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.Size = new System.Drawing.Size(380, 23);
            this.txtMemberName.TabIndex = 1;
            // txtMemberName sert aussi pour la recherche
            //
            // lblMemberName
            //
            this.lblMemberName.AutoSize = true;
            this.lblMemberName.Location = new System.Drawing.Point(15, 31);
            this.lblMemberName.Name = "lblMemberName";
            this.lblMemberName.Size = new System.Drawing.Size(73, 15);
            this.lblMemberName.TabIndex = 0;
            this.lblMemberName.Text = "Nom / Rech:";
            //
            // dgvMembers
            //
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Location = new System.Drawing.Point(8, 142);
            this.dgvMembers.MultiSelect = false;
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowTemplate.Height = 25;
            this.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.Size = new System.Drawing.Size(760, 385);
            this.dgvMembers.TabIndex = 2;
            // Ajout de l'événement SelectionChanged pour remplir automatiquement les champs
            this.dgvMembers.SelectionChanged += new System.EventHandler(this.dgvMembers_SelectionChanged);
            //
            // tabBorrows
            //
            this.tabBorrows.Controls.Add(this.groupBoxCurrentBorrowings);
            this.tabBorrows.Controls.Add(this.groupBoxNewBorrowing);
            this.tabBorrows.Location = new System.Drawing.Point(4, 24);
            this.tabBorrows.Name = "tabBorrows";
            this.tabBorrows.Size = new System.Drawing.Size(776, 533);
            this.tabBorrows.TabIndex = 2;
            this.tabBorrows.Text = "Emprunts";
            this.tabBorrows.UseVisualStyleBackColor = true;
            //
            // groupBoxCurrentBorrowings
            //
            this.groupBoxCurrentBorrowings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCurrentBorrowings.Controls.Add(this.btnReturnBook);
            this.groupBoxCurrentBorrowings.Controls.Add(this.dgvBorrowings);
            this.groupBoxCurrentBorrowings.Location = new System.Drawing.Point(8, 150); // Positionné sous le premier groupbox
            this.groupBoxCurrentBorrowings.Name = "groupBoxCurrentBorrowings";
            this.groupBoxCurrentBorrowings.Size = new System.Drawing.Size(760, 375); // Prend le reste de la hauteur
            this.groupBoxCurrentBorrowings.TabIndex = 1;
            this.groupBoxCurrentBorrowings.TabStop = false;
            this.groupBoxCurrentBorrowings.Text = "Emprunts en Cours / Retours";
            //
            // btnReturnBook
            //
            this.btnReturnBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturnBook.Location = new System.Drawing.Point(16, 340); // En bas du groupbox
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(180, 23);
            this.btnReturnBook.TabIndex = 1;
            this.btnReturnBook.Text = "Retourner Livre Sélectionné";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            //
            // dgvBorrowings
            //
            this.dgvBorrowings.AllowUserToAddRows = false;
            this.dgvBorrowings.AllowUserToDeleteRows = false;
            this.dgvBorrowings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBorrowings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowings.Location = new System.Drawing.Point(6, 22); // Haut du groupbox
            this.dgvBorrowings.MultiSelect = false;
            this.dgvBorrowings.Name = "dgvBorrowings";
            this.dgvBorrowings.ReadOnly = true;
            this.dgvBorrowings.RowTemplate.Height = 25;
            this.dgvBorrowings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowings.Size = new System.Drawing.Size(748, 312); // Prend l'espace avant le bouton
            this.dgvBorrowings.TabIndex = 0;
            //
            // groupBoxNewBorrowing
            //
            this.groupBoxNewBorrowing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNewBorrowing.Controls.Add(this.btnBorrow);
            this.groupBoxNewBorrowing.Controls.Add(this.dtpDueDate);
            this.groupBoxNewBorrowing.Controls.Add(this.lblDueDate);
            this.groupBoxNewBorrowing.Controls.Add(this.cmbBorrowBook);
            this.groupBoxNewBorrowing.Controls.Add(this.lblBorrowBook);
            this.groupBoxNewBorrowing.Controls.Add(this.cmbBorrowMember);
            this.groupBoxNewBorrowing.Controls.Add(this.lblBorrowMember);
            this.groupBoxNewBorrowing.Location = new System.Drawing.Point(8, 6);
            this.groupBoxNewBorrowing.Name = "groupBoxNewBorrowing";
            this.groupBoxNewBorrowing.Size = new System.Drawing.Size(760, 138); // Taille exemple
            this.groupBoxNewBorrowing.TabIndex = 0;
            this.groupBoxNewBorrowing.TabStop = false;
            this.groupBoxNewBorrowing.Text = "Nouvel Emprunt";
            //
            // btnBorrow
            //
            this.btnBorrow.Location = new System.Drawing.Point(310, 100);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(140, 23);
            this.btnBorrow.TabIndex = 6;
            this.btnBorrow.Text = "Enregistrer l'Emprunt";
            this.btnBorrow.UseVisualStyleBackColor = true;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            //
            // dtpDueDate
            //
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Location = new System.Drawing.Point(140, 70);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(120, 23);
            this.dtpDueDate.TabIndex = 5;
            //
            // lblDueDate
            //
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(15, 74);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(110, 15);
            this.lblDueDate.TabIndex = 4;
            this.lblDueDate.Text = "Date Retour Prévue:";
            //
            // cmbBorrowBook
            //
            this.cmbBorrowBook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBorrowBook.FormattingEnabled = true;
            this.cmbBorrowBook.Location = new System.Drawing.Point(410, 30);
            this.cmbBorrowBook.Name = "cmbBorrowBook";
            this.cmbBorrowBook.Size = new System.Drawing.Size(330, 23);
            this.cmbBorrowBook.TabIndex = 3;
            // Assurez-vous que ValueMember = "Id", DisplayMember = "Title" dans le code
            //
            // lblBorrowBook
            //
            this.lblBorrowBook.AutoSize = true;
            this.lblBorrowBook.Location = new System.Drawing.Point(370, 34);
            this.lblBorrowBook.Name = "lblBorrowBook";
            this.lblBorrowBook.Size = new System.Drawing.Size(35, 15);
            this.lblBorrowBook.TabIndex = 2;
            this.lblBorrowBook.Text = "Livre:";
            //
            // cmbBorrowMember
            //
            this.cmbBorrowMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBorrowMember.FormattingEnabled = true;
            this.cmbBorrowMember.Location = new System.Drawing.Point(70, 30);
            this.cmbBorrowMember.Name = "cmbBorrowMember";
            this.cmbBorrowMember.Size = new System.Drawing.Size(280, 23);
            this.cmbBorrowMember.TabIndex = 1;
            // Assurez-vous que ValueMember = "Id", DisplayMember = "Name" dans le code
            //
            // lblBorrowMember
            //
            this.lblBorrowMember.AutoSize = true;
            this.lblBorrowMember.Location = new System.Drawing.Point(15, 34);
            this.lblBorrowMember.Name = "lblBorrowMember";
            this.lblBorrowMember.Size = new System.Drawing.Size(54, 15);
            this.lblBorrowMember.TabIndex = 0;
            this.lblBorrowMember.Text = "Membre:";
            //
            // tabDashboard
            //
            this.tabDashboard.Controls.Add(this.btnSendOverdueAlerts);
            this.tabDashboard.Controls.Add(this.btnRefreshDashboard);
            this.tabDashboard.Controls.Add(this.lblOverdueBooksCount);
            this.tabDashboard.Controls.Add(this.lblBorrowedBooksCount);
            this.tabDashboard.Controls.Add(this.lblTotalMembersCount);
            this.tabDashboard.Controls.Add(this.lblTotalBooksCount);
            this.tabDashboard.Controls.Add(this.lblOverdueBooksPrompt);
            this.tabDashboard.Controls.Add(this.lblBorrowedBooksPrompt);
            this.tabDashboard.Controls.Add(this.lblTotalMembersPrompt);
            this.tabDashboard.Controls.Add(this.lblTotalBooksPrompt);
            this.tabDashboard.Location = new System.Drawing.Point(4, 24);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(776, 533);
            this.tabDashboard.TabIndex = 3;
            this.tabDashboard.Text = "Tableau de Bord";
            this.tabDashboard.UseVisualStyleBackColor = true;
            //
            // btnSendOverdueAlerts
            //
            this.btnSendOverdueAlerts.Location = new System.Drawing.Point(40, 220);
            this.btnSendOverdueAlerts.Name = "btnSendOverdueAlerts";
            this.btnSendOverdueAlerts.Size = new System.Drawing.Size(150, 23);
            this.btnSendOverdueAlerts.TabIndex = 9;
            this.btnSendOverdueAlerts.Text = "Envoyer Alertes Retard";
            this.btnSendOverdueAlerts.UseVisualStyleBackColor = true;
            this.btnSendOverdueAlerts.Click += new System.EventHandler(this.btnSendOverdueAlerts_Click);
            //
            // btnRefreshDashboard
            //
            this.btnRefreshDashboard.Location = new System.Drawing.Point(40, 180);
            this.btnRefreshDashboard.Name = "btnRefreshDashboard";
            this.btnRefreshDashboard.Size = new System.Drawing.Size(150, 23);
            this.btnRefreshDashboard.TabIndex = 8;
            this.btnRefreshDashboard.Text = "Rafraîchir Statistiques";
            this.btnRefreshDashboard.UseVisualStyleBackColor = true;
            this.btnRefreshDashboard.Click += new System.EventHandler(this.btnRefreshDashboard_Click);
            //
            // lblOverdueBooksCount
            //
            this.lblOverdueBooksCount.AutoSize = true;
            this.lblOverdueBooksCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOverdueBooksCount.Location = new System.Drawing.Point(250, 140);
            this.lblOverdueBooksCount.Name = "lblOverdueBooksCount";
            this.lblOverdueBooksCount.Size = new System.Drawing.Size(15, 17); // Taille exemple
            this.lblOverdueBooksCount.TabIndex = 7;
            this.lblOverdueBooksCount.Text = "0";
            //
            // lblBorrowedBooksCount
            //
            this.lblBorrowedBooksCount.AutoSize = true;
            this.lblBorrowedBooksCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBorrowedBooksCount.Location = new System.Drawing.Point(250, 110);
            this.lblBorrowedBooksCount.Name = "lblBorrowedBooksCount";
            this.lblBorrowedBooksCount.Size = new System.Drawing.Size(15, 17);
            this.lblBorrowedBooksCount.TabIndex = 6;
            this.lblBorrowedBooksCount.Text = "0";
            //
            // lblTotalMembersCount
            //
            this.lblTotalMembersCount.AutoSize = true;
            this.lblTotalMembersCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalMembersCount.Location = new System.Drawing.Point(250, 80);
            this.lblTotalMembersCount.Name = "lblTotalMembersCount";
            this.lblTotalMembersCount.Size = new System.Drawing.Size(15, 17);
            this.lblTotalMembersCount.TabIndex = 5;
            this.lblTotalMembersCount.Text = "0";
            //
            // lblTotalBooksCount
            //
            this.lblTotalBooksCount.AutoSize = true;
            this.lblTotalBooksCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalBooksCount.Location = new System.Drawing.Point(250, 50);
            this.lblTotalBooksCount.Name = "lblTotalBooksCount";
            this.lblTotalBooksCount.Size = new System.Drawing.Size(15, 17);
            this.lblTotalBooksCount.TabIndex = 4;
            this.lblTotalBooksCount.Text = "0";
            //
            // lblOverdueBooksPrompt
            //
            this.lblOverdueBooksPrompt.AutoSize = true;
            this.lblOverdueBooksPrompt.Location = new System.Drawing.Point(40, 140);
            this.lblOverdueBooksPrompt.Name = "lblOverdueBooksPrompt";
            this.lblOverdueBooksPrompt.Size = new System.Drawing.Size(91, 15);
            this.lblOverdueBooksPrompt.TabIndex = 3;
            this.lblOverdueBooksPrompt.Text = "Livres en retard:";
            //
            // lblBorrowedBooksPrompt
            //
            this.lblBorrowedBooksPrompt.AutoSize = true;
            this.lblBorrowedBooksPrompt.Location = new System.Drawing.Point(40, 110);
            this.lblBorrowedBooksPrompt.Name = "lblBorrowedBooksPrompt";
            this.lblBorrowedBooksPrompt.Size = new System.Drawing.Size(175, 15);
            this.lblBorrowedBooksPrompt.TabIndex = 2;
            this.lblBorrowedBooksPrompt.Text = "Livres actuellement empruntés:";
            //
            // lblTotalMembersPrompt
            //
            this.lblTotalMembersPrompt.AutoSize = true;
            this.lblTotalMembersPrompt.Location = new System.Drawing.Point(40, 80);
            this.lblTotalMembersPrompt.Name = "lblTotalMembersPrompt";
            this.lblTotalMembersPrompt.Size = new System.Drawing.Size(155, 15);
            this.lblTotalMembersPrompt.TabIndex = 1;
            this.lblTotalMembersPrompt.Text = "Nombre de membres inscrits:";
            //
            // lblTotalBooksPrompt
            //
            this.lblTotalBooksPrompt.AutoSize = true;
            this.lblTotalBooksPrompt.Location = new System.Drawing.Point(40, 50);
            this.lblTotalBooksPrompt.Name = "lblTotalBooksPrompt";
            this.lblTotalBooksPrompt.Size = new System.Drawing.Size(126, 15);
            this.lblTotalBooksPrompt.TabIndex = 0;
            this.lblTotalBooksPrompt.Text = "Nombre total de livres:";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); // Standard .NET 6+
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561); // Taille exemple
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainForm";
            this.Text = "Gestion de Bibliothèque";
            this.Load += new System.EventHandler(this.MainForm_Load); // Event Load
            this.tabControlMain.ResumeLayout(false);
            this.tabBooks.ResumeLayout(false);
            this.groupBoxBooks.ResumeLayout(false);
            this.groupBoxBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCopiesAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.tabMembers.ResumeLayout(false);
            this.groupBoxMembers.ResumeLayout(false);
            this.groupBoxMembers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.tabBorrows.ResumeLayout(false);
            this.groupBoxCurrentBorrowings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowings)).EndInit();
            this.groupBoxNewBorrowing.ResumeLayout(false);
            this.groupBoxNewBorrowing.PerformLayout();
            this.tabDashboard.ResumeLayout(false);
            this.tabDashboard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.GroupBox groupBoxBooks;
        private System.Windows.Forms.Button btnClearBookFields;
        private System.Windows.Forms.Button btnSearchBook;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnUpdateBook;
        private System.Windows.Forms.TextBox txtBookYear;
        private System.Windows.Forms.Label lblBookYear;
        private System.Windows.Forms.TextBox txtBookISBN;
        private System.Windows.Forms.Label lblBookISBN;
        private System.Windows.Forms.TextBox txtBookAuthor;
        private System.Windows.Forms.Label lblBookAuthor;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.NumericUpDown numCopiesAvailable;
        private System.Windows.Forms.Label lblCopiesAvailable;
        private System.Windows.Forms.TabPage tabMembers;
        private System.Windows.Forms.GroupBox groupBoxMembers;
        private System.Windows.Forms.Button btnClearMemberFields;
        private System.Windows.Forms.Button btnSearchMember;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnUpdateMember;
        private System.Windows.Forms.TextBox txtMemberEmail;
        private System.Windows.Forms.Label lblMemberEmail;
        private System.Windows.Forms.TextBox txtMemberName;
        private System.Windows.Forms.Label lblMemberName;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.TabPage tabBorrows;
        private System.Windows.Forms.GroupBox groupBoxCurrentBorrowings;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.DataGridView dgvBorrowings;
        private System.Windows.Forms.GroupBox groupBoxNewBorrowing;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.ComboBox cmbBorrowBook;
        private System.Windows.Forms.Label lblBorrowBook;
        private System.Windows.Forms.ComboBox cmbBorrowMember;
        private System.Windows.Forms.Label lblBorrowMember;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.Button btnRefreshDashboard;
        private System.Windows.Forms.Label lblOverdueBooksCount;
        private System.Windows.Forms.Label lblBorrowedBooksCount;
        private System.Windows.Forms.Label lblTotalMembersCount;
        private System.Windows.Forms.Label lblTotalBooksCount;
        private System.Windows.Forms.Label lblOverdueBooksPrompt;
        private System.Windows.Forms.Label lblBorrowedBooksPrompt;
        private System.Windows.Forms.Label lblTotalMembersPrompt;
        private System.Windows.Forms.Label lblTotalBooksPrompt;
        private System.Windows.Forms.Button btnSendOverdueAlerts;
        private System.Windows.Forms.Button btnImportBooks;
        private System.Windows.Forms.Button btnExportBooks;
        private System.Windows.Forms.Button btnImportMembers;
        private System.Windows.Forms.Button btnExportMembers;
    }
}
