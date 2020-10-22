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
            this.ddsUseMips = new System.Windows.Forms.CheckBox();
            this.dxtSpeed = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.ddsUseCrunch = new System.Windows.Forms.CheckBox();
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
            this.crunchPanel.Controls.Add(this.ddsUseMips);
            this.crunchPanel.Controls.Add(this.dxtSpeed);
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
            // ddsUseMips
            // 
            this.ddsUseMips.AutoSize = true;
            this.ddsUseMips.Location = new System.Drawing.Point(149, 37);
            this.ddsUseMips.Name = "ddsUseMips";
            this.ddsUseMips.Size = new System.Drawing.Size(15, 14);
            this.ddsUseMips.TabIndex = 22;
            this.ddsUseMips.UseVisualStyleBackColor = true;
            this.ddsUseMips.CheckedChanged += new System.EventHandler(this.useMipsCbox_CheckedChanged);
            // 
            // dxtSpeed
            // 
            this.dxtSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dxtSpeed.FormattingEnabled = true;
            this.dxtSpeed.Items.AddRange(new object[] {
            "superfast",
            "fast",
            "normal",
            "better",
            "uber"});
            this.dxtSpeed.Location = new System.Drawing.Point(149, 7);
            this.dxtSpeed.Name = "dxtSpeed";
            this.dxtSpeed.Size = new System.Drawing.Size(83, 21);
            this.dxtSpeed.TabIndex = 19;
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
            // ddsUseCrunch
            // 
            this.ddsUseCrunch.AutoSize = true;
            this.ddsUseCrunch.Location = new System.Drawing.Point(162, 70);
            this.ddsUseCrunch.Name = "ddsUseCrunch";
            this.ddsUseCrunch.Size = new System.Drawing.Size(15, 14);
            this.ddsUseCrunch.TabIndex = 8;
            this.ddsUseCrunch.UseVisualStyleBackColor = true;
            this.ddsUseCrunch.CheckedChanged += new System.EventHandler(this.crunchDdsCbox_CheckedChanged);
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
            this.Controls.Add(this.ddsUseCrunch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DdsOptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDS Format Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DdsOptionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.DdsOptionsWindow_Load);
            this.crunchPanel.ResumeLayout(false);
            this.crunchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel crunchPanel;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox ddsUseMips;
        private System.Windows.Forms.ComboBox dxtSpeed;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox ddsUseCrunch;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button doneBtn;
    }
}