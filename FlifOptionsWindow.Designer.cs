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
            this.effortCombox = new System.Windows.Forms.ComboBox();
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
            this.label10.Location = new System.Drawing.Point(264, 70);
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
            this.label31.Size = new System.Drawing.Size(80, 13);
            this.label31.TabIndex = 29;
            this.label31.Text = "Encoding Effort";
            // 
            // effortCombox
            // 
            this.effortCombox.FormattingEnabled = true;
            this.effortCombox.Items.AddRange(new object[] {
            "100",
            "80",
            "60",
            "40",
            "20",
            "0"});
            this.effortCombox.Location = new System.Drawing.Point(153, 67);
            this.effortCombox.Name = "effortCombox";
            this.effortCombox.Size = new System.Drawing.Size(100, 21);
            this.effortCombox.TabIndex = 31;
            this.effortCombox.Text = "50";
            this.effortCombox.SelectedIndexChanged += new System.EventHandler(this.effortCombox_SelectedIndexChanged);
            // 
            // FlifOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.effortCombox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FlifOptionsWindow";
            this.Text = "FlifOptionsWindow";
            this.Load += new System.EventHandler(this.FlifOptionsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox effortCombox;
    }
}