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
    public partial class frmInPhieu : Form
    {
        List<DTO.HangDTO> listHang;
        DTO.HoaDonDTO hoaDon;
        DTO.KhachHangDTO khachHang;
        public frmInPhieu(List< DTO.HangDTO> hangDTO,DTO.HoaDonDTO hoaDon,DTO.KhachHangDTO khachHang)
        {
            InitializeComponent();
            this.hoaDon = hoaDon;
            listHang= hangDTO;
            this.khachHang = khachHang;
            Loading();
        }

        private void Loading()
        {
            crpPhieuHang1.SetDataSource(listHang);
            crpPhieuHang1.SetParameterValue("nameKhachHang", khachHang.StrTenKH);
            crpPhieuHang1.SetParameterValue("diaChi", khachHang.StrDiaChi);
            crpPhieuHang1.SetParameterValue("soDienThoai", khachHang.IntSDT);
            crpPhieuHang1.SetParameterValue("maHoaDon", hoaDon.IntMaHoaDon);
            crpPhieuHang1.SetParameterValue("ngayLap", hoaDon.DtmNgayLap);
            crpvInPhieu.ReportSource = crpPhieuHang1;
            crpvInPhieu.Refresh();
        }
    }
}
