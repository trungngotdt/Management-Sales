using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.DTO
{
    public class ChiTietHoaDonDTO
    {
        private int intMaHoaDon;
        private string strMaHang;
        private float fltDonGia;
        private int intSoLuong;
        public int IntMaHoaDon { get => intMaHoaDon; set => intMaHoaDon = value; }
        public string StrMaHang { get => strMaHang; set => strMaHang = value; }
        public float FltDonGia { get => fltDonGia; set => fltDonGia = value; }
        public int IntSoLuong { get => intSoLuong; set => intSoLuong = value; }
        public ChiTietHoaDonDTO()
        {

        }
        public ChiTietHoaDonDTO(System.Data.DataRow row)
        {
            this.IntMaHoaDon =int.Parse( row["MaHoaDon"].ToString());
            this.StrMaHang = row["MaHang"].ToString();
            this.FltDonGia = float.Parse(row["DonGia"].ToString());
            this.IntSoLuong = int.Parse(row["SoLuong"].ToString());
        }
        public ChiTietHoaDonDTO(int MaHoaDon,string MaHang,float DonGia,int SoLuong)
        {
            this.StrMaHang = MaHang;
            this.IntMaHoaDon = MaHoaDon;
            this.IntSoLuong = SoLuong;
            this.FltDonGia = DonGia;
        }
    }
}
