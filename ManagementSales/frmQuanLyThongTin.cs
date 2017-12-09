using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
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
using Microsoft.Practices.ServiceLocation;

namespace ManagementSales
{
    public partial class frmQuanLyThongTin : Form
    {
        //private QuanLyThongTinBUS quanLyThongTin { get => Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<QuanLyThongTinBUS>(); }
        private IQuanLyThongTinBUS quanLyThongTin;

        public frmQuanLyThongTin(IQuanLyThongTinBUS quanLyThongTinBUS)
        {
            InitializeComponent();
            this.quanLyThongTin = quanLyThongTinBUS;
            Loading();
        }

        #region Common Method

        void ConfigForListView()
        {
            lvwThongKeHH.Columns.Add(new ColumnHeader() { Text = "Mã Hàng" });
            lvwThongKeHH.Columns.Add(new ColumnHeader() { Text = "Tên Hàng" });
            lvwThongKeHH.Columns.Add(new ColumnHeader() { Text = "Đơn Giá" });
            lvwThongKeHH.Columns.Add(new ColumnHeader() { Text = "Số Lượng" });

            lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Mã Hàng" });
            lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Tên Hàng" });
            lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Đơn Giá" });
            lvwChiTietHoaDon.Columns.Add(new ColumnHeader() { Text = "Số Lượng" });


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

        void Loading()
        {

            try
            {

                this.ActiveControl = txtSDTKhachHang;
                ConfigForListView();
                cboTenHang.DataSource = quanLyThongTin.DataSourceForCombobox();
                StatusControlLapPhieu(false);

                /*txtID.Text = Program.IDStaff;
                txtName.Text = Program.NameStaff;
                txtRole.Text = Program.RoleStaff;
                txtRole.Enabled = false;
                txtRole.ReadOnly = true;
                txtID.Enabled = false;
                txtID.ReadOnly = true;
                txtName.Enabled = false;
                txtName.ReadOnly = true;
                txtNameStaff.Text = txtName.Text;*/
                DefaultSetControl();
                DisEnableControl();
                this.AcceptButton = btnKiemTraKH;
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        /// <summary>
        /// Các trạng thái và giá trị của một số <see cref="Control"/> khi mới Loading
        /// Các <see cref="Control"/> gồm <see cref="txtID"/> và <see cref="txtName"/> và <see cref="txtRole"/> và <see cref="txtNameStaff"/>
        /// </summary>
        void DefaultSetControl()
        {
            txtID.Enabled = false;
            txtID.ReadOnly = true;
            txtID.Text = Program.IDStaff;
            txtName.Enabled = false;
            txtName.ReadOnly = true;
            txtName.Text = Program.NameStaff;
            txtRole.Text = Program.RoleStaff;
            txtRole.Enabled = false;
            txtRole.ReadOnly = true;
            txtNameStaff.Text = txtName.Text;
        }

        /// <summary>
        /// Thêm dữ liệu vào listview ; dữ liệu ở đây là một danh sách có kiểu <see cref="DTO.HangDTO"/>
        /// </summary>
        /// <param name="listHang"></param>
        void AddDataToListView(List<DTO.HangDTO> listHang, ListView listView)
        {
            listHang.ForEach((x) =>
            {
                ListViewItem listViewItem = new ListViewItem() { Text = x.StrMaHang };
                listViewItem.SubItems.Add(x.StrTenHang);
                listViewItem.SubItems.Add(x.FltDonGia.ToString());
                listViewItem.SubItems.Add(x.IntSoLuong.ToString());
                listView.Items.Add(listViewItem);
            });
        }
        #endregion

        //===================================================================================================================//

        //===================================================================================================================//

        #region Thống Kê Hàng Hóa 
        private void BntHienHangHoa_Click(object sender, EventArgs e)
        {
            try
            {

                lvwThongKeHH.Items.Clear();
                var data = quanLyThongTin.ShowAllHang();
                AddDataToListView(data, lvwThongKeHH);
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        /// <summary>
        /// Kiểm tra các <see cref="CheckBox"/> bao gồm <see cref="chkDonGia"/> và <see cref="chkMaHang"/> và <see cref="chkSoLuong"/> và <see cref="chkTenHang"/>
        /// có bị unchecked hay không
        /// </summary>
        /// <returns>Trả về <see cref="true"/> khi tất cả các <see cref="CheckBox"/> không được check </returns>
        bool IsUnCheckCheckBox()
        {
            var kiemTraCheckBox = chkDonGia.CheckState == CheckState.Unchecked &&
                chkMaHang.CheckState == CheckState.Unchecked &&
                chkSoLuong.CheckState == CheckState.Unchecked &&
                chkTenHang.CheckState == CheckState.Unchecked;
            return kiemTraCheckBox;
        }

        private void BtnSapXep_Click(object sender, EventArgs e)
        {
            try
            {

                lvwThongKeHH.Items.Clear();
                if ((rdbBeDenLon.Checked == false && rdbLonDenBe.Checked == false) || IsUnCheckCheckBox())
                {
                    MessageBox.Show("Làm ơn tích vào ít nhất một ô lựa chọn", "Thiếu lựa chọn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*var kiemTraCheckBox = chkDonGia.CheckState == CheckState.Unchecked &&
                    chkMaHang.CheckState == CheckState.Unchecked &&
                    chkSoLuong.CheckState == CheckState.Unchecked &&
                    chkTenHang.CheckState == CheckState.Unchecked;*/
                /*if (IsUnCheckCheckBox())
                {
                    MessageBox.Show("Làm on đánh dấu vào ít nhất một ô", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }*/
                var MaHang = chkMaHang.CheckState == CheckState.Checked ? "MaHang" : "";
                var TenHang = chkTenHang.CheckState == CheckState.Checked ? "TenHang" : "";
                var SoLuong = chkSoLuong.CheckState == CheckState.Checked ? "SoLuong" : "";
                var DonGia = chkDonGia.CheckState == CheckState.Checked ? "DonGia" : "";
                var asc = rdbBeDenLon.Checked;
                var data = quanLyThongTin.SortBy(new object[] { MaHang, TenHang, SoLuong, DonGia }, asc);
                AddDataToListView(data, lvwThongKeHH);
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {

                lvwThongKeHH.Items.Clear();
                var checkNullTextBox = txtTen.Text.Trim().Length == 0;// String.IsNullOrEmpty(txtTen.Text) || String.IsNullOrWhiteSpace(txtTen.Text);
                if ((rdbChinhXac.Checked == false && rdbGanDung.Checked == false) || checkNullTextBox == true)
                {
                    MessageBox.Show("Xin hãy nhập thông tin ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var typeSearch = rdbChinhXac.Checked == true ? true : false;
                var data = quanLyThongTin.SearchBy(txtTen.Text, typeSearch);
                AddDataToListView(data, lvwThongKeHH);
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }
        #endregion

        //===================================================================================================================//

        //===================================================================================================================//

        #region Lập Phiếu

        #region Method

        /// <summary>
        /// 
        /// </summary>
        void DefaultValue()
        {
            nudSoLuong.Value = decimal.Parse("1");
            txtDonGia.Text = "";
        }

        /// <summary>
        /// Kiểm tra <see cref="lvwChiTietHoaDon"/> có bất cứ items nào được chọn không
        /// </summary>
        bool KiemTraListViewSelect()
        {
            var beSelect = lvwChiTietHoaDon.SelectedIndices.Count > 0;
            if (beSelect)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sử dụng làm cờ cho các button 
        /// Khi giá trị <paramref name="flag"/> bằng <see cref="true"/> thì <see cref="btnKiemTraKH"/> ,
        /// <see cref="btnInPhieu"/> , <seealso cref="btnSua"/>
        /// , <see cref="btnThemHang"/> được kích hoạt ( hay  Enabled bằng true) và ngược lại
        /// </summary>
        /// <param name="flag">Có giá trị true hoặc false</param>
        void FlagForButton(bool flag)
        {
            btnKiemTraKH.Enabled = flag;
            btnInPhieu.Enabled = flag;
            btnHuy.Enabled = flag;
            btnXoa.Enabled = flag;
            btnThemHang.Enabled = flag;
            btnKiemTraKH.Enabled = flag;
            btnCapNhat.Enabled = !flag;
            rdbXuat.Enabled = flag;
            rdbNhap.Enabled = flag;
            rdbHangMoi.Enabled = flag;
            rdbHangTrongKho.Enabled = flag;
        }


        /// <summary>
        /// Kiểm tra các xem đã chọn Loại Hàng ,Đơn Giá ,Loại Phiếu chưa.
        /// Trả về true nếu tất cả điều hợp lệ
        /// </summary>
        /// <returns></returns>
        private bool KiemTraHopLe()
        {
            var kiemTraDonGia = txtDonGia.Enabled ? txtDonGia.Text.Trim().Length > 0 : true;//trả ra true khi các txtDonGia được điền đầu đủ
            var kiemTraTenHang = cboTenHang.Text.Trim().Length > 0;//trả ra true khi cboTenHang được điền đầu đủ
            var loaiHang = rdbHangMoi.Checked == true || rdbHangTrongKho.Checked == true;
            var loaiPhieu = rdbNhap.Checked == true || rdbXuat.Checked == true;
            var ketQua = kiemTraDonGia && kiemTraTenHang && loaiPhieu && loaiHang;
            return ketQua;
        }

        /// <summary>
        /// Chỉ cho phép nhập số 
        /// <param name="e"></param>
        private void KhongChoNhapChu(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Xem đã <see cref="RadioButton"/> loại hóa đơn đã được chọn chưa
        /// </summary>
        /// <returns></returns>
        bool KiemTraLoaiHD()
        {
            var check = rdbNhap.Checked || rdbXuat.Checked;
            return check;
        }

        /// <summary>
        /// Gán các giá trị của các Control theo giá <paramref name="flag"/> 
        /// </summary>
        /// <param name="flag"></param>
        private void StatusControlLapPhieu(bool flag)
        {
            btnCapNhat.Enabled = flag;
            btnSua.Enabled = flag;
            btnXoa.Enabled = flag;
            txtDonGia.Enabled = flag;
            btnInPhieu.Enabled = flag;
            btnThemHang.Enabled = flag;
            nudSoLuong.Enabled = flag;
            cboTenHang.Enabled = flag;
            nudSoLuong.Enabled = flag;
            lvwChiTietHoaDon.Enabled = flag;
            rdbHangMoi.Enabled = flag;
            rdbHangTrongKho.Enabled = flag;
            rdbNhap.Enabled = flag;
            rdbXuat.Enabled = flag;
            if (!flag)
            {
                lvwChiTietHoaDon.Items.Clear();
                nudSoLuong.Value = 1;
            }
        }


        #endregion

        private void BtnKiemTraKH_Click(object sender, EventArgs e)
        {
            try
            {

                var sdtKH = txtSDTKhachHang.Text;
                var kiemTra = !(sdtKH.Trim().Length > 0); //String.IsNullOrEmpty(sdtKH) || String.IsNullOrWhiteSpace(sdtKH);
                if (kiemTra)
                {
                    MessageBox.Show("Điền số điện thoại");
                    StatusControlLapPhieu(false);
                    return;
                }
                var tenKH = quanLyThongTin.GetTenKH(txtSDTKhachHang.Text);
                txtTenKhachHang.Text = tenKH?.ToString();
                if (tenKH != null)
                {
                    StatusControlLapPhieu(true);
                }
                else
                {
                    StatusControlLapPhieu(false);
                    var dialogResult = MessageBox.Show("Số điện thoại không tồn tại.\n Chọn Yes nếu muốn tạo mới và No để quay về", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        using (var frmThemKhachHang = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<frmThemKhachHang>())
                        {
                            frmThemKhachHang.ShowDialog();
                        }
                    }
                }
                btnCapNhat.Enabled = false;
                this.AcceptButton = btnThemHang;
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }


        /// <summary>
        /// Đưa trạng thái của <see cref=" txtDonGia"/> và <see cref="cboTenHang"/> về các trạng thái khác nhau tùy vào tùy loại đơn hàng  
        /// </summary>
        /// <param name="flag"></param>
        void StateFortxtDonGiaAndcboTenHang(bool flag)
        {
            txtDonGia.Enabled = flag;
            cboTenHang.DropDownStyle = flag == true ? ComboBoxStyle.Simple : ComboBoxStyle.DropDownList;
            cboTenHang.Text = flag == true ? "" : cboTenHang.Text;
        }

        /// <summary>
        /// Kiểm tra các yếu tố,yếu tố ở đây là đã chọn loại hóa đơn chưa mới được check <see cref="rdbHangMoi"/> hoặc <see cref="rdbHangTrongKho"/>
        /// </summary>
        /// <param name="radioButton"> chỉ có được <see cref="rdbHangTrongKho"/> hoặc <see cref="rdbHangMoi"/></param>
        /// <returns></returns>
        bool CheckValueRdbHang(RadioButton radioButton)
        {
            if ((!KiemTraLoaiHD()) && (radioButton.Checked))
            {
                MessageBox.Show("Chọn loại hóa đơn trước", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                radioButton.Checked = false;
                return false;
            }
            return true;
        }


        private void RdbHangMoi_CheckedChanged(object sender, EventArgs e)
        {
            /*if ((!KiemTraLoaiHD()) && (rdbHangMoi.Checked))
            {
                MessageBox.Show("Chọn loại hóa đơn trước", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rdbHangMoi.Checked = false;
                return;
            }
            txtDonGia.Enabled = true;
            cboTenHang.DropDownStyle = ComboBoxStyle.Simple;
            cboTenHang.Text = "";*/
            if (!CheckValueRdbHang(rdbHangMoi))
            {
                return;
            }
            StateFortxtDonGiaAndcboTenHang(true);
        }

        private void RdbHangTrongKho_CheckedChanged(object sender, EventArgs e)
        {
            /*if (!KiemTraLoaiHD() && (rdbHangTrongKho.Checked))
            {
                MessageBox.Show("Chọn loại hóa đơn trước", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rdbHangTrongKho.Checked = false;
                return;
            }
            txtDonGia.Enabled = false;
            cboTenHang.DropDownStyle = ComboBoxStyle.DropDownList;*/

            if (!CheckValueRdbHang(rdbHangTrongKho))
            {
                return;
            }
            StateFortxtDonGiaAndcboTenHang(false);

        }

        private void BtnThemHang_Click(object sender, EventArgs e)
        {
            try
            {

                //Kiểm tra các ô đơn giá , tên hàng có để trống không
                if (!KiemTraHopLe())
                {
                    MessageBox.Show("Điền vào chỗ trống");
                    return;
                }
                var tenHang = cboTenHang.Text;
                var maHang = rdbHangMoi.Checked == true ? "-1" : quanLyThongTin.GetMaHang(tenHang).ToString();
                //Kiểm tra xem mặt hàng đó có trong listview chưa nếu có thì tăng mặt hàng đó lên theo số lượng thêm vào
                bool checkTenHangInListView = lvwChiTietHoaDon.FindItemWithText(tenHang) != null ? true : false;//trả ra true khi tìm thấy có tên hàng trong listview và ngược lại
                if (checkTenHangInListView)
                {
                    //Cái này là cộng dồn vào cột số lượng khi món hàng thêm vào đã có
                    lvwChiTietHoaDon.FindItemWithText(tenHang).SubItems[3].Text = (int.Parse(lvwChiTietHoaDon.FindItemWithText(tenHang).SubItems[3].Text)
                        + int.Parse(nudSoLuong.Value.ToString())).ToString();
                }
                //Nếu không thì thêm mặt hàng đó vào listview
                else
                {
                    var donGia = rdbHangTrongKho.Checked == true ? quanLyThongTin.LayDonGia(tenHang) : txtDonGia.Text;
                    DTO.HangDTO hang = new DTO.HangDTO(maHang, tenHang, float.Parse(donGia.ToString()), int.Parse(nudSoLuong.Value.ToString()));
                    List<DTO.HangDTO> list = new List<DTO.HangDTO>
                {
                    hang
                };
                    AddDataToListView(list, lvwChiTietHoaDon);
                }
                DefaultValue();
                ///
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                return;
            }

        }

        private void RxtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            KhongChoNhapChu(e);
        }


        private void TxtSDTKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            KhongChoNhapChu(e);
            this.AcceptButton = btnKiemTraKH;
        }

        private void BtnInPhieu_Click(object sender, EventArgs e)
        {
            var kiemtra = lvwChiTietHoaDon.Items.Count == 0;
            if (kiemtra)
            {
                MessageBox.Show("Điền đơn hàng");
                return;
            }
            List<DTO.HangDTO> list = new List<DTO.HangDTO>();

            foreach (ListViewItem item in lvwChiTietHoaDon.Items)
            {
                /*var maHang = item.SubItems[0].Text.ToString();
                var tenHang = item.SubItems[1].Text.ToString();
                var donGia = float.Parse(item.SubItems[2].Text.ToString());
                var soLuong = int.Parse(item.SubItems[3].Text.ToString());*/
                DTO.HangDTO chiTietHoaDon = new DTO.HangDTO()
                {
                    StrMaHang = item.SubItems[0].Text.ToString(),
                    StrTenHang = item.SubItems[1].Text.ToString(),
                    FltDonGia = float.Parse(item.SubItems[2].Text.ToString()),
                    IntSoLuong = int.Parse(item.SubItems[3].Text.ToString()),
                };
                /*DTO.HangDTO chiTietHoaDon = new DTO.HangDTO(maHang, tenHang, donGia, soLuong);*/
                list.Add(chiTietHoaDon);
            }
            var khachHang = quanLyThongTin.GetKhachHangBySDT(int.Parse(txtSDTKhachHang.Text.ToString()));
            
            int maHD = int.Parse((((int)DateTime.Now.TimeOfDay.TotalSeconds).ToString() + ((int)DateTime.Now.DayOfYear).ToString()));
            DTO.HoaDonDTO hoaDon = new DTO.HoaDonDTO(maHD, 0, "", 0, DateTime.Now, "0");
            //
            this.Cursor = Cursors.WaitCursor;
            using (frmInPhieu inPhieu = new frmInPhieu(list, hoaDon, khachHang))
            {
                this.Cursor = Cursors.Default;
                inPhieu.ShowDialog();
            }
        }


        private void BtnSua_Click(object sender, EventArgs e)
        {
            var text = btnSua.Text;
            if (text.Equals("Sửa"))
            {
                btnSua.Text = "Xong";
                FlagForButton(false);
            }
            else
            {
                btnSua.Text = "Sửa";
                FlagForButton(true);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (KiemTraListViewSelect())
            {
                MessageBox.Show("Chọn Item trước", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            var index = lvwChiTietHoaDon.SelectedIndices.OfType<int>().Single();
            lvwChiTietHoaDon.Items[index].Remove();
            StatusControlLapPhieu(true);
        }

        //ListViewItem GetListViewItem

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            var index = lvwChiTietHoaDon.SelectedIndices.OfType<int>().Single();
            var items = lvwChiTietHoaDon.Items[index];
            var maHang = items.SubItems[0].Text;
            if (maHang.Equals("-1") && txtDonGia.Text.Trim().Length <= 0)
            {
                MessageBox.Show("Điền đầy đủ vào chỗ trống", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lvwChiTietHoaDon.Items[index].Remove();
            BtnThemHang_Click(sender, e);
        }


        private void LvwChiTietHoaDon_MouseClick(object sender, MouseEventArgs e)
        {
            var checkValue = btnSua.Text.Equals("Xong") && lvwChiTietHoaDon.SelectedIndices.Count > 0;
            if (checkValue)
            {
                var index = lvwChiTietHoaDon.SelectedIndices.OfType<int>().Single();
                var items = lvwChiTietHoaDon.Items[index];
                var maHang = items.SubItems[0].Text;
                txtDonGia.Enabled = maHang.Equals("-1");
                cboTenHang.DropDownStyle = maHang.Equals("-1") ? ComboBoxStyle.Simple : ComboBoxStyle.DropDownList;
                cboTenHang.Text = items.SubItems[1].Text;
                txtDonGia.Text = items.SubItems[2].Text;
                var soLuong = items.SubItems[3].Text.ToString();
                nudSoLuong.Value = decimal.Parse(soLuong);
            }
        }


        private int flagRdb = 0;
        private void RdbXuat_CheckedChanged(object sender, EventArgs e)
        {
            var countItem = lvwChiTietHoaDon.Items.Count > 0;
            if (flagRdb == 1)
            {
                flagRdb = 0;
                return;
            }
            else if (countItem)
            {
                var reslut = MessageBox.Show("Bạn sẽ xóa hóa đơn cũ.Và sẽ tạo hóa đơn mới?", "NHẮC NHỞ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reslut == DialogResult.Yes)
                {
                    lvwChiTietHoaDon.Items.Clear();
                }
                else
                {
                    flagRdb = 1;
                    if (rdbNhap.Checked == true)
                    {
                        rdbNhap.Checked = false;
                        rdbXuat.Checked = true;
                    }
                    else
                    {
                        rdbNhap.Checked = true;
                    }
                }
            }
            if (rdbXuat.Checked == true)
            {
                rdbHangTrongKho.Checked = true;
                rdbHangMoi.Enabled = false;
            }
            else
            {
                rdbHangTrongKho.Checked = false;
                rdbHangMoi.Enabled = true;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc không", "Hủy hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                lvwChiTietHoaDon.Items.Clear();
            }
        }

        #endregion

        //===================================================================================================================//

        //===================================================================================================================//

        #region Xuất/Nhập Hàng


        #region Method

        public void EnableControl()
        {
            dgrvHang.Enabled = true;
            btnNhapHang.Enabled = true;
            btnXuatHang.Enabled = true;
            btnChonFile.Enabled = true;
        }

        public void DisEnableControl()
        {
            dgrvHang.Enabled = false;
            btnNhapHang.Enabled = false;
            btnXuatHang.Enabled = false;
            btnChonFile.Enabled = false;
        }
        #endregion
        private void BtnNhapHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgrvHang.Rows.Count != 0)
                {
                    var maKH = quanLyThongTin.GetMaKH(txtSDT.Text);
                    var date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + " " + DateTime.Now.ToLongTimeString();
                    quanLyThongTin.InsertHoaDon(new object[] { maKH, "Nhập", Program.IDStaff, date, txtNameStaff.Text });
                    //var data = new object[] { maKH, "Xuất", Program.IDStaff, date, txtNameStaff.Text };
                    var maHD = quanLyThongTin.GetMaHoaDon(int.Parse(maKH.ToString()), "Nhập", int.Parse(Program.IDStaff.ToString()), date, txtNameStaff.Text);
                    var mess = quanLyThongTin.NhapXuatHang(dgrvHang, true, maHD);
                    if (mess.Trim().Length != 0)
                    {
                        MessageBox.Show("Mã hàng " + mess + "không thể nhập .Vui lòng xem xét lại", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }
        
        private void BtnXuatHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgrvHang.Rows.Count != 0)
                {
                    var maKH = quanLyThongTin.GetMaKH(txtSDT.Text);
                    var date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + " " + DateTime.Now.ToLongTimeString();
                    quanLyThongTin.InsertHoaDon(new object[] { maKH, "Xuất", Program.IDStaff, date, txtNameStaff.Text });
                    //var data = new object[] { maKH, "Xuất", Program.IDStaff, date, txtNameStaff.Text };
                    var maHD = quanLyThongTin.GetMaHoaDon(int.Parse(maKH.ToString()), "Xuất", int.Parse(Program.IDStaff.ToString()), date, txtNameStaff.Text);
                    var mess = quanLyThongTin.NhapXuatHang(dgrvHang, false, maHD);
                    if (mess.Trim().Length != 0)
                    {
                        MessageBox.Show("Mã hàng " + mess + "không thể xuất .Vui lòng xem xét lại", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thành công", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }

        private async void BtnChonFile_ClickAsync(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {

                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataTable.Columns.Add("Mã Hàng");
                dataTable.Columns.Add("Tên Hàng");
                dataTable.Columns.Add("Đơn Giá");
                dataTable.Columns.Add("Số Lượng");
                dataTable.Columns.Add("Ghi Chú");

                //dgrvHang.Rows.Clear();
                Cursor.Current = Cursors.WaitCursor;
                using (var Opf = new OpenFileDialog() { Filter = "Excel Workbook[97-2003] | *.xls|Excel Workbook|*.xlsx", ValidateNames = true })
                {
                    if (Opf.ShowDialog() == DialogResult.OK)
                    {
                        var data = await quanLyThongTin.ReadAsync(new Excel.Application(), Opf.FileName);
                        var count = data.Count;
                        data.ForEach(x =>
                        {
                            dataTable.Rows.Add(x.Item1, x.Item2, x.Item3, x.Item4, x.Item5);
                            /*dataTable.Rows.Add(x.Item2);
                            dataTable.Rows.Add(x.Item3);
                            dataTable.Rows.Add(x.Item4);
                            dataTable.Rows.Add(x.Item5);*/
                        });

                        dataTable.AcceptChanges();
                        dgrvHang.DataSource = dataTable;
                    }

                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
                string error = "0x80010105";
                if (ex.Message.Contains(error))
                {
                    MessageBox.Show("Có ba cách sửa :\n" +
                        "1: Theo chỉ dẫn sau Excel > File > Options > Add-ins > Manage, sau đó chọn COM add - ins > Go trong ô hiện ra bỏ chọn FoxitReader PDF Creator COM Add-in\n" +
                        "2:Thử lại\n" +
                        "3:Liên hệ quản trị viên", "HƯỚNG DẪN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                var sdtKH = txtSDT.Text;
                var kiemTra = String.IsNullOrEmpty(sdtKH) || String.IsNullOrWhiteSpace(sdtKH);
                if (kiemTra)
                {
                    MessageBox.Show("Điền số điện thoại");
                    //txtSDTKhachHang.Focus();
                    return;
                }
                var tenKH = quanLyThongTin.GetTenKH(txtSDT.Text);
                txtTenKH.Text = tenKH?.ToString();
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
                    }
                }
            }
            catch (Exception ex)
            {
                WarningMessageBox(ex);
            }
        }


        private void TxtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            KhongChoNhapChu(e);
            this.AcceptButton = btnKiemTra;
        }

        #endregion

        private void frmQuanLyThongTin_FormClosing(object sender, FormClosingEventArgs e)
        {

            Program.OpenFrmDangNhap = true;
            Program.OpenFrmQuanLyThongTin = false;
            Program.CloseFrm = false;
        }

        private void btnChangeInfo_Click(object sender, EventArgs e)
        {

            using (var fromChangeInfo = ServiceLocator.Current.GetInstance<frmChangeInfo>())
            {
                fromChangeInfo.ShowDialog();
            }
        }
    }
}
