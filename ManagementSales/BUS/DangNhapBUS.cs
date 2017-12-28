using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSales.BUS
{
    public class DangNhapBUS : IDangNhapBUS
    {
        private IDataProvider dataProvider;
        public DangNhapBUS(IDataProvider data)
        {
            this.dataProvider = data;
        }

        /// <summary>
        /// Trả về chức vụ ứng với tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ChucVu(string name)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXEC USP_CheckChucVu @Name", new object[] { name }).ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Kiểm tra xem đăng nhập có đúng không
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        public bool IsDangNhap(string name, string Pass)
        {
            try
            {
                var passHash = dataProvider.ExecuteScalar("EXECUTE USP_GetPassword @name", new object[] { name })?.GetHashCode();
                return passHash.GetHashCode() == Pass.GetHashCode();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //throw new NotImplementedException();
        }

        public void Log(string data,string path)
        {
            //string path = Application.StartupPath + "\\log.txt";           
            using (var fileStream =File.Open(path,FileMode.Append))
            {
                StreamWriter sWriter = new StreamWriter(fileStream, Encoding.UTF8);
                sWriter.WriteLine(data,true);
                sWriter.Flush();
                sWriter.Close();
                fileStream.Close();
            }
            

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Trả về Mã Nhân Viên dựa vào tên đăng nhập
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string MaNV(string name)
        {
            try
            {
                var id = dataProvider.ExecuteScalar("EXEC USP_GetMaNVByNameSignIn @name", new object[] { name });
                return id==null?"null":id.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Trả về Tên Nhân Viên dựa vào Mả Nhân Viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string TenNV(int id)
        {
            try
            {
                return dataProvider.ExecuteScalar("EXEC USP_GetTenNV @id", new object[] { id }).ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //throw new NotImplementedException();
        }
    }
}
