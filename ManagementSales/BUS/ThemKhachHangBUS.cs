using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;

namespace ManagementSales.BUS
{
    public class ThemKhachHangBUS : IThemKhachHangBUS
    {

        private IDataProvider dataProvider;
        public ThemKhachHangBUS(IDataProvider data)
        {
            this.dataProvider = data;
        }

        /// <summary>
        ///Thêm một khách hàng vào cơ sở dữ liệu .Trả về <see cref="true"/> nếu thành công và ngược lại 
        ///Theo thứ tự tên , sdt , giới tính , địa chỉ , loại khách hàng
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool InsertKhachHang(object[] values)
        {
            return dataProvider.ExecuteNonQuery("EXECUTE USP_InsertKhachHang @ten , @sdt , @gioitinh , @diachi , @loaikh", values)>0;
            //throw new NotImplementedException();
        }
    }
}
