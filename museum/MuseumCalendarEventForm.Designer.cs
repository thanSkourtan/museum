namespace museum
{
    partial class MuseumCalendarEventForm
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
            this.eventFormpictureBox = new System.Windows.Forms.PictureBox();
            this.eventFormDate = new System.Windows.Forms.Label();
            this.eventFormText = new System.Windows.Forms.Label();
            this.eventFormTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventFormpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // eventFormpictureBox
            // 
            this.eventFormpictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eventFormpictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventFormpictureBox.Location = new System.Drawing.Point(0, 0);
            this.eventFormpictureBox.Name = "eventFormpictureBox";
            this.eventFormpictureBox.Size = new System.Drawing.Size(494, 259);
            this.eventFormpictureBox.TabIndex = 0;
            this.eventFormpictureBox.TabStop = false;
            // 
            // eventFormDate
            // 
            this.eventFormDate.AutoSize = true;
            this.eventFormDate.Location = new System.Drawing.Point(9, 349);
            this.eventFormDate.Name = "eventFormDate";
            this.eventFormDate.Size = new System.Drawing.Size(80, 13);
            this.eventFormDate.TabIndex = 1;
            this.eventFormDate.Text = "eventFormDate";
            // 
            // eventFormText
            // 
            this.eventFormText.AutoSize = true;
            this.eventFormText.Location = new System.Drawing.Point(9, 380);
            this.eventFormText.MaximumSize = new System.Drawing.Size(450, 0);
            this.eventFormText.Name = "eventFormText";
            this.eventFormText.Size = new System.Drawing.Size(78, 13);
            this.eventFormText.TabIndex = 2;
            this.eventFormText.Text = "eventFormText";
            // 
            // eventFormTitle
            // 
            this.eventFormTitle.AutoSize = true;
            this.eventFormTitle.Location = new System.Drawing.Point(12, 304);
            this.eventFormTitle.MaximumSize = new System.Drawing.Size(450, 0);
            this.eventFormTitle.Name = "eventFormTitle";
            this.eventFormTitle.Size = new System.Drawing.Size(77, 13);
            this.eventFormTitle.TabIndex = 3;
            this.eventFormTitle.Text = "eventFormTitle";
            // 
            // MuseumCalendarEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 577);
            this.Controls.Add(this.eventFormTitle);
            this.Controls.Add(this.eventFormText);
            this.Controls.Add(this.eventFormDate);
            this.Controls.Add(this.eventFormpictureBox);
            this.MaximumSize = new System.Drawing.Size(510, 615);
            this.MinimumSize = new System.Drawing.Size(510, 615);
            this.Name = "MuseumCalendarEventForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MuseumCalendarEventForm";
            ((System.ComponentModel.ISupportInitialize)(this.eventFormpictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox eventFormpictureBox;
        private System.Windows.Forms.Label eventFormDate;
        private System.Windows.Forms.Label eventFormText;
        private System.Windows.Forms.Label eventFormTitle;
    }
}