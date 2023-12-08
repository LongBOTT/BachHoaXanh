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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label2 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            btnLogin = new Button();
            panel1 = new Panel();
            tbAccount = new Guna.UI2.WinForms.Guna2TextBox();
            tbPass = new Guna.UI2.WinForms.Guna2TextBox();
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
            panel1.Controls.Add(tbPass);
            panel1.Controls.Add(tbAccount);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(81, 537);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(611, 429);
            panel1.TabIndex = 2;
            // 
            // tbAccount
            // 
            tbAccount.CustomizableEdges = customizableEdges3;
            tbAccount.DefaultText = "";
            tbAccount.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbAccount.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbAccount.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbAccount.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbAccount.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbAccount.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbAccount.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbAccount.Location = new Point(148, 83);
            tbAccount.Name = "tbAccount";
            tbAccount.PasswordChar = '\0';
            tbAccount.PlaceholderText = "";
            tbAccount.SelectedText = "";
            tbAccount.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tbAccount.Size = new Size(402, 45);
            tbAccount.TabIndex = 7;
            tbAccount.KeyPress += tbAccount_KeyPress_1;
            // 
            // tbPass
            // 
            tbPass.CustomizableEdges = customizableEdges1;
            tbPass.DefaultText = "";
            tbPass.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbPass.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbPass.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbPass.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbPass.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbPass.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbPass.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbPass.Location = new Point(148, 192);
            tbPass.Name = "tbPass";
            tbPass.PasswordChar = '\0';
            tbPass.PlaceholderText = "";
            tbPass.SelectedText = "";
            tbPass.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tbPass.Size = new Size(402, 45);
            tbPass.TabIndex = 8;
            tbPass.TextChanged += tbPass_TextChanged;
            tbPass.KeyPress += tbPass_KeyPress;
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
        private Label label3;
        private Label label2;
        private Button btnLogin;
        private LinkLabel linkLabel1;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox tbAccount;
        private Guna.UI2.WinForms.Guna2TextBox tbPass;
    }
}

