using ManagementSales.BUS.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSales.DTO;
using System.Data;
using ManagementSales.DAO;
using Microsoft.Office.Interop.Excel;
using AutoMapper;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ManagementSales.BUS
{
    public class QuanLyThongTinBUS : IQuanLyThongTinBUS
    {
        private IDataProvider dataProvider;

        public QuanLyThongTinBUS(IDataProvider data)
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
            //throw new NotImplementedException();
        }

        /// <summary>
        /// trả về mảng các chuỗi <see cref="string"/> làm giá trị cho datasource của <see cref="System.Windows.Forms.ComboBox"/>
        /// </summary>
        /// <returns></returns>
        public string[] DataSourceForCombobox()
        {
            var source = SourceComplete("SELECT h.TenHang FROM dbo.Hang h WHERE h.SoLuong>0");
            return source;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về danh sách các dòng trong bảng ChiTietHoaDon
        /// </summary>
        /// <returns></returns>
        public List<ChiTietHoaDonDTO> GetListChiTietHoaDon()
        {
            List<ChiTietHoaDonDTO> list = new List<ChiTietHoaDonDTO>();
            var data = dataProvider.ExecuteQuery("SELECT cthd.* FROM dbo.ChiTietHoaDon cthd");
            foreach (DataRow item in data.Rows)
            {
                ChiTietHoaDonDTO hang = new ChiTietHoaDonDTO(item);
                list.Add(hang);
            }
            return list;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về MaKhach theo SDT
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        public object GetMaKH(string sdtKH)
        {
            return dataProvider.ExecuteScalar("EXECUTE USP_GetMaKHBySDT  @sdt", new object[] { int.Parse(sdtKH) });
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về MaHang theo TenHang
        /// </summary>
        /// <param name="tenKH"></param>
        /// <returns></returns>
        public object GetMaHang(string tenHang)
        {
            return dataProvider.ExecuteScalar("EXECUTE USP_GetMaHang @name", new object[] { tenHang });
        }

        /// <summary>
        /// Trả về tên Khách hàng thông qua SDT
        /// </summary>
        /// <param name="sdtKH"></param>
        /// <returns></returns>
        public object GetTenKH(string sdtKH)
        {
            return dataProvider.ExecuteScalar("EXECUTE USP_GetTenKH @sdt", new object[] { int.Parse(sdtKH) });
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về giá trị của mặt hàng thông qua <paramref name="tenhang"/>
        /// </summary>
        /// <param name="tenhang"></param>
        /// <returns></returns>
        public object LayDonGia(string tenhang)
        {
            var donGia = dataProvider.ExecuteScalar("EXECUTE USP_GetDonGia @name", new object[] { tenhang });
            return donGia;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về kết quả sau khi tìm kiếm theo yêu cầu
        /// </summary>
        /// <param name="values"></param>
        /// <param name="correct"></param>
        /// <returns></returns>
        public List<HangDTO> SearchBy(object values, bool correct)
        {
            List<HangDTO> listHang = new List<HangDTO>();
            var str = correct == true ? values.ToString() : $"%{values.ToString()}%";
            var data = dataProvider.ExecuteQuery("USP_SearchHang @name", new object[] { str });
            foreach (DataRow item in data.Rows)
            {
                HangDTO hang = new HangDTO(item);
                listHang.Add(hang);
            }
            return listHang;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Hiển thị tất cả dữ liệu về hàng
        /// </summary>
        /// <returns></returns>
        public List<HangDTO> ShowAllHang()
        {
            List<HangDTO> listHang = new List<HangDTO>();
            var data = dataProvider.ExecuteQuery("SELECT h.* FROM dbo.Hang h");
            foreach (DataRow item in data.Rows)
            {
                HangDTO hang = new HangDTO(item);
                listHang.Add(hang);
            }
            return listHang;
            //throw new NotImplementedException();
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
        /// Sắp xếp tăng giảm theo yêu cầu
        /// </summary>
        /// <param name="values"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public List<HangDTO> SortBy(object[] values, bool asc)
        {
            List<HangDTO> listHang = new List<HangDTO>();
            string query = "SELECT * FROM dbo.Hang ORDER BY ";
            string type = asc == true ? " ASC" : " DESC";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(query);
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].Equals(""))
                {
                    continue;
                }
                if (i != 0 && query.Length != stringBuilder.Length)
                {
                    stringBuilder.Append(" , ");
                }
                stringBuilder.Append(values[i].ToString());
                stringBuilder.Append(type);
            }
            var data = dataProvider.ExecuteQuery(stringBuilder.ToString());
            foreach (DataRow item in data.Rows)
            {
                HangDTO hang = new HangDTO(item);
                listHang.Add(hang);
            }
            return listHang;
        }

        /// <summary>
        /// Lấy thông tin khách hàng dựa vào số điện thoại
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        public KhachHangDTO GetKhachHangBySDT(int sdt)
        {
            var data = dataProvider.ExecuteQuery("USP_GetKHBySDT @SDT", new object[] { sdt });
            KhachHangDTO khachHang = new KhachHangDTO(data.Rows.OfType<DataRow>().Single());
            return khachHang;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy ra Mã Hàng và Số lượng trong database và lưu lại dữ liệu trong dạng <see cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetMaHangAndSoLuong()
        {
            var data = dataProvider.ExecuteQuery("SELECT h.* FROM dbo.Hang h");
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataRow, HangDTO>().ForMember(x => x.StrMaHang, p => p.MapFrom(s => s["MaHang"].ToString()))
.ForMember(x => x.IntSoLuong, p => p.MapFrom(s => int.Parse(s["SoLuong"].ToString())));
            });
            IMapper iMapper = config.CreateMapper();
            var map = iMapper.Map<List<DataRow>, List<HangDTO>>(data.Rows.OfType<DataRow>().ToList());
            var dictionary = map.ToDictionary(x => x.StrMaHang, p => p.IntSoLuong);
            return dictionary;
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
        /// Thực hiện nhập xuất hàng
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="plus"><see cref="true"/> thì là nhập hàng ,<see cref="false"/> là xuất hàng</param>
        /// <returns></returns>
        public string NhapXuatHang(DataGridView dataGridView,bool plus,object maHD)
        {
            var data = TransDataGridViewToDictionary(dataGridView,plus);
            var dic = GetMaHangAndSoLuong();
            string mess = "";
            foreach (var item in data.Item1)
            {
                var para = new object[] {item.Key.ToString(),item.Value.ToString() };
                bool checkUpdate = dataProvider.ExecuteNonQuery("USP_UpdateSoLuongHang @id , @number ",para) >0;
                var donGia = dataProvider.ExecuteScalar("USP_GetDonGiaByMH @id", new object[] {item.Key.ToString() });

                var soLuong = dataProvider.ExecuteScalar("USP_GetSoLuong  @id", new object[] { item.Key.ToString() });
                var values = new object[] { maHD, item.Key, donGia ,int.Parse( soLuong.ToString())-dic[item.Key.ToString()]};
                //var values = new object[] { maHD, listData[0]..ToString() };
                if (checkUpdate)
                {
                    dataProvider.ExecuteNonQuery("EXEC USP_InsertChiTietHoaDon @idbill , @idgoods , @price , @number ", values);
                }
                mess = checkUpdate ?   "":$"{item.Key}"+mess.ToString();
            }
            data.Item2.ForEach(x => 
            {
                var para = new object[] {x.StrMaHang,x.StrTenHang,x.FltDonGia,x.IntSoLuong,x.StrGhiChu };
                bool checkInsert=  dataProvider.ExecuteNonQuery("USP_InsertHang @id , @name , @price , @number , @notice ", para)>0;
                //var soLuong = dataProvider.ExecuteScalar("USP_GetSoLuong  @id", new object[] { x.StrMaHang });

                var values = new object[] { maHD, para[0], para[2], int.Parse(para[3].ToString()) };
                if (checkInsert)
                {
                    dataProvider.ExecuteNonQuery("EXEC USP_InsertChiTietHoaDon @idbill , @idgoods , @price , @number ", values) ;
                }
                mess = checkInsert ? "" : $"{x.StrMaHang}" + mess.ToString();
            });

            return mess;
        }

        /// <summary>
        /// Trả về danh sách 
        /// Trong đó <see cref="Dictionary{TKey, TValue}"/> là hàng cũ được bổ sung
        /// Còn <see cref="List{DTO.HangDTO}"/> là hàng mới nhập vào
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="Plus"><see cref="true"/> thì là nhập hàng ,<see cref="false"/> là xuất hàng</param>
        /// <returns></returns>
        public Tuple< Dictionary<string, int>,List<HangDTO>> TransDataGridViewToDictionary(DataGridView dataGridView,bool Plus)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataGridViewRow, HangDTO>().ForMember(x => x.StrMaHang, p => p.MapFrom(s => s.Cells["Mã Hàng"].Value.ToString()))
                .ForMember(x => x.StrTenHang, p => p.MapFrom(s => s.Cells["Tên Hàng"].Value.ToString()))
                .ForMember(x => x.FltDonGia, p => p.MapFrom(s => float.Parse(s.Cells["Đơn Giá"].Value.ToString())))
                .ForMember(x => x.StrGhiChu, p => p.MapFrom(s => s.Cells["Ghi Chú"].Value ?? "null"))
                .ForMember(x => x.IntSoLuong, p => p.MapFrom(s => int.Parse(s.Cells["Số Lượng"].Value.ToString())));
            });

            //((DataTable)dataGridView.DataSource).RowsOfType<DataRow>().Select(p => p.ItemArray).ToList()[1]
            var config2 = new MapperConfiguration(cfg => {
                cfg.CreateMap<object[], HangDTO>().ForMember(x => x.StrMaHang, p => p.MapFrom(s => s[0].ToString()))
               .ForMember(x => x.StrTenHang, p => p.MapFrom(s => s[1].ToString()))
               .ForMember(x => x.FltDonGia, p => p.MapFrom(s =>float.Parse( s[2].ToString())))
               .ForMember(x => x.IntSoLuong, p => p.MapFrom(s =>int.Parse( s[3].ToString())))
               .ForMember(x => x.StrGhiChu, p => p.MapFrom(s => s[1].ToString()));

            });
            List<HangDTO> listData;
            IMapper iMapper;
            var count= dataGridView.RowCount == 0;
            if (count)
            {
                iMapper = config2.CreateMapper();
                listData = iMapper.Map<List<object[]>, List<HangDTO>>(((System.Data.DataTable)dataGridView.DataSource).Rows.OfType<DataRow>().Select(p => p.ItemArray).ToList());
            }
            else
            {
                iMapper = config.CreateMapper();
                listData = iMapper.Map<List<DataGridViewRow>, List<HangDTO>>(dataGridView.Rows.OfType<DataGridViewRow>().ToList());
            }
            var dic = GetMaHangAndSoLuong();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<HangDTO> list = new List<HangDTO>();
            listData.ForEach(x =>
            {
                if (String.IsNullOrEmpty(x.StrMaHang))
                {

                }
                else
                {
                    if (dic.ContainsKey(x.StrMaHang.Trim()))
                    {
                        var old = dic[x.StrMaHang.Trim()];
                        //dic.Remove(x.StrMaHang);
                        var number = Plus ? old + x.IntSoLuong : old - x.IntSoLuong;
                        dictionary.Add(x.StrMaHang, number);
                    }
                    else
                    {
                        list.Add(x);
                        //dic.Add(x.StrMaHang, x.IntSoLuong);
                    }
                }
            });
            return new Tuple<Dictionary<string, int>, List<HangDTO>>( dictionary,list);
        }
        #region Đọc file excel

        /// <summary>
        /// Lấy ra WorkBook của file excel
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Workbooks GetWorkBooks(_Application application)
        {
            Workbooks workbooks = application.Workbooks;
            return workbooks;
        }

        /// <summary>
        /// Lấy ra một sheet trong excel 
        /// Mặc định là sheet đầu tiên
        /// </summary>
        /// <param name="workbooks"></param>
        /// <param name="address"></param>
        /// <param name="i">Mặc định là 1</param>
        /// <returns></returns>
        public Worksheet GetWorkSheet(Workbooks workbooks, string address, int i = 1)
        {
            

            var resultworkbooks = workbooks.Open(address).Worksheets[i];
            return resultworkbooks;
        }

        /// <summary>
        /// Lấy ra vùng giá trị trong worksheet
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public Range GetRange(Worksheet worksheet)
        {
            var range = worksheet.UsedRange;
            return range;
        }

        /// <summary>
        /// Sử dụng hàm <see cref="GetRange(Worksheet)"/> để lấy ra các vùng giá trị trước
        /// Từ vùng giá trị lấy ra các vùng giá trị nhỏ hơn ( các ô )
        /// </summary>
        /// <param name="range"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Range GetRangeValueWithIndex(Range range, int i, int j)
        {
            Range result = range[i, j];
            return result;
        }

        /// <summary>
        /// Sử dụng hàm <see cref="GetRangeValueWithIndex(Range, int, int)"/> trước để lấy ra các ô giá trị.
        /// Từ các ô giá trị ta lấy ra các giá trị của ô đó dưới dạng <see cref="String"/>
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public string GetValueOfRange(Range range)
        {
            object value = range.Value2 == null ? "null" : range.Value2; ;
            return value.ToString();
        }

        /// <summary>
        /// Xử lý bất đồng bộ hàm ReadWithInteropExcel phục vụ cho việc song song
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Task<List<Tuple<string, string, string, string, string>>> ReadAsync(_Application application, string address)
        {
            return Task.Factory.StartNew(() => ReadWithInteropExcel(application, address));
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Đọc file excel và lấy 4 trường dữ liệu ra 
        /// 4 trường dữ liệu ở đây bao gồm MaHang ,TenHang ,DonGia ,SoLuong ,GhiChu
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Tuple<string, string, string, string, string>> ReadWithInteropExcel(_Application application, string address)
        {
            object row;
            object row2;
            object row3;
            object row4;
            object row5;
            List<Tuple<string, string, string, string, string>> list = new List<Tuple<string, string, string, string, string>>();
            var workBooks = GetWorkBooks(application);
            var workSheet = GetWorkSheet(workBooks, address);
            var range = GetRange(workSheet);
            int countRow = range.Rows.Count;
            int countColumn = range.Columns.Count;
            for (int i = 2; i <= countRow; i++)
            {
                for (int j = 1; j <= countColumn; j++)
                {
                    var value1 = GetRangeValueWithIndex(range, i, j);
                    row = GetValueOfRange(value1); //worksheet.Cells[i, j].Value2==null?"a":"b";
                    j++;
                    var value2 = GetRangeValueWithIndex(range, i, j);
                    row2 = GetValueOfRange(value2);// worksheet.Cells[i, j].Value2;

                    j++;
                    var value3 = GetRangeValueWithIndex(range, i, j);
                    row3 = GetValueOfRange(value3);
                    j++;
                    var value4 = GetRangeValueWithIndex(range, i, j);
                    row4 = GetValueOfRange(value4);
                    j++;
                    var value5 = GetRangeValueWithIndex(range, i, j);
                    row5 = GetValueOfRange(value5);

                    list.Add(new Tuple<string, string, string, string, string>(row.ToString(), row2.ToString(), row3.ToString(), row4.ToString(), row5.ToString()));

                }
            }

            workBooks.Close();
            return new List<Tuple<string, string, string, string, string>>(list);
            //throw new NotImplementedException();
        }
        #endregion
    }
}
