namespace ManagementSales
{
    partial class frmInPhieu
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
            this.crpvInPhieu = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crpPhieuHang1 = new ManagementSales.crpPhieuHang();
            this.SuspendLayout();
            // 
            // crpvInPhieu
            // 
            this.crpvInPhieu.ActiveViewIndex = -1;
            this.crpvInPhieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crpvInPhieu.Cursor = System.Windows.Forms.Cursors.Default;
            this.crpvInPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crpvInPhieu.Location = new System.Drawing.Point(0, 0);
            this.crpvInPhieu.Name = "crpvInPhieu";
            this.crpvInPhieu.Size = new System.Drawing.Size(758, 475);
            this.crpvInPhieu.TabIndex = 0;
            // 
            // frmInPhieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 475);
            this.Controls.Add(this.crpvInPhieu);
            this.Name = "frmInPhieu";
            this.Text = "frmInPhieu";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crpvInPhieu;
        private crpPhieuHang crpPhieuHang1;
    }
}