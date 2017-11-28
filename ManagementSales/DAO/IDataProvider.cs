using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.DAO
{
    public interface IDataProvider
    {

        /// <summary>
        /// Trả về dòng một cộng một của bảng dữ liệu
        /// </summary>
        /// <param name="query"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        object ExecuteScalar(string query, object[] value = null);


        /// <summary>
        /// Trả về số dòng bị câu lệnh truy vấn ảnh hưởng 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string query, object[] value = null);

        /// <summary>
        /// Thực thi câu lệnh truy vấn ;trả ra các giá trị truy vấn được vào bảng <see cref="DataTable"/>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        System.Data.DataTable ExecuteQuery(string query, object[] value = null);
    }
}
