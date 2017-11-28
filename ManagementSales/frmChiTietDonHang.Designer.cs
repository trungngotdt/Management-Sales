namespace ManagementSales
{
    partial class frmChiTietDonHang
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
            this.dgrvChiTietDonHang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvChiTietDonHang)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrvChiTietDonHang
            // 
            this.dgrvChiTietDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvChiTietDonHang.Location = new System.Drawing.Point(12, 12);
            this.dgrvChiTietDonHang.MultiSelect = false;
            this.dgrvChiTietDonHang.Name = "dgrvChiTietDonHang";
            this.dgrvChiTietDonHang.ReadOnly = true;
            this.dgrvChiTietDonHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrvChiTietDonHang.Size = new System.Drawing.Size(529, 366);
            this.dgrvChiTietDonHang.TabIndex = 0;
            // 
            // frmChiTietDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 390);
            this.Controls.Add(this.dgrvChiTietDonHang);
            this.Name = "frmChiTietDonHang";
            this.Text = "frmChiTietDonHang";
            this.Load += new System.EventHandler(this.frmChiTietDonHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrvChiTietDonHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrvChiTietDonHang;
    }
}