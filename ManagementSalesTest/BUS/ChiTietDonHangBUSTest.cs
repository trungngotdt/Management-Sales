using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using ManagementSales.BUS;
using ManagementSales.DAO;
using ManagementSales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangTest.BUS
{
    class ChiTietDonHangBUSTest
    {
        private Mock<IDataProvider> mockIDataProvider;
        private ChiTietDonHangBUS chiTietDonHangBUS;

        [SetUp]
        public void SetUp()
        {
            mockIDataProvider = new Mock<IDataProvider>();
            chiTietDonHangBUS = new ChiTietDonHangBUS(mockIDataProvider.Object);
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


        [TestCase(2)]
        [TestCase(1)]
        public void GetDataChiTietDonHang(int index)
        {
            var data = GenerateDataTable<ChiTietHoaDonDTO>(10);
            mockIDataProvider.Setup(x => x.ExecuteQuery(It.IsNotNull<string>(), new object[] { index })).Returns(data);
            chiTietDonHangBUS.GetDataChiTietDonHang(index);
            mockIDataProvider.VerifyAll();
            
        }

    }
}
