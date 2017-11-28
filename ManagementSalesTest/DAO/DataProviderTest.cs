using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ManagementSales.DAO;

namespace QuanLyBanHangTest.DAO
{
    [TestFixture]
    class DataProviderTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
        }

        [Test]
        public void ClassTest()
        {
            var result = typeof(DataProvider).GetInterfaces().Contains(typeof(IDataProvider));
            var result1 = typeof(DataProvider).IsPublic;
            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }


    }
}
