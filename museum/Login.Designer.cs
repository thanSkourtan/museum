namespace museum
{
    partial class Login
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
            this.username = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.textBoxUserLoginForm = new System.Windows.Forms.TextBox();
            this.textBoxPasswordLoginForm = new System.Windows.Forms.TextBox();
            this.okButtonLoginForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(23, 33);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(58, 13);
            this.username.TabIndex = 0;
            this.username.Text = "Username:";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(25, 88);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(56, 13);
            this.password.TabIndex = 1;
            this.password.Text = "Password:";
            // 
            // textBoxUserLoginForm
            // 
            this.textBoxUserLoginForm.Location = new System.Drawing.Point(103, 30);
            this.textBoxUserLoginForm.Name = "textBoxUserLoginForm";
            this.textBoxUserLoginForm.Size = new System.Drawing.Size(154, 20);
            this.textBoxUserLoginForm.TabIndex = 2;
            // 
            // textBoxPasswordLoginForm
            // 
            this.textBoxPasswordLoginForm.Location = new System.Drawing.Point(103, 85);
            this.textBoxPasswordLoginForm.Name = "textBoxPasswordLoginForm";
            this.textBoxPasswordLoginForm.PasswordChar = '*';
            this.textBoxPasswordLoginForm.Size = new System.Drawing.Size(154, 20);
            this.textBoxPasswordLoginForm.TabIndex = 3;
            // 
            // okButtonLoginForm
            // 
            this.okButtonLoginForm.Location = new System.Drawing.Point(103, 167);
            this.okButtonLoginForm.Name = "okButtonLoginForm";
            this.okButtonLoginForm.Size = new System.Drawing.Size(75, 23);
            this.okButtonLoginForm.TabIndex = 4;
            this.okButtonLoginForm.Text = "Ok";
            this.okButtonLoginForm.UseVisualStyleBackColor = true;
            this.okButtonLoginForm.Click += new System.EventHandler(this.okButtonLoginForm_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 212);
            this.Controls.Add(this.okButtonLoginForm);
            this.Controls.Add(this.textBoxPasswordLoginForm);
            this.Controls.Add(this.textBoxUserLoginForm);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox textBoxUserLoginForm;
        private System.Windows.Forms.TextBox textBoxPasswordLoginForm;
        private System.Windows.Forms.Button okButtonLoginForm;
    }
}