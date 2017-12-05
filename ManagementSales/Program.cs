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
using System.IO;

namespace ManagementSales
{
    static class Program
    {
        public static bool OpenFrmHoaDonThanhToan { get; set; }
        public static bool OpenFrmQuanLyThongTin { get; set; }
        public static bool OpenFrmQuanLy { get; set; }
        public static bool OpenFrmDangNhap { get; set; }
        public static bool CloseFrm { get; set; }

        public static string NameStaff { get; set; }
        public static string IDStaff { get; set; }
        public static string RoleStaff { get; set; }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OpenFrmDangNhap = false;
            OpenFrmHoaDonThanhToan = false;
            OpenFrmQuanLyThongTin = false;
            OpenFrmQuanLy = false;
            CloseFrm = false;
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
            string path = Application.StartupPath + "\\log.txt";
            var checkFileExist = File.Exists(path);
            if (!checkFileExist)
            {
                var fileCreate = File.Create(path);
                fileCreate.Close();
            }

            while (CloseFrm!=true)
            {
                Application.Run(new frmDangNhap());//new frmQuanLyThongTin());//frmHoaDonThanhToan());//new frmQuanLy());//

                if (OpenFrmDangNhap)
                {
                    Application.Run(new frmDangNhap());
                }
                else if (OpenFrmQuanLyThongTin)
                {
                    Application.Run(new frmQuanLyThongTin());
                }
                else if (OpenFrmHoaDonThanhToan)
                {
                    Application.Run(new frmHoaDonThanhToan());
                }
                else if (OpenFrmQuanLy)
                {
                    Application.Run(new frmQuanLy());
                }
            }

        }
    }
}
