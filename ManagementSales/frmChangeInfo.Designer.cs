namespace ManagementSales
{
    partial class frmChangeInfo
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtReName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRePass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbChangeBoth = new System.Windows.Forms.RadioButton();
            this.rdbChangePass = new System.Windows.Forms.RadioButton();
            this.rdbChangeName = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(322, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên đăng nhập mới";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(131, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtReName
            // 
            this.txtReName.Location = new System.Drawing.Point(131, 63);
            this.txtReName.Name = "txtReName";
            this.txtReName.Size = new System.Drawing.Size(135, 20);
            this.txtReName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nhập lại tên đăng nhập";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(131, 19);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(135, 20);
            this.txtPass.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mật khẩu mới";
            // 
            // txtRePass
            // 
            this.txtRePass.Location = new System.Drawing.Point(131, 70);
            this.txtRePass.Name = "txtRePass";
            this.txtRePass.Size = new System.Drawing.Size(135, 20);
            this.txtRePass.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nhập mật khẩu";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRePass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Location = new System.Drawing.Point(15, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mật khẩu";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtReName);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Location = new System.Drawing.Point(15, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tên Đăng Nhập";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbChangeBoth);
            this.groupBox3.Controls.Add(this.rdbChangePass);
            this.groupBox3.Controls.Add(this.rdbChangeName);
            this.groupBox3.Location = new System.Drawing.Point(301, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 100);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lựa chọn";
            // 
            // rdbChangeBoth
            // 
            this.rdbChangeBoth.AutoSize = true;
            this.rdbChangeBoth.Location = new System.Drawing.Point(6, 70);
            this.rdbChangeBoth.Name = "rdbChangeBoth";
            this.rdbChangeBoth.Size = new System.Drawing.Size(73, 17);
            this.rdbChangeBoth.TabIndex = 2;
            this.rdbChangeBoth.TabStop = true;
            this.rdbChangeBoth.Text = "Đổi cả hai";
            this.rdbChangeBoth.UseVisualStyleBackColor = true;
            this.rdbChangeBoth.CheckedChanged += new System.EventHandler(this.RdbChangeBoth_CheckedChanged);
            // 
            // rdbChangePass
            // 
            this.rdbChangePass.AutoSize = true;
            this.rdbChangePass.Location = new System.Drawing.Point(6, 43);
            this.rdbChangePass.Name = "rdbChangePass";
            this.rdbChangePass.Size = new System.Drawing.Size(88, 17);
            this.rdbChangePass.TabIndex = 1;
            this.rdbChangePass.TabStop = true;
            this.rdbChangePass.Text = "Đổi mật khẩu";
            this.rdbChangePass.UseVisualStyleBackColor = true;
            this.rdbChangePass.CheckedChanged += new System.EventHandler(this.RdbChangePass_CheckedChanged);
            // 
            // rdbChangeName
            // 
            this.rdbChangeName.AutoSize = true;
            this.rdbChangeName.Location = new System.Drawing.Point(6, 20);
            this.rdbChangeName.Name = "rdbChangeName";
            this.rdbChangeName.Size = new System.Drawing.Size(59, 17);
            this.rdbChangeName.TabIndex = 0;
            this.rdbChangeName.TabStop = true;
            this.rdbChangeName.Text = "Đổi tên";
            this.rdbChangeName.UseVisualStyleBackColor = true;
            this.rdbChangeName.CheckedChanged += new System.EventHandler(this.RdbChangeName_CheckedChanged);
            // 
            // frmChangeInfo
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 261);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeInfo";
            this.Text = "Đổi thông tin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtReName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRePass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbChangeBoth;
        private System.Windows.Forms.RadioButton rdbChangePass;
        private System.Windows.Forms.RadioButton rdbChangeName;
    }
}