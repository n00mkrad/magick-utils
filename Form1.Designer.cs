namespace MagickUtils
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
        protected override void Dispose (bool disposing)
        {
            if(disposing && (components != null))
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
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pathTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.extTbox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.qualityMaxCombox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.delSrcCbox = new System.Windows.Forms.CheckBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.qualityCombox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.formatCombox = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.filterModeCombox = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.scaleModeCombox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.maxScaleCombox = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.minScaleCombox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.scaleResampleCombox = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.autoLevelBtn = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.recursiveCbox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.logTbox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.remAlphaWhite = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.remAlphaBlack = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathTextbox
            // 
            this.pathTextbox.Location = new System.Drawing.Point(119, 13);
            this.pathTextbox.Name = "pathTextbox";
            this.pathTextbox.Size = new System.Drawing.Size(603, 20);
            this.pathTextbox.TabIndex = 0;
            this.pathTextbox.TextChanged += new System.EventHandler(this.pathTextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Root Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Extension Wildcard:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // extTbox
            // 
            this.extTbox.Location = new System.Drawing.Point(119, 44);
            this.extTbox.Name = "extTbox";
            this.extTbox.Size = new System.Drawing.Size(88, 20);
            this.extTbox.TabIndex = 2;
            this.extTbox.Text = "*";
            this.extTbox.TextChanged += new System.EventHandler(this.extTbox_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 96);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 250);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.qualityMaxCombox);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.delSrcCbox);
            this.tabPage1.Controls.Add(this.startBtn);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.qualityCombox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.formatCombox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(702, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Convert, Compress";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(220, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Select output format";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(220, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Leave empty to not use randomized quality";
            // 
            // qualityMaxCombox
            // 
            this.qualityMaxCombox.FormattingEnabled = true;
            this.qualityMaxCombox.Items.AddRange(new object[] {
            "100",
            "70",
            "50",
            "30",
            "10",
            "0"});
            this.qualityMaxCombox.Location = new System.Drawing.Point(103, 60);
            this.qualityMaxCombox.Name = "qualityMaxCombox";
            this.qualityMaxCombox.Size = new System.Drawing.Size(111, 21);
            this.qualityMaxCombox.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Quality Max:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(220, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Check to delete input file after conversion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Delete Source:";
            // 
            // delSrcCbox
            // 
            this.delSrcCbox.AutoSize = true;
            this.delSrcCbox.Location = new System.Drawing.Point(103, 90);
            this.delSrcCbox.Name = "delSrcCbox";
            this.delSrcCbox.Size = new System.Drawing.Size(15, 14);
            this.delSrcCbox.TabIndex = 12;
            this.delSrcCbox.UseVisualStyleBackColor = true;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(6, 195);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(91, 23);
            this.startBtn.TabIndex = 11;
            this.startBtn.Text = "Convert";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.convertStartBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(220, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(241, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "0-100. JPEG quality or PNG compression strength";
            // 
            // qualityCombox
            // 
            this.qualityCombox.FormattingEnabled = true;
            this.qualityCombox.Items.AddRange(new object[] {
            "100",
            "70",
            "50",
            "30",
            "10",
            "0"});
            this.qualityCombox.Location = new System.Drawing.Point(103, 33);
            this.qualityCombox.Name = "qualityCombox";
            this.qualityCombox.Size = new System.Drawing.Size(111, 21);
            this.qualityCombox.TabIndex = 9;
            this.qualityCombox.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Quality Min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Format:";
            // 
            // formatCombox
            // 
            this.formatCombox.FormattingEnabled = true;
            this.formatCombox.Items.AddRange(new object[] {
            "JPEG",
            "PNG",
            "DDS",
            "TGA"});
            this.formatCombox.Location = new System.Drawing.Point(103, 6);
            this.formatCombox.Name = "formatCombox";
            this.formatCombox.Size = new System.Drawing.Size(111, 21);
            this.formatCombox.TabIndex = 0;
            this.formatCombox.Text = "JPEG";
            this.formatCombox.SelectedIndexChanged += new System.EventHandler(this.formatCombox_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.filterModeCombox);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.scaleModeCombox);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.maxScaleCombox);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.minScaleCombox);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.scaleResampleCombox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(702, 224);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Scale, Resample";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label19.Location = new System.Drawing.Point(220, 117);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 13);
            this.label19.TabIndex = 34;
            this.label19.Text = "Scaling filter to use";
            // 
            // filterModeCombox
            // 
            this.filterModeCombox.FormattingEnabled = true;
            this.filterModeCombox.Items.AddRange(new object[] {
            "Mitchell",
            "Bicubic",
            "Nearest (Point)",
            "Random (Box/Mitchell/Bicubic)",
            "Random (All)"});
            this.filterModeCombox.Location = new System.Drawing.Point(103, 114);
            this.filterModeCombox.Name = "filterModeCombox";
            this.filterModeCombox.Size = new System.Drawing.Size(111, 21);
            this.filterModeCombox.TabIndex = 33;
            this.filterModeCombox.Text = "Mitchell";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 117);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 13);
            this.label20.TabIndex = 32;
            this.label20.Text = "Filter Mode:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Scale";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DoScaleBtn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label17.Location = new System.Drawing.Point(220, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(200, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Scale by percentage or to a target height";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 29;
            this.label18.Text = "Scale Mode:";
            // 
            // scaleModeCombox
            // 
            this.scaleModeCombox.FormattingEnabled = true;
            this.scaleModeCombox.Items.AddRange(new object[] {
            "Percentage",
            "Height (Pixels)"});
            this.scaleModeCombox.Location = new System.Drawing.Point(103, 33);
            this.scaleModeCombox.Name = "scaleModeCombox";
            this.scaleModeCombox.Size = new System.Drawing.Size(111, 21);
            this.scaleModeCombox.TabIndex = 28;
            this.scaleModeCombox.Text = "Percentage";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label13.Location = new System.Drawing.Point(220, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(280, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Maximum scale (leave empty to not use randomized scale)";
            // 
            // maxScaleCombox
            // 
            this.maxScaleCombox.FormattingEnabled = true;
            this.maxScaleCombox.Items.AddRange(new object[] {
            "100",
            "70",
            "50",
            "30",
            "10",
            "0"});
            this.maxScaleCombox.Location = new System.Drawing.Point(103, 87);
            this.maxScaleCombox.Name = "maxScaleCombox";
            this.maxScaleCombox.Size = new System.Drawing.Size(111, 21);
            this.maxScaleCombox.TabIndex = 26;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Scale Max:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label15.Location = new System.Drawing.Point(220, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(195, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Scale (or minimum for randomized scale)";
            // 
            // minScaleCombox
            // 
            this.minScaleCombox.FormattingEnabled = true;
            this.minScaleCombox.Items.AddRange(new object[] {
            "100",
            "70",
            "50",
            "30",
            "10",
            "0"});
            this.minScaleCombox.Location = new System.Drawing.Point(103, 60);
            this.minScaleCombox.Name = "minScaleCombox";
            this.minScaleCombox.Size = new System.Drawing.Size(111, 21);
            this.minScaleCombox.TabIndex = 23;
            this.minScaleCombox.Text = "50";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Scale Min:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(220, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(248, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Scale or resample (resize to original size afterwards)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Processing:";
            // 
            // scaleResampleCombox
            // 
            this.scaleResampleCombox.FormattingEnabled = true;
            this.scaleResampleCombox.Items.AddRange(new object[] {
            "Scale",
            "Resample"});
            this.scaleResampleCombox.Location = new System.Drawing.Point(103, 6);
            this.scaleResampleCombox.Name = "scaleResampleCombox";
            this.scaleResampleCombox.Size = new System.Drawing.Size(111, 21);
            this.scaleResampleCombox.TabIndex = 19;
            this.scaleResampleCombox.Text = "Scale";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(702, 224);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Delete, Filter, Group Files";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(702, 224);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Other Image Processing";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.autoLevelBtn);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 80);
            this.panel1.TabIndex = 0;
            // 
            // autoLevelBtn
            // 
            this.autoLevelBtn.Location = new System.Drawing.Point(4, 19);
            this.autoLevelBtn.Name = "autoLevelBtn";
            this.autoLevelBtn.Size = new System.Drawing.Size(213, 23);
            this.autoLevelBtn.TabIndex = 12;
            this.autoLevelBtn.Text = "Auto-Level";
            this.autoLevelBtn.UseVisualStyleBackColor = true;
            this.autoLevelBtn.Click += new System.EventHandler(this.autoLevelBtn_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(106, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "Color Processing:";
            // 
            // recursiveCbox
            // 
            this.recursiveCbox.AutoSize = true;
            this.recursiveCbox.Checked = true;
            this.recursiveCbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recursiveCbox.Location = new System.Drawing.Point(551, 47);
            this.recursiveCbox.Name = "recursiveCbox";
            this.recursiveCbox.Size = new System.Drawing.Size(171, 17);
            this.recursiveCbox.TabIndex = 5;
            this.recursiveCbox.Text = "Recursive (Include Subfolders)";
            this.recursiveCbox.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 526);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(710, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // logTbox
            // 
            this.logTbox.Location = new System.Drawing.Point(12, 352);
            this.logTbox.Multiline = true;
            this.logTbox.Name = "logTbox";
            this.logTbox.ReadOnly = true;
            this.logTbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTbox.Size = new System.Drawing.Size(710, 168);
            this.logTbox.TabIndex = 7;
            this.logTbox.Text = "Ready...";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(238, 13);
            this.label23.TabIndex = 20;
            this.label23.Text = "Delete images with either side being smaller than:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1024",
            "512",
            "256",
            "128",
            "64"});
            this.comboBox1.Location = new System.Drawing.Point(250, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(64, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.Text = "1024";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(315, 9);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "px";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(339, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 23);
            this.button2.TabIndex = 32;
            this.button2.Text = "Delete Small Images";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.remAlphaBlack);
            this.panel2.Controls.Add(this.remAlphaWhite);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Location = new System.Drawing.Point(236, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 80);
            this.panel2.TabIndex = 13;
            // 
            // remAlphaWhite
            // 
            this.remAlphaWhite.Location = new System.Drawing.Point(4, 19);
            this.remAlphaWhite.Name = "remAlphaWhite";
            this.remAlphaWhite.Size = new System.Drawing.Size(213, 23);
            this.remAlphaWhite.TabIndex = 12;
            this.remAlphaWhite.Text = "Remove Alpha (White Background)";
            this.remAlphaWhite.UseVisualStyleBackColor = true;
            this.remAlphaWhite.Click += new System.EventHandler(this.remAlphaWhite_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(138, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "Remove Transparency:";
            // 
            // remAlphaBlack
            // 
            this.remAlphaBlack.Location = new System.Drawing.Point(4, 48);
            this.remAlphaBlack.Name = "remAlphaBlack";
            this.remAlphaBlack.Size = new System.Drawing.Size(213, 23);
            this.remAlphaBlack.TabIndex = 13;
            this.remAlphaBlack.Text = "Remove Alpha (Black Background)";
            this.remAlphaBlack.UseVisualStyleBackColor = true;
            this.remAlphaBlack.Click += new System.EventHandler(this.remAlphaBlack_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 561);
            this.Controls.Add(this.logTbox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.recursiveCbox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.extTbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "NMKD\'s Dataset Toolbox";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox extTbox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox recursiveCbox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox formatCombox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox qualityCombox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox delSrcCbox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox qualityMaxCombox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox logTbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox scaleResampleCombox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox scaleModeCombox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox maxScaleCombox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox minScaleCombox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox filterModeCombox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button autoLevelBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button remAlphaBlack;
        private System.Windows.Forms.Button remAlphaWhite;
        private System.Windows.Forms.Label label22;
    }
}