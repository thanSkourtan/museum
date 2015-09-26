namespace museum
{
    partial class SignUpForm
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
            this.SignUpPassword = new System.Windows.Forms.TextBox();
            this.SignUpUserName = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.SignUpLast = new System.Windows.Forms.TextBox();
            this.SignUpName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SignUpEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignUpPassword
            // 
            this.SignUpPassword.Location = new System.Drawing.Point(175, 106);
            this.SignUpPassword.Name = "SignUpPassword";
            this.SignUpPassword.PasswordChar = '*';
            this.SignUpPassword.Size = new System.Drawing.Size(154, 20);
            this.SignUpPassword.TabIndex = 7;
            // 
            // SignUpUserName
            // 
            this.SignUpUserName.Location = new System.Drawing.Point(175, 51);
            this.SignUpUserName.Name = "SignUpUserName";
            this.SignUpUserName.Size = new System.Drawing.Size(154, 20);
            this.SignUpUserName.TabIndex = 6;
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(97, 109);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(56, 13);
            this.password.TabIndex = 5;
            this.password.Text = "Password:";
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(95, 54);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(58, 13);
            this.username.TabIndex = 4;
            this.username.Text = "Username:";
            // 
            // SignUpLast
            // 
            this.SignUpLast.Location = new System.Drawing.Point(175, 212);
            this.SignUpLast.Name = "SignUpLast";
            this.SignUpLast.Size = new System.Drawing.Size(154, 20);
            this.SignUpLast.TabIndex = 11;
            // 
            // SignUpName
            // 
            this.SignUpName.Location = new System.Drawing.Point(175, 157);
            this.SignUpName.Name = "SignUpName";
            this.SignUpName.Size = new System.Drawing.Size(154, 20);
            this.SignUpName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Last:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name:";
            // 
            // SignUpEmail
            // 
            this.SignUpEmail.Location = new System.Drawing.Point(175, 263);
            this.SignUpEmail.Name = "SignUpEmail";
            this.SignUpEmail.Size = new System.Drawing.Size(154, 20);
            this.SignUpEmail.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "E-mail:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 411);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SignUpEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SignUpLast);
            this.Controls.Add(this.SignUpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SignUpPassword);
            this.Controls.Add(this.SignUpUserName);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Name = "SignUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignUpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SignUpPassword;
        private System.Windows.Forms.TextBox SignUpUserName;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.TextBox SignUpLast;
        private System.Windows.Forms.TextBox SignUpName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SignUpEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;

    }
}