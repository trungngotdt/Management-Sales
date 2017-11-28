using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Moq;
using NUnit.Framework;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System.Data;
using FizzWare.NBuilder;
using ManagementSales.DTO;

namespace QuanLyBanHangTest.BUS
{
    [TestFixture]
    class HoaDonThanhToanBUSTest
    {
        private HoaDonThanhToanBUS hoaDonThanhToanBUS;
        private Mock<IDataProvider> mockIDataProvider;

        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            hoaDonThanhToanBUS = new HoaDonThanhToanBUS(mockIDataProvider.Object);
        }

        [Test]
        public void ClassTest()
        {
            var result = typeof(HoaDonThanhToanBUS).GetInterfaces().Contains(typeof(IHoaDonThanhToanBUS));
            var result1 = typeof(HoaDonThanhToanBUS).IsPublic;
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }


        private DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new DataTable(typeof(T).Name);
            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name));
            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));
            return datatable;
        }

        [TestCase("query", null)]
        public void SourceForAutoCompleteTest(string query, object[] values)
        {
            var data = GenerateDataTable<ChiTietHoaDonDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(query, values)).Returns(data);
            hoaDonThanhToanBUS.SourceForAutoComplete(query, values);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("query",null)]
        public void SourceCompleteTest(string query, object[] values)
        {
            var data= GenerateDataTable<HangDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(query, values)).Returns(data);
            var result = hoaDonThanhToanBUS.SourceComplete(query, values);
            var resultExcept= data.AsEnumerable().ToList().Select(p => p.ItemArray).Select(p => p.FirstOrDefault()).OfType<String>().ToArray();
            Assert.IsTrue(result.Count() == resultExcept.Count());
        }
        
        [TestCase( null, "value1", "value2")]
        public void AutoCompleteTest( object[] values, string value1, string value2)
        {
            var array = new string[] { value1, value2 };
            var valueResult = new System.Windows.Forms.AutoCompleteStringCollection();
            

            var data = GenerateDataTable<HangDTO>(20);
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), values)).Returns(data);
            //var result = hoaDonThanhToanBUS.SourceComplete(query, values);

            valueResult.AddRange(array);
            //mockIDataProvider.Setup(x => x.SourceForAutoComplete(It.IsNotNull<string>(), null)).Returns(valueResult);
            hoaDonThanhToanBUS.AutoComplete(new System.Windows.Forms.TextBox());
            mockIDataProvider.VerifyAll();
        }

        [TestCase(null,"a")]
        public void AutoCompleTestException(object[] values,string a)
        {
            /*try
            {*/

                var data = GenerateDataTable<KhachHangDTO>(10);

                mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), values)).Throws(new Exception());
                //mockIDataProvider.Setup(x => x.SourceForAutoComplete(It.IsNotNull<string>(), null)).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.AutoComplete(new System.Windows.Forms.TextBox()));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.That(ex.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            }*/
        }
        

        [TestCase("name", "value")]
        [TestCase("name1", "value1")]
        public void LayDonGiaTest(string name, string value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(value);
            var result = hoaDonThanhToanBUS.LayDonGia(name);
            mockIDataProvider.VerifyAll();
            Assert.IsTrue(result.Equals(value));

        }

        [Test]
        public void LayDonGiaTestException()
        {
            /*try
            {*/
                mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { null })).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.LayDonGia(null));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
           /* }
            catch (Exception ex)
            {
                mockIDataProvider.VerifyAll();
                Assert.IsTrue(ex.GetType() == typeof(Exception));
            }*/
        }
        
        [TestCase("value", "value1")]
        public void DataSourceForComboboxTest(string value, string value1)
        {
            var data = GenerateDataTable<NhanVienDTO>(10);

            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(data);
            var result = hoaDonThanhToanBUS.DataSourceForCombobox();
            mockIDataProvider.VerifyAll();
            Assert.IsTrue(result.Count<string>() == data.Rows.Count);
        }

        [Test]
        public void DataSourceForComboboxTestException()
        {
            /*try
            {*/
                var data = GenerateDataTable<NhanVienDTO>(10);

                mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.DataSourceForCombobox());
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
            }*/
        }
        
        [Test]
        public void InsertKhachHangTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), new object[] { "value", "value1", "value2", "value3", "value4" })).Returns(1);
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), new object[] { "value0", "value1", "value2", "value3", "value4" })).Returns(0);

            var result = hoaDonThanhToanBUS.InsertKhachHang(new object[] { "value", "value1", "value2", "value3", "value4" });
            var result1 = hoaDonThanhToanBUS.InsertKhachHang(new object[] { "value0", "value1", "value2", "value3", "value4" });

            mockIDataProvider.VerifyAll();
            Assert.IsTrue(result);
            Assert.IsFalse(result1);
        }

        [Test]
        public void InsertKhachHangTestException()
        {
            /*try
            {*/
                mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.InsertKhachHang(null));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            //}
            /*catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            }*/
        }

        [TestCase(new object[] { "value", "value1", "value2", "value3" }, 1, true)]
        [TestCase(new object[] { "value0", "value1", "value2", "value3" }, 0, false)]
        public void InsertChiTietHoaDonTest(object[] array, int value, bool valueReslut)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), array)).Returns(value);
            var reslut = hoaDonThanhToanBUS.InsertChiTietHoaDon(array);
            Assert.IsTrue(reslut == valueReslut);
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void InsertChiTietHoaDonTestException()
        {
            /*try
            {*/
                mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.InsertChiTietHoaDon(null));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            //}
            /*catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            }*/
        }

        [TestCase("name", "value")]
        [TestCase("name1", "value1")]
        public void GetMaHangTest(string name, string value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(value);
            var result = hoaDonThanhToanBUS.GetMaHang(name);
            Assert.AreEqual(result, value);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("name")]
        [TestCase(null)]
        public void GetMaHangTestException(string name)
        {
            //try
            //{
                mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.GetMaHang(name));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            //}
            /*catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            }*/
        }


        [TestCase(new object[] { "value", "value1", "value2", "value3" }, 1, true)]
        [TestCase(new object[] { "value0", "value1", "value2", "value3" }, 0, false)]
        public void InsertHoaDonTest(object[] array, int value, bool valueReslut)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), array)).Returns(value);
            var result = hoaDonThanhToanBUS.InsertHoaDon(array);

            Assert.IsTrue(result == valueReslut);
            mockIDataProvider.VerifyAll();
        }

        [TestCase(new object[] { "value", "value1", "value2", "value3" }, typeof(Exception))]
        [TestCase(new object[] { null }, typeof(Exception))]
        public void InsertHoaDonTestException(object[] array, Type type)
        {
            /*try
            {*/
                mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), array)).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.InsertHoaDon(array));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == type);
                mockIDataProvider.VerifyAll();
            }*/
        }

        [TestCase("321", "value")]
        [TestCase("123", "value1")]
        public void GetMaKHTest(string number, string value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(number) })).Returns(value);
            var reslut = hoaDonThanhToanBUS.GetMaKH(number);

            Assert.AreEqual(reslut, value);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("123", typeof(Exception))]
        //[TestCase(null, typeof(System.Reflection.TargetInvocationException))]
        public void GetMaKHTestException(string number, Type value)
        {
            //try
            //{
                mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(number) })).Throws(new Exception());
                var exception=Assert.Catch<Exception>(()=> hoaDonThanhToanBUS.GetMaKH(number));
                Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
            //}
            /*catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == value);
                mockIDataProvider.VerifyAll();
            }*/
        }

        [TestCase("321", "value")]
        [TestCase("123", "value1")]
        public void GetTenKHTest(string number, string value)
        {

            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(number) })).Returns(value);
            var reslut = hoaDonThanhToanBUS.GetTenKH(number);

            Assert.AreEqual(reslut, value);
            mockIDataProvider.VerifyAll();
        }

        [TestCase("123", typeof(Exception))]
        //[TestCase(null, typeof(TargetInvocationException))]
        public void GetTenKHTestException(string number, Type value)
        {
            //try
            //{
                mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { int.Parse(number) })).Throws(new Exception());
                var exception = Assert.Catch<Exception>(() => hoaDonThanhToanBUS.GetTenKH(number));
                Assert.IsTrue(exception.GetType() == value);
                mockIDataProvider.VerifyAll();
            //}
            //catch (Exception ex)
            //{
            //    Assert.IsTrue(ex.GetType() == value);
            //}
                
        }

        [TestCase(new object[]{ 1, "value1", 2, "value2", "value3"},"value")]
        [TestCase(new object[] { 2, "value2", 2, "value3", "value4" }, "value5")]
        public void GetMaHoaDonTest(object[] array,object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), array)).Returns(value);
            var result = hoaDonThanhToanBUS.GetMaHoaDon((int) array[0], array[1].ToString(),(int) array[2], array[3].ToString(), array[4].ToString());

            Assert.IsTrue(value.Equals(result));
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetMaHoaDonTestException()
        {
            var array = new object[] { 1, "value1", 2, "value2", "value3" };
            var arrayNull = new object[] { 1,null,1,null,null };
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), array)).Throws(new Exception());
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), arrayNull)).Throws(new Exception());

            var exception = Assert.Catch<Exception>(
                () => hoaDonThanhToanBUS.GetMaHoaDon((int)array[0], array[1].ToString(), (int)array[2], array[3].ToString(), array[4].ToString()));

            var exceptionNull = Assert.Catch<Exception>(
                () => hoaDonThanhToanBUS.GetMaHoaDon(1,null,1,null,null));

            Assert.IsTrue(typeof(Exception) == exceptionNull.GetType());

            Assert.IsTrue(typeof( Exception) == exception.GetType());
            mockIDataProvider.VerifyAll();
        }

        [TestCase(new object[] { "id1", "number1" },true,1)]
        [TestCase(new object[] {"id","number" },true,1)]
        public void UpdateHangHoaTest(object[] arg,bool value,int number)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), arg)).Returns(number);
            var result = hoaDonThanhToanBUS.UpdateHangHoa(arg);
            Assert.IsTrue(result == value);
            mockIDataProvider.VerifyAll();
        }


        [TestCase(new object[] { "id1", "number1" }, typeof(Exception))]
        [TestCase(new object[] { "id"}, typeof(Exception))]
        [TestCase(new object[] { null }, typeof(Exception))]
        public void UpdateHangHoaTestException(object[] arg, Type value)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), arg)).Throws(new Exception());
            var exception = Assert.Catch<Exception>(
                () => hoaDonThanhToanBUS.UpdateHangHoa(arg));
            Assert.IsTrue(value == exception.GetType());
            //mockIDataProvider.Setup(x => x.Update(It.IsNotNull<string>(), arg)).Returns(number);
        }

        [TestCase(new object[] {"id" },"value")]
        [TestCase(new object[] { "id1" }, "value1")]
        public void GetSoLuongTest(object[] array,object value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), array)).Returns(value);
            var reslut = hoaDonThanhToanBUS.GetSoLuong(array);
            Assert.IsTrue(reslut.Equals(value));
        }


        [TestCase(new object[] { "id" }, typeof(Exception))]
        [TestCase(new object[] { "id1" }, typeof(Exception))]
        public void GetSoLuongTestException(object[] array, Type value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), array)).Throws(new Exception());
            var exception = Assert.Catch<Exception>(
                () => hoaDonThanhToanBUS.GetSoLuong(array));

            Assert.IsTrue(value== exception.GetType());
        }
    }
}
