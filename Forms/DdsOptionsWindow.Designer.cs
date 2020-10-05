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
            this.crunchPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.useMipsCbox = new System.Windows.Forms.CheckBox();
            this.dxtQualCombox = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.crunchDdsCbox = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.doneBtn = new System.Windows.Forms.Button();
            this.crunchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // crunchPanel
            // 
            this.crunchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crunchPanel.Controls.Add(this.label3);
            this.crunchPanel.Controls.Add(this.label2);
            this.crunchPanel.Controls.Add(this.label32);
            this.crunchPanel.Controls.Add(this.useMipsCbox);
            this.crunchPanel.Controls.Add(this.dxtQualCombox);
            this.crunchPanel.Controls.Add(this.label30);
            this.crunchPanel.Enabled = false;
            this.crunchPanel.Location = new System.Drawing.Point(12, 90);
            this.crunchPanel.Name = "crunchPanel";
            this.crunchPanel.Size = new System.Drawing.Size(440, 58);
            this.crunchPanel.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(251, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Generate mipmaps (or use existing)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(251, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Encoding speed/effort";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 37);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(132, 13);
            this.label32.TabIndex = 23;
            this.label32.Text = "Generate/Reuse Mipmaps";
            // 
            // useMipsCbox
            // 
            this.useMipsCbox.AutoSize = true;
            this.useMipsCbox.Location = new System.Drawing.Point(149, 37);
            this.useMipsCbox.Name = "useMipsCbox";
            this.useMipsCbox.Size = new System.Drawing.Size(15, 14);
            this.useMipsCbox.TabIndex = 22;
            this.useMipsCbox.UseVisualStyleBackColor = true;
            this.useMipsCbox.CheckedChanged += new System.EventHandler(this.useMipsCbox_CheckedChanged);
            // 
            // dxtQualCombox
            // 
            this.dxtQualCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dxtQualCombox.FormattingEnabled = true;
            this.dxtQualCombox.Items.AddRange(new object[] {
            "superfast",
            "fast",
            "normal",
            "better",
            "uber"});
            this.dxtQualCombox.Location = new System.Drawing.Point(149, 7);
            this.dxtQualCombox.Name = "dxtQualCombox";
            this.dxtQualCombox.Size = new System.Drawing.Size(83, 21);
            this.dxtQualCombox.TabIndex = 19;
            this.dxtQualCombox.SelectedIndexChanged += new System.EventHandler(this.dxtQualCombox_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(3, 10);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(103, 13);
            this.label30.TabIndex = 20;
            this.label30.Text = "DXT Quality/Speed:";
            // 
            // crunchDdsCbox
            // 
            this.crunchDdsCbox.AutoSize = true;
            this.crunchDdsCbox.Location = new System.Drawing.Point(162, 70);
            this.crunchDdsCbox.Name = "crunchDdsCbox";
            this.crunchDdsCbox.Size = new System.Drawing.Size(15, 14);
            this.crunchDdsCbox.TabIndex = 8;
            this.crunchDdsCbox.UseVisualStyleBackColor = true;
            this.crunchDdsCbox.CheckedChanged += new System.EventHandler(this.crunchDdsCbox_CheckedChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(12, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(120, 13);
            this.label31.TabIndex = 21;
            this.label31.Text = "Use Crunch compressor";
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(264, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(179, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "If unchecked, ImageMagick is used.";
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
            // DdsOptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 236);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crunchPanel);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.crunchDdsCbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DdsOptionsWindow";
            this.Text = "DDS Format Options";
            this.Load += new System.EventHandler(this.DdsOptionsWindow_Load);
            this.crunchPanel.ResumeLayout(false);
            this.crunchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel crunchPanel;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox useMipsCbox;
        private System.Windows.Forms.ComboBox dxtQualCombox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox crunchDdsCbox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button doneBtn;
    }
}