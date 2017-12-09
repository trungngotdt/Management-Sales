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
    public partial class frmQuanLy : Form
    {
        //private QuanLyBUS quanLy { get => Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<QuanLyBUS>(); }
        private IQuanLyBUS quanLy;

        public frmQuanLy(IQuanLyBUS quanLyBUS )
        {
            this.quanLy = quanLyBUS;
            InitializeComponent();
        }

        private void FrmQuanLy_Load(object sender, EventArgs e)
        {
            FlagForNV(true);
            GetDataNhanVien();
            txtChucVuNV.Enabled = false;
            txtID.Text = Program.IDStaff;
            txtName.Text = Program.NameStaff;
            txtRole.Text = Program.RoleStaff;
            txtRole.Enabled = false;
            txtRole.ReadOnly = true;
            txtID.Enabled = false;
            txtID.ReadOnly = true;
            txtName.Enabled = false;
            txtName.ReadOnly = true;
            //txtMaHang.Enabled = true;

            dgrvNhanVien.ClearSelection();
        }
        
        #region common

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

        /// <summary>
        /// Cho tất cả các textbox của Nhân Viên về giá trị rỗng
        /// </summary>
        void ClearAllTextBoxNV()
        {
            txtChucVuNV.Text = "";
            txtDiaChiNV.Text = "";
            txtEmailNV.Text = "";
            txtMaNV.Text = "";
            txtSDTNV.Text = "";
            txtTenNV.Text = "";

        }

        /// <summary>
        /// Sử dụng cờ cho tab Khách hàng
        /// Khi flag bằng true thì vô hiệu hóa các nút bấm và textbox ngoại trừ <see cref="txtMaNV"/>
        /// </summary>
        /// <param name="flag"></param>
        void FlagForKH(bool flag)
        {
            txtDiaChiKhach.Enabled = !flag;
            txtLoaiKhach.Enabled = !flag;
            txtSDTKhach.Enabled = !flag;
            txtTenKhach.Enabled = !flag;
            txtGoiTinh.Enabled = !flag;
            btnThemKhach.Enabled = !flag;
            btnLamSachKhach.Enabled = !flag;
            btnCapNhatKhach.Enabled = !flag;
        }

        /// <summary>
        /// Sử dụng cờ cho tab Nhân Viên
        /// Khi flag bằng true thì vô hiệu hóa các nút bấm và textbox ngoại trừ <see cref="txtMaNV"/>
        /// </summary>
        /// <param name="flag">Khi flag bằng true thì vô hiệu hóa các nút bấm và textbox ngoại trừ <see cref="txtMaNV"/></param>
        void FlagForNV(bool flag)
        {
            txtChucVuNV.Enabled = !flag;
            txtDiaChiNV.Enabled = !flag;
            txtEmailNV.Enabled = !flag;
            txtTenNV.Enabled = !flag;
            txtSDTNV.Enabled = !flag;
            btnCapNhat.Enabled = !flag;
            btnThem.Enabled = !flag;
            btnLamSachNV.Enabled = !flag;

        }

        /// <summary>
        /// Sử dụng cờ cho tab Hang
        /// Khi flag bằng true thì vô hiệu hóa các nút bấm và textbox ngoại trừ <see cref="txtMaHang"/>
        /// </summary>
        /// <param name="flag">Khi flag bằng true thì vô hiệu hóa các nút bấm và textbox ngoại trừ <see cref="txtMaHang"/></param>
        void FlagForHang(bool flag)
        {
            txtGhiChu.Enabled = !flag;
            txtDonGia.Enabled = !flag;
            txtGhiChu.Enabled = !flag;
            txtSoLuong.Enabled = !flag;
            txtTenHang.Enabled = !flag;
            btnCapNhapHang.Enabled = !flag;
            btnThemHang.Enabled = !flag;
            btnLamSachHang.Enabled = !flag;
        }

        /// <summary>
        /// Kiểm tra textbox khác null hay rỗng
        /// Trả về true nếu textbox có dữ liệu
        /// </summary>
        /// <returns></returns>
        bool CheckTextBoxNV()
        {
            var isDiaChi = txtDiaChiNV.Text.Trim().Length > 0;
            var isTen = txtTenNV.Text.Trim().Length > 0;
            var isChucVu = txtChucVuNV.Text.Trim().Length > 0;
            var isDT = txtSDTNV.Text.Trim().Length > 0;
            var isEmail = txtEmailNV.Text.Trim().Length > 0;
            return isChucVu && isDiaChi && isDT && isEmail && isTen;
        }

        void GetDataDonHang()
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgrvDonHang.DataSource = quanLy.GetDataDonHang();
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Không thể lấy dữ liệu .Tên lỗi {e.Message.ToString()}", "Không thể truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        void GetDataKhachHang()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgrvKhachHang.DataSource = quanLy.GetDataKH();
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Không thể lấy dữ liệu .Tên lỗi {e.Message.ToString()}", "Không thể truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// Kiểm tra các <see cref="TextBox"/> của Khách Hàng xem có để trống không .
        /// Ngoài trừ hai <see cref="TextBox"/> là <see cref="txtMaKhach"/> và <see cref="txtGoiTinh"/>
        /// </summary>
        /// <returns></returns>
        bool CheckTextBoxKH()
        {
            var isDiaChi = txtDiaChiKhach.Text.Trim().Length > 0;
            var isTen = txtTenKhach.Text.Trim().Length > 0;
            var isLoaiKH = txtLoaiKhach.Text.Trim().Length > 0;
            var isDT = txtSDTKhach.Text.Trim().Length > 0;
            return isLoaiKH && isDiaChi && isDT && isTen;
        }

        /// <summary>
        /// Kiểm tra về <paramref name="CheckTextBox"/> ở đây bao gồm <see cref="CheckTextBoxKH"/> hoặc <see cref="CheckTextBoxNV"/> đã ổn chưa
        /// Và hiển thị cảnh báo khi có vấn đề
        /// </summary>
        /// <param name="CheckTextBox">Bao gồm <see cref="CheckTextBoxKH"/> hoặc <see cref="CheckTextBoxNV"/> </param>
        /// <returns></returns>
        bool ValidateBeforeAction(bool CheckTextBox)
        {
            this.Cursor = Cursors.WaitCursor;
            if (!CheckTextBox)
            {
                MessageBox.Show("Điền đầy đủ thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra các <see cref="TextBox"/> của Khách Hàng xem có để trống không .
        /// Ngoài trừ hai <see cref="TextBox"/> là <see cref="txtMaHang"/> và <see cref="txtGhiChu"/>
        /// </summary>
        /// <returns></returns>
        bool CheckTextBoxHang()
        {
            var isTen = txtTenHang.Text.Trim().Length > 0;
            var isSoLuong = txtSoLuong.Text.Trim().Length > 0;
            var isDonGia = txtDonGia.Text.Trim().Length > 0;
            return isTen && isDonGia && isSoLuong;
        }

        /// <summary>
        /// Cho tất cả các giá trị của textbox Khách hàng về rỗng
        /// </summary>
        void ClearAllTextBoxKH()
        {
            txtTenKhach.Text = "";
            txtDiaChiKhach.Text = "";
            txtGoiTinh.Text = "";
            txtLoaiKhach.Text = "";
            txtSDTKhach.Text = "";
        }

        /// <summary>
        /// Cho tất cả các giá trị của textbox Hàng về rỗng
        /// </summary>
        void ClearAllTextBoxHang()
        {
            txtTenHang.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtGhiChu.Text = "";

        }

        /// <summary>
        /// Lấy tất  cả dữ liệu Hàng
        /// </summary>
        void GetDataHang()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgrvHang.DataSource = quanLy.GetDataHang();
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Lỗi không thể lấy dữ liệu.Lỗi {e.Message.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Lấy tất cả dữ liệu của Nhân Viên
        /// </summary>
        void GetDataNhanVien()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgrvNhanVien.DataSource = quanLy.GetDataNV();
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Lỗi không thể lấy dữ liệu.Lỗi {e.Message.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }


        public void InputNumber(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        private void TabCnQuanLy_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name.ToString().Equals(tabPgKhach.Name))
            {
                FlagForKH(true);
                GetDataKhachHang();
                dgrvKhachHang.ClearSelection();
            }
            else if (e.TabPage.Name.Equals(tabPgNV.Name))
            {
                FlagForNV(true);
                GetDataNhanVien();
                dgrvNhanVien.ClearSelection();
            }
            else if (e.TabPage.Name.Equals(tabPgHang.Name))
            {
                FlagForHang(true);
                GetDataHang();
                dgrvHang.ClearSelection();
            }
            else if(e.TabPage.Name.Equals(tagPgDonHang.Name))
            {
                GetDataDonHang();
                dgrvDonHang.ClearSelection();
            }
        }

        //===================================================================================

        //===================================================================================

        #region Nhân Viên

        private void TxtSDTNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(sender, e);
            /*
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }*/
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            var text = btnSua.Text;
            if (text.Equals("Sửa"))
            {
                FlagForNV(false);
                btnThem.Enabled = true;
                btnSua.Text = "Xong";
            }
            else if (text.Equals("Xong"))
            {
                FlagForNV(true);
                btnSua.Text = "Sửa";
            }

        }


        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            /*if (!CheckTextBoxNV())
            {
                MessageBox.Show("Vui Lòng điền đầy đủ thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            if (!ValidateBeforeAction(CheckTextBoxNV()))
            {
                return;
            }
            try
            {
                var isUpdate = quanLy.UpdateNV(new object[] { txtMaNV.Text, txtTenNV.Text, txtChucVuNV.Text, txtDiaChiNV.Text, txtSDTNV.Text, txtEmailNV.Text });
                if (isUpdate)
                {
                    MessageBox.Show("Thành công", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show("Có lỗi trong quá trình sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClearAllTextBoxNV();
            GetDataNhanVien();
            this.Cursor = Cursors.Default;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            /*if (!CheckTextBoxNV())
            {
                MessageBox.Show("Vui Lòng điền đầy đủ thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            if (!ValidateBeforeAction(CheckTextBoxNV()))
            {
                return;
            }
            try
            {
                var checkChucVu = txtChucVuNV.Text.Equals("NV") || txtChucVuNV.Text.Equals("GD") || txtChucVuNV.Text.Equals("TK");
                if (!checkChucVu)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Chức vụ không thể là " + txtChucVuNV.Text+"\nCó 3 chức vụ mặc định : NV,GD,TK","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                //Mật khẩu là SDT
                var isInsert = quanLy.InsertNV(new object[] { txtTenNV.Text, txtChucVuNV.Text, txtDiaChiNV.Text, txtSDTNV.Text, txtEmailNV.Text });
                if (isInsert)
                {
                    MessageBox.Show("Thêm thành công", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show("Có lỗi trong quá trình thêm");
            }
            ClearAllTextBoxNV();
            GetDataNhanVien();
            this.Cursor = Cursors.Default;
        }



        private void BtnLamSachNV_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxNV();
        }

        private void DgrvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            txtMaNV.Text = dgrvNhanVien.Rows[index].Cells["MaNV"].Value.ToString();
            txtDiaChiNV.Text = dgrvNhanVien.Rows[index].Cells["DiaChi"].Value.ToString();
            txtChucVuNV.Text = dgrvNhanVien.Rows[index].Cells["ChucVu"].Value.ToString();
            txtEmailNV.Text = dgrvNhanVien.Rows[index].Cells["Email"].Value.ToString();
            txtSDTNV.Text = dgrvNhanVien.Rows[index].Cells["DienThoai"].Value.ToString();
            txtTenNV.Text = dgrvNhanVien.Rows[index].Cells["TenNV"].Value.ToString();
        }



        private void BtnShow_Click(object sender, EventArgs e)
        {
            GetDataNhanVien();
        }

        #endregion

        //===================================================================================

        //===================================================================================

        #region Khách Hàng

        private void BtnThemKhach_Click(object sender, EventArgs e)
        {
            /*this.Cursor = Cursors.WaitCursor;
            if (!CheckTextBoxKH())
            {
                MessageBox.Show("Điền đầy đủ thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }
            */
            if (!ValidateBeforeAction(CheckTextBoxKH()))
            {
                return;
            }
            try
            {
                var check = quanLy.InsertKH(new object[] { txtTenKhach.Text, txtSDTKhach.Text, txtGoiTinh.Text, txtDiaChiKhach.Text, txtLoaiKhach.Text });
                if (check)
                {
                    MessageBox.Show("Thành công trong việc thêm khách hàng");
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Lỗi không thể thêm khách hàng .Lỗi {ex.Message.ToString()}", "Lỗi Thêm Khách Hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            ClearAllTextBoxKH();
            GetDataKhachHang();
            this.Cursor = Cursors.Default;

        }

        private void BtnSuaKhach_Click(object sender, EventArgs e)
        {
            var text = btnSuaKhach.Text;
            if (text.Equals("Sửa"))
            {
                FlagForKH(false);
                btnThemKhach.Enabled = true;
                btnSuaKhach.Text = "Xong";
            }
            else if (text.Equals("Xong"))
            {
                FlagForKH(true);
                btnSuaKhach.Text = "Sửa";
            }
        }

        private void BtnCapNhatKhach_Click(object sender, EventArgs e)
        {
            /*if (!CheckTextBoxKH())
            {
                MessageBox.Show("Điền đầy đủ thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */

            if (!ValidateBeforeAction(CheckTextBoxKH()))
            {
                return;
            }
            try
            {
                var check = quanLy.UpdateKH(new object[] { txtMaKhach.Text, txtTenKhach.Text, txtSDTKhach.Text, txtGoiTinh.Text, txtDiaChiKhach.Text, txtLoaiKhach.Text });
                if (check)
                {
                    MessageBox.Show("Cập nhật thành công");
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Không thể cập nhật thông tin khách hàng.Lỗi {ex.Message.ToString()}", "Lỗi Cập Nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            ClearAllTextBoxKH();
            GetDataKhachHang();
            this.Cursor = Cursors.Default;
        }

        private void BtnLamSachKhach_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxKH();
        }


        private void TxtSDTKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(sender, e);
            /*
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            */
        }

        private void TxtGoiTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            var charBinary = e.KeyChar == '0' || e.KeyChar == '1';
            if (!charBinary || txtGoiTinh.Text.Length >= 1)
            {
                if (!char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void DgrvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            txtMaKhach.Text = dgrvKhachHang.Rows[index].Cells["MaKH"].Value.ToString();
            txtTenKhach.Text = dgrvKhachHang.Rows[index].Cells["TenKH"].Value.ToString();
            txtSDTKhach.Text = dgrvKhachHang.Rows[index].Cells["SDT"].Value.ToString();
            txtGoiTinh.Text = dgrvKhachHang.Rows[index].Cells["GioiTinh"].Value.ToString();
            txtDiaChiKhach.Text = dgrvKhachHang.Rows[index].Cells["DiaChi"].Value.ToString();
            txtLoaiKhach.Text = dgrvKhachHang.Rows[index].Cells["LoaiKhachHang"].Value.ToString();
        }

        private void BtnShowKH_Click(object sender, EventArgs e)
        {
            GetDataKhachHang();
        }
        #endregion

        //===================================================================================

        //===================================================================================

        #region Hàng

        private void BtnHienThiHang_Click(object sender, EventArgs e)
        {
            GetDataHang();
        }




        private void TxtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(sender, e);
            /*
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }*/
        }

        private void TxtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(sender, e);
            /*
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            */
        }

        private void DgrvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            txtMaHang.Text = dgrvHang.Rows[index].Cells["MaHang"].Value.ToString();
            txtTenHang.Text = dgrvHang.Rows[index].Cells["TenHang"].Value.ToString();
            txtSoLuong.Text = dgrvHang.Rows[index].Cells["SoLuong"].Value.ToString();
            txtDonGia.Text = dgrvHang.Rows[index].Cells["DonGia"].Value.ToString();
            txtGhiChu.Text = dgrvHang.Rows[index].Cells["GhiChu"].Value.ToString()==null?"null": dgrvHang.Rows[index].Cells["GhiChu"].Value.ToString();
        }

        private void BtnSuaHang_Click(object sender, EventArgs e)
        {
            var text = btnSuaHang.Text;
            if (text.Equals("Sửa"))
            {
                FlagForHang(false);
                btnThemHang.Enabled = true;
                btnSuaHang.Text = "Xong";
            }
            else if (text.Equals("Xong"))
            {
                FlagForHang(true);
                btnSuaHang.Text = "Sửa";
            }
        }

        private void BtnLamSachHang_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxHang();
        }

        private void BtnCapNhapHang_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeAction(CheckTextBoxHang()))
            {
                return;
            }
            try
            {
                var check = quanLy.UpdateHang(new object[] { txtMaHang.Text, txtTenHang.Text, txtDonGia.Text, txtSoLuong.Text, txtGhiChu.Text ?? "null" });
                if (check)
                {
                    MessageBox.Show("Cập nhật thành công");
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Không thể cập nhật thông tin khách hàng.Lỗi {ex.Message.ToString()}", "Lỗi Cập Nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            ClearAllTextBoxHang();
            GetDataHang();
            this.Cursor = Cursors.Default;
        }

        private void BtnThemHang_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeAction(CheckTextBoxHang()))
            {
                return;
            }
            try
            {
                var check = quanLy.InsertHang(new object[] { txtMaHang.Text, txtTenHang.Text, txtDonGia.Text, txtSoLuong.Text, txtGhiChu.Text ?? "null" });
                if (check)
                {
                    MessageBox.Show("Cập nhật thành công");
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                //MessageBox.Show($"Không thể cập nhật thông tin khách hàng.Lỗi {ex.Message.ToString()}", "Lỗi Cập Nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            ClearAllTextBoxHang();
            GetDataHang();
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region ChiTietDonHang

        private void BtnHienThiDonHang_Click(object sender, EventArgs e)
        {
            GetDataDonHang();
        }

        private void BtnHienThiChiTietDonHang_Click(object sender, EventArgs e)
        {
            if (dgrvDonHang.SelectedRows==null)
            {
                MessageBox.Show("Test");
            }
            else
            {
                var index = dgrvDonHang.SelectedRows.OfType<DataGridViewRow>().Select(p => p.Cells[0].Value.ToString()).Single();
                //var maHD=dgrvDonHang.Rows[dgrvDonHang.sele]
                using (var chiTietDonHang = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<frmChiTietDonHang>())
                {
                    chiTietDonHang.Index = int.Parse(index);
                    chiTietDonHang.ShowDialog();
                }
            }
        }
        #endregion

        private void FrmQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.OpenFrmDangNhap = true;
            Program.OpenFrmQuanLy = false;
            Program.CloseFrm = false;
        }

        private void BtnChangeInfo_Click(object sender, EventArgs e)
        {

            using (var fromChangeInfo = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<frmChangeInfo>())
            {
                fromChangeInfo.ShowDialog();
            }
        }
    }
}
