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
            this.formatOptionsBtn = new System.Windows.Forms.Button();
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.lowercaseCbox = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.diffSuffixCombox = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.normalSuffixCombox = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.groupNormalsBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.delFilesNotMatchingExtBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.delSmallImagesBtn = new System.Windows.Forms.Button();
            this.delSmallImagesSizeCombox = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.colorDepthCombox = new System.Windows.Forms.ComboBox();
            this.applyColorDepthBtn = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.remAlphaBlack = new System.Windows.Forms.Button();
            this.remAlphaWhite = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.autoLevelBtn = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.recursiveCbox = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.logTbox = new System.Windows.Forms.TextBox();
            this.ignoreIncompatCbox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Path:";
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
            this.tabPage1.Controls.Add(this.formatOptionsBtn);
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
            // formatOptionsBtn
            // 
            this.formatOptionsBtn.Location = new System.Drawing.Point(223, 6);
            this.formatOptionsBtn.Name = "formatOptionsBtn";
            this.formatOptionsBtn.Size = new System.Drawing.Size(100, 21);
            this.formatOptionsBtn.TabIndex = 18;
            this.formatOptionsBtn.Text = "Format Options";
            this.formatOptionsBtn.UseVisualStyleBackColor = true;
            this.formatOptionsBtn.Click += new System.EventHandler(this.formatOptionsBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(220, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Leave empty for non-randomized quality";
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
            this.label7.Size = new System.Drawing.Size(159, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Delete input file after conversion";
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
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "JPEG/PNG: 0-100, Crunch: 0-255";
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
            "TGA",
            "WEBP",
            "JPEG 2000",
            "FLIF"});
            this.formatCombox.Location = new System.Drawing.Point(103, 6);
            this.formatCombox.Name = "formatCombox";
            this.formatCombox.Size = new System.Drawing.Size(111, 21);
            this.formatCombox.TabIndex = 0;
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
            this.tabPage3.Controls.Add(this.panel5);
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(702, 224);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Delete, Filter, Group Files";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lowercaseCbox);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Controls.Add(this.diffSuffixCombox);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.normalSuffixCombox);
            this.panel5.Controls.Add(this.label27);
            this.panel5.Controls.Add(this.groupNormalsBtn);
            this.panel5.Location = new System.Drawing.Point(6, 80);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(690, 60);
            this.panel5.TabIndex = 35;
            // 
            // lowercaseCbox
            // 
            this.lowercaseCbox.AutoSize = true;
            this.lowercaseCbox.Checked = true;
            this.lowercaseCbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lowercaseCbox.Location = new System.Drawing.Point(571, 34);
            this.lowercaseCbox.Name = "lowercaseCbox";
            this.lowercaseCbox.Size = new System.Drawing.Size(114, 17);
            this.lowercaseCbox.TabIndex = 8;
            this.lowercaseCbox.Text = "Rename To Lower";
            this.lowercaseCbox.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(282, 37);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(91, 13);
            this.label29.TabIndex = 34;
            this.label29.Text = "Diffuse Suffix List:";
            // 
            // diffSuffixCombox
            // 
            this.diffSuffixCombox.FormattingEnabled = true;
            this.diffSuffixCombox.Items.AddRange(new object[] {
            ",_d",
            ",_d,_diffuse,_diff,_tex,_texture"});
            this.diffSuffixCombox.Location = new System.Drawing.Point(379, 32);
            this.diffSuffixCombox.Name = "diffSuffixCombox";
            this.diffSuffixCombox.Size = new System.Drawing.Size(180, 21);
            this.diffSuffixCombox.TabIndex = 35;
            this.diffSuffixCombox.Text = ",_d";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 37);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(91, 13);
            this.label28.TabIndex = 33;
            this.label28.Text = "Normal Suffix List:";
            // 
            // normalSuffixCombox
            // 
            this.normalSuffixCombox.FormattingEnabled = true;
            this.normalSuffixCombox.Items.AddRange(new object[] {
            "_n",
            "_n,_normal,_norm,_bump,_bumpmap,_normalmap,_normals"});
            this.normalSuffixCombox.Location = new System.Drawing.Point(96, 32);
            this.normalSuffixCombox.Name = "normalSuffixCombox";
            this.normalSuffixCombox.Size = new System.Drawing.Size(180, 21);
            this.normalSuffixCombox.TabIndex = 33;
            this.normalSuffixCombox.Text = "_n";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(3, 8);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(245, 13);
            this.label27.TabIndex = 8;
            this.label27.Text = "Group Normal Maps With Diffuse Textures";
            // 
            // groupNormalsBtn
            // 
            this.groupNormalsBtn.Location = new System.Drawing.Point(485, 3);
            this.groupNormalsBtn.Name = "groupNormalsBtn";
            this.groupNormalsBtn.Size = new System.Drawing.Size(200, 23);
            this.groupNormalsBtn.TabIndex = 32;
            this.groupNormalsBtn.Text = "Group Textures";
            this.groupNormalsBtn.UseVisualStyleBackColor = true;
            this.groupNormalsBtn.Click += new System.EventHandler(this.groupNormalsBtn_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.delFilesNotMatchingExtBtn);
            this.panel4.Location = new System.Drawing.Point(6, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(690, 31);
            this.panel4.TabIndex = 34;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 8);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(267, 13);
            this.label26.TabIndex = 8;
            this.label26.Text = "Delete Files Not Matching Extension Wildcard";
            // 
            // delFilesNotMatchingExtBtn
            // 
            this.delFilesNotMatchingExtBtn.Location = new System.Drawing.Point(485, 3);
            this.delFilesNotMatchingExtBtn.Name = "delFilesNotMatchingExtBtn";
            this.delFilesNotMatchingExtBtn.Size = new System.Drawing.Size(200, 23);
            this.delFilesNotMatchingExtBtn.TabIndex = 32;
            this.delFilesNotMatchingExtBtn.Text = "Delete Files";
            this.delFilesNotMatchingExtBtn.UseVisualStyleBackColor = true;
            this.delFilesNotMatchingExtBtn.Click += new System.EventHandler(this.delFilesNotMatchingExtBtn_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.delSmallImagesBtn);
            this.panel3.Controls.Add(this.delSmallImagesSizeCombox);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(690, 31);
            this.panel3.TabIndex = 33;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 8);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(122, 13);
            this.label25.TabIndex = 8;
            this.label25.Text = "Delete Small Images";
            // 
            // delSmallImagesBtn
            // 
            this.delSmallImagesBtn.Location = new System.Drawing.Point(485, 3);
            this.delSmallImagesBtn.Name = "delSmallImagesBtn";
            this.delSmallImagesBtn.Size = new System.Drawing.Size(200, 23);
            this.delSmallImagesBtn.TabIndex = 32;
            this.delSmallImagesBtn.Text = "Delete Small Images";
            this.delSmallImagesBtn.UseVisualStyleBackColor = true;
            this.delSmallImagesBtn.Click += new System.EventHandler(this.delSmallImagesBtn_Click);
            // 
            // delSmallImagesSizeCombox
            // 
            this.delSmallImagesSizeCombox.FormattingEnabled = true;
            this.delSmallImagesSizeCombox.Items.AddRange(new object[] {
            "1024",
            "512",
            "256",
            "128",
            "64"});
            this.delSmallImagesSizeCombox.Location = new System.Drawing.Point(395, 4);
            this.delSmallImagesSizeCombox.Name = "delSmallImagesSizeCombox";
            this.delSmallImagesSizeCombox.Size = new System.Drawing.Size(64, 21);
            this.delSmallImagesSizeCombox.TabIndex = 19;
            this.delSmallImagesSizeCombox.Text = "1024";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(461, 8);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(18, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "px";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(159, 8);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(235, 13);
            this.label23.TabIndex = 20;
            this.label23.Text = "Delete images with either side being smaller than";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel6);
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
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.colorDepthCombox);
            this.panel6.Controls.Add(this.applyColorDepthBtn);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Location = new System.Drawing.Point(6, 80);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(690, 31);
            this.panel6.TabIndex = 13;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(285, 8);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(39, 13);
            this.label34.TabIndex = 35;
            this.label34.Text = "Colors:";
            // 
            // colorDepthCombox
            // 
            this.colorDepthCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorDepthCombox.FormattingEnabled = true;
            this.colorDepthCombox.Items.AddRange(new object[] {
            "24 bits (>16m colors)",
            "16 bits (>65k colors)",
            "12 bits (4096 colors)",
            "8 bits (256 colors)",
            "7 bits (128 colors)",
            "6 bits (64 colors)",
            "5 bits (32 colors)",
            "4 bits (16 colors)"});
            this.colorDepthCombox.Location = new System.Drawing.Point(330, 4);
            this.colorDepthCombox.Name = "colorDepthCombox";
            this.colorDepthCombox.Size = new System.Drawing.Size(150, 21);
            this.colorDepthCombox.TabIndex = 20;
            // 
            // applyColorDepthBtn
            // 
            this.applyColorDepthBtn.Location = new System.Drawing.Point(485, 3);
            this.applyColorDepthBtn.Name = "applyColorDepthBtn";
            this.applyColorDepthBtn.Size = new System.Drawing.Size(200, 23);
            this.applyColorDepthBtn.TabIndex = 12;
            this.applyColorDepthBtn.Text = "Apply Color Depth";
            this.applyColorDepthBtn.UseVisualStyleBackColor = true;
            this.applyColorDepthBtn.Click += new System.EventHandler(this.applyColorDepthBtn_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(3, 8);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(122, 13);
            this.label33.TabIndex = 8;
            this.label33.Text = "Reduce Color Depth";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.remAlphaBlack);
            this.panel2.Controls.Add(this.remAlphaWhite);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Location = new System.Drawing.Point(6, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 31);
            this.panel2.TabIndex = 13;
            // 
            // remAlphaBlack
            // 
            this.remAlphaBlack.Location = new System.Drawing.Point(485, 3);
            this.remAlphaBlack.Name = "remAlphaBlack";
            this.remAlphaBlack.Size = new System.Drawing.Size(200, 23);
            this.remAlphaBlack.TabIndex = 13;
            this.remAlphaBlack.Text = "Remove Alpha (Black Background)";
            this.remAlphaBlack.UseVisualStyleBackColor = true;
            this.remAlphaBlack.Click += new System.EventHandler(this.remAlphaBlack_Click);
            // 
            // remAlphaWhite
            // 
            this.remAlphaWhite.Location = new System.Drawing.Point(279, 3);
            this.remAlphaWhite.Name = "remAlphaWhite";
            this.remAlphaWhite.Size = new System.Drawing.Size(200, 23);
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
            this.label22.Location = new System.Drawing.Point(3, 8);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "Remove Transparency";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.autoLevelBtn);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 31);
            this.panel1.TabIndex = 0;
            // 
            // autoLevelBtn
            // 
            this.autoLevelBtn.Location = new System.Drawing.Point(485, 3);
            this.autoLevelBtn.Name = "autoLevelBtn";
            this.autoLevelBtn.Size = new System.Drawing.Size(200, 23);
            this.autoLevelBtn.TabIndex = 12;
            this.autoLevelBtn.Text = "Auto-Level";
            this.autoLevelBtn.UseVisualStyleBackColor = true;
            this.autoLevelBtn.Click += new System.EventHandler(this.autoLevelBtn_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "Auto Adjust";
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
            this.recursiveCbox.CheckedChanged += new System.EventHandler(this.recursiveCbox_CheckedChanged);
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
            this.logTbox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.logTbox.Location = new System.Drawing.Point(12, 352);
            this.logTbox.Multiline = true;
            this.logTbox.Name = "logTbox";
            this.logTbox.ReadOnly = true;
            this.logTbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTbox.Size = new System.Drawing.Size(710, 168);
            this.logTbox.TabIndex = 7;
            this.logTbox.Text = "Ready...";
            // 
            // ignoreIncompatCbox
            // 
            this.ignoreIncompatCbox.AutoSize = true;
            this.ignoreIncompatCbox.Checked = true;
            this.ignoreIncompatCbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreIncompatCbox.Location = new System.Drawing.Point(353, 46);
            this.ignoreIncompatCbox.Name = "ignoreIncompatCbox";
            this.ignoreIncompatCbox.Size = new System.Drawing.Size(192, 17);
            this.ignoreIncompatCbox.TabIndex = 8;
            this.ignoreIncompatCbox.Text = "Ignore Incompatible File Extensions";
            this.ignoreIncompatCbox.UseVisualStyleBackColor = true;
            this.ignoreIncompatCbox.CheckedChanged += new System.EventHandler(this.ignoreIncompatCbox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(734, 561);
            this.Controls.Add(this.ignoreIncompatCbox);
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
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NMKD\'s MagickUtils";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button delSmallImagesBtn;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox delSmallImagesSizeCombox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button remAlphaBlack;
        private System.Windows.Forms.Button remAlphaWhite;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button delFilesNotMatchingExtBtn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox normalSuffixCombox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button groupNormalsBtn;
        private System.Windows.Forms.CheckBox lowercaseCbox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox diffSuffixCombox;
        private System.Windows.Forms.CheckBox ignoreIncompatCbox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button applyColorDepthBtn;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox colorDepthCombox;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button formatOptionsBtn;
    }
}