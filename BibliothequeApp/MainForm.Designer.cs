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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            tabControlMain = new TabControl();
            tabBooks = new TabPage();
            groupBoxBooks = new GroupBox();
            numCopiesAvailable = new NumericUpDown();
            lblCopiesAvailable = new Label();
            btnImportBooks = new Button();
            btnExportBooks = new Button();
            btnClearBookFields = new Button();
            btnSearchBook = new Button();
            btnDeleteBook = new Button();
            btnAddBook = new Button();
            btnUpdateBook = new Button();
            txtBookYear = new TextBox();
            lblBookYear = new Label();
            txtBookISBN = new TextBox();
            lblBookISBN = new Label();
            txtBookAuthor = new TextBox();
            lblBookAuthor = new Label();
            txtBookTitle = new TextBox();
            lblBookTitle = new Label();
            dgvBooks = new DataGridView();
            tabMembers = new TabPage();
            groupBoxMembers = new GroupBox();
            btnImportMembers = new Button();
            btnExportMembers = new Button();
            btnClearMemberFields = new Button();
            btnSearchMember = new Button();
            btnDeleteMember = new Button();
            btnAddMember = new Button();
            btnUpdateMember = new Button();
            txtMemberEmail = new TextBox();
            lblMemberEmail = new Label();
            txtMemberName = new TextBox();
            lblMemberName = new Label();
            dtpSubscriptionEnd = new DateTimePicker();
            lblSubscriptionEnd = new Label();
            dgvMembers = new DataGridView();
            tabBorrows = new TabPage();
            groupBoxCurrentBorrowings = new GroupBox();
            btnReturnBook = new Button();
            dgvBorrowings = new DataGridView();
            groupBoxNewBorrowing = new GroupBox();
            btnBorrow = new Button();
            dtpDueDate = new DateTimePicker();
            lblDueDate = new Label();
            cmbBorrowBook = new ComboBox();
            lblBorrowBook = new Label();
            cmbBorrowMember = new ComboBox();
            lblBorrowMember = new Label();
            tabDashboard = new TabPage();
            chartStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblChartDescription = new Label();
            btnSendOverdueAlerts = new Button();
            btnRefreshDashboard = new Button();
            lblOverdueBooksCount = new Label();
            lblBorrowedBooksCount = new Label();
            lblTotalMembersCount = new Label();
            lblTotalBooksCount = new Label();
            lblOverdueBooksPrompt = new Label();
            lblBorrowedBooksPrompt = new Label();
            lblTotalMembersPrompt = new Label();
            lblTotalBooksPrompt = new Label();
            tabControlMain.SuspendLayout();
            tabBooks.SuspendLayout();
            groupBoxBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCopiesAvailable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            tabMembers.SuspendLayout();
            groupBoxMembers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            tabBorrows.SuspendLayout();
            groupBoxCurrentBorrowings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBorrowings).BeginInit();
            groupBoxNewBorrowing.SuspendLayout();
            tabDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartStatistics).BeginInit();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.Controls.Add(tabBooks);
            tabControlMain.Controls.Add(tabMembers);
            tabControlMain.Controls.Add(tabBorrows);
            tabControlMain.Controls.Add(tabDashboard);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tabControlMain.ItemSize = new Size(140, 30);
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1117, 561);
            tabControlMain.SizeMode = TabSizeMode.Fixed;
            tabControlMain.TabIndex = 0;
            // 
            // tabBooks
            // 
            tabBooks.Controls.Add(groupBoxBooks);
            tabBooks.Controls.Add(dgvBooks);
            tabBooks.Location = new Point(4, 34);
            tabBooks.Name = "tabBooks";
            tabBooks.Padding = new Padding(3);
            tabBooks.Size = new Size(1109, 523);
            tabBooks.TabIndex = 0;
            tabBooks.Text = "Livres";
            tabBooks.UseVisualStyleBackColor = true;
            // 
            // groupBoxBooks
            // 
            groupBoxBooks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxBooks.BackColor = Color.White;
            groupBoxBooks.Controls.Add(numCopiesAvailable);
            groupBoxBooks.Controls.Add(lblCopiesAvailable);
            groupBoxBooks.Controls.Add(btnImportBooks);
            groupBoxBooks.Controls.Add(btnExportBooks);
            groupBoxBooks.Controls.Add(btnClearBookFields);
            groupBoxBooks.Controls.Add(btnSearchBook);
            groupBoxBooks.Controls.Add(btnDeleteBook);
            groupBoxBooks.Controls.Add(btnAddBook);
            groupBoxBooks.Controls.Add(btnUpdateBook);
            groupBoxBooks.Controls.Add(txtBookYear);
            groupBoxBooks.Controls.Add(lblBookYear);
            groupBoxBooks.Controls.Add(txtBookISBN);
            groupBoxBooks.Controls.Add(lblBookISBN);
            groupBoxBooks.Controls.Add(txtBookAuthor);
            groupBoxBooks.Controls.Add(lblBookAuthor);
            groupBoxBooks.Controls.Add(txtBookTitle);
            groupBoxBooks.Controls.Add(lblBookTitle);
            groupBoxBooks.FlatStyle = FlatStyle.Flat;
            groupBoxBooks.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxBooks.ForeColor = Color.FromArgb(33, 37, 41);
            groupBoxBooks.Location = new Point(8, 6);
            groupBoxBooks.Name = "groupBoxBooks";
            groupBoxBooks.Padding = new Padding(12, 8, 12, 8);
            groupBoxBooks.Size = new Size(1093, 167);
            groupBoxBooks.TabIndex = 1;
            groupBoxBooks.TabStop = false;
            groupBoxBooks.Text = "Détails / Recherche Livre";
            // 
            // numCopiesAvailable
            // 
            numCopiesAvailable.Font = new Font("Segoe UI", 10F);
            numCopiesAvailable.Location = new Point(765, 66);
            numCopiesAvailable.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numCopiesAvailable.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCopiesAvailable.Name = "numCopiesAvailable";
            numCopiesAvailable.Size = new Size(80, 30);
            numCopiesAvailable.TabIndex = 15;
            numCopiesAvailable.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numCopiesAvailable.ValueChanged += numCopiesAvailable_ValueChanged;
            // 
            // lblCopiesAvailable
            // 
            lblCopiesAvailable.AutoSize = true;
            lblCopiesAvailable.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCopiesAvailable.Location = new Point(648, 68);
            lblCopiesAvailable.Name = "lblCopiesAvailable";
            lblCopiesAvailable.Size = new Size(111, 23);
            lblCopiesAvailable.TabIndex = 14;
            lblCopiesAvailable.Text = "Exemplaires:";
            // 
            // btnImportBooks
            // 
            btnImportBooks.BackColor = Color.FromArgb(0, 120, 215);
            btnImportBooks.Cursor = Cursors.Hand;
            btnImportBooks.FlatAppearance.BorderSize = 0;
            btnImportBooks.FlatStyle = FlatStyle.Flat;
            btnImportBooks.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImportBooks.ForeColor = Color.White;
            btnImportBooks.Location = new Point(659, 111);
            btnImportBooks.Name = "btnImportBooks";
            btnImportBooks.Size = new Size(100, 36);
            btnImportBooks.TabIndex = 13;
            btnImportBooks.Text = "Importer (CSV)";
            btnImportBooks.UseVisualStyleBackColor = false;
            btnImportBooks.Click += btnImportBooks_Click;
            // 
            // btnExportBooks
            // 
            btnExportBooks.BackColor = Color.Blue;
            btnExportBooks.Cursor = Cursors.Hand;
            btnExportBooks.FlatAppearance.BorderSize = 0;
            btnExportBooks.FlatStyle = FlatStyle.Flat;
            btnExportBooks.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportBooks.ForeColor = Color.White;
            btnExportBooks.Location = new Point(549, 111);
            btnExportBooks.Name = "btnExportBooks";
            btnExportBooks.Size = new Size(100, 36);
            btnExportBooks.TabIndex = 12;
            btnExportBooks.Text = "Exporter (CSV)";
            btnExportBooks.UseVisualStyleBackColor = false;
            btnExportBooks.Click += btnExportBooks_Click;
            // 
            // btnClearBookFields
            // 
            btnClearBookFields.BackColor = Color.DarkKhaki;
            btnClearBookFields.Cursor = Cursors.Hand;
            btnClearBookFields.FlatAppearance.BorderSize = 0;
            btnClearBookFields.FlatStyle = FlatStyle.Flat;
            btnClearBookFields.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearBookFields.ForeColor = Color.White;
            btnClearBookFields.Location = new Point(439, 111);
            btnClearBookFields.Name = "btnClearBookFields";
            btnClearBookFields.Size = new Size(100, 36);
            btnClearBookFields.TabIndex = 11;
            btnClearBookFields.Text = "Effacer Champs";
            btnClearBookFields.UseVisualStyleBackColor = false;
            btnClearBookFields.Click += btnClearBookFields_Click;
            // 
            // btnSearchBook
            // 
            btnSearchBook.BackColor = Color.FromArgb(0, 120, 215);
            btnSearchBook.Cursor = Cursors.Hand;
            btnSearchBook.FlatAppearance.BorderSize = 0;
            btnSearchBook.FlatStyle = FlatStyle.Flat;
            btnSearchBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearchBook.ForeColor = Color.White;
            btnSearchBook.Location = new Point(329, 111);
            btnSearchBook.Name = "btnSearchBook";
            btnSearchBook.Size = new Size(100, 36);
            btnSearchBook.TabIndex = 10;
            btnSearchBook.Text = "Rechercher";
            btnSearchBook.UseVisualStyleBackColor = false;
            btnSearchBook.Click += btnSearchBook_Click;
            // 
            // btnDeleteBook
            // 
            btnDeleteBook.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteBook.Cursor = Cursors.Hand;
            btnDeleteBook.FlatAppearance.BorderSize = 0;
            btnDeleteBook.FlatStyle = FlatStyle.Flat;
            btnDeleteBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteBook.ForeColor = Color.White;
            btnDeleteBook.Location = new Point(215, 111);
            btnDeleteBook.Name = "btnDeleteBook";
            btnDeleteBook.Size = new Size(104, 36);
            btnDeleteBook.TabIndex = 9;
            btnDeleteBook.Text = "Supprimer Sel.";
            btnDeleteBook.UseVisualStyleBackColor = false;
            btnDeleteBook.Click += btnDeleteBook_Click;
            // 
            // btnAddBook
            // 
            btnAddBook.BackColor = Color.FromArgb(40, 167, 69);
            btnAddBook.Cursor = Cursors.Hand;
            btnAddBook.FlatAppearance.BorderSize = 0;
            btnAddBook.FlatStyle = FlatStyle.Flat;
            btnAddBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddBook.ForeColor = Color.White;
            btnAddBook.Location = new Point(109, 111);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(100, 36);
            btnAddBook.TabIndex = 8;
            btnAddBook.Text = "Ajouter";
            btnAddBook.UseVisualStyleBackColor = false;
            btnAddBook.Click += btnAddBook_Click;
            // 
            // btnUpdateBook
            // 
            btnUpdateBook.BackColor = Color.FromArgb(0, 120, 215);
            btnUpdateBook.Cursor = Cursors.Hand;
            btnUpdateBook.FlatAppearance.BorderSize = 0;
            btnUpdateBook.FlatStyle = FlatStyle.Flat;
            btnUpdateBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdateBook.ForeColor = Color.White;
            btnUpdateBook.Location = new Point(765, 111);
            btnUpdateBook.Name = "btnUpdateBook";
            btnUpdateBook.Size = new Size(150, 36);
            btnUpdateBook.TabIndex = 9;
            btnUpdateBook.Text = "Mettre à Jour";
            btnUpdateBook.UseVisualStyleBackColor = false;
            btnUpdateBook.Click += btnUpdateBook_Click;
            // 
            // txtBookYear
            // 
            txtBookYear.Font = new Font("Segoe UI", 10F);
            txtBookYear.Location = new Point(529, 66);
            txtBookYear.Name = "txtBookYear";
            txtBookYear.Size = new Size(100, 30);
            txtBookYear.TabIndex = 7;
            // 
            // lblBookYear
            // 
            lblBookYear.AutoSize = true;
            lblBookYear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookYear.Location = new Point(444, 69);
            lblBookYear.Name = "lblBookYear";
            lblBookYear.Size = new Size(65, 23);
            lblBookYear.TabIndex = 6;
            lblBookYear.Text = "Année:";
            // 
            // txtBookISBN
            // 
            txtBookISBN.Font = new Font("Segoe UI", 10F);
            txtBookISBN.Location = new Point(495, 24);
            txtBookISBN.Name = "txtBookISBN";
            txtBookISBN.Size = new Size(180, 30);
            txtBookISBN.TabIndex = 5;
            // 
            // lblBookISBN
            // 
            lblBookISBN.AutoSize = true;
            lblBookISBN.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookISBN.Location = new Point(435, 28);
            lblBookISBN.Name = "lblBookISBN";
            lblBookISBN.Size = new Size(54, 23);
            lblBookISBN.TabIndex = 4;
            lblBookISBN.Text = "ISBN:";
            lblBookISBN.Click += lblBookISBN_Click;
            // 
            // txtBookAuthor
            // 
            txtBookAuthor.Font = new Font("Segoe UI", 10F);
            txtBookAuthor.Location = new Point(129, 65);
            txtBookAuthor.Name = "txtBookAuthor";
            txtBookAuthor.Size = new Size(280, 30);
            txtBookAuthor.TabIndex = 3;
            // 
            // lblBookAuthor
            // 
            lblBookAuthor.AutoSize = true;
            lblBookAuthor.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookAuthor.Location = new Point(29, 68);
            lblBookAuthor.Name = "lblBookAuthor";
            lblBookAuthor.Size = new Size(71, 23);
            lblBookAuthor.TabIndex = 2;
            lblBookAuthor.Text = "Auteur:";
            // 
            // txtBookTitle
            // 
            txtBookTitle.Font = new Font("Segoe UI", 10F);
            txtBookTitle.Location = new Point(130, 28);
            txtBookTitle.Name = "txtBookTitle";
            txtBookTitle.Size = new Size(280, 30);
            txtBookTitle.TabIndex = 1;
            // 
            // lblBookTitle
            // 
            lblBookTitle.AutoSize = true;
            lblBookTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBookTitle.Location = new Point(15, 31);
            lblBookTitle.Name = "lblBookTitle";
            lblBookTitle.Size = new Size(109, 23);
            lblBookTitle.TabIndex = 0;
            lblBookTitle.Text = "Titre / Rech:";
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 247, 250);
            dgvBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvBooks.DefaultCellStyle = dataGridViewCellStyle3;
            dgvBooks.EnableHeadersVisualStyles = false;
            dgvBooks.Location = new Point(13, 177);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.RowTemplate.Height = 25;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(1093, 338);
            dgvBooks.TabIndex = 0;
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
            // 
            // tabMembers
            // 
            tabMembers.Controls.Add(groupBoxMembers);
            tabMembers.Controls.Add(dgvMembers);
            tabMembers.Location = new Point(4, 34);
            tabMembers.Name = "tabMembers";
            tabMembers.Padding = new Padding(3);
            tabMembers.Size = new Size(1109, 523);
            tabMembers.TabIndex = 1;
            tabMembers.Text = "Membres";
            tabMembers.UseVisualStyleBackColor = true;
            // 
            // groupBoxMembers
            // 
            groupBoxMembers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxMembers.BackColor = Color.White;
            groupBoxMembers.Controls.Add(btnImportMembers);
            groupBoxMembers.Controls.Add(btnExportMembers);
            groupBoxMembers.Controls.Add(btnClearMemberFields);
            groupBoxMembers.Controls.Add(btnSearchMember);
            groupBoxMembers.Controls.Add(btnDeleteMember);
            groupBoxMembers.Controls.Add(btnAddMember);
            groupBoxMembers.Controls.Add(btnUpdateMember);
            groupBoxMembers.Controls.Add(txtMemberEmail);
            groupBoxMembers.Controls.Add(lblMemberEmail);
            groupBoxMembers.Controls.Add(txtMemberName);
            groupBoxMembers.Controls.Add(lblMemberName);
            groupBoxMembers.Controls.Add(dtpSubscriptionEnd);
            groupBoxMembers.Controls.Add(lblSubscriptionEnd);
            groupBoxMembers.FlatStyle = FlatStyle.Flat;
            groupBoxMembers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxMembers.ForeColor = Color.FromArgb(33, 37, 41);
            groupBoxMembers.Location = new Point(8, 6);
            groupBoxMembers.Name = "groupBoxMembers";
            groupBoxMembers.Padding = new Padding(12, 8, 12, 8);
            groupBoxMembers.Size = new Size(1093, 150);
            groupBoxMembers.TabIndex = 3;
            groupBoxMembers.TabStop = false;
            groupBoxMembers.Text = "Détails / Recherche Membre";
            // 
            // btnImportMembers
            // 
            btnImportMembers.BackColor = Color.FromArgb(0, 120, 215);
            btnImportMembers.Cursor = Cursors.Hand;
            btnImportMembers.FlatAppearance.BorderSize = 0;
            btnImportMembers.FlatStyle = FlatStyle.Flat;
            btnImportMembers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnImportMembers.ForeColor = Color.White;
            btnImportMembers.Location = new Point(647, 101);
            btnImportMembers.Name = "btnImportMembers";
            btnImportMembers.Size = new Size(100, 38);
            btnImportMembers.TabIndex = 9;
            btnImportMembers.Text = "Importer (CSV)";
            btnImportMembers.UseVisualStyleBackColor = false;
            btnImportMembers.Click += btnImportMembers_Click;
            // 
            // btnExportMembers
            // 
            btnExportMembers.BackColor = Color.Blue;
            btnExportMembers.Cursor = Cursors.Hand;
            btnExportMembers.FlatAppearance.BorderSize = 0;
            btnExportMembers.FlatStyle = FlatStyle.Flat;
            btnExportMembers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnExportMembers.ForeColor = Color.White;
            btnExportMembers.Location = new Point(537, 101);
            btnExportMembers.Name = "btnExportMembers";
            btnExportMembers.Size = new Size(100, 38);
            btnExportMembers.TabIndex = 8;
            btnExportMembers.Text = "Exporter (CSV)";
            btnExportMembers.UseVisualStyleBackColor = false;
            btnExportMembers.Click += btnExportMembers_Click;
            // 
            // btnClearMemberFields
            // 
            btnClearMemberFields.BackColor = Color.DarkKhaki;
            btnClearMemberFields.Cursor = Cursors.Hand;
            btnClearMemberFields.FlatAppearance.BorderSize = 0;
            btnClearMemberFields.FlatStyle = FlatStyle.Flat;
            btnClearMemberFields.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearMemberFields.ForeColor = SystemColors.Window;
            btnClearMemberFields.Location = new Point(427, 101);
            btnClearMemberFields.Name = "btnClearMemberFields";
            btnClearMemberFields.Size = new Size(100, 38);
            btnClearMemberFields.TabIndex = 7;
            btnClearMemberFields.Text = "Effacer Champs";
            btnClearMemberFields.UseVisualStyleBackColor = false;
            btnClearMemberFields.Click += btnClearMemberFields_Click;
            // 
            // btnSearchMember
            // 
            btnSearchMember.BackColor = Color.FromArgb(0, 120, 215);
            btnSearchMember.Cursor = Cursors.Hand;
            btnSearchMember.FlatAppearance.BorderSize = 0;
            btnSearchMember.FlatStyle = FlatStyle.Flat;
            btnSearchMember.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearchMember.ForeColor = Color.White;
            btnSearchMember.Location = new Point(321, 101);
            btnSearchMember.Name = "btnSearchMember";
            btnSearchMember.Size = new Size(100, 38);
            btnSearchMember.TabIndex = 6;
            btnSearchMember.Text = "Rechercher";
            btnSearchMember.UseVisualStyleBackColor = false;
            btnSearchMember.Click += btnSearchMember_Click;
            // 
            // btnDeleteMember
            // 
            btnDeleteMember.BackColor = Color.FromArgb(220, 53, 69);
            btnDeleteMember.Cursor = Cursors.Hand;
            btnDeleteMember.FlatAppearance.BorderSize = 0;
            btnDeleteMember.FlatStyle = FlatStyle.Flat;
            btnDeleteMember.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteMember.ForeColor = Color.White;
            btnDeleteMember.Location = new Point(215, 101);
            btnDeleteMember.Name = "btnDeleteMember";
            btnDeleteMember.Size = new Size(100, 38);
            btnDeleteMember.TabIndex = 5;
            btnDeleteMember.Text = "Supprimer Sel.";
            btnDeleteMember.UseVisualStyleBackColor = false;
            btnDeleteMember.Click += btnDeleteMember_Click;
            // 
            // btnAddMember
            // 
            btnAddMember.BackColor = Color.FromArgb(40, 167, 69);
            btnAddMember.Cursor = Cursors.Hand;
            btnAddMember.FlatAppearance.BorderSize = 0;
            btnAddMember.FlatStyle = FlatStyle.Flat;
            btnAddMember.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddMember.ForeColor = Color.White;
            btnAddMember.Location = new Point(109, 101);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Size = new Size(100, 38);
            btnAddMember.TabIndex = 4;
            btnAddMember.Text = "Ajouter";
            btnAddMember.UseVisualStyleBackColor = false;
            btnAddMember.Click += btnAddMember_Click;
            // 
            // btnUpdateMember
            // 
            btnUpdateMember.BackColor = Color.FromArgb(0, 120, 215);
            btnUpdateMember.Cursor = Cursors.Hand;
            btnUpdateMember.FlatAppearance.BorderSize = 0;
            btnUpdateMember.FlatStyle = FlatStyle.Flat;
            btnUpdateMember.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdateMember.ForeColor = Color.White;
            btnUpdateMember.Location = new Point(753, 101);
            btnUpdateMember.Name = "btnUpdateMember";
            btnUpdateMember.Size = new Size(143, 38);
            btnUpdateMember.TabIndex = 10;
            btnUpdateMember.Text = "Mettre a Jour";
            btnUpdateMember.UseVisualStyleBackColor = false;
            btnUpdateMember.Click += btnUpdateMember_Click;
            // 
            // txtMemberEmail
            // 
            txtMemberEmail.Font = new Font("Segoe UI", 10F);
            txtMemberEmail.Location = new Point(150, 64);
            txtMemberEmail.Name = "txtMemberEmail";
            txtMemberEmail.Size = new Size(380, 30);
            txtMemberEmail.TabIndex = 3;
            // 
            // lblMemberEmail
            // 
            lblMemberEmail.AutoSize = true;
            lblMemberEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMemberEmail.Location = new Point(40, 67);
            lblMemberEmail.Name = "lblMemberEmail";
            lblMemberEmail.Size = new Size(59, 23);
            lblMemberEmail.TabIndex = 2;
            lblMemberEmail.Text = "Email:";
            // 
            // txtMemberName
            // 
            txtMemberName.Font = new Font("Segoe UI", 10F);
            txtMemberName.Location = new Point(150, 28);
            txtMemberName.Name = "txtMemberName";
            txtMemberName.Size = new Size(380, 30);
            txtMemberName.TabIndex = 1;
            // 
            // lblMemberName
            // 
            lblMemberName.AutoSize = true;
            lblMemberName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMemberName.Location = new Point(15, 31);
            lblMemberName.Name = "lblMemberName";
            lblMemberName.Size = new Size(110, 23);
            lblMemberName.TabIndex = 0;
            lblMemberName.Text = "Nom / Rech:";
            // 
            // dtpSubscriptionEnd
            // 
            dtpSubscriptionEnd.Font = new Font("Segoe UI", 10F);
            dtpSubscriptionEnd.Format = DateTimePickerFormat.Short;
            dtpSubscriptionEnd.Location = new Point(723, 43);
            dtpSubscriptionEnd.Name = "dtpSubscriptionEnd";
            dtpSubscriptionEnd.ShowCheckBox = true;
            dtpSubscriptionEnd.Size = new Size(200, 30);
            dtpSubscriptionEnd.TabIndex = 4;
            dtpSubscriptionEnd.Value = new DateTime(2026, 5, 13, 12, 48, 11, 981);
            // 
            // lblSubscriptionEnd
            // 
            lblSubscriptionEnd.AutoSize = true;
            lblSubscriptionEnd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubscriptionEnd.Location = new Point(571, 49);
            lblSubscriptionEnd.Name = "lblSubscriptionEnd";
            lblSubscriptionEnd.Size = new Size(155, 23);
            lblSubscriptionEnd.TabIndex = 5;
            lblSubscriptionEnd.Text = "Fin Abonnements:";
            // 
            // dgvMembers
            // 
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(245, 247, 250);
            dgvMembers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvMembers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMembers.BackgroundColor = Color.White;
            dgvMembers.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvMembers.DefaultCellStyle = dataGridViewCellStyle6;
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.Location = new Point(8, 162);
            dgvMembers.MultiSelect = false;
            dgvMembers.Name = "dgvMembers";
            dgvMembers.ReadOnly = true;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.RowHeadersWidth = 51;
            dgvMembers.RowTemplate.Height = 25;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.Size = new Size(1093, 355);
            dgvMembers.TabIndex = 2;
            dgvMembers.SelectionChanged += dgvMembers_SelectionChanged;
            // 
            // tabBorrows
            // 
            tabBorrows.Controls.Add(groupBoxCurrentBorrowings);
            tabBorrows.Controls.Add(groupBoxNewBorrowing);
            tabBorrows.Location = new Point(4, 34);
            tabBorrows.Name = "tabBorrows";
            tabBorrows.Size = new Size(1109, 523);
            tabBorrows.TabIndex = 2;
            tabBorrows.Text = "Emprunts";
            tabBorrows.UseVisualStyleBackColor = true;
            // 
            // groupBoxCurrentBorrowings
            // 
            groupBoxCurrentBorrowings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxCurrentBorrowings.BackColor = Color.White;
            groupBoxCurrentBorrowings.Controls.Add(btnReturnBook);
            groupBoxCurrentBorrowings.Controls.Add(dgvBorrowings);
            groupBoxCurrentBorrowings.FlatStyle = FlatStyle.Flat;
            groupBoxCurrentBorrowings.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxCurrentBorrowings.ForeColor = Color.FromArgb(33, 37, 41);
            groupBoxCurrentBorrowings.Location = new Point(8, 161);
            groupBoxCurrentBorrowings.Name = "groupBoxCurrentBorrowings";
            groupBoxCurrentBorrowings.Padding = new Padding(12, 8, 12, 8);
            groupBoxCurrentBorrowings.Size = new Size(1093, 354);
            groupBoxCurrentBorrowings.TabIndex = 1;
            groupBoxCurrentBorrowings.TabStop = false;
            groupBoxCurrentBorrowings.Text = "Emprunts en Cours / Retours";
            // 
            // btnReturnBook
            // 
            btnReturnBook.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReturnBook.BackColor = Color.FromArgb(220, 53, 69);
            btnReturnBook.Cursor = Cursors.Hand;
            btnReturnBook.FlatAppearance.BorderSize = 0;
            btnReturnBook.FlatStyle = FlatStyle.Flat;
            btnReturnBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReturnBook.ForeColor = Color.White;
            btnReturnBook.Location = new Point(16, 307);
            btnReturnBook.Name = "btnReturnBook";
            btnReturnBook.Size = new Size(180, 35);
            btnReturnBook.TabIndex = 1;
            btnReturnBook.Text = "Retourner Livre Sélectionné";
            btnReturnBook.UseVisualStyleBackColor = false;
            btnReturnBook.Click += btnReturnBook_Click;
            // 
            // dgvBorrowings
            // 
            dgvBorrowings.AllowUserToAddRows = false;
            dgvBorrowings.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(245, 247, 250);
            dgvBorrowings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dgvBorrowings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBorrowings.BackgroundColor = Color.White;
            dgvBorrowings.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvBorrowings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvBorrowings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dgvBorrowings.DefaultCellStyle = dataGridViewCellStyle9;
            dgvBorrowings.EnableHeadersVisualStyles = false;
            dgvBorrowings.Location = new Point(6, 24);
            dgvBorrowings.MultiSelect = false;
            dgvBorrowings.Name = "dgvBorrowings";
            dgvBorrowings.ReadOnly = true;
            dgvBorrowings.RowHeadersVisible = false;
            dgvBorrowings.RowHeadersWidth = 51;
            dgvBorrowings.RowTemplate.Height = 25;
            dgvBorrowings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBorrowings.Size = new Size(1072, 289);
            dgvBorrowings.TabIndex = 0;
            // 
            // groupBoxNewBorrowing
            // 
            groupBoxNewBorrowing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxNewBorrowing.BackColor = Color.White;
            groupBoxNewBorrowing.Controls.Add(btnBorrow);
            groupBoxNewBorrowing.Controls.Add(dtpDueDate);
            groupBoxNewBorrowing.Controls.Add(lblDueDate);
            groupBoxNewBorrowing.Controls.Add(cmbBorrowBook);
            groupBoxNewBorrowing.Controls.Add(lblBorrowBook);
            groupBoxNewBorrowing.Controls.Add(cmbBorrowMember);
            groupBoxNewBorrowing.Controls.Add(lblBorrowMember);
            groupBoxNewBorrowing.FlatStyle = FlatStyle.Flat;
            groupBoxNewBorrowing.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxNewBorrowing.ForeColor = Color.FromArgb(33, 37, 41);
            groupBoxNewBorrowing.Location = new Point(8, 6);
            groupBoxNewBorrowing.Name = "groupBoxNewBorrowing";
            groupBoxNewBorrowing.Padding = new Padding(12, 8, 12, 8);
            groupBoxNewBorrowing.Size = new Size(1093, 149);
            groupBoxNewBorrowing.TabIndex = 0;
            groupBoxNewBorrowing.TabStop = false;
            groupBoxNewBorrowing.Text = "Nouvel Emprunt";
            // 
            // btnBorrow
            // 
            btnBorrow.BackColor = Color.FromArgb(40, 167, 69);
            btnBorrow.Cursor = Cursors.Hand;
            btnBorrow.FlatAppearance.BorderSize = 0;
            btnBorrow.FlatStyle = FlatStyle.Flat;
            btnBorrow.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBorrow.ForeColor = Color.White;
            btnBorrow.Location = new Point(337, 100);
            btnBorrow.Name = "btnBorrow";
            btnBorrow.Size = new Size(140, 38);
            btnBorrow.TabIndex = 6;
            btnBorrow.Text = "Enregistrer l'Emprunt";
            btnBorrow.UseVisualStyleBackColor = false;
            btnBorrow.Click += btnBorrow_Click;
            // 
            // dtpDueDate
            // 
            dtpDueDate.Font = new Font("Segoe UI", 10F);
            dtpDueDate.Format = DateTimePickerFormat.Short;
            dtpDueDate.Location = new Point(192, 68);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(120, 30);
            dtpDueDate.TabIndex = 5;
            // 
            // lblDueDate
            // 
            lblDueDate.AutoSize = true;
            lblDueDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDueDate.Location = new Point(15, 74);
            lblDueDate.Name = "lblDueDate";
            lblDueDate.Size = new Size(171, 23);
            lblDueDate.TabIndex = 4;
            lblDueDate.Text = "Date Retour Prévue:";
            // 
            // cmbBorrowBook
            // 
            cmbBorrowBook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBorrowBook.Font = new Font("Segoe UI", 10F);
            cmbBorrowBook.FormattingEnabled = true;
            cmbBorrowBook.Location = new Point(464, 30);
            cmbBorrowBook.Name = "cmbBorrowBook";
            cmbBorrowBook.Size = new Size(330, 31);
            cmbBorrowBook.TabIndex = 3;
            cmbBorrowBook.SelectedIndexChanged += cmbBorrowBook_SelectedIndexChanged;
            // 
            // lblBorrowBook
            // 
            lblBorrowBook.AutoSize = true;
            lblBorrowBook.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBorrowBook.Location = new Point(404, 34);
            lblBorrowBook.Name = "lblBorrowBook";
            lblBorrowBook.Size = new Size(54, 23);
            lblBorrowBook.TabIndex = 2;
            lblBorrowBook.Text = "Livre:";
            // 
            // cmbBorrowMember
            // 
            cmbBorrowMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBorrowMember.Font = new Font("Segoe UI", 10F);
            cmbBorrowMember.FormattingEnabled = true;
            cmbBorrowMember.Location = new Point(104, 30);
            cmbBorrowMember.Name = "cmbBorrowMember";
            cmbBorrowMember.Size = new Size(280, 31);
            cmbBorrowMember.TabIndex = 1;
            // 
            // lblBorrowMember
            // 
            lblBorrowMember.AutoSize = true;
            lblBorrowMember.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBorrowMember.Location = new Point(15, 34);
            lblBorrowMember.Name = "lblBorrowMember";
            lblBorrowMember.Size = new Size(83, 23);
            lblBorrowMember.TabIndex = 0;
            lblBorrowMember.Text = "Membre:";
            // 
            // tabDashboard
            // 
            tabDashboard.BackColor = Color.WhiteSmoke;
            tabDashboard.Controls.Add(chartStatistics);
            tabDashboard.Controls.Add(lblChartDescription);
            tabDashboard.Controls.Add(btnSendOverdueAlerts);
            tabDashboard.Controls.Add(btnRefreshDashboard);
            tabDashboard.Controls.Add(lblOverdueBooksCount);
            tabDashboard.Controls.Add(lblBorrowedBooksCount);
            tabDashboard.Controls.Add(lblTotalMembersCount);
            tabDashboard.Controls.Add(lblTotalBooksCount);
            tabDashboard.Controls.Add(lblOverdueBooksPrompt);
            tabDashboard.Controls.Add(lblBorrowedBooksPrompt);
            tabDashboard.Controls.Add(lblTotalMembersPrompt);
            tabDashboard.Controls.Add(lblTotalBooksPrompt);
            tabDashboard.Location = new Point(4, 34);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(3);
            tabDashboard.Size = new Size(1109, 523);
            tabDashboard.TabIndex = 3;
            tabDashboard.Text = "Tableau de Bord";
            // 
            // chartStatistics
            // 
            chartStatistics.BackColor = Color.Transparent;
            chartStatistics.BorderlineColor = Color.FromArgb(0, 120, 215);
            chartStatistics.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartStatistics.BorderlineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea1.BackColor = Color.WhiteSmoke;
            chartArea1.Name = "MainChartArea";
            chartStatistics.ChartAreas.Add(chartArea1);
            legend1.BackColor = Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "MainLegend";
            chartStatistics.Legends.Add(legend1);
            chartStatistics.Location = new Point(229, 220);
            chartStatistics.Name = "chartStatistics";
            series1.ChartArea = "MainChartArea";
            series1.Color = Color.FromArgb(0, 120, 215);
            series1.Legend = "MainLegend";
            series1.Name = "Livres";
            series2.ChartArea = "MainChartArea";
            series2.Color = Color.FromArgb(255, 140, 0);
            series2.Legend = "MainLegend";
            series2.Name = "Emprunts";
            series3.ChartArea = "MainChartArea";
            series3.Color = Color.FromArgb(220, 53, 69);
            series3.Legend = "MainLegend";
            series3.Name = "En retard";
            chartStatistics.Series.Add(series1);
            chartStatistics.Series.Add(series2);
            chartStatistics.Series.Add(series3);
            chartStatistics.Size = new Size(750, 300);
            chartStatistics.TabIndex = 10;
            chartStatistics.Text = "Statistiques de la bibliothèque";
            title1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Statistiques mensuelles";
            chartStatistics.Titles.Add(title1);
            // 
            // lblChartDescription
            // 
            lblChartDescription.AutoSize = true;
            lblChartDescription.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChartDescription.Location = new Point(446, 178);
            lblChartDescription.Name = "lblChartDescription";
            lblChartDescription.Size = new Size(300, 25);
            lblChartDescription.TabIndex = 11;
            lblChartDescription.Text = "Statistiques des 6 derniers mois :";
            // 
            // btnSendOverdueAlerts
            // 
            btnSendOverdueAlerts.BackColor = Color.FromArgb(220, 53, 69);
            btnSendOverdueAlerts.Cursor = Cursors.Hand;
            btnSendOverdueAlerts.FlatAppearance.BorderSize = 0;
            btnSendOverdueAlerts.FlatStyle = FlatStyle.Flat;
            btnSendOverdueAlerts.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendOverdueAlerts.ForeColor = Color.White;
            btnSendOverdueAlerts.Location = new Point(511, 110);
            btnSendOverdueAlerts.Name = "btnSendOverdueAlerts";
            btnSendOverdueAlerts.Size = new Size(150, 36);
            btnSendOverdueAlerts.TabIndex = 9;
            btnSendOverdueAlerts.Text = "Envoyer Alertes Retard";
            btnSendOverdueAlerts.UseVisualStyleBackColor = false;
            btnSendOverdueAlerts.Click += btnSendOverdueAlerts_Click;
            // 
            // btnRefreshDashboard
            // 
            btnRefreshDashboard.BackColor = Color.FromArgb(0, 120, 215);
            btnRefreshDashboard.Cursor = Cursors.Hand;
            btnRefreshDashboard.FlatAppearance.BorderSize = 0;
            btnRefreshDashboard.FlatStyle = FlatStyle.Flat;
            btnRefreshDashboard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefreshDashboard.ForeColor = Color.White;
            btnRefreshDashboard.Location = new Point(486, 50);
            btnRefreshDashboard.Name = "btnRefreshDashboard";
            btnRefreshDashboard.Size = new Size(198, 36);
            btnRefreshDashboard.TabIndex = 8;
            btnRefreshDashboard.Text = "Rafraichir Statistiques";
            btnRefreshDashboard.UseVisualStyleBackColor = false;
            btnRefreshDashboard.Click += btnRefreshDashboard_Click;
            // 
            // lblOverdueBooksCount
            // 
            lblOverdueBooksCount.AutoSize = true;
            lblOverdueBooksCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblOverdueBooksCount.ForeColor = Color.FromArgb(220, 53, 69);
            lblOverdueBooksCount.Location = new Point(334, 143);
            lblOverdueBooksCount.Name = "lblOverdueBooksCount";
            lblOverdueBooksCount.Size = new Size(35, 41);
            lblOverdueBooksCount.TabIndex = 7;
            lblOverdueBooksCount.Text = "0";
            // 
            // lblBorrowedBooksCount
            // 
            lblBorrowedBooksCount.AutoSize = true;
            lblBorrowedBooksCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBorrowedBooksCount.ForeColor = Color.FromArgb(255, 140, 0);
            lblBorrowedBooksCount.Location = new Point(334, 110);
            lblBorrowedBooksCount.Name = "lblBorrowedBooksCount";
            lblBorrowedBooksCount.Size = new Size(35, 41);
            lblBorrowedBooksCount.TabIndex = 6;
            lblBorrowedBooksCount.Text = "0";
            // 
            // lblTotalMembersCount
            // 
            lblTotalMembersCount.AutoSize = true;
            lblTotalMembersCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalMembersCount.ForeColor = Color.FromArgb(0, 120, 215);
            lblTotalMembersCount.Location = new Point(334, 78);
            lblTotalMembersCount.Name = "lblTotalMembersCount";
            lblTotalMembersCount.Size = new Size(35, 41);
            lblTotalMembersCount.TabIndex = 5;
            lblTotalMembersCount.Text = "0";
            // 
            // lblTotalBooksCount
            // 
            lblTotalBooksCount.AutoSize = true;
            lblTotalBooksCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalBooksCount.ForeColor = Color.FromArgb(0, 120, 215);
            lblTotalBooksCount.Location = new Point(334, 37);
            lblTotalBooksCount.Name = "lblTotalBooksCount";
            lblTotalBooksCount.Size = new Size(35, 41);
            lblTotalBooksCount.TabIndex = 4;
            lblTotalBooksCount.Text = "0";
            lblTotalBooksCount.Click += lblTotalBooksCount_Click;
            // 
            // lblOverdueBooksPrompt
            // 
            lblOverdueBooksPrompt.AutoSize = true;
            lblOverdueBooksPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOverdueBooksPrompt.Location = new Point(40, 156);
            lblOverdueBooksPrompt.Name = "lblOverdueBooksPrompt";
            lblOverdueBooksPrompt.Size = new Size(155, 25);
            lblOverdueBooksPrompt.TabIndex = 3;
            lblOverdueBooksPrompt.Text = "Livres en retard:";
            // 
            // lblBorrowedBooksPrompt
            // 
            lblBorrowedBooksPrompt.AutoSize = true;
            lblBorrowedBooksPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBorrowedBooksPrompt.Location = new Point(40, 123);
            lblBorrowedBooksPrompt.Name = "lblBorrowedBooksPrompt";
            lblBorrowedBooksPrompt.Size = new Size(288, 25);
            lblBorrowedBooksPrompt.TabIndex = 2;
            lblBorrowedBooksPrompt.Text = "Livres actuellement empruntés:";
            // 
            // lblTotalMembersPrompt
            // 
            lblTotalMembersPrompt.AutoSize = true;
            lblTotalMembersPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalMembersPrompt.Location = new Point(40, 85);
            lblTotalMembersPrompt.Name = "lblTotalMembersPrompt";
            lblTotalMembersPrompt.Size = new Size(272, 25);
            lblTotalMembersPrompt.TabIndex = 1;
            lblTotalMembersPrompt.Text = "Nombre de membres inscrits:";
            // 
            // lblTotalBooksPrompt
            // 
            lblTotalBooksPrompt.AutoSize = true;
            lblTotalBooksPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalBooksPrompt.Location = new Point(40, 50);
            lblTotalBooksPrompt.Name = "lblTotalBooksPrompt";
            lblTotalBooksPrompt.Size = new Size(215, 25);
            lblTotalBooksPrompt.TabIndex = 0;
            lblTotalBooksPrompt.Text = "Nombre total de livres:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1117, 561);
            Controls.Add(tabControlMain);
            Font = new Font("Segoe UI", 10F);
            Name = "MainForm";
            Text = "Gestion de Bibliothèque";
            Load += MainForm_Load;
            tabControlMain.ResumeLayout(false);
            tabBooks.ResumeLayout(false);
            groupBoxBooks.ResumeLayout(false);
            groupBoxBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCopiesAvailable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            tabMembers.ResumeLayout(false);
            groupBoxMembers.ResumeLayout(false);
            groupBoxMembers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            tabBorrows.ResumeLayout(false);
            groupBoxCurrentBorrowings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBorrowings).EndInit();
            groupBoxNewBorrowing.ResumeLayout(false);
            groupBoxNewBorrowing.PerformLayout();
            tabDashboard.ResumeLayout(false);
            tabDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartStatistics).EndInit();
            ResumeLayout(false);

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
        private System.Windows.Forms.DateTimePicker dtpSubscriptionEnd;
        private System.Windows.Forms.Label lblSubscriptionEnd;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatistics;
        private System.Windows.Forms.Label lblChartDescription;
    }
}