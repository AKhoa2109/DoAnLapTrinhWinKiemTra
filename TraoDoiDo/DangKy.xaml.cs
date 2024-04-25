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
using System.Windows.Shapes;
using TraoDoiDo.Database;
using TraoDoiDo.Models;
using TraoDoiDo.ViewModels;

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for DangKy.xaml
    /// </summary>
    public partial class DangKy : Window
    {
        public DangKy()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnDangKy_Click(object sender, RoutedEventArgs e)
        {

            TaiKhoanDao tkDao = new TaiKhoanDao();
            TaiKhoan tk = new TaiKhoan(txtTenDangNhap.Text, txtMatKhau.Password, null);
            NguoiDung khachHang = new NguoiDung(null, txtHoTen.Text, cbGioiTinh.Text, dtpNgaySinh.Text, txtCMND.Text, txtEmail.Text, txtSdt.Text, txtDiaChi.Text, imageDaiDien.Tag.ToString(), tk, "");
            ThongTinKhachHangViewModel ttkh = new ThongTinKhachHangViewModel(khachHang);
            NguoiDungDao khachHangDao = new NguoiDungDao();
            bool check = ttkh.kiemTraCacTextBox();
            if (check)
            {
                bool addKhachHang = false;
                bool addTaiKhoan = false;
                try
                {
                    khachHangDao.Them(khachHang);
                    addKhachHang = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể thêm khách hàng: " + ex.Message);
                }
                try
                {
                    tkDao.Them(tk);
                    addTaiKhoan = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể thêm tài khoản: " + ex.Message);
                }
                if (addKhachHang && addTaiKhoan)
                {
                    MessageBox.Show("Đăng kí thành công");
                }

            }

        }
    }
}
