﻿namespace ManagementSales
{
    partial class frmHoaDonThanhToan
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
            this.txtNameStaff = new System.Windows.Forms.TextBox();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.txtSDTKhachHang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.cboTenHang = new System.Windows.Forms.ComboBox();
            this.lvwChiTietHoaDon = new System.Windows.Forms.ListView();
            this.btnThemHang = new System.Windows.Forms.Button();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnKiemTraKH = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnChangeInfo = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnHuyDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNameStaff
            // 
            this.txtNameStaff.Enabled = false;
            this.txtNameStaff.Location = new System.Drawing.Point(76, 63);
            this.txtNameStaff.Name = "txtNameStaff";
            this.txtNameStaff.ReadOnly = true;
            this.txtNameStaff.Size = new System.Drawing.Size(104, 20);
            this.txtNameStaff.TabIndex = 0;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Enabled = false;
            this.txtTenKhachHang.Location = new System.Drawing.Point(76, 13);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.ReadOnly = true;
            this.txtTenKhachHang.Size = new System.Drawing.Size(104, 20);
            this.txtTenKhachHang.TabIndex = 0;
            // 
            // txtSDTKhachHang
            // 
            this.txtSDTKhachHang.Location = new System.Drawing.Point(226, 13);
            this.txtSDTKhachHang.Name = "txtSDTKhachHang";
            this.txtSDTKhachHang.Size = new System.Drawing.Size(104, 20);
            this.txtSDTKhachHang.TabIndex = 2;
            this.txtSDTKhachHang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSDTKhachHang_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Khách Hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "SDT";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(629, 411);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(75, 23);
            this.btnThanhToan.TabIndex = 10;
            this.btnThanhToan.Text = "Thanh Toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.BtnThanhToan_Click);
            // 
            // cboTenHang
            // 
            this.cboTenHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTenHang.FormattingEnabled = true;
            this.cboTenHang.Location = new System.Drawing.Point(68, 12);
            this.cboTenHang.Name = "cboTenHang";
            this.cboTenHang.Size = new System.Drawing.Size(104, 21);
            this.cboTenHang.TabIndex = 11;
            // 
            // lvwChiTietHoaDon
            // 
            this.lvwChiTietHoaDon.FullRowSelect = true;
            this.lvwChiTietHoaDon.GridLines = true;
            this.lvwChiTietHoaDon.Location = new System.Drawing.Point(14, 144);
            this.lvwChiTietHoaDon.MultiSelect = false;
            this.lvwChiTietHoaDon.Name = "lvwChiTietHoaDon";
            this.lvwChiTietHoaDon.Size = new System.Drawing.Size(690, 252);
            this.lvwChiTietHoaDon.TabIndex = 12;
            this.lvwChiTietHoaDon.UseCompatibleStateImageBehavior = false;
            this.lvwChiTietHoaDon.View = System.Windows.Forms.View.Details;
            this.lvwChiTietHoaDon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LvwChiTietHoaDon_MouseClick);
            // 
            // btnThemHang
            // 
            this.btnThemHang.Location = new System.Drawing.Point(226, 9);
            this.btnThemHang.Name = "btnThemHang";
            this.btnThemHang.Size = new System.Drawing.Size(104, 23);
            this.btnThemHang.TabIndex = 13;
            this.btnThemHang.Text = "Thêm Hàng";
            this.btnThemHang.UseVisualStyleBackColor = true;
            this.btnThemHang.Click += new System.EventHandler(this.BtnThemHang_Click);
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(68, 64);
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(104, 20);
            this.nudSoLuong.TabIndex = 14;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Nhân Viên";
            // 
            // btnKiemTraKH
            // 
            this.btnKiemTraKH.Location = new System.Drawing.Point(226, 60);
            this.btnKiemTraKH.Name = "btnKiemTraKH";
            this.btnKiemTraKH.Size = new System.Drawing.Size(100, 23);
            this.btnKiemTraKH.TabIndex = 17;
            this.btnKiemTraKH.Text = "Kiểm Tra Khách Hàng";
            this.btnKiemTraKH.UseVisualStyleBackColor = true;
            this.btnKiemTraKH.Click += new System.EventHandler(this.BtnKiemTraKH_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnKiemTraKH);
            this.groupBox1.Controls.Add(this.txtTenKhachHang);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNameStaff);
            this.groupBox1.Controls.Add(this.txtSDTKhachHang);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnThemHang);
            this.groupBox2.Controls.Add(this.nudSoLuong);
            this.groupBox2.Controls.Add(this.cboTenHang);
            this.groupBox2.Location = new System.Drawing.Point(368, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 100);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Số Lượng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tên Hàng";
            // 
            // btnChangeInfo
            // 
            this.btnChangeInfo.Location = new System.Drawing.Point(594, 9);
            this.btnChangeInfo.Name = "btnChangeInfo";
            this.btnChangeInfo.Size = new System.Drawing.Size(104, 23);
            this.btnChangeInfo.TabIndex = 20;
            this.btnChangeInfo.Text = "Đổi thông tin";
            this.btnChangeInfo.UseVisualStyleBackColor = true;
            this.btnChangeInfo.Click += new System.EventHandler(this.BtnChangeInfo_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(13, 411);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 21;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(119, 411);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 22;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Location = new System.Drawing.Point(240, 411);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnCapNhat.TabIndex = 23;
            this.btnCapNhat.Text = "Cập Nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.BtnCapNhat_Click);
            // 
            // btnHuyDon
            // 
            this.btnHuyDon.Location = new System.Drawing.Point(356, 411);
            this.btnHuyDon.Name = "btnHuyDon";
            this.btnHuyDon.Size = new System.Drawing.Size(75, 23);
            this.btnHuyDon.TabIndex = 24;
            this.btnHuyDon.Text = "Hủy Đơn";
            this.btnHuyDon.UseVisualStyleBackColor = true;
            this.btnHuyDon.Click += new System.EventHandler(this.BtnHuyDon_Click);
            // 
            // frmHoaDonThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 446);
            this.Controls.Add(this.btnHuyDon);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnChangeInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvwChiTietHoaDon);
            this.Controls.Add(this.btnThanhToan);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHoaDonThanhToan";
            this.Text = "Hóa Đơn Thanh Toán";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmHoaDonThanhToan_FormClosing);
            this.Load += new System.EventHandler(this.FrmHoaDonThanhToan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNameStaff;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.TextBox txtSDTKhachHang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.ComboBox cboTenHang;
        private System.Windows.Forms.ListView lvwChiTietHoaDon;
        private System.Windows.Forms.Button btnThemHang;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnKiemTraKH;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnChangeInfo;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnHuyDon;
    }
}