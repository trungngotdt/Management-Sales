using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.DTO
{
    public class KhachHangDTO
    {
        private string strMaKH;
        private string strTenKH;
        private int intSDT;
        private Nullable<bool> blnGioiTinh;
        private string strDiaChi;
        private string strLoaiKhachHang;

        public string StrMaKH { get => strMaKH; set => strMaKH = value; }
        public string StrTenKH { get => strTenKH; set => strTenKH = value; }
        public int IntSDT { get => intSDT; set => intSDT = value; }
        public Nullable<bool> BlnGioiTinh { get => blnGioiTinh; set => blnGioiTinh = value; }
        public string StrDiaChi { get => strDiaChi; set => strDiaChi = value; }
        public string StrLoaiKhachHang { get => strLoaiKhachHang; set => strLoaiKhachHang = value; }

        public KhachHangDTO()
        {

        }

        public KhachHangDTO(System.Data.DataRow row)
        {
            this.StrDiaChi = row["DiaChi"].ToString();
            this.StrTenKH = row["TenKH"].ToString();
            this.StrLoaiKhachHang = row["LoaiKhachHang"].ToString();
            this.IntSDT = int.Parse(row["SDT"].ToString());
            this.StrMaKH = row["MaKH"].ToString();
            var gioiTinh = row["GioiTinh"].ToString();
            if (String.IsNullOrEmpty(gioiTinh.ToString()))
            {
                this.BlnGioiTinh = null;
            }
            else
            {
                this.BlnGioiTinh = gioiTinh == "1" ? true : false;
            }
        }

        public KhachHangDTO(string MaKH, string TenKH, int SDT, bool? GioiTinh, string DiaChi, string LoaiKhachHang)
        {
            this.StrDiaChi = DiaChi;
            this.StrMaKH = MaKH;
            this.StrLoaiKhachHang = LoaiKhachHang;
            this.StrTenKH = TenKH;
            this.IntSDT = SDT;
            this.BlnGioiTinh = GioiTinh;
        }
    }
}
