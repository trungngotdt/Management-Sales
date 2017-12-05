using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS.Interfaces
{
    public interface IDangNhapBUS
    {
        /// <summary>
        /// Ghi lại hoạt động đăng nhập
        /// </summary>
        /// <param name="data"></param>
        void Log(string data,string path);

        /// <summary>
        /// Trả về Mã Nhân Viên dựa vào tên đăng nhập
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string MaNV(string name);

        /// <summary>
        /// Trả về Tên Nhân Viên dựa vào Mả Nhân Viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string TenNV(int id);

        /// <summary>
        /// Trả về chức vụ ứng với tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string ChucVu( string name);

        /// <summary>
        /// Kiểm tra xem đăng nhập có đúng không
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        bool IsDangNhap(string name,string Pass);
    }
}
