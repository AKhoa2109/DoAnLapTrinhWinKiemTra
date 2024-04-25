using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
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
using TraoDoiDo.Models;
using TraoDoiDo.Database;
using TraoDoiDo.ViewModels;
using MaterialDesignThemes.Wpf;
namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for QuanLy.xaml
    /// </summary>
    public partial class QuanLyUC : UserControl
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        List<NguoiDung> listNguoiDung = new List<NguoiDung>();
        private TaiKhoanDao tkDao = new TaiKhoanDao();
        NguoiDungDao ngDungDao = new NguoiDungDao();
        DanhGiaNguoiDangDao danhGiaDao = new DanhGiaNguoiDangDao();
        public QuanLyUC()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += QuanLyUC_Loaded;
        }
        public QuanLyUC(NguoiDung nguoiDung)
        {
            InitializeComponent();
            DataContext = this;
            Loaded += QuanLyUC_Loaded;
        }
        private void QuanLyUC_Loaded(object sender, RoutedEventArgs e)
        {
            //QUAN LY SAN PHAM
            HienThi_QuanLySanPham();
            HienThi_QuanLyNguoiDung();

        }
        private void HienThi_QuanLySanPham()
        {
            try
            {
                conn.Open();
                string sqlStr = "SELECT IdSanPham, Ten,LinkAnhBia,Loai,SoLuong,SoLuongDaBan,GiaGoc,GiaBan,PhiShip,TrangThai FROM SanPham";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string linkAnh = reader.GetString(2);
                    string loai = reader.GetString(3);
                    string sl = reader.GetString(4);
                    string slDaBan = reader.GetString(5);
                    string giaBanGoc = reader.GetString(6);
                    string giaBan = reader.GetString(7);
                    string phiShip = reader.GetString(8);
                    string trangThai = reader.GetString(9);
                    lsvQuanLySanPham.Items.Add(new { Id = id.ToString(), Name = name, Anh = linkAnh, Type = loai, Quantity = sl, DaBan = slDaBan, GiaGoc = giaBanGoc, GiaBan = giaBan, ShippingFee = phiShip, TrangThai = trangThai });


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void HienThi_QuanLyNguoiDung()
        {
            try
            {
                conn.Open();
                string sqlStr = "SELECT IdNguoiDung, HoTenNguoiDung,CMNDNguoiDung,GioiTinhNguoiDung,SdtNguoiDung,NgaySinhNguoiDung,DiaChiNguoiDung,EmailNguoiDung,AnhNguoiDung FROM NguoiDung";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NguoiDung nguoiDung = new NguoiDung(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(3), reader.GetString(5), reader.GetString(2), reader.GetString(7), reader.GetString(4), reader.GetString(6), reader.GetString(8), tkDao.TimKiemBangId(reader.GetInt32(0).ToString()), "");
                    listNguoiDung.Add(nguoiDung);
                    lsvQuanLyNguoiDung.Items.Add(new { UserId = nguoiDung.Id, FullName = nguoiDung.HoTen, Identification = nguoiDung.Cmnd, Gender = nguoiDung.GioiTinh, PhoneNumber = nguoiDung.Sdt, DateOfBirth = nguoiDung.NgaySinh, Address = nguoiDung.DiaChi, Email = nguoiDung.Email });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //Tab Quản lý người dùng


        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                dynamic dataItem = item.DataContext;
                int index = lsvQuanLyNguoiDung.Items.IndexOf(dataItem);
                ThongTinNguoiDang f = new ThongTinNguoiDang(listNguoiDung[index].Id);
                f.Show();
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }


        private void btnDuyet_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nếu chưa duyệt thì chuyển sang đã duyệt\nNếu đã duyệt rồi, thì khi ấn nút này nó báo là sp đã được duyệt(hoặc vô hiệu hóa cái nút được thì càng tốt)");
        }
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null)
                return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        private void cbSoSao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ComboBox comboBox = sender as ComboBox;
            try
            {
                lsvQuanLyNguoiDung.Items.Clear();
                string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem).Content.ToString().Trim();
                if (string.Equals(selectedItemContent, "Tất cả"))
                    HienThi_QuanLyNguoiDung();
                else
                {
                    HienThiNguoiDangUyTien(selectedItemContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HienThiNguoiDangUyTien(string selectedItemContent)
        {
            List<string> soSaoTB = new List<string>();
            
            List<DanhGiaNguoiDang> dsDanhGiaSoSao = danhGiaDao.TinhTrungBinhSoSao(selectedItemContent);
            foreach (var danhGia in dsDanhGiaSoSao)
            {
                    NguoiDung nguoiDung = danhGiaDao.LoadThongTinNguoiDang(danhGia.IdNguoiDang);
                    lsvQuanLyNguoiDung.Items.Add(new { UserId = nguoiDung.Id, FullName = nguoiDung.HoTen, Identification = nguoiDung.Cmnd, Gender = nguoiDung.GioiTinh, PhoneNumber = nguoiDung.Sdt, DateOfBirth = nguoiDung.NgaySinh, Address = nguoiDung.DiaChi, Email = nguoiDung.Email });
            }
           
        }

        private void cbNgayMua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            try
            {
                lsvQuanLyNguoiDung.Items.Clear();
                string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem).Content.ToString().Trim();
                if (string.Equals(selectedItemContent, "Tất cả"))
                    HienThi_QuanLyNguoiDung();
                else
                {
                    HienThiNguoiDangUyTien(selectedItemContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
