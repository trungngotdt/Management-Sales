using ManagementSales.BUS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagementSales.DAO;

namespace ManagementSales.BUS
{
    public class ChiTietDonHangBUS : IChiTietDonHangBUS
    {
        private IDataProvider dataProvider;

        public ChiTietDonHangBUS(IDataProvider data)
        {
            this.dataProvider = data;
        }

        public DataTable GetDataChiTietDonHang(int index)
        {
            return dataProvider.ExecuteQuery("USP_GetChiTietHoaDon @id",new object[] {index });
            //throw new NotImplementedException();
        }
    }
}
