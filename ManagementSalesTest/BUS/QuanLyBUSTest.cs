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

namespace QuanLyBanHangTest.BUS
{
    [TestFixture]
    public class QuanLyBUSTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        private QuanLyBUS quanLyBUS;
        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            quanLyBUS = new QuanLyBUS(mockIDataProvider.Object);
        }

        [Test]
        public void ClassTest()
        {
            var result = typeof(QuanLyBUS).GetInterfaces().Contains(typeof(IQuanLyBUS));
            var result1 = typeof(QuanLyBUS).IsPublic;
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }

        [Test]
        public void GetDataKHTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(),  null )).Returns(new System.Data.DataTable());
            quanLyBUS.GetDataKH();
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataKHTestException()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
            var exception=Assert.Catch<Exception>(()=> quanLyBUS.GetDataKH());
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataDHTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(new System.Data.DataTable());
            quanLyBUS.GetDataDonHang();
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataDHTestException()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => quanLyBUS.GetDataDonHang());
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataHangTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(new System.Data.DataTable());
            quanLyBUS.GetDataHang();
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataHangTestException()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => quanLyBUS.GetDataHang());
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataNVTest()
        {
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Returns(new System.Data.DataTable());
            quanLyBUS.GetDataNV();
            mockIDataProvider.VerifyAll();
        }

        [Test]
        public void GetDataNVTestException()
        { 
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), null)).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => quanLyBUS.GetDataNV());
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }


        [TestCase(new object[] {"value1","value2","value2","value3","value4" },1,true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5" }, 0,false)]
        public void InsertKHTest(object[] para,int number,bool value)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result = quanLyBUS.InsertKH(para);
            Assert.AreEqual(result, value);
        }

        [TestCase(new object[] {"null" })]
        public void InsertKHTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.InsertKH(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns("");
            var excep = Assert.Catch<Exception>(() => quanLyBUS.InsertKH(para));
            Assert.IsTrue(excep.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }


        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value4" }, 1, true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5" }, 0, false)]
        public void InsertHangTest(object[] para, int number, bool value)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(null);
            var result = quanLyBUS.InsertHang(para);
            Assert.AreEqual(result, value);
        }

        [TestCase(new object[] { "null","k" })]
        public void InsertHangTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.InsertHang(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns("");
            var excep = Assert.Catch<Exception>(() => quanLyBUS.InsertHang(para));
            Assert.IsTrue(excep.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }


        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value4" }, 1, true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5" }, 0, false)]
        public void InsertNVTest(object[] para, int number, bool value)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result = quanLyBUS.InsertNV(para);
            Assert.AreEqual(result, value);
        }

        [TestCase(new object[] { "null" })]
        [TestCase(new object[] {"l",1})]
        public void InsertNVTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.InsertNV(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns("");
            var excep = Assert.Catch<Exception>(() => quanLyBUS.InsertNV(para));
            Assert.IsTrue(excep.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }


        [TestCase(new object[] { "null" })]
        public void InsertNVTestException2(object[] para)
        {
            try
            {

                mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>(); quanLyBUS.InsertNV(para);
                //var exception = Assert.Throws<Exception>(() => quanLyBUS.InsertNV(para));
            }
            catch (Exception exception)
            {
Assert.IsTrue(exception.GetType() == typeof(Exception));
                mockIDataProvider.VerifyAll();
                
            }
            
            
        }

        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value4","value5" }, 1, true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5","value7" }, 0, false)]
        public void UpdateKHTest(object[] para, int number, bool value)
        {

            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result = quanLyBUS.UpdateKH(para);
            Assert.AreEqual(result, value);
        }


        [TestCase(new object[] { "null" })]
        public void UpdateKHTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.UpdateKH(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }



        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value4", "value5" }, 1, true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5", "value7" }, 0, false)]
        public void UpdateHangTest(object[] para, int number, bool value)
        {

            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result = quanLyBUS.UpdateHang(para);
            Assert.AreEqual(result, value);
        }


        [TestCase(new object[] { "null" })]
        public void UpdateHangTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.UpdateHang(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }

        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value4", "value5" }, 1, true)]
        [TestCase(new object[] { "value1", "value2", "value2", "value3", "value5", "value7" }, 0, false)]
        public void UpdateNVTest(object[] para, int number, bool value)
        {

            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result = quanLyBUS.UpdateNV(para);
            Assert.AreEqual(result, value);
        }


        [TestCase(new object[] { "null" })]
        public void UpdateNVTestException(object[] para)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Throws<Exception>();
            var exception = Assert.Catch<Exception>(() => quanLyBUS.UpdateNV(para));
            Assert.IsTrue(exception.GetType() == typeof(Exception));
            mockIDataProvider.VerifyAll();
        }
    }
}
