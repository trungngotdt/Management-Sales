using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.DTO
{
    public class NhanVienDTO
    {
        private int intMaNV;
        private string strTenNV;
        private string strChucVu;
        private string strDiaChi;
        private int intDienThoai;
        private string strEmail;
        private string strTenDangNhap;
        private string strMatKhau;

        public int StrMaNV { get => intMaNV; set => intMaNV = value; }
        public string StrTenNV { get => strTenNV; set => strTenNV = value; }
        public string StrChucVu { get => strChucVu; set => strChucVu = value; }
        public string StrDiaChi { get => strDiaChi; set => strDiaChi = value; }
        public int StrDienThoai { get => intDienThoai; set => intDienThoai = value; }
        public string StrEmail { get => strEmail; set => strEmail = value; }
        public string StrTenDangNhap { get => strTenDangNhap; set => strTenDangNhap = value; }
        public string StrMatKhau { get => strMatKhau; set => strMatKhau = value; }

        public NhanVienDTO()
        {

        }
        public NhanVienDTO(int manv,string tennv,string chucVu,string diaChi,int dienThoai,string Email,string tenDangNhap,string matKhau)
        {
            this.StrMaNV = manv;
            this.StrTenNV = tennv;
            this.StrChucVu = chucVu;
            this.StrDiaChi = diaChi;
            this.StrDienThoai = dienThoai;
            this.StrEmail = Email;
            this.StrTenDangNhap = tenDangNhap;
            this.StrMatKhau = matKhau;
        }
        public NhanVienDTO(DataRow row)
        {
            this.StrMaNV =int.Parse( row["MaNV"].ToString());
            this.StrTenNV = row["TenNV"].ToString();
            this.StrChucVu= row["ChucVu"].ToString();
            this.StrDiaChi= row["DiaChi"].ToString();
            this.StrDienThoai=int.Parse( row["DienThoai"].ToString());
            this.StrEmail= row["Email"].ToString();
            this.StrTenDangNhap= row["TenDangNhap"].ToString();
            this.StrMatKhau= row["MatKhau"].ToString();
        }
    }
}
