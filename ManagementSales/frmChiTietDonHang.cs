using Microsoft.Practices.ServiceLocation;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSales
{
    public partial class frmChiTietDonHang : Form
    {
        private int index;

        public IChiTietDonHangBUS chiTietDonHang { get => ServiceLocator.Current.GetInstance<ChiTietDonHangBUS>(); }
        public frmChiTietDonHang(int index)
        {
            InitializeComponent();
            this.index = index;
        }


        /// <summary>
        ///Hiển thị thông báo khi có bất kì <see cref="Exception"/> nào bị phát hiện 
        /// </summary>
        /// <param name="ex"></param>
        void WarningMessageBox(Exception ex)
        {
            MessageBox.Show($"Lỗi trong quá trình thực thi.Mã lỗi :\n {ex.Message.ToString()} \\\n Vui lòng liên hệ người quản trị " +
                $"hoặc nhân viên để được nhận thêm sự " +
                $" hỗ trợ", "Lỗi Trong Quá Trình Thực Thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Cursor = Cursors.Default;
        }


        private void frmChiTietDonHang_Load(object sender, EventArgs e)
        {
            try
            {
                dgrvChiTietDonHang.DataSource = chiTietDonHang.GetDataChiTietDonHang(index);
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }
    }
}
