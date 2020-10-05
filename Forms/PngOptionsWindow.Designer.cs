namespace MagickUtils.Forms
{
    partial class PngOptionsWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.pngColorDepth = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "PNG (Portable Network Graphics) Options";
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(12, 201);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(440, 23);
            this.doneBtn.TabIndex = 26;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // pngColorDepth
            // 
            this.pngColorDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pngColorDepth.FormattingEnabled = true;
            this.pngColorDepth.Items.AddRange(new object[] {
            "Same As Original",
            "24-bit RGB",
            "32-bit RGBA"});
            this.pngColorDepth.Location = new System.Drawing.Point(162, 67);
            this.pngColorDepth.Name = "pngColorDepth";
            this.pngColorDepth.Size = new System.Drawing.Size(290, 21);
            this.pngColorDepth.TabIndex = 27;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 70);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(116, 13);
            this.label30.TabIndex = 28;
            this.label30.Text = "Color Type / Bit Depth:";
            // 
            // PngOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.pngColorDepth);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PngOptionsWindow";
            this.Text = "PNG Format Options";
            this.Load += new System.EventHandler(this.PngOptionsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.ComboBox pngColorDepth;
        private System.Windows.Forms.Label label30;
    }
}