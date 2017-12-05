using Moq;
using NUnit.Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManagementSales.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSales.DAO;
using System.IO;
using SystemWrapper;
using System.Windows.Forms;

namespace ManagementSales.BUS.Tests
{
    [TestFixture]
    public class DangNhapBUSTests
    {
        private Mock<IDataProvider> mockIDataProvider;
        private DangNhapBUS dangNhapBUS;

        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            dangNhapBUS = new DangNhapBUS(mockIDataProvider.Object);
        }

        [TestCase("name2", "value2")]
        [TestCase("name3", "value3")]
        public void ChucVuTestAllow(string name, string value)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(value);
            var result = dangNhapBUS.ChucVu(name);
            Assert.AreEqual(value, result);
            mockIDataProvider.VerifyAll();
            //Assert.Fail();
        }

        [TestCase(null, typeof(Exception))]
        [TestCase("name", typeof(Exception))]
        public void ChucVuTestException(string name, Type value)
        {
            /*try
            {*/
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => dangNhapBUS.ChucVu(name));
            Assert.IsTrue(exception.GetType() == value);
            mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                //Assert.True(ex.Message == "kkk");
                Assert.True(ex.GetType() == typeof(Exception));
            }*/

            //mockIDataProvider.Setup(x => x.ChucVu(It.IsNotNull<string>(), new object[] { "name" })).Returns(new Exception());
            //var result = ;
            //Assert.That(() => dangNhapBUS.ChucVu("name"), Throws.Exception);
            //Assert.That(()=>dangNhapBUS.ChucVu(null), Throws.Exception);
            // Assert.Throws<Exception>(() => dangNhapBUS.ChucVu(null));
            //mockIDataProvider.VerifyAll();
        }

        [TestCase( "A")]
        [TestCase("B")]
        public void LogTest(string content)
        {
            string path = Path.GetTempPath() + "\\log.txt";
            var file = File.Create(path);
            file.Close();
            var mockIO = new Mock<SystemWrapper.IO.IFileWrap>();
            mockIO.Setup(x => x.Open(It.IsNotNull<string>(), FileMode.Open));//.Returns(mockIO.Object.Open(It.IsNotNull<string>(), FileMode.Open));
            dangNhapBUS.Log("",path);
            
            File.Delete(path);

        }

        [TestCase("name", "pass")]
        [TestCase("name1", "pass1")]
        public void IsDangNhapTest(string name, string pass)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(pass);
            var result = dangNhapBUS.IsDangNhap(name, pass);
            var resultFalse = dangNhapBUS.IsDangNhap(name, "passs");
            Assert.True(result);
            Assert.False(resultFalse);
            mockIDataProvider.VerifyAll();
        }

        [TestCase(null, "pass", typeof(Exception))]
        [TestCase("name", "pass1", typeof(Exception))]
        public void IsDangNhapTestException(string name, string pass, Type value)
        {
            /*try
            {*/
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => dangNhapBUS.IsDangNhap(name, pass));
            Assert.IsTrue(exception.GetType() == value);
            mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.That(ex.GetType() == typeof(Exception));
                //throw;
            }*/
        }

        [TestCase("name", "ma")]
        [TestCase("name1", "ma1")]
        public void MaNVTest(string name, string maNV)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { name })).Returns(maNV);
            var result = dangNhapBUS.MaNV(name);
            Assert.True(result.Equals(maNV));
            mockIDataProvider.VerifyAll();
        }

        [TestCase(null, typeof(Exception))]
        [TestCase("name", typeof(Exception))]
        public void MaNVTestException(string name, Type value)
        {
            /*try
            {*/
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsAny<string>(), new object[] { name })).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => dangNhapBUS.MaNV(name));
            Assert.IsTrue(exception.GetType() == value);
            mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.True(ex.GetType() == typeof(Exception));
            }*/
        }

        [TestCase(1, "name")]
        [TestCase(2, "name1")]
        public void TenNVTest(int id, string name)
        {
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { id })).Returns(name);
            var result = dangNhapBUS.TenNV(id);
            Assert.True(result.Equals(name));
            mockIDataProvider.VerifyAll();
        }

        [TestCase(null, typeof(Exception))]
        [TestCase(1, typeof(Exception))]
        public void TenNVTestException(int id, Type value)
        {
            /*try
            {*/
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), new object[] { id })).Throws(new Exception());
            var exception = Assert.Catch<Exception>(() => dangNhapBUS.TenNV(id));
            Assert.IsTrue(exception.GetType() == value);
            mockIDataProvider.VerifyAll();
            /*}
            catch (Exception ex)
            {
                Assert.True(ex.GetType() == typeof(Exception));
            }*/

        }
    }
}