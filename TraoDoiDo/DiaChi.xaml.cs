using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for DiaChiNhanHang.xaml
    /// </summary>
    public partial class DiaChi : Window
    {
        public string tongThanhToan;
        NguoiDung ngDung = new NguoiDung(); // người tương đối (mua hoặc bán)
        GiaoDichDao gdichDao = new GiaoDichDao();
        NguoiDungDao ngDungDao = new NguoiDungDao();
        GioHangDao gioHangDao = new GioHangDao();
        SanPhamDao sanPhamDao = new SanPhamDao();
        TrangThaiDonHangDao trangThaiDonHangDao = new TrangThaiDonHangDao();
        QuanLyDonHangDao quanLyDonHangDao = new QuanLyDonHangDao();
        List<TrangThaiDonHang> dsSanPhamDeThanhToan = new List<TrangThaiDonHang>();
        public DiaChi()
        {
            InitializeComponent();
            Loaded += FDiaChi_Loaded;
        } 
        public DiaChi(NguoiDung kh)
        {
            InitializeComponent();
            ngDung = kh;
            Loaded += FDiaChi_Loaded;
        } 
        public DiaChi(NguoiDung kh, List<TrangThaiDonHang> dsSanPhamDeThanhToan)
        {
            InitializeComponent();
            ngDung = kh;
            this.dsSanPhamDeThanhToan = dsSanPhamDeThanhToan;
            Loaded += FDiaChi_Loaded;
        } 
        

        private void btnXacNhanThanhToan_Click_1(object sender, RoutedEventArgs e)
        {
            bool co = false;
            try
            {
                capNhatThongTinCaNhan();
                foreach (var dong in dsSanPhamDeThanhToan)
                { 
                    NguoiDung nguoiDang = sanPhamDao.timKiemNguoiDangTheoIdSP(dong.IdSanPham);
                    QuanLyDonHang quanLyDonHang = new QuanLyDonHang(null, nguoiDang.Id, ngDung.Id, dong.IdSanPham,"Chờ đóng gói",null);

                    quanLyDonHangDao.Xoa(quanLyDonHang); //Xóa trước khi thêm, do ràng buộc unique //quanLyDonHang nay bên phía người bán
                    quanLyDonHangDao.Them(quanLyDonHang);

                    trangThaiDonHangDao.Xoa(dong);  //Trạng thái don hàng bên phía người mua
                    trangThaiDonHangDao.Them(dong);

                    gioHangDao.Xoa(dong.IdSanPham, dong.IdNguoiMua); // Xóa khỏi giỏ hàng sau khi thanh toán
                }

                double tienTT = Convert.ToDouble(ngDung.Tien) - Convert.ToDouble(tongThanhToan);
                if (Convert.ToDouble(tongThanhToan) == 0)
                    MessageBox.Show("Xin hãy chọn món đồ thanh toán", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                if (tienTT < 0)
                    MessageBox.Show("Số tiền trong tài khoản của bạn không đủ vui lòng nạp thêm!!!!", "Thông báo",MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    gdichDao.CapNhatSoTien(tienTT.ToString(), ngDung.Id);
                    co = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (co)
                MessageBox.Show("Thanh toán thành công");
        }
        private void capNhatThongTinCaNhan() // ???????????, không biết sửa sao luôn, tại chưa coi mấy cái model check này
        {
            NguoiDung user = new NguoiDung(ngDung.Id, txtHoTen.Text, ngDung.GioiTinh, ngDung.NgaySinh, ngDung.Cmnd, txtEmail.Text, txtSoDienThoai.Text, txtDiaChi.Text, ngDung.Anh, ngDung.TaiKhoan, ngDung.Tien);
            ThongTinKhachHangViewModel ttkh = new ThongTinKhachHangViewModel(user);
            //bool check = ttkh.kiemTraCacTextBox();
            //if(check)
            //{
                try
                {
                    ngDungDao.CapNhatDiaChi(user);
                    NguoiDung nguoi  = ngDungDao.TimKiemThongTinTheoIdNguoi(ngDung.Id);
                    txtHoTen.Text = nguoi.HoTen;
                    txtSoDienThoai.Text = nguoi.Sdt;
                    txtEmail.Text = nguoi.Email;
                    txtDiaChi.Text = nguoi.DiaChi;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            //}
            
        }

        private void FDiaChi_Loaded(object sender, RoutedEventArgs e)
        {
            txtHoTen.Text = ngDung.HoTen.ToString();
            txtSoDienThoai.Text = ngDung.Sdt.ToString();
            txtEmail.Text = ngDung.Email.ToString();
            txtDiaChi.Text = ngDung.DiaChi.ToString();
        }
    }
}
