using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FizzWare.NBuilder;
using ManagementSales.DTO;
using System.Data;

namespace QuanLyBanHangTest.DTO
{
    class DTOTest
    {
        private HoaDonDTO hoaDonDTO;
        private KhachHangDTO khachHangDTO;
        private ChiTietHoaDonDTO chiTietHoaDonDTO;
        private NhanVienDTO nhanVienDTO;
        private HangDTO hangDTO;


        private DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new DataTable(typeof(T).Name);
            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name.Remove(0, 3)));
            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            return datatable;
        }

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestAll()
        {
            var dataChiTietHoaDon = GenerateDataTable<ChiTietHoaDonDTO>(10);
            var dataHoaDon = GenerateDataTable<HoaDonDTO>(10);
            var dataNV = GenerateDataTable<NhanVienDTO>(10);
            var dataKH = GenerateDataTable<KhachHangDTO>(10);
            var dataHang = GenerateDataTable<HangDTO>(10);

            var listChiTiet =new List<ChiTietHoaDonDTO>();
            var listHoaDon = new List<HoaDonDTO>();
            var listNV = new List<NhanVienDTO>();
            var listKH = new List<KhachHangDTO>();
            var listHang = new List<HangDTO>();

            var hd= Builder<HoaDonDTO>.CreateNew().Build();
            var cthd = Builder<ChiTietHoaDonDTO>.CreateNew().Build();
            var nv = Builder<NhanVienDTO>.CreateNew().Build();
            var kh = Builder<KhachHangDTO>.CreateNew().Build();
            var hang = Builder<HangDTO>.CreateNew().Build();

            hoaDonDTO = new HoaDonDTO(hd.IntMaHoaDon, hd.IntMaKH, hd.StrLoaiHoaDon, hd.IntMaNV, hd.DtmNgayLap, hd.StrNguoiLap);
            chiTietHoaDonDTO = new ChiTietHoaDonDTO(cthd.IntMaHoaDon, cthd.StrMaHang, cthd.FltDonGia, cthd.IntSoLuong);
            nhanVienDTO = new NhanVienDTO(nv.StrMaNV, nv.StrTenNV, nv.StrChucVu, nv.StrDiaChi, nv.StrDienThoai, nv.StrEmail, nv.StrTenDangNhap, nv.StrMatKhau);
            khachHangDTO = new KhachHangDTO(kh.StrMaKH, kh.StrTenKH, kh.IntSDT, kh.BlnGioiTinh, kh.StrDiaChi, kh.StrLoaiKhachHang);
            hangDTO = new HangDTO(hang.StrMaHang, hang.StrTenHang, hang.FltDonGia,hang.IntSoLuong, hang.StrGhiChu);
            
            dataHang.Rows.OfType<DataRow>().ToList().ForEach(x => listHang.Add(new HangDTO(x)));
            dataKH.Rows.OfType<DataRow>().ToList().ForEach(row =>
            {
                row["GioiTinh"] = "1"; listKH.Add(new KhachHangDTO(row));
            });
            dataKH.Rows.OfType<DataRow>().ToList().ForEach(row => {
                row["GioiTinh"] = ""; listKH.Add(new KhachHangDTO(row));
                });
            dataNV.Rows.OfType<DataRow>().ToList().ForEach(x => listNV.Add(new NhanVienDTO(x)));
            dataChiTietHoaDon.Rows.OfType<DataRow>().ToList().ForEach(x => listChiTiet.Add(new ChiTietHoaDonDTO(x)));
            dataHoaDon.Rows.OfType<DataRow>().ToList().ForEach(x => listHoaDon.Add(new HoaDonDTO(x)));
        }
    }
}
