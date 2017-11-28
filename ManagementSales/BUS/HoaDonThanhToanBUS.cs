using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSales.BUS
{
    public class HoaDonThanhToanBUS : IHoaDonThanhToanBUS
    {
        private IDataProvider dataProvider;

        public HoaDonThanhToanBUS(IDataProvider data)
        {
            this.dataProvider = data;
        }



        /// <summary>
        /// trả về chuổi <see cref="string"/> có thể lấy làm dữ liệu cho các gợi ý
        /// </summary>
        /// <returns></returns>
        public string[] SourceComplete(string query, object[] values = null)
        {
            var data = dataProvider.ExecuteQuery(query, values);
            var source = data.AsEnumerable().ToList().Select(p => p.ItemArray).Select(p => p.FirstOrDefault()).OfType<String>().ToArray();
            return source;
        }

        /// <summary>
        /// Trả về chuỗi <see cref="AutoCompleteStringCollection"/> phục vụ cho chức năng AutoComplete của TextBox
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public AutoCompleteStringCollection SourceForAutoComplete(string query, object[] values = null)
        {
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(SourceComplete(query, values));
            return autoCompleteStringCollection;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gán giá trị cho phần <see cref="AutoCompleteSource"/>  của <see cref="TextBox"/> 
        /// </summary>
        /// <param name="textBox"></param>
        public void AutoComplete(TextBox textBox)
        {
            try
            {
                textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = SourceForAutoComplete("SELECT kh.TenKH FROM dbo.KhachHang kh");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Trả về giá trị của mặt hàng thông qua <paramref name="tenhang"/>
        /// </summary>
        /// <param name="tenhang"></param>
        /// <returns></returns>
        public object LayDonGia(string tenhang)
        {
            try
            {
                var donGia = dataProvider.ExecuteScalar("EXECUTE USP_GetDonGia @name", new object[] { tenhang });
                return donGia;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// trả về mảng các chuỗi <see cref="string"/> làm giá trị cho datasource của <see cref="ComboBox"/>
        /// </summary>
        /// <returns></returns>
        public string[] DataSourceForCombobox()
        {
            try
            {
                var source = SourceComplete("SELECT h.TenHang FROM dbo.Hang h WHERE h.SoLuong>0");
                return source;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Thêm một khách hàng vào cơ sở dữ liệu
        /// Trả về true nếu thành công ,flase nếu thất bại 
        /// Dữ liệu truyền vào theo thứ tự TenKH,SDT,GioiTinh,DiaChi,LoaiKhachHang
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool InsertKhachHang(object[] values)
        {
            try
            {
                return dataProvider.ExecuteNonQuery("EXEC USP_InsertKhachHang @name , @phone , @sex , @address , @level", values) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về true khi chèn dữ liệu thành công ,chen dữ liệu vào bảng ChiTietHoaDon
        /// Dữ liêu chen vào phải theo thứ tự MaHoaDon,MaHang,DonGia,SoLuong
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool InsertChiTietHoaDon(object[] values = null)
        {
            try
            {
                return dataProvider.ExecuteNonQuery("EXEC USP_InsertChiTietHoaDon @idbill , @idgoods , @price , @number ", values) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }//throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về MaHang theo TenHang
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        public object GetMaHang(string tenHang)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXECUTE USP_GetMaHang @name", new object[] { tenHang });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chèn dữ liệu HoaDon vào database
        /// Với các giá trị theo thứ tự MaKH,LoaiHoaDon,MaNV, NgayLap,NguoiLap
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool InsertHoaDon(object[] values)
        {
            try
            {
                return dataProvider.ExecuteNonQuery("EXEC USP_InsertHoaDon   @idcustomer , @typebill , @idstaff , @date , @namestaff", values) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trả về MaKH theo SDT
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        public object GetMaKH(string sdtKH)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXECUTE USP_GetMaKHBySDT  @sdt", new object[] { int.Parse(sdtKH) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về TenKhachHang dựa vào SDT
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        public object GetTenKH(string sdt)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXECUTE USP_GetTenKH @sdt", new object[] { int.Parse(sdt) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trả về giá trị MaHoaDon
        /// </summary>
        /// <param name="MaKH"></param>
        /// <param name="LoaiHD"></param>
        /// <param name="MaNV"></param>
        /// <param name="NgayLap"></param>
        /// <param name="TenNV"></param>
        /// <returns></returns>
        public object GetMaHoaDon(int MaKH, string LoaiHD, int MaNV, string NgayLap, string TenNV)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXECUTE USP_GetMaHoaDon @idcustomer , @type , @idstaff , @date , @namestaff ", new object[] { MaKH, LoaiHD, MaNV, NgayLap, TenNV });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cập nhật số lượng hàng trong kho theo mả hàng
        /// Theo thứ tự MaHang , Soluong mới 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool UpdateHangHoa(object[] values = null)
        {
            try
            {
                return dataProvider.ExecuteNonQuery("EXECUTE USP_UpdateSoLuongHang @idgoods , @number ", values) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Lấy số lượng hàng dựa vào mã hàng
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public object GetSoLuong(object[] values = null)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXECUTE USP_GetSoLuong @idgoods ", values);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //throw new NotImplementedException();
        }
    }
}
