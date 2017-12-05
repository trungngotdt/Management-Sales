using Microsoft.Practices.ServiceLocation;
using ManagementSales.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementSales.BUS.Interfaces;

namespace ManagementSales
{
    public partial class frmThemKhachHang : Form
    {
        private IThemKhachHangBUS themKhachHang;// { get => ServiceLocator.Current.GetInstance<ThemKhachHangBUS>(); }

        public frmThemKhachHang(IThemKhachHangBUS themKhachHangBUS)
        {
            this.themKhachHang = themKhachHangBUS;
            InitializeComponent();
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
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (CheckTextBox())
                {
                    MessageBox.Show("Không được để trống ô nào");
                    
                }
                else
                {
                    var gioiTinh = cboNam.Checked == true ? 1 : 0;
                    var isThemKH = themKhachHang.InsertKhachHang(new object[] { txtTenKhachHang.Text, int.Parse(txtSDT.Text), gioiTinh, txtDiaChi.Text, txtLoaiKH.Text });
                    /*if (isThemKH)
                    {
                        MessageBox.Show("Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
                    string mess = isThemKH == true ? "Thành Công" : "Có lỗi xảy ra";
                    MessageBox.Show(mess,"THÔNG BÁO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void TxtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Kiểm tra xem textbox có để trống không
        /// </summary>
        private bool CheckTextBox()
        {
            var chechkSDT = String.IsNullOrEmpty(txtSDT.Text) || String.IsNullOrWhiteSpace(txtSDT.Text);
            var checkTen = String.IsNullOrEmpty(txtTenKhachHang.Text) || String.IsNullOrWhiteSpace(txtTenKhachHang.Text);
            var chechDiaChi = String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrWhiteSpace(txtDiaChi.Text);
            var chechLoaiKH = String.IsNullOrEmpty(txtLoaiKH.Text) || String.IsNullOrWhiteSpace(txtLoaiKH.Text);
            var checkGioiTinh = !(cboNam.Checked || cboNu.Checked);
            var checkTextBox = chechDiaChi || chechkSDT || chechLoaiKH || checkTen||checkGioiTinh;
            return checkTextBox;
        }
    }
}
