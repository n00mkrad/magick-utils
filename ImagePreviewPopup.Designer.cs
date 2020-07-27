namespace MagickUtils
{
    partial class ImagePreviewPopup
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
            this.previewPicbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPicbox
            // 
            this.previewPicbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPicbox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewPicbox.Location = new System.Drawing.Point(10, 12);
            this.previewPicbox.Name = "previewPicbox";
            this.previewPicbox.Size = new System.Drawing.Size(863, 839);
            this.previewPicbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewPicbox.TabIndex = 0;
            this.previewPicbox.TabStop = false;
            // 
            // ImagePreviewPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 861);
            this.Controls.Add(this.previewPicbox);
            this.Name = "ImagePreviewPopup";
            this.Text = "ImagePreviewPopup";
            this.Load += new System.EventHandler(this.ImagePreviewPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox previewPicbox;
    }
}