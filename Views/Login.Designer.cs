namespace Bachhoaxanh
{
    partial class FormLogin
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
            label2 = new Label();
            label3 = new Label();
            tbAccount = new TextBox();
            tbPass = new TextBox();
            linkLabel1 = new LinkLabel();
            btnLogin = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(4, 92);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 1;
            label2.Text = "Tài khoản";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(4, 209);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 25);
            label3.TabIndex = 2;
            label3.Text = "Mật khẩu";
            label3.Click += label3_Click;
            // 
            // tbAccount
            // 
            tbAccount.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbAccount.Location = new Point(136, 68);
            tbAccount.Margin = new Padding(4, 15, 4, 0);
            tbAccount.Multiline = true;
            tbAccount.Name = "tbAccount";
            tbAccount.Size = new Size(399, 69);
            tbAccount.TabIndex = 3;
            // 
            // tbPass
            // 
            tbPass.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbPass.Location = new Point(136, 185);
            tbPass.Margin = new Padding(4, 5, 4, 5);
            tbPass.Multiline = true;
            tbPass.Name = "tbPass";
            tbPass.PasswordChar = '*';
            tbPass.Size = new Size(399, 70);
            tbPass.TabIndex = 4;
            // 
            // linkLabel1
            // 
            linkLabel1.ActiveLinkColor = Color.Yellow;
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.ForeColor = Color.WhiteSmoke;
            linkLabel1.LinkColor = Color.White;
            linkLabel1.Location = new Point(421, 262);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(116, 20);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Quên mật khẩu?";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(255, 242, 47);
            btnLogin.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogin.ForeColor = Color.FromArgb(4, 118, 68);
            btnLogin.Location = new Point(223, 345);
            btnLogin.Margin = new Padding(4, 5, 4, 5);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(175, 85);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(tbPass);
            panel1.Controls.Add(tbAccount);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(81, 537);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(611, 429);
            panel1.TabIndex = 2;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = BachHoaXanh.Properties.Resources.login;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1600, 1017);
            Controls.Add(panel1);
            DoubleBuffered = true;
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormLogin";
            Text = "Form1";
            Load += FormLogin_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox tbAccount;
        private Label label3;
        private Label label2;
        private TextBox tbPass;
        private Button btnLogin;
        private LinkLabel linkLabel1;
        private Panel panel1;
    }
}

