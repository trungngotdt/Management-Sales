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



            while (CloseFrm != true)
            {
                var list = ServiceLocator.Current.GetAllInstances<ChiTietDonHangBUS>();
                var dangNhap = ServiceLocator.Current.GetInstance<frmDangNhap>();
                var hoaDonThanhToan = ServiceLocator.Current.GetInstance<frmHoaDonThanhToan>();
                var quanLyThongTin = ServiceLocator.Current.GetInstance<frmQuanLyThongTin>();
                var quanLy = ServiceLocator.Current.GetInstance<frmQuanLy>();
                //new frmQuanLyThongTin());//frmHoaDonThanhToan());//new frmQuanLy());//
                Application.Run(dangNhap);
                if (OpenFrmDangNhap)
                {
                    Application.Run(dangNhap);
                }
                else if (OpenFrmQuanLyThongTin)
                {
                    Application.Run(quanLyThongTin);
                }
                else if (OpenFrmHoaDonThanhToan)
                {
                    Application.Run(hoaDonThanhToan);
                }
                else if (OpenFrmQuanLy)
                {
                    Application.Run(quanLy);
                }
            }

        }
    }
}
