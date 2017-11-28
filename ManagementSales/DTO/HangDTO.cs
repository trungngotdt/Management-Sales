using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.DTO
{
    public class HangDTO
    {
        private string strMaHang;
        private string strTenHang;
        private int intSoLuong;
        private float fltDonGia;
        private string strGhiChu;

        public string StrMaHang { get => strMaHang; set => strMaHang = value; }
        public string StrTenHang { get => strTenHang; set => strTenHang = value; }
        public int IntSoLuong { get => intSoLuong; set => intSoLuong = value; }
        public float FltDonGia { get => fltDonGia; set => fltDonGia = value; }
        public string StrGhiChu { get => strGhiChu; set => strGhiChu = value; }

        public HangDTO()
        {

        }
        public HangDTO(System.Data.DataRow row)
        {
            this.StrGhiChu= row["GhiChu"].ToString();
            this.FltDonGia =float.Parse( row["DonGia"].ToString());
            this.StrTenHang = row["TenHang"].ToString();
            this.StrMaHang = row["MaHang"].ToString();
            this.IntSoLuong =int.Parse( row["SoLuong"].ToString());
        }
        public HangDTO(string MaHang,string TenHang,float DonGia,int SoLuong,string GhiChu=null)
        {
            this.StrGhiChu = GhiChu;
            this.StrMaHang = MaHang;
            this.StrTenHang = TenHang;
            this.FltDonGia = DonGia;
            this.IntSoLuong = SoLuong;
        }
    }
}
