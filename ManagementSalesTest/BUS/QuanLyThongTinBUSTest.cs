using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System.Data;
using FizzWare.NBuilder;
using ManagementSales.DTO;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using AutoMapper;
using System.Windows.Forms;

namespace QuanLyBanHangTest.BUS
{
    [TestFixture]
    class QuanLyThongTinBUSTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        private QuanLyThongTinBUS quanLyThongTinBUS;
        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            quanLyThongTinBUS = new QuanLyThongTinBUS(mockIDataProvider.Object);
        }


        private System.Data.DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new System.Data.DataTable(typeof(T).Name);
            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name.Remove(0, 3)));
            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            return datatable;
        }

        

        [Test]
        public void ClassTest()
        {
            var result = typeof(QuanLyThongTinBUS).GetInterfaces().Contains(typeof(IQuanLyThongTinBUS));
            var result1 = typeof(QuanLyThongTinBUS).IsPublic;
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }

        [Test]
        public void InsertHDTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(1);
            var result = quanLyThongTinBUS.InsertHoaDon(new object[] { "a" });
            Assert.NotNull(result);
        }

        [Test]
        public void InsertHDTestException()
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => quanLyThongTinBUS.InsertHoaDon(new object[] {"a" }));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetMaHDTestException()
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => quanLyThongTinBUS.GetMaHoaDon(1, "a", 1, "a", "a"));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }


        [TestCase(1,"a",1,"a","a")]
        public void GetMaHoaDonTest(int MaKH, string LoaiHD, int MaNV, string NgayLap, string TenNV)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns("");
            var result = quanLyThongTinBUS.GetMaHoaDon(MaKH, LoaiHD, MaNV, NgayLap, TenNV);
            Assert.NotNull(result);
        }

        [Test]
        public void GetMaHangAndSoLuongTest()
        {
            var data = GenerateDataTable<HangDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            var result = quanLyThongTinBUS.GetMaHangAndSoLuong();
            Assert.NotNull(result);

        }

        [Test]
        public void TransDataGridViewToDictionaryTest()
        {
            var datatable = new System.Data.DataTable(typeof(HangDTO).Name);
            datatable.Columns.Add("Mã Hàng");
            datatable.Columns.Add("Tên Hàng");
            datatable.Columns.Add("Đơn Giá");
            datatable.Columns.Add("Số Lượng");
            datatable.Columns.Add("Ghi Chú");
            Builder<HangDTO>.CreateListOfSize(10).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            //return datatable;
            System.Windows.Forms.DataGridView dataGridView = new System.Windows.Forms.DataGridView();
            datatable.AcceptChanges();

            dataGridView.DataSource = datatable;

            var listHang = Builder<HangDTO>.CreateListOfSize(10).Build();
            List<HangDTO> list = new List<HangDTO>(listHang);


            var data = GenerateDataTable<HangDTO>(10);

            var datatable2 = new System.Data.DataTable(typeof(HangDTO).Name);

            var data2 = GenerateDataTable<HangDTO>(12);

            //var mockIMapper = new Mock<IMapper>();
            //mockIMapper.Setup(x => x.Map<List<DataGridViewRow>, List<HangDTO>>(It.IsNotNull<List<DataGridViewRow>>())).Returns(list);// dataGridView.Rows.OfType<DataGridViewRow>().ToList())).Returns(list);

            System.Windows.Forms.DataGridView dataGridView1 = new System.Windows.Forms.DataGridView
            {
                ColumnCount = 5
            };
            dataGridView1.Columns[0].Name = "Mã Hàng";
            dataGridView1.Columns[1].Name = "Tên Hàng";
            dataGridView1.Columns[2].Name = "Đơn Giá";
            dataGridView1.Columns[3].Name = "Ghi Chú";
            dataGridView1.Columns[4].Name = "Số Lượng";


            string[] row = new string[] { "1", "Product 1", "1000", "", "50" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000", "", "20" };
            dataGridView1.Rows.Add(row);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(1);
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(10);
            var result1 = quanLyThongTinBUS.TransDataGridViewToDictionary(dataGridView1, true);//,mockIMapper.Object);
            dataGridView.DataSource = data;
            var result2 = quanLyThongTinBUS.TransDataGridViewToDictionary(dataGridView, false);//,mockIMapper.Object);
            dataGridView.DataSource = data2;
            var result = quanLyThongTinBUS.NhapXuatHang(dataGridView, true,"jh");
            var result3 = quanLyThongTinBUS.TransDataGridViewToDictionary(dataGridView, false);
            dataGridView.DataSource = datatable2;
            var result4 = quanLyThongTinBUS.TransDataGridViewToDictionary(dataGridView, true);
            

           // Assert.IsNotNull(result);
            Assert.IsNotEmpty(result1.Item2);
            Assert.IsEmpty(result1.Item1);
            Assert.IsNotEmpty(result2.Item1);
            Assert.IsEmpty(result2.Item2);
            Assert.IsNotEmpty(result3.Item1);
            Assert.IsNotEmpty(result3.Item2);
            Assert.IsEmpty(result4.Item1);
            Assert.IsEmpty(result4.Item2);
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void DataSourceForComboboxTest()
        {

            var data = GenerateDataTable<HoaDonDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);

            var result = quanLyThongTinBUS.DataSourceForCombobox();
            var resultExcept = data.AsEnumerable().ToList().Select(p => p.ItemArray).Select(p => p.FirstOrDefault()).OfType<String>().ToArray();
            Assert.IsTrue(result.Count() == resultExcept.Count());
            mockIDataProvider.VerifyAll();
        }

        [TestCase("query", null)]
        public void SourceCompleteTest(string query, object[] values)
        {
            var data = GenerateDataTable<HangDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(query, values)).Returns(data);
            var result = quanLyThongTinBUS.SourceComplete(query, values);
            var resultExcept = data.AsEnumerable().ToList().Select(p => p.ItemArray).Select(p => p.FirstOrDefault()).OfType<String>().ToArray();
            Assert.IsTrue(result.Count() == resultExcept.Count());
            mockIDataProvider.VerifyAll();
        }


        [Test]
        public void GetListChiTietHoaDon()
        {

            var data = GenerateDataTable<ChiTietHoaDonDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            var expectResult = new List<ChiTietHoaDonDTO>();
            foreach (DataRow item in data.Rows)
            {
                ChiTietHoaDonDTO hang = new ChiTietHoaDonDTO(item);
                expectResult.Add(hang);
            }
            bool acc = false;
            var result = quanLyThongTinBUS.GetListChiTietHoaDon();
            for (int i = 0; i < result.LongCount(); i++)
            {
                acc = result[i].FltDonGia.Equals(expectResult[i].FltDonGia) &&
                    result[i].IntMaHoaDon.Equals(expectResult[i].IntMaHoaDon) &&
                    result[i].IntSoLuong.Equals(expectResult[i].IntSoLuong) &&
                    result[i].StrMaHang.Equals(expectResult[i].StrMaHang);
            }
            //var cc= result.SequenceEqual(expectResult);
            //var first = result.Except(expectResult).ToList();
            //var second = expectResult.Except(result).ToList();
            Assert.IsTrue(acc);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("name", "result")]
        [TestCase("name", "result")]
        public void GetMaHangTest(string name, object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(value);
            var result = quanLyThongTinBUS.GetMaHang(name);
            Assert.That(result.Equals(value));
            mockIDataProvider.VerifyAll();
        }


        [TestCase("123", "result")]
        [TestCase("1234", "result")]
        public void GetTenKHTest(string name, object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(name) })).Returns(value);
            var result = quanLyThongTinBUS.GetTenKH(name);
            Assert.That(result.Equals(value));
            mockIDataProvider.VerifyAll();
        }


        [TestCase("123", "result")]
        [TestCase("1234", "result")]
        public void GetMaKHTest(string name, object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(name) })).Returns(value);
            var result = quanLyThongTinBUS.GetMaKH(name);
            Assert.That(result.Equals(value));
            mockIDataProvider.VerifyAll();
        }

        [TestCase("name", "result")]
        [TestCase("name", "result")]
        public void LayDonGiaTest(string name, object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(value);
            var result = quanLyThongTinBUS.LayDonGia(name);
            Assert.That(result.Equals(value));
            mockIDataProvider.VerifyAll();
        }

        [TestCase("name", true)]
        [TestCase("name1", false)]
        public void SearchByTest(object values, bool correct)
        {
            var data = GenerateDataTable<HangDTO>(10);
            var str = correct == true ? values.ToString() : $"%{values.ToString()}%";
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), new object[] { str })).Returns(data);
            var result = quanLyThongTinBUS.SearchBy(values, correct);
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void ShowAllHangTest()
        {
            var data = GenerateDataTable<HangDTO>(10);
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            var result = quanLyThongTinBUS.ShowAllHang();
            bool isTrue = false;
            var expectResult = new List<HangDTO>();
            foreach (DataRow item in data.Rows)
            {
                HangDTO hang = new HangDTO(item);
                expectResult.Add(hang);
            }
            for (int i = 0; i < result.LongCount(); i++)
            {
                isTrue = result[i].StrTenHang.Equals(expectResult[i].StrTenHang) &&
                    result[i].FltDonGia.Equals(expectResult[i].FltDonGia) &&
                    result[i].StrGhiChu.Equals(expectResult[i].StrGhiChu) &&
                    result[i].IntSoLuong.Equals(expectResult[i].IntSoLuong) &&
                    result[i].StrMaHang.Equals(expectResult[i].StrMaHang);
            }
            Assert.IsTrue(isTrue);
            mockIDataProvider.VerifyAll();
        }

        [TestCase(new object[] { "namea , ", "", " ", " , " }, false)]
        [TestCase(new object[] { "name" }, true)]
        public void SortBy(object[] values, bool asc)
        {

            var data = GenerateDataTable<HangDTO>(10);
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            var result = quanLyThongTinBUS.SortBy(values, asc);
            bool isTrue = false;
            var expectResult = new List<HangDTO>();
            foreach (DataRow item in data.Rows)
            {
                HangDTO hang = new HangDTO(item);
                expectResult.Add(hang);
            }
            for (int i = 0; i < result.LongCount(); i++)
            {
                isTrue = result[i].StrTenHang.Equals(expectResult[i].StrTenHang) &&
                    result[i].FltDonGia.Equals(expectResult[i].FltDonGia) &&
                    result[i].StrGhiChu.Equals(expectResult[i].StrGhiChu) &&
                    result[i].IntSoLuong.Equals(expectResult[i].IntSoLuong) &&
                    result[i].StrMaHang.Equals(expectResult[i].StrMaHang);
            }
            Assert.IsTrue(isTrue);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("a", 5, 5)]
        [TestCase("b", 0, 0)]
        public void AllMethodForReadExcelTest(string a, int i, int j)
        {
            Mock<Excel.Application> mock_applcation = new Mock<Excel.Application>();
            Mock<Excel.Worksheet> mockSheet = new Mock<Excel.Worksheet>();
            Mock<Workbooks> mockWorkbooks = new Mock<Workbooks>();
            Mock<Workbook> mockWorkbook = new Mock<Workbook>();
            var mockRange = new Mock<Excel.Range>();

            mockWorkbooks.Setup(x => x.Close());
            mockRange.Setup(x => x[It.IsNotNull<int>(), It.IsNotNull<int>()]).Returns(mockRange.Object);
            mockRange.Setup(x => x.Count).Returns(4);
            mockRange.Setup(x => x.Columns).Returns(mockRange.Object);
            mockRange.Setup(x => x.Rows).Returns(mockRange.Object);
            mockRange.Setup(x => x.Value2).Returns("");
            mockSheet.Setup(x => x.UsedRange).Returns(mockRange.Object);
            mock_applcation.Setup(x => x.Workbooks).Returns(mockWorkbooks.Object);
            mockWorkbooks.Setup(x => x.Open(It.IsNotNull<string>(), null, null, null, null, null, null, null, null, null, null, null, null, null, null).Worksheets[1]).Returns(mockSheet.Object);


            var resultWorkbooks = quanLyThongTinBUS.GetWorkBooks(mock_applcation.Object);
            var resultWorksheet = quanLyThongTinBUS.GetWorkSheet(mockWorkbooks.Object, "a");
            var reslut = quanLyThongTinBUS.ReadWithInteropExcel(mock_applcation.Object, "a");
            var resultRange = quanLyThongTinBUS.GetRange(mockSheet.Object);
            var RangeValue = quanLyThongTinBUS.GetRangeValueWithIndex(mockRange.Object, 4, 4);
            var resultRanges = quanLyThongTinBUS.GetValueOfRange(mockRange.Object);

            Assert.NotNull(quanLyThongTinBUS.ReadAsync(mock_applcation.Object, "a"));
            Assert.NotNull(resultRange);
            Assert.NotNull(resultWorkbooks);
            Assert.NotNull(resultWorksheet);
            Assert.NotNull(RangeValue);
            Assert.NotNull(resultRanges);

            mockRange.VerifyAll();
            mockSheet.VerifyAll();
            mockWorkbook.VerifyAll();
            mock_applcation.VerifyAll();
        }

        [TestCase(123)]
        [TestCase(456)]
        public void GetKhachHangBySDTTest(int sdt)
        {
            var data = GenerateDataTable<KhachHangDTO>(1);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), new object[] { sdt })).Returns(data);
            var result = quanLyThongTinBUS.GetKhachHangBySDT(sdt);
            KhachHangDTO khachHang = new KhachHangDTO(data.Rows.OfType<DataRow>().Single());
            Assert.IsTrue(result.BlnGioiTinh.Equals(khachHang.BlnGioiTinh) &&
                result.IntSDT.Equals(khachHang.IntSDT) &&
                result.StrDiaChi.Equals(khachHang.StrDiaChi) &&
                result.StrLoaiKhachHang.Equals(khachHang.StrLoaiKhachHang) &&
                result.StrMaKH.Equals(khachHang.StrMaKH) &&
                result.StrTenKH.Equals(khachHang.StrTenKH));
            mockIDataProvider.VerifyAll();
        }
    }
}
