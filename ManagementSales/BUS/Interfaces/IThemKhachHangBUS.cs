using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS.Interfaces
{
    public interface IThemKhachHangBUS
    {
        // <summary>
        ///Thêm một khách hàng vào cơ sở dữ liệu .Trả về <see cref="true"/> nếu thành công và ngược lại 
        ///Theo thứ tự tên , sdt , giới tính , địa chỉ , loại khách hàng
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        bool InsertKhachHang(object[] values);

    }
}
