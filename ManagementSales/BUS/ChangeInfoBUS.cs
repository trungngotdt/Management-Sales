using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS
{
    public class ChangeInfoBUS : IChangeInfoBUS
    {
        private IDataProvider dataProvider;
        public ChangeInfoBUS(IDataProvider data)
        {
            this.dataProvider = data;
        }

        public bool ChangeInfo(string name, string pass, string id)
        {
            
            if (!(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass)))
            {

                return dataProvider.ExecuteNonQuery("EXECUTE USP_UpdateNVNameSignInPass @id , @name , @pass ", new object[] { id, name, pass }) > 0;
            }
            else if (!String.IsNullOrEmpty(name))
            {
                var isExist= dataProvider.ExecuteScalar("EXEC USP_GetMaNVByNameSignIn @name", new object[] { name })?.ToString();
                if (isExist!=null)
                {
                    return false;
                }
                return dataProvider.ExecuteNonQuery("EXECUTE USP_UpdateNVNameSignIn @id , @name  ", new object[] { id, name }) > 0;
            }
            else if (!String.IsNullOrEmpty(pass))
            {
                return dataProvider.ExecuteNonQuery("EXECUTE USP_UpdateNVNamePass @id , @pass  ", new object[] { id, pass }) > 0;
            }
            return false;
            //throw new NotImplementedException();
        }
    }
}
