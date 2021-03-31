namespace MagickUtils
{
    partial class DdsOptionsWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.ddsEnc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ddsCompressionType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ddsEnableMips = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "DDS (DirectDraw Surface) Options";
            // 
            // doneBtn
            // 
            this.doneBtn.Location = new System.Drawing.Point(12, 201);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(440, 23);
            this.doneBtn.TabIndex = 25;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // ddsEnc
            // 
            this.ddsEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddsEnc.FormattingEnabled = true;
            this.ddsEnc.Items.AddRange(new object[] {
            "ImageMagick (Native)",
            "NvCompress (Best Compression, Slow BC7)",
            "Texconv (Fast BC7)",
            "Crunch (Adjustable Quality)"});
            this.ddsEnc.Location = new System.Drawing.Point(154, 67);
            this.ddsEnc.Name = "ddsEnc";
            this.ddsEnc.Size = new System.Drawing.Size(250, 21);
            this.ddsEnc.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "DDS Encoder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "DDS Compression";
            // 
            // ddsCompressionType
            // 
            this.ddsCompressionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddsCompressionType.FormattingEnabled = true;
            this.ddsCompressionType.Items.AddRange(new object[] {
            "ARGB",
            "BC1 (DXT1)",
            "BC2 (DXT3)",
            "BC3 (DXT5)",
            "BC4",
            "BC5",
            "BC6",
            "BC7 (Texconv Recommended)"});
            this.ddsCompressionType.Location = new System.Drawing.Point(154, 94);
            this.ddsCompressionType.Name = "ddsCompressionType";
            this.ddsCompressionType.Size = new System.Drawing.Size(250, 21);
            this.ddsCompressionType.TabIndex = 42;
            this.ddsCompressionType.SelectedIndexChanged += new System.EventHandler(this.ddsCompressionType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Enable Mipmaps";
            // 
            // ddsEnableMips
            // 
            this.ddsEnableMips.AutoSize = true;
            this.ddsEnableMips.Location = new System.Drawing.Point(154, 123);
            this.ddsEnableMips.Name = "ddsEnableMips";
            this.ddsEnableMips.Size = new System.Drawing.Size(15, 14);
            this.ddsEnableMips.TabIndex = 45;
            this.ddsEnableMips.UseVisualStyleBackColor = true;
            // 
            // DdsOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.ddsEnableMips);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ddsCompressionType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddsEnc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DdsOptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDS Format Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DdsOptionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.DdsOptionsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.ComboBox ddsEnc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddsCompressionType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ddsEnableMips;
    }
}