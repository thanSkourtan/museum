namespace museum
{
    partial class VisitorBook
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.visitorBookButtonSend = new System.Windows.Forms.Button();
            this.visitorMessageBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.visitorBookButtonSend);
            this.splitContainer1.Panel2.Controls.Add(this.visitorMessageBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1023, 637);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 5;
            // 
            // visitorBookButtonSend
            // 
            this.visitorBookButtonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.visitorBookButtonSend.Location = new System.Drawing.Point(475, 269);
            this.visitorBookButtonSend.Name = "visitorBookButtonSend";
            this.visitorBookButtonSend.Size = new System.Drawing.Size(75, 23);
            this.visitorBookButtonSend.TabIndex = 6;
            this.visitorBookButtonSend.Text = "Αποστολή";
            this.visitorBookButtonSend.UseVisualStyleBackColor = true;
            this.visitorBookButtonSend.Click += new System.EventHandler(this.visitorBookButtonSend_Click);
            // 
            // visitorMessageBox
            // 
            this.visitorMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.visitorMessageBox.Location = new System.Drawing.Point(12, 53);
            this.visitorMessageBox.Name = "visitorMessageBox";
            this.visitorMessageBox.Size = new System.Drawing.Size(999, 196);
            this.visitorMessageBox.TabIndex = 5;
            this.visitorMessageBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Γράψτε το δικό σας μήνυμα";
            // 
            // VisitorBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 637);
            this.Controls.Add(this.splitContainer1);
            this.Name = "VisitorBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisitorBook";
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox visitorMessageBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button visitorBookButtonSend;
    }
}