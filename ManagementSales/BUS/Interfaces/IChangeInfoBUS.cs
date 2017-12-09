using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSales.BUS.Interfaces
{
    public interface IChangeInfoBUS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        bool ChangeInfo(string name, string pass,string id);
    }
}
