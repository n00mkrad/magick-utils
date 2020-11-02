namespace MagickUtils.Forms
{
    partial class JpegOptionsWindow
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
            this.mozJpegOptionsPanel = new System.Windows.Forms.Panel();
            this.jpegChromaSubsampling = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.jpegEnc = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mozJpegOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mozJpegOptionsPanel
            // 
            this.mozJpegOptionsPanel.Controls.Add(this.jpegChromaSubsampling);
            this.mozJpegOptionsPanel.Controls.Add(this.label2);
            this.mozJpegOptionsPanel.Location = new System.Drawing.Point(5, 94);
            this.mozJpegOptionsPanel.Name = "mozJpegOptionsPanel";
            this.mozJpegOptionsPanel.Size = new System.Drawing.Size(447, 54);
            this.mozJpegOptionsPanel.TabIndex = 39;
            // 
            // jpegChromaSubsampling
            // 
            this.jpegChromaSubsampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.jpegChromaSubsampling.FormattingEnabled = true;
            this.jpegChromaSubsampling.Items.AddRange(new object[] {
            "Normal (4:2:0)",
            "Medium (4:2:2)",
            "Disabled (4:4:4)"});
            this.jpegChromaSubsampling.Location = new System.Drawing.Point(149, 0);
            this.jpegChromaSubsampling.Name = "jpegChromaSubsampling";
            this.jpegChromaSubsampling.Size = new System.Drawing.Size(125, 21);
            this.jpegChromaSubsampling.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Chroma Subsampling";
            // 
            // jpegEnc
            // 
            this.jpegEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.jpegEnc.FormattingEnabled = true;
            this.jpegEnc.Items.AddRange(new object[] {
            "Built-In (ImageMagick)",
            "MozJpeg"});
            this.jpegEnc.Location = new System.Drawing.Point(154, 67);
            this.jpegEnc.Name = "jpegEnc";
            this.jpegEnc.Size = new System.Drawing.Size(125, 21);
            this.jpegEnc.TabIndex = 38;
            this.jpegEnc.SelectedIndexChanged += new System.EventHandler(this.jpegEnc_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(12, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 13);
            this.label31.TabIndex = 37;
            this.label31.Text = "JPEG Encoder";
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(12, 201);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(440, 23);
            this.doneBtn.TabIndex = 36;
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
            this.label1.Size = new System.Drawing.Size(363, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "JPEG (Joint Photographic Experts Group) Options";
            // 
            // JpegOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.mozJpegOptionsPanel);
            this.Controls.Add(this.jpegEnc);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "JpegOptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JPEG Format Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JpegOptionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.JpegOptionsWindow_Load);
            this.mozJpegOptionsPanel.ResumeLayout(false);
            this.mozJpegOptionsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mozJpegOptionsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox jpegEnc;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox jpegChromaSubsampling;
    }
}