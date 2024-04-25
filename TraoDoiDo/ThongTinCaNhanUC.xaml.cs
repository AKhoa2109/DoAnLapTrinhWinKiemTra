using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraoDoiDo.Database;
using TraoDoiDo.Models;
using Microsoft.Win32;
using TraoDoiDo.ViewModels;
namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for ThongTinCaNhanUC.xaml
    /// </summary>
    public partial class ThongTinCaNhanUC : UserControl
    {
        NguoiDung kh = new NguoiDung();

        public ThongTinCaNhanUC()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += UCThongTinCaNhan_Loaded;
        }
        public ThongTinCaNhanUC(NguoiDung nguoiDung)
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += UCThongTinCaNhan_Loaded;
            kh = nguoiDung;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UCThongTinCaNhan_Loaded(object sender, RoutedEventArgs e)
        {
            txtHoTen.Text = kh.HoTen;
            txtCmnd.Text = kh.Cmnd;
            txtDiaChi.Text = kh.DiaChi;
            txtId.Text = kh.Id;
            txtSdt.Text = kh.Sdt;
            txtTenDangNhap.Text = kh.TaiKhoan.TenDangNhap;
            txtMatKhau.Password = kh.TaiKhoan.MatKhau;
            txtEmail.Text = kh.Email;
            cbGioiTinh.Text = kh.GioiTinh; 
             
            //DateTime selectedDate = DateTime.ParseExact(kh.NgaySinh,"dd/MM/yyyy",null);
            //dtpNgaySinh.SelectedDate = selectedDate;

            imageHinhDaiDien.Source = new BitmapImage(new Uri(XuLyAnh.layDuongDanDayDuToiFileAnhDaiDien(kh.Anh)));
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            TaiKhoan taiKhoan = new TaiKhoan(txtTenDangNhap.Text, txtMatKhau.Password, txtId.Text);
            NguoiDung nguoiDung = new NguoiDung(txtId.Text, txtHoTen.Text, cbGioiTinh.Text, dtpNgaySinh.Text, txtCmnd.Text, txtEmail.Text, txtSdt.Text, txtDiaChi.Text, imageHinhDaiDien.Source.ToString(), taiKhoan, "");
            NguoiDungDao nguoiDungDao = new NguoiDungDao();
            nguoiDungDao.CapNhat(nguoiDung);
        }

        private void btnDoiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            TaiKhoan taiKhoan = new TaiKhoan(txtTenDangNhap.Text, txtMatKhau.Password, txtId.Text);
            TaiKhoanDao taiKhoanDao = new TaiKhoanDao();
            taiKhoanDao.CapNhat(taiKhoan);
        }

        private void btnChonAnh_Click(object sender, RoutedEventArgs e)
        {
            chonAnh();
        }

        public void chonAnh()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files(*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files(*.*)|*.*";
            if (file.ShowDialog() == true)
            {
                string imagePath = file.FileName;
                BitmapImage hinh = new BitmapImage(new Uri(imagePath));
                imageHinhDaiDien.Source = hinh;
            }
        }
    }
}
