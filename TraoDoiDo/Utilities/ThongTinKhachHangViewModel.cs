using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;
using TraoDoiDo.Database;
using System.Windows;
using TraoDoiDo.ViewModels;
namespace TraoDoiDo.ViewModels
{
    public class ThongTinKhachHangViewModel : ThuocTinhViewModel
    {
        private NguoiDung khachHang = new NguoiDung();

        public NguoiDung KhachHangThongTin { get => khachHang; set => khachHang = value; }

        public string TenDangNhap { get => khachHang.TaiKhoan.TenDangNhap; set { khachHang.TaiKhoan.TenDangNhap = value; OnPropertyChanged(); } }
        public string MatKhau { get => khachHang.TaiKhoan.MatKhau; set { khachHang.TaiKhoan.MatKhau = value; OnPropertyChanged(); } }
        public string HoTen { get => khachHang.HoTen; set { khachHang.HoTen = value; OnPropertyChanged(); } }
        public string GioiTinh { get => khachHang.GioiTinh; set { khachHang.GioiTinh = value; OnPropertyChanged(); } }
        public string NgaySinh { get => khachHang.NgaySinh; set { khachHang.NgaySinh = value; OnPropertyChanged(); } }
        public string SDT { get => khachHang.Sdt; set { khachHang.Sdt = value; OnPropertyChanged(); } }
        public string CMND { get => khachHang.Cmnd; set { khachHang.Cmnd = value; OnPropertyChanged(); } }
        public string Email { get => khachHang.Email; set { khachHang.Email = value; OnPropertyChanged(); } }
        public string DiaChi { get => khachHang.DiaChi; set { khachHang.DiaChi = value; OnPropertyChanged(); } }
        public string Anh { get => khachHang.Anh; set { khachHang.Anh = value; OnPropertyChanged(); } }
        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }
        KiemTraDinhDang kiemTra = new KiemTraDinhDang();
        public ThongTinKhachHangViewModel()
        {

        }
        public ThongTinKhachHangViewModel(NguoiDung kh)
        {
            khachHang = kh;
        }
        public bool kiemTraCacTextBox()
        {
            DateTime ngaySinh = DateTime.ParseExact(NgaySinh, "dd/MM/yyyy", null);
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(TenDangNhap) || string.IsNullOrWhiteSpace(MatKhau) ||
                string.IsNullOrWhiteSpace(CMND) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(SDT) || string.IsNullOrWhiteSpace(DiaChi) ||
                    string.IsNullOrWhiteSpace(HoTen) || string.IsNullOrWhiteSpace(GioiTinh)
                || string.IsNullOrWhiteSpace(NgaySinh))
            {
                ErrorMessage = XuLyTienIch.TinNhanTrongKhongHopLe;
                MessageBox.Show(ErrorMessage);
                return false;
            }
            if (!kiemTra.kiemTraNgaySinh(ngaySinh))
            {
                ErrorMessage = XuLyTienIch.TinNhanNgaySinhKhongHopLe;
                MessageBox.Show(ErrorMessage);
                return false;
            }
            if (!kiemTra.kiemTraEmail(Email))
            {
                ErrorMessage = XuLyTienIch.TinNhanEmailKhongHopLe;
                MessageBox.Show(ErrorMessage);
                return false;
            }
            if (!kiemTra.kiemTraSoDienThoai(SDT))
            {
                ErrorMessage = XuLyTienIch.TinNhanSdtKhongHopLe;
                MessageBox.Show(ErrorMessage);
                return false;
            }
            if (!kiemTra.kiemTraCMND(CMND))
            {
                ErrorMessage = XuLyTienIch.TinNhanCMNDKhongHopLe;
                MessageBox.Show(ErrorMessage);
                return false;
            }
            return true;
        }

    }
}
