using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ManagementSales.DAO;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;

namespace QuanLyBanHangTest.BUS
{
    [TestFixture]
    class ThemKhachHangBUSTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        private ThemKhachHangBUS themKhachHangBUS;

        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            themKhachHangBUS = new ThemKhachHangBUS(mockIDataProvider.Object);
        }

        [Test]
        public void ClassTest()
        {
            var result = typeof(ThemKhachHangBUS).GetInterfaces().Contains(typeof(IThemKhachHangBUS));
            var result1 = typeof(ThemKhachHangBUS).IsPublic;
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }

        [TestCase(new object[] { "a", "b", "c","d" }, false,0)]
        [TestCase(new object[] {"a","b","c" },true,1)]
        public void InsertTest(object[] para,bool value,int number)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), para)).Returns(number);
            var result= themKhachHangBUS.InsertKhachHang(para);
            Assert.IsTrue(result == value);
        }
    }
}
