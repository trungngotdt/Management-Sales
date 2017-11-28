using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS.Interfaces
{
    public interface IHoaDonThanhToanBUS
    {

        /// <summary>
        /// Trả về chuỗi <see cref="AutoCompleteStringCollection"/> phục vụ cho chức năng AutoComplete của TextBox
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        System.Windows.Forms.AutoCompleteStringCollection SourceForAutoComplete(string query, object[] values = null);

        /// <summary>
        /// trả về chuổi <see cref="string"/> có thể lấy làm dữ liệu cho các gợi ý
        /// </summary>
        /// <returns></returns>
        string[] SourceComplete(string query, object[] values = null);

        /// <summary>
        /// Lấy số lượng hàng dựa vào mã hàng
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        object GetSoLuong(object[] values = null);

        /// <summary>
        /// Cập nhật số lượng hàng trong kho theo mả hàng
        /// Theo thứ tự MaHang , Soluong mới 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        bool UpdateHangHoa( object[] values = null);

        /// <summary>
        /// Trả về giá trị MaHoaDon
        /// </summary>
        /// <param name="MaKH"></param>
        /// <param name="LoaiHD"></param>
        /// <param name="MaNV"></param>
        /// <param name="NgayLap"></param>
        /// <param name="TenNV"></param>
        /// <returns></returns>
        object GetMaHoaDon(int MaKH, string LoaiHD, int MaNV, string NgayLap, string TenNV);

        /// <summary>
        /// Trả về TenKhachHang dựa vào SDT
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        object GetTenKH(string sdt);

        /// <summary>
        /// Trả về MaKH theo SDT
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        object GetMaKH(string tenKH);

        /// <summary>
        /// Chèn dữ liệu HoaDon vào database
        /// Với các giá trị theo thứ tự MaKH,LoaiHoaDon,MaNV, NgayLap,NguoiLap
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        bool InsertHoaDon(object[] values);

        /// <summary>
        /// Trả về MaHang ứng với tên hàng
        /// </summary>
        /// <param name="tenHang"></param>
        /// <returns></returns>
        object GetMaHang(string tenHang);

        /// <summary>
        /// Trả về true khi chèn dữ liệu thành công ,chen dữ liệu vào bảng ChiTietHoaDon
        /// Dữ liêu chen vào phải theo thứ tự MaHoaDon,MaHang,DonGia,SoLuong
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        bool InsertChiTietHoaDon( object[] values = null);

        /// <summary>
        /// Gán giá trị cho phần <see cref="AutoCompleteSource"/>  của <see cref="TextBox"/> 
        /// </summary>
        /// <param name="textBox"></param>
        void AutoComplete(System.Windows.Forms.TextBox textBox);

        /// <summary>
        /// Trả về giá trị của mặt hàng thông qua <paramref name="tenhang"/>
        /// </summary>
        /// <param name="tenhang"></param>
        /// <returns></returns>
        object LayDonGia(string tenhang);

        /// <summary>
        /// Trả về mảng các chuỗi <see cref="string"/> làm giá trị cho datasource của <see cref="ComboBox"/>
        /// </summary>
        /// <returns></returns>
        string[] DataSourceForCombobox();

        /// <summary>
        /// Thêm một khách hàng vào cơ sở dữ liệu
        /// Trả về true nếu thành công ,flase nếu thất bại 
        /// Dữ liệu truyền vào theo thứ tự MaKH,TenKH,SDT,GioiTinh,DiaChi,LoaiKhachHang
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        bool InsertKhachHang(object[] values);
    }
}
