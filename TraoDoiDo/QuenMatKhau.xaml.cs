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
using TraoDoiDo.ViewModels;

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for QuenMatKhau.xaml
    /// </summary>
    public partial class QuenMatKhau : Window
    {
        KiemTraDinhDang kiemTra = new KiemTraDinhDang();
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NguoiDungDao khacHangDao = new NguoiDungDao();
                string mk = "";
                if (kiemTra.kiemTraEmail(txtThongTinTaiKhoan.Text))
                {
                    mk = khacHangDao.TimKiemMatKhauBangEmail(txtThongTinTaiKhoan.Text);
                }
                if (kiemTra.kiemTraSoDienThoai(txtThongTinTaiKhoan.Text))
                {
                    mk = khacHangDao.TimKiemMatKhauBangSdt(txtThongTinTaiKhoan.Text);
                }
                if (!string.IsNullOrWhiteSpace(mk))
                    MessageBox.Show($"Mật khẩu của khách hàng là: {mk}");
                else
                    MessageBox.Show($"Không tìm thấy email");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
