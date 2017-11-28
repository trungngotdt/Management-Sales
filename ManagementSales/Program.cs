using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ManagementSales.BUS;
using ManagementSales.BUS.Interfaces;
using ManagementSales.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementSales
{
    static class Program
    {
        public static bool OpenFrmHoaDonThanhToan { get; set; }
        public static bool OpenFrmQuanLyThongTin { get; set; }
        public static bool OpenFrmQuanLy { get; set; }

        public static string NameStaff { get; set; }
        public static string IDStaff { get; set; }
        public static string RoleStaff { get; set; }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OpenFrmHoaDonThanhToan = false;
            OpenFrmQuanLyThongTin = false;
            OpenFrmQuanLy = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<IDataProvider, DataProvider>();

            unityContainer.RegisterType<IHoaDonThanhToanBUS, HoaDonThanhToanBUS>();
            unityContainer.RegisterType<IDangNhapBUS, DangNhapBUS>();
            unityContainer.RegisterType<IThemKhachHangBUS, ThemKhachHangBUS>();
            unityContainer.RegisterType<IQuanLyThongTinBUS, QuanLyThongTinBUS>();
            unityContainer.RegisterType<IQuanLyBUS, QuanLyBUS>();
            unityContainer.RegisterType<IChiTietDonHangBUS, ChiTietDonHangBUS>();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

            Application.Run(new frmDangNhap());//new frmQuanLyThongTin());//frmHoaDonThanhToan());//new frmQuanLy());//
            if (OpenFrmQuanLyThongTin)
            {
                Application.Run(new frmQuanLyThongTin());
            }
            else if (OpenFrmHoaDonThanhToan)
            {
                Application.Run(new frmHoaDonThanhToan());
            }
            else if(OpenFrmQuanLy)
            {
                Application.Run(new frmQuanLy());
            }
        }
    }
}
