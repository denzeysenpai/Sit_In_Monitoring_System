namespace Sit_In_Monitoring
{
    partial class Form1
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTime = new System.Windows.Forms.Label();
            this.tm2 = new System.Windows.Forms.Panel();
            this.tm1 = new System.Windows.Forms.Panel();
            this.dateToday = new ItachiUIBunifu.DateTimePickerBunifuItachi();
            this.pnlStudsRec = new System.Windows.Forms.Panel();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlLoginFrame = new System.Windows.Forms.Panel();
            this.btnStart = new ItachiUIBunifu.ButtonBunifuItachi();
            this.mrgn = new System.Windows.Forms.Panel();
            this.l2 = new System.Windows.Forms.Panel();
            this.placeholder2 = new System.Windows.Forms.Label();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.mrgl = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Panel();
            this.placeholder1 = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBoxBunifuItachi1 = new ItachiUIBunifu.PictureBoxBunifuItachi();
            this.panel1 = new System.Windows.Forms.Panel();
            this.exitButton = new System.Windows.Forms.Button();
            this.CLOCK = new System.Windows.Forms.Timer(this.components);
            this.UI = new System.Windows.Forms.Timer(this.components);
            this.pnlConfirmExit = new System.Windows.Forms.Panel();
            this.pnlDesign = new System.Windows.Forms.Panel();
            this.btnCancelIn = new ItachiUIBunifu.ButtonBunifuItachi();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new ItachiUIBunifu.ButtonBunifuItachi();
            this.borderpass = new System.Windows.Forms.Panel();
            this.pass = new System.Windows.Forms.Panel();
            this.placeholder3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.mensahe = new System.Windows.Forms.Label();
            this.ENVIRONMENT = new System.Windows.Forms.Timer(this.components);
            this.DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STUDENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FULLNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_IN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_OUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOG_OUT = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tm2.SuspendLayout();
            this.tm1.SuspendLayout();
            this.pnlStudsRec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.pnlLoginFrame.SuspendLayout();
            this.mrgn.SuspendLayout();
            this.l2.SuspendLayout();
            this.mrgl.SuspendLayout();
            this.l1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBunifuItachi1)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlConfirmExit.SuspendLayout();
            this.pnlDesign.SuspendLayout();
            this.borderpass.SuspendLayout();
            this.pass.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Century Gothic", 35.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.lblTime.Location = new System.Drawing.Point(59, -2);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(232, 56);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "00:00:00ff";
            this.lblTime.Click += new System.EventHandler(this.CalendarClick);
            // 
            // tm2
            // 
            this.tm2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(223)))), ((int)(((byte)(235)))));
            this.tm2.Controls.Add(this.tm1);
            this.tm2.Location = new System.Drawing.Point(27, 166);
            this.tm2.Name = "tm2";
            this.tm2.Size = new System.Drawing.Size(422, 92);
            this.tm2.TabIndex = 3;
            // 
            // tm1
            // 
            this.tm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.tm1.Controls.Add(this.dateToday);
            this.tm1.Controls.Add(this.lblTime);
            this.tm1.Location = new System.Drawing.Point(6, 6);
            this.tm1.Name = "tm1";
            this.tm1.Size = new System.Drawing.Size(408, 80);
            this.tm1.TabIndex = 3;
            // 
            // dateToday
            // 
            this.dateToday.BorderColor = System.Drawing.Color.Aqua;
            this.dateToday.BorderSize = 0;
            this.dateToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dateToday.Location = new System.Drawing.Point(82, 46);
            this.dateToday.MinimumSize = new System.Drawing.Size(4, 35);
            this.dateToday.Name = "dateToday";
            this.dateToday.Size = new System.Drawing.Size(234, 35);
            this.dateToday.SkinColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(179)))), ((int)(((byte)(199)))));
            this.dateToday.TabIndex = 4;
            this.dateToday.TextColor = System.Drawing.Color.White;
            // 
            // pnlStudsRec
            // 
            this.pnlStudsRec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(223)))), ((int)(((byte)(235)))));
            this.pnlStudsRec.Controls.Add(this.DataGrid);
            this.pnlStudsRec.Controls.Add(this.label1);
            this.pnlStudsRec.Location = new System.Drawing.Point(468, 61);
            this.pnlStudsRec.Name = "pnlStudsRec";
            this.pnlStudsRec.Size = new System.Drawing.Size(983, 673);
            this.pnlStudsRec.TabIndex = 4;
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DATE,
            this.STUDENT_ID,
            this.FULLNAME,
            this.TIME_IN,
            this.TIME_OUT,
            this.LOG_OUT});
            this.DataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.DataGrid.Location = new System.Drawing.Point(25, 62);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.Size = new System.Drawing.Size(941, 597);
            this.DataGrid.TabIndex = 0;
            this.DataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.label1.Location = new System.Drawing.Point(398, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 40);
            this.label1.TabIndex = 6;
            this.label1.Text = "STUDENTS LOGGED IN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.label2.Location = new System.Drawing.Point(134, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(332, 63);
            this.label2.TabIndex = 6;
            this.label2.Text = "SIT-IN FORM";
            // 
            // pnlLoginFrame
            // 
            this.pnlLoginFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(223)))), ((int)(((byte)(235)))));
            this.pnlLoginFrame.Controls.Add(this.btnStart);
            this.pnlLoginFrame.Controls.Add(this.mrgn);
            this.pnlLoginFrame.Controls.Add(this.mrgl);
            this.pnlLoginFrame.Controls.Add(this.label5);
            this.pnlLoginFrame.Location = new System.Drawing.Point(28, 278);
            this.pnlLoginFrame.Name = "pnlLoginFrame";
            this.pnlLoginFrame.Size = new System.Drawing.Size(418, 292);
            this.pnlLoginFrame.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnStart.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnStart.BorderRadius = 10;
            this.btnStart.BorderSize = 0;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(59, 193);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(301, 64);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START TIME";
            this.btnStart.TextColor = System.Drawing.Color.White;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // mrgn
            // 
            this.mrgn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(146)))), ((int)(((byte)(191)))));
            this.mrgn.Controls.Add(this.l2);
            this.mrgn.Location = new System.Drawing.Point(58, 134);
            this.mrgn.Name = "mrgn";
            this.mrgn.Size = new System.Drawing.Size(302, 54);
            this.mrgn.TabIndex = 0;
            // 
            // l2
            // 
            this.l2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.l2.Controls.Add(this.placeholder2);
            this.l2.Controls.Add(this.txtStudentName);
            this.l2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.l2.Location = new System.Drawing.Point(1, 1);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(300, 52);
            this.l2.TabIndex = 0;
            this.l2.Click += new System.EventHandler(this.userClick2);
            // 
            // placeholder2
            // 
            this.placeholder2.AutoSize = true;
            this.placeholder2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.placeholder2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeholder2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(196)))), ((int)(((byte)(209)))));
            this.placeholder2.Location = new System.Drawing.Point(66, 16);
            this.placeholder2.Name = "placeholder2";
            this.placeholder2.Size = new System.Drawing.Size(168, 21);
            this.placeholder2.TabIndex = 7;
            this.placeholder2.Text = "STUDENT FULL NAME";
            this.placeholder2.Click += new System.EventHandler(this.fullNameClick);
            // 
            // txtStudentName
            // 
            this.txtStudentName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.txtStudentName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStudentName.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentName.Location = new System.Drawing.Point(23, 10);
            this.txtStudentName.MaxLength = 22;
            this.txtStudentName.Name = "txtStudentName";
            this.txtStudentName.Size = new System.Drawing.Size(254, 32);
            this.txtStudentName.TabIndex = 0;
            this.txtStudentName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStudentName.TextChanged += new System.EventHandler(this.fullNameHasInput);
            // 
            // mrgl
            // 
            this.mrgl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(146)))), ((int)(((byte)(191)))));
            this.mrgl.Controls.Add(this.l1);
            this.mrgl.Location = new System.Drawing.Point(58, 74);
            this.mrgl.Name = "mrgl";
            this.mrgl.Size = new System.Drawing.Size(302, 54);
            this.mrgl.TabIndex = 0;
            // 
            // l1
            // 
            this.l1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.l1.Controls.Add(this.placeholder1);
            this.l1.Controls.Add(this.txtStudentID);
            this.l1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.l1.Location = new System.Drawing.Point(1, 1);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(300, 52);
            this.l1.TabIndex = 0;
            this.l1.Click += new System.EventHandler(this.userClick);
            // 
            // placeholder1
            // 
            this.placeholder1.AutoSize = true;
            this.placeholder1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.placeholder1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeholder1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(196)))), ((int)(((byte)(209)))));
            this.placeholder1.Location = new System.Drawing.Point(66, 16);
            this.placeholder1.Name = "placeholder1";
            this.placeholder1.Size = new System.Drawing.Size(169, 21);
            this.placeholder1.TabIndex = 7;
            this.placeholder1.Text = "STUDENT ID NUMBER";
            this.placeholder1.Click += new System.EventHandler(this.idClick);
            // 
            // txtStudentID
            // 
            this.txtStudentID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.txtStudentID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStudentID.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentID.Location = new System.Drawing.Point(23, 10);
            this.txtStudentID.MaxLength = 11;
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(254, 32);
            this.txtStudentID.TabIndex = 0;
            this.txtStudentID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStudentID.TextChanged += new System.EventHandler(this.idNumberHasInput);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.label5.Location = new System.Drawing.Point(96, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 36);
            this.label5.TabIndex = 6;
            this.label5.Text = "STUDENT LOGIN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.label3.Location = new System.Drawing.Point(149, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "DEPARTMENT OF NETWORK AND TECHNICAL SERVICES";
            // 
            // pictureBoxBunifuItachi1
            // 
            this.pictureBoxBunifuItachi1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.pictureBoxBunifuItachi1.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.pictureBoxBunifuItachi1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(223)))), ((int)(((byte)(235)))));
            this.pictureBoxBunifuItachi1.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.pictureBoxBunifuItachi1.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            this.pictureBoxBunifuItachi1.BorderSize = 5;
            this.pictureBoxBunifuItachi1.GradientAngle = 50F;
            this.pictureBoxBunifuItachi1.Image = global::Sit_In_Monitoring.Properties.Resources.dntsLogo;
            this.pictureBoxBunifuItachi1.Location = new System.Drawing.Point(12, 11);
            this.pictureBoxBunifuItachi1.Name = "pictureBoxBunifuItachi1";
            this.pictureBoxBunifuItachi1.Size = new System.Drawing.Size(132, 132);
            this.pictureBoxBunifuItachi1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBunifuItachi1.TabIndex = 5;
            this.pictureBoxBunifuItachi1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Sit_In_Monitoring.Properties.Resources.agtang;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1472, 40);
            this.panel1.TabIndex = 0;
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(125)))), ((int)(((byte)(140)))));
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(144)))), ((int)(((byte)(161)))));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(1428, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(44, 40);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "X";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // CLOCK
            // 
            this.CLOCK.Enabled = true;
            this.CLOCK.Interval = 1000;
            this.CLOCK.Tick += new System.EventHandler(this.ORASAN);
            // 
            // UI
            // 
            this.UI.Enabled = true;
            this.UI.Interval = 10;
            this.UI.Tick += new System.EventHandler(this.TextBoxFocus);
            // 
            // pnlConfirmExit
            // 
            this.pnlConfirmExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(146)))), ((int)(((byte)(191)))));
            this.pnlConfirmExit.Controls.Add(this.pnlDesign);
            this.pnlConfirmExit.Controls.Add(this.mensahe);
            this.pnlConfirmExit.Location = new System.Drawing.Point(446, 204);
            this.pnlConfirmExit.Name = "pnlConfirmExit";
            this.pnlConfirmExit.Size = new System.Drawing.Size(575, 342);
            this.pnlConfirmExit.TabIndex = 8;
            // 
            // pnlDesign
            // 
            this.pnlDesign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(223)))), ((int)(((byte)(235)))));
            this.pnlDesign.Controls.Add(this.btnCancelIn);
            this.pnlDesign.Controls.Add(this.label4);
            this.pnlDesign.Controls.Add(this.btnConfirm);
            this.pnlDesign.Controls.Add(this.borderpass);
            this.pnlDesign.Location = new System.Drawing.Point(8, 35);
            this.pnlDesign.Name = "pnlDesign";
            this.pnlDesign.Size = new System.Drawing.Size(559, 299);
            this.pnlDesign.TabIndex = 8;
            // 
            // btnCancelIn
            // 
            this.btnCancelIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnCancelIn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnCancelIn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnCancelIn.BorderRadius = 10;
            this.btnCancelIn.BorderSize = 0;
            this.btnCancelIn.FlatAppearance.BorderSize = 0;
            this.btnCancelIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelIn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelIn.ForeColor = System.Drawing.Color.White;
            this.btnCancelIn.Location = new System.Drawing.Point(125, 200);
            this.btnCancelIn.Name = "btnCancelIn";
            this.btnCancelIn.Size = new System.Drawing.Size(299, 44);
            this.btnCancelIn.TabIndex = 2;
            this.btnCancelIn.Text = "CANCEL";
            this.btnCancelIn.TextColor = System.Drawing.Color.White;
            this.btnCancelIn.UseVisualStyleBackColor = false;
            this.btnCancelIn.Click += new System.EventHandler(this.btnCancelIn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.label4.Location = new System.Drawing.Point(123, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(299, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "INPUT PASSWORD TO EXIT";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnConfirm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(141)))), ((int)(((byte)(158)))));
            this.btnConfirm.BorderRadius = 10;
            this.btnConfirm.BorderSize = 0;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(125, 152);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(299, 44);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "CONFIRM";
            this.btnConfirm.TextColor = System.Drawing.Color.White;
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // borderpass
            // 
            this.borderpass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(146)))), ((int)(((byte)(191)))));
            this.borderpass.Controls.Add(this.pass);
            this.borderpass.Location = new System.Drawing.Point(123, 90);
            this.borderpass.Name = "borderpass";
            this.borderpass.Size = new System.Drawing.Size(302, 54);
            this.borderpass.TabIndex = 0;
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.pass.Controls.Add(this.placeholder3);
            this.pass.Controls.Add(this.txtPass);
            this.pass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pass.Location = new System.Drawing.Point(1, 1);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(300, 52);
            this.pass.TabIndex = 0;
            this.pass.Click += new System.EventHandler(this.passClick);
            // 
            // placeholder3
            // 
            this.placeholder3.AutoSize = true;
            this.placeholder3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.placeholder3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.placeholder3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(196)))), ((int)(((byte)(209)))));
            this.placeholder3.Location = new System.Drawing.Point(81, 16);
            this.placeholder3.Name = "placeholder3";
            this.placeholder3.Size = new System.Drawing.Size(143, 21);
            this.placeholder3.TabIndex = 7;
            this.placeholder3.Text = "DNTS PASSWORD";
            this.placeholder3.Click += new System.EventHandler(this.idClick2);
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(242)))), ((int)(((byte)(250)))));
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(23, 10);
            this.txtPass.MaxLength = 11;
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(254, 32);
            this.txtPass.TabIndex = 0;
            this.txtPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.TextChanged += new System.EventHandler(this.PassHasInput);
            // 
            // mensahe
            // 
            this.mensahe.AutoSize = true;
            this.mensahe.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mensahe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(92)))));
            this.mensahe.Location = new System.Drawing.Point(233, 11);
            this.mensahe.Name = "mensahe";
            this.mensahe.Size = new System.Drawing.Size(98, 16);
            this.mensahe.TabIndex = 7;
            this.mensahe.Text = "CONFIRM EXIT";
            // 
            // ENVIRONMENT
            // 
            this.ENVIRONMENT.Enabled = true;
            this.ENVIRONMENT.Interval = 1;
            this.ENVIRONMENT.Tick += new System.EventHandler(this.ENVI_EXIT);
            // 
            // DATE
            // 
            this.DATE.Frozen = true;
            this.DATE.HeaderText = "DATE";
            this.DATE.Name = "DATE";
            this.DATE.ReadOnly = true;
            this.DATE.Width = 70;
            // 
            // STUDENT_ID
            // 
            this.STUDENT_ID.Frozen = true;
            this.STUDENT_ID.HeaderText = "STUDENT ID";
            this.STUDENT_ID.Name = "STUDENT_ID";
            this.STUDENT_ID.Width = 170;
            // 
            // FULLNAME
            // 
            this.FULLNAME.Frozen = true;
            this.FULLNAME.HeaderText = "FULLNAME";
            this.FULLNAME.Name = "FULLNAME";
            this.FULLNAME.Width = 360;
            // 
            // TIME_IN
            // 
            this.TIME_IN.Frozen = true;
            this.TIME_IN.HeaderText = "TIME IN";
            this.TIME_IN.Name = "TIME_IN";
            // 
            // TIME_OUT
            // 
            this.TIME_OUT.Frozen = true;
            this.TIME_OUT.HeaderText = "TIME OUT";
            this.TIME_OUT.Name = "TIME_OUT";
            // 
            // LOG_OUT
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.LOG_OUT.DefaultCellStyle = dataGridViewCellStyle1;
            this.LOG_OUT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LOG_OUT.Frozen = true;
            this.LOG_OUT.HeaderText = "LOG OUT";
            this.LOG_OUT.Name = "LOG_OUT";
            this.LOG_OUT.Text = "LOG OUT";
            this.LOG_OUT.ToolTipText = "Log out student";
            this.LOG_OUT.UseColumnTextForButtonValue = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1472, 759);
            this.Controls.Add(this.pnlConfirmExit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBoxBunifuItachi1);
            this.Controls.Add(this.pnlLoginFrame);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlStudsRec);
            this.Controls.Add(this.tm2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIT-IN FORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWillBeClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.tm2.ResumeLayout(false);
            this.tm1.ResumeLayout(false);
            this.tm1.PerformLayout();
            this.pnlStudsRec.ResumeLayout(false);
            this.pnlStudsRec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.pnlLoginFrame.ResumeLayout(false);
            this.pnlLoginFrame.PerformLayout();
            this.mrgn.ResumeLayout(false);
            this.l2.ResumeLayout(false);
            this.l2.PerformLayout();
            this.mrgl.ResumeLayout(false);
            this.l1.ResumeLayout(false);
            this.l1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBunifuItachi1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlConfirmExit.ResumeLayout(false);
            this.pnlConfirmExit.PerformLayout();
            this.pnlDesign.ResumeLayout(false);
            this.pnlDesign.PerformLayout();
            this.borderpass.ResumeLayout(false);
            this.pass.ResumeLayout(false);
            this.pass.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel tm2;
        private System.Windows.Forms.Panel tm1;
        private ItachiUIBunifu.DateTimePickerBunifuItachi dateToday;
        private System.Windows.Forms.Panel pnlStudsRec;
        private ItachiUIBunifu.PictureBoxBunifuItachi pictureBoxBunifuItachi1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlLoginFrame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel mrgl;
        private System.Windows.Forms.Panel l1;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.Panel mrgn;
        private System.Windows.Forms.Panel l2;
        private System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Timer CLOCK;
        private System.Windows.Forms.Timer UI;
        private System.Windows.Forms.Label placeholder1;
        private ItachiUIBunifu.ButtonBunifuItachi btnStart;
        private System.Windows.Forms.Label placeholder2;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlConfirmExit;
        private System.Windows.Forms.Panel pnlDesign;
        private System.Windows.Forms.Label mensahe;
        private ItachiUIBunifu.ButtonBunifuItachi btnCancelIn;
        private ItachiUIBunifu.ButtonBunifuItachi btnConfirm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel borderpass;
        private System.Windows.Forms.Panel pass;
        private System.Windows.Forms.Label placeholder3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Timer ENVIRONMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STUDENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FULLNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIME_IN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIME_OUT;
        private System.Windows.Forms.DataGridViewButtonColumn LOG_OUT;
    }
}

