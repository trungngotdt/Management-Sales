using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ManagementSales.DAO;
using ManagementSales.BUS;

namespace ManagementSalesTest.BUS
{
    [TestFixture]
    class ChangeInfoTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        private ChangeInfoBUS changeInfoBUS;
        [SetUp]
        public void Setup()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            changeInfoBUS = new ChangeInfoBUS(mockIDataProvider.Object);
        }

        [TestCase(null, "a", "", 1, null)]
        [TestCase("a", null, "", 0, "c")]
        [TestCase("a", null, "", 0, null)]
        [TestCase("a", "b", "", 1, null)]
        [TestCase("", "", "", 2, "c")]
        [TestCase("a","a","a",1,"a")]
        public void ChangeInfo(string name, string pass, string id, int number, object scalar)
        {
            mockIDataProvider.Setup(x => x.ExecuteNonQuery(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(number);
            mockIDataProvider.Setup(x => x.ExecuteScalar(It.IsNotNull<string>(), It.IsNotNull<object[]>())).Returns(scalar);
            var result = changeInfoBUS.ChangeInfo(name, pass, id);
            Assert.IsNotNull(result);
            mockIDataProvider.Verify();
        }
    }
}
