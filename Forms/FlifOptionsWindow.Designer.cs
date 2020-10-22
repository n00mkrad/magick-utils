namespace MagickUtils
{
    partial class FlifOptionsWindow
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
            this.doneBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.flifEffort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flifEnc = new System.Windows.Forms.ComboBox();
            this.flifWrapperPanel = new System.Windows.Forms.Panel();
            this.flifWrapperPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(12, 201);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(440, 23);
            this.doneBtn.TabIndex = 27;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "FLIF (Free Lossless Image Format) Options";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(280, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Higher ist better but also slower";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(12, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 13);
            this.label31.TabIndex = 29;
            this.label31.Text = "FLIF Encoder";
            // 
            // flifEffort
            // 
            this.flifEffort.FormattingEnabled = true;
            this.flifEffort.Items.AddRange(new object[] {
            "100",
            "80",
            "60",
            "40",
            "20",
            "0"});
            this.flifEffort.Location = new System.Drawing.Point(149, 0);
            this.flifEffort.Name = "flifEffort";
            this.flifEffort.Size = new System.Drawing.Size(125, 21);
            this.flifEffort.TabIndex = 31;
            this.flifEffort.Text = "50";
            this.flifEffort.SelectedIndexChanged += new System.EventHandler(this.effortCombox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Encoding Effort";
            // 
            // flifEnc
            // 
            this.flifEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flifEnc.FormattingEnabled = true;
            this.flifEnc.Items.AddRange(new object[] {
            "Magick.NET Native",
            "flif.exe Wrapper"});
            this.flifEnc.Location = new System.Drawing.Point(154, 67);
            this.flifEnc.Name = "flifEnc";
            this.flifEnc.Size = new System.Drawing.Size(125, 21);
            this.flifEnc.TabIndex = 33;
            this.flifEnc.SelectedIndexChanged += new System.EventHandler(this.flifEnc_SelectedIndexChanged);
            // 
            // flifWrapperPanel
            // 
            this.flifWrapperPanel.Controls.Add(this.flifEffort);
            this.flifWrapperPanel.Controls.Add(this.label10);
            this.flifWrapperPanel.Controls.Add(this.label2);
            this.flifWrapperPanel.Location = new System.Drawing.Point(5, 94);
            this.flifWrapperPanel.Name = "flifWrapperPanel";
            this.flifWrapperPanel.Size = new System.Drawing.Size(447, 54);
            this.flifWrapperPanel.TabIndex = 34;
            // 
            // FlifOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.flifWrapperPanel);
            this.Controls.Add(this.flifEnc);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FlifOptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLIF Format Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlifOptionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.FlifOptionsWindow_Load);
            this.flifWrapperPanel.ResumeLayout(false);
            this.flifWrapperPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox flifEffort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox flifEnc;
        private System.Windows.Forms.Panel flifWrapperPanel;
    }
}