using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS.Interfaces
{
    public interface IChiTietDonHangBUS
    {
        /// <summary>
        /// Lấy tất cả thông tin dựa vào chỉ số truyền vào ( chỉ số này là mã hóa đơn )
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        System.Data.DataTable GetDataChiTietDonHang(int index);
    }
}
