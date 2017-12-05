using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ManagementSales.BUS.Interfaces
{
    public interface IQuanLyThongTinBUS
    {
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
        /// Chèn dữ liệu HoaDon vào database
        /// Với các giá trị theo thứ tự MaKH,LoaiHoaDon,MaNV, NgayLap,NguoiLap
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        bool InsertHoaDon(object[] values);

        /// <summary>
        /// Lấy khách hàng theo số điện thoại
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        DTO.KhachHangDTO GetKhachHangBySDT(int sdt);

        /// <summary>
        /// Trả về giá trị của mặt hàng thông qua <paramref name="tenhang"/>
        /// </summary>
        /// <param name="tenhang"></param>
        /// <returns></returns>
        object LayDonGia(string tenhang);

        /// <summary>
        /// Trả về MaHang theo TenHang
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        object GetMaHang(string tenHang);

        /// <summary>
        /// trả về mảng các chuỗi <see cref="string"/> làm giá trị cho datasource của <see cref="ComboBox"/>
        /// </summary>
        /// <returns></returns>
        string[] DataSourceForCombobox();
        /// <summary>
        ///Trả về Tên khách hành theo sdt 
        /// </summary>
        /// <param name="sdtKH"></param>
        /// <returns></returns>
        object GetTenKH(string sdtKH);

        /// <summary>
        /// Trả về MaKhach theo SDT
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        object GetMaKH(string sdtKH);

        /// <summary>
        /// Trả về danh sách ChiTietHoaDon
        /// </summary>
        /// <returns></returns>
        List<DTO.ChiTietHoaDonDTO> GetListChiTietHoaDon();
        
        /// <summary>
        /// Trả về kết quả sau khi tìm kiếm theo yêu cầu
        /// </summary>
        /// <param name="values"></param>
        /// <param name="correct"></param>
        /// <returns></returns>
        List<ManagementSales.DTO.HangDTO> SearchBy(object values, bool correct);

        /// <summary>
        /// Sắp xếp tăng giảm theo yêu cầu
        /// </summary>
        /// <param name="values"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        List<DTO.HangDTO> SortBy(object[] values, bool asc);

        /// <summary>
        /// Hiển thị tất cả dữ liệu về hàng
        /// </summary>
        /// <returns></returns>
        List<DTO.HangDTO> ShowAllHang();

        /// <summary>
        /// Lấy ra Mã Hàng và Số lượng trong database và lưu lại dữ liệu trong dạng <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetMaHangAndSoLuong();

        /// <summary>
        /// Trả về danh sách 
        /// Trong đó <see cref="Dictionary{TKey, TValue}"/> là hàng cũ được bổ sung
        /// Còn <see cref="List{DTO.HangDTO}"/> là hàng mới nhập vào
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="Plus"><see cref="true"/> thì là nhập hàng ,<see cref="false"/> là xuất hàng</param>
        /// <returns></returns>
        Tuple<Dictionary<string, int>, List<DTO.HangDTO>> TransDataGridViewToDictionary(DataGridView dataGridView,bool Plus);

        /// <summary>
        /// Thực hiện nhập xuất hàng
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="plus"><see cref="true"/> thì là nhập hàng ,<see cref="false"/> là xuất hàng</param>
        /// <returns></returns>
        string NhapXuatHang(DataGridView dataGridView, bool plus,object maHD);

        /// <summary>
        /// Xử lý bất đồng bộ hàm ReadWithInteropExcel phục vụ cho việc song song
        /// </summary>
        /// <param name="address"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        Task<List<Tuple<string, string, string, string, string>>> ReadAsync(_Application application, string address);

        /// <summary>
        /// Đọc file excel và lấy 4 trường dữ liệu ra 
        /// 4 trường dữ liệu ở đây bao gồm MaHang ,TenHang ,DonGia ,SoLuong ,GhiChu
        /// </summary>
        /// <param name="address"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        List<Tuple<string, string, string, string, string>> ReadWithInteropExcel(_Application application, string address);

        /// <summary>
        /// Lấy ra vùng giá trị trong worksheet
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        Range GetRange(Worksheet worksheet);

        /// <summary>
        /// Lấy ra một sheet trong excel 
        /// Mặc định là sheet đầu tiên
        /// </summary>
        /// <param name="workbooks"></param>
        /// <param name="address"></param>
        /// <param name="i">Mặc định là 1</param>
        /// <returns></returns>
        Worksheet GetWorkSheet(Workbooks workbooks, string address,int i=1);

        /// <summary>
        /// Sử dụng hàm <see cref="GetRange(Worksheet)"/> để lấy ra các vùng giá trị trước
        /// Từ vùng giá trị lấy ra các vùng giá trị nhỏ hơn ( các ô )
        /// </summary>
        /// <param name="range"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        Range GetRangeValueWithIndex(Range range, int i, int j);

        /// <summary>
        /// Sử dụng hàm <see cref="GetRangeValueWithIndex(Range, int, int)"/> trước để lấy ra các ô giá trị.
        /// Từ các ô giá trị ta lấy ra các giá trị của ô đó dưới dạng <see cref="String"/>
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        string GetValueOfRange(Range range);

        /// <summary>
        /// Lấy ra WorkBook của file excel
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        Workbooks GetWorkBooks(_Application application);
    }
}
