using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagementSales.DAO;
using System.Diagnostics;
using ManagementSales.BUS;
using Microsoft.Practices.ServiceLocation;
using ManagementSales.BUS.Interfaces;

namespace ManagementSales
{
    public partial class frmHoaDonThanhToan : Form
    {
        private IHoaDonThanhToanBUS hoaDonThanhToan;
        //public HoaDonThanhToanBUS hoaDonThanhToan { get => ServiceLocator.Current.GetInstance<HoaDonThanhToanBUS>(); }
        public string TenNhanVien { get; set; }
        public frmHoaDonThanhToan(IHoaDonThanhToanBUS hoaDonThanhToanBUS)
        {
            InitializeComponent();
            this.hoaDonThanhToan = hoaDonThanhToanBUS;
        }

        public void Loading()
        {
            try
            {
                txtNameStaff.Text = Program.NameStaff;

                txtSDTKhachHang.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSDTKhachHang.AutoCompleteSource = AutoCompleteSource.CustomSource;
                hoaDonThanhToan.AutoComplete(txtSDTKhachHang);
                cboTenHang.DataSource = hoaDonThanhToan.DataSourceForCombobox();
                lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Tên Hàng" });
                lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Đơn Giá" });
                lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Số Lượng" });

                DisEnableControl();
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
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

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var maKH = hoaDonThanhToan.GetMaKH(txtSDTKhachHang.Text);
                var date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + " " + DateTime.Now.ToLongTimeString();
                hoaDonThanhToan.InsertHoaDon(new object[] { maKH, "Bán Hàng", Program.IDStaff, date, txtNameStaff.Text });
                var kiemtra = lvwChiTietHoaDon.Items.Count == 0;
                if (kiemtra)
                {
                    MessageBox.Show("Điền đơn hàng");
                }
                else
                {
                    var maHD = hoaDonThanhToan.GetMaHoaDon(int.Parse(maKH.ToString()), "Bán Hàng", int.Parse(Program.IDStaff.ToString()), date, txtNameStaff.Text);
                    foreach (ListViewItem item in lvwChiTietHoaDon.Items)
                    {
                        var maHang = hoaDonThanhToan.GetMaHang(item.SubItems[0].Text);
                        var soLuongHang = int.Parse(hoaDonThanhToan.GetSoLuong(new object[] { maHang }).ToString());
                        var donGia = float.Parse(item.SubItems[1].Text);
                        var soLuong = int.Parse(item.SubItems[2].Text);
                        hoaDonThanhToan.InsertChiTietHoaDon(new object[] { maHD, maHang, donGia, soLuong });
                        hoaDonThanhToan.UpdateHangHoa(new object[] { maHang, soLuongHang - soLuong });
                        //var element = item.SubItems.OfType<ListViewItem.ListViewSubItem>().Select(p => p.Text);
                    }
                    MessageBox.Show("Hoàn Thành Đơn Hàng", "Tình Trạng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        private void BtnThemHang_Click(object sender, EventArgs e)
        {
            try
            {

                var tenHang = cboTenHang.Text;
                bool checkTenHangInListView = lvwChiTietHoaDon.FindItemWithText(tenHang) != null ? true : false;//trả ra true khi tìm thấy có tên hàng trong listview và ngược lại
                if (checkTenHangInListView)
                {
                    lvwChiTietHoaDon.FindItemWithText(tenHang).SubItems[2].Text = (int.Parse(lvwChiTietHoaDon.FindItemWithText(tenHang).SubItems[2].Text)
                        + int.Parse(nudSoLuong.Value.ToString())).ToString();
                }
                else
                {
                    ListViewItem listViewItem = new ListViewItem() { Text = tenHang };
                    var donGia = hoaDonThanhToan.LayDonGia(tenHang);
                    listViewItem.SubItems.Add(donGia.ToString());
                    listViewItem.SubItems.Add(nudSoLuong.Value.ToString());
                    lvwChiTietHoaDon.Items.Add(listViewItem);
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        /// <summary>
        /// Chỉ cho nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSDTKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void BtnKiemTraKH_Click(object sender, EventArgs e)
        {
            try
            {

                var sdtKH = txtSDTKhachHang.Text;
                var kiemTra = String.IsNullOrEmpty(sdtKH) || String.IsNullOrWhiteSpace(sdtKH);
                if (kiemTra)
                {
                    MessageBox.Show("Điền số điện thoại");
                    //txtSDTKhachHang.Focus();
                    return;
                }
                var tenKH = hoaDonThanhToan.GetTenKH(txtSDTKhachHang.Text);
                txtTenKhachHang.Text = tenKH?.ToString();
                if (tenKH != null)
                {
                    EnableControl();
                }
                else
                {
                    DisEnableControl();
                    var dialogResult = MessageBox.Show("Số điện thoại không tồn tại\n Chọn YES khi bạn  muốn tạo khách hàng mới và NO ngược lại", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (var frmThemKhachHang = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<frmThemKhachHang>())
                        {
                            frmThemKhachHang.ShowDialog();
                        }
                        //frmThemKhachHang  = new frmThemKhachHang();

                    }
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        private void DisEnableControl()
        {
            lvwChiTietHoaDon.Items.Clear();
            nudSoLuong.Value = 1;
            cboTenHang.Enabled = false;
            nudSoLuong.Enabled = false;
            lvwChiTietHoaDon.Enabled = false;
            btnThemHang.Enabled = false;
            btnThanhToan.Enabled = false;

        }

        private void EnableControl()
        {
            cboTenHang.Enabled = true;
            nudSoLuong.Enabled = true;
            lvwChiTietHoaDon.Enabled = true;
            btnThemHang.Enabled = true;
            btnThanhToan.Enabled = true;
        }

        private void frmHoaDonThanhToan_FormClosing(object sender, FormClosingEventArgs e)
        {

            Program.OpenFrmDangNhap = true;
            Program.OpenFrmHoaDonThanhToan = false;
            Program.CloseFrm = false;
        }

        private void BtnChangeInfo_Click(object sender, EventArgs e)
        {
            using (var fromChangeInfo = ServiceLocator.Current.GetInstance<frmChangeInfo>())
            {
                fromChangeInfo.ShowDialog();
            }
        }

        private void frmHoaDonThanhToan_Load(object sender, EventArgs e)
        {
            Loading();
        }
    }
}
