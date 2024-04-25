using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels;
using TraoDoiDo.ViewModels;
using System.Reflection;
using TraoDoiDo.Models;
using TraoDoiDo.Database; 



namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for ThongTinChiTietSanPham.xaml
    /// </summary>
    public partial class ThongTinChiTietSanPham : Window
    {
        public string idNguoiMua;
       
        public string idNguoiDang;
        public string idSanPham ;
        private SanPhamUC[] DanhSachSanPhamUC ;
        public string linkAnhBia = "";
        SanPham sanPham = new SanPham(); // sanPham là dùng chung, sp là dùng khi gọi DAO
        
        SanPhamDao spDao = new SanPhamDao();
        QuanLyDonHangDao quanLyDonHangDao = new QuanLyDonHangDao();
        NguoiDungDao khDao = new NguoiDungDao();
        private List<string> danhSachAnh = new List<string>();

        private int currentIndex = -1;
        public ObservableCollection<ListViewItem> Items { get; set; }

        public ThongTinChiTietSanPham()
        { 
            InitializeComponent();
            
            Loaded += LoadAnhVaMoTa;
            Loaded += LoadThongTinSanPham;
            Loaded += btnAnhSau_Click;
            Loaded += LoadThongTinNguoiDang;
        }

        public ThongTinChiTietSanPham(SanPham sp)
        { 
            sanPham = sp;
         
            InitializeComponent();
            
            Loaded += LoadThongTinSanPham;
            Loaded += LoadAnhVaMoTa;
            Loaded += btnAnhSau_Click;
            Loaded += LoadThongTinNguoiDang;
            
          
        }

        #region THÊM SẢN PHẨM CÙNG LOẠI VÀO MỤC KHÁM PHÁ THÊM


        


        private void LoadSanPhamlenWpnlHienThi(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SanPham> dsSanPhamCungLoai = spDao.LoadSanPhamCungLoai(sanPham);
                DanhSachSanPhamUC = new SanPhamUC[dsSanPhamCungLoai.Count + 1];
                wpnlHienThiSPCungLoai.Children.Clear();
                int i = 0;
                foreach(var dong in dsSanPhamCungLoai)
                {
                    int yeuThich = 0;
                    if (!string.IsNullOrEmpty(dong.IdNguoiMua)) //Neu nguoi mua co trong bang yeu thich (tức đang yêu thich một sản phẩm nào đó)
                    {
                        yeuThich = 1;
                    }
                    DanhSachSanPhamUC[i] = new SanPhamUC(yeuThich, idNguoiMua);

                    DanhSachSanPhamUC[i].txtbIdSanPham.Text = dong.Id.ToString();
                    DanhSachSanPhamUC[i].txtbTen.Text = dong.Ten;

                    string tenFileAnh = dong.LinkAnh;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    string duongDanhAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);
                    bitmap.UriSource = new Uri(duongDanhAnh);
                    bitmap.EndInit();
                    // Gán BitmapImage tới Source của Image control
                    DanhSachSanPhamUC[i].imgSP.Source = bitmap;

                    DanhSachSanPhamUC[i].txtbGiaGoc.Text = dong.GiaGoc;
                    DanhSachSanPhamUC[i].txtbGiaBan.Text = dong.GiaBan;
                    DanhSachSanPhamUC[i].txtbNoiBan.Text = dong.NoiBan;
                    DanhSachSanPhamUC[i].txtbLoai.Text = dong.Loai;

                    //SanPham sanPham = new SanPham(id, name, imageUrl); 
                    //lsvQuanLySanPham.Items.Add(sanPham);
                    DanhSachSanPhamUC[i].Margin = new Thickness(8);
                    DanhSachSanPhamUC[i].btnBoYeuThich.Visibility = Visibility.Collapsed;
                    DanhSachSanPhamUC[i].btnThemVaoYeuThich.Visibility = Visibility.Collapsed;
                    wpnlHienThiSPCungLoai.Children.Add(DanhSachSanPhamUC[i]);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void LoadThongTinNguoiDang(object sender, RoutedEventArgs e)
        {
            string linkAnh = khDao.TimKiemLinkAnh(idNguoiDang);
            imgAnhNguoiDang.Source = new BitmapImage(new Uri(XuLyAnh.layDuongDanDayDuToiFileAnhDaiDien(linkAnh)));
            
            DanhGiaNguoiDangDao nguoiDangDao = new DanhGiaNguoiDangDao();
            DanhGiaNguoiDang nguoiDang = nguoiDangDao.timTenNguoiDangVaNhanXet(idNguoiDang);
            txtbTenNguoiDang.Text = nguoiDang.TenNguoiDang;
            txtbSoLuotDanhGia.Text = nguoiDang.SoLuotDanhGia;
        }


        private void LoadAnhVaMoTa(object sender, RoutedEventArgs e)
        {
            try
            {
                List<MoTaAnhSanPham> dsMoTaAnh = spDao.LoadAnhVaMoTa(sanPham);

                foreach (var dong in dsMoTaAnh)
                {
                    linkAnhBia = dong.LinkAnhBia; //Cập nhật biến toàn cục(global)    linkAnhBia đã khai báo đầu trang
                    string moTa = dong.MoTa;
                    string linkAnhMinhHoa = XuLyAnh.layDuongDanDayDuToiFileAnh(dong.LinkAnhMinhHoa);
                    danhSachAnh.Add(linkAnhMinhHoa); //Lấy đường dẫn để bỏ vào hình bên trên
                    if (moTa.Trim() == "")
                        continue;
                    //lsvThongTinChiTietSP.Items.Add(new { LinkAnhMinhHoa = linkAnhMinhHoa, MoTa = moTa });
                    lsvThongTinChiTietSP.Items.Add(new { MoTa = "- " + moTa }); 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadThongTinSanPham(object sender, RoutedEventArgs e)
        {
            try
            {
               
                SanPham sp = spDao.LoadThongTinMotSanPhamTheoid(sanPham.Id);
                string soLuong = sp.SoLuong;
                string soLuongDaBan = sp.SoLuongDaBan;
                txtbSoLuongConLai.Text = (Convert.ToInt32(soLuong) - Convert.ToInt32(soLuongDaBan)).ToString();
                txtbTen.Text = sp.Ten;
                txtbLoai.Text = sp.Loai;
                txtbGiaGoc.Text = sp.GiaGoc;
                txtbGiaBan.Text = sp.GiaBan;
                txtbPhiShip.Text = sp.PhiShip;
                txtbNoiBan.Text = sp.NoiBan;
                txtbXuatXu.Text = sp.XuatXu;
                txtbNgayMua.Text = sp.NgayMua;
                txtbPhanTramConMoi.Text = sp.PhanTramMoi;
                txtbMoTaChung.Text = sp.MoTaChung;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { 
                LoadSanPhamlenWpnlHienThi(sender, e);
            }
        }

        private void ThongTinNguoiDang_Click(object sender, RoutedEventArgs e)
        {
            ThongTinNguoiDang f = new ThongTinNguoiDang(idNguoiDang.ToString());
            f.Show();
        }


        private void DisplayImage()
        {
            // Load and display the current image
            string imagePath = danhSachAnh[currentIndex];
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imagePath);
            bitmapImage.EndInit();

            imgAnhSP.Source = bitmapImage;
        }


        private void btnAnhTruoc_Click(object sender, RoutedEventArgs e)
        {
            if (danhSachAnh.Count > 0)
            {
                // Move to the previous image
                currentIndex--;

                // Check if we have reached the beginning of the list
                if (currentIndex < 0)
                {
                    // Set currentIndex to the last image index
                    currentIndex = danhSachAnh.Count - 1;
                }

                DisplayImage();
            }
        }

        private void btnAnhSau_Click(object sender, RoutedEventArgs e)
        {
            if (danhSachAnh.Count > 0)
            {
                // Move to the next image
                currentIndex++;

                // Check if we have reached the end of the list
                if (currentIndex >= danhSachAnh.Count)
                {
                    // Set currentIndex back to the first image index
                    currentIndex = 0;
                }

                DisplayImage();
            }
        }


        private void btnThemVaoGioHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SanPham sp = spDao.timKiemToanBoBangId(sanPham.Id);
                
                GioHang gioHang = new GioHang(idNguoiMua, idSanPham, txtbSoLuongHienTai.Text, null, null, null, null, null, null);
                GioHangDao gioHangDao = new GioHangDao();
                gioHangDao.Xoa(gioHang.IdSanPham, gioHang.IdNguoiMua);
                gioHangDao.Them(gioHang);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnTang_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtbSoLuongHienTai.Text);
            if (n + 1 <= Convert.ToInt32(txtbSoLuongConLai.Text))
            {
                n += 1;
            }

            txtbSoLuongHienTai.Text = n.ToString();


        }

        private void btnGiam_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtbSoLuongHienTai.Text);
            if (n - 1 > 0)
            {
                n -= 1;
            }
            txtbSoLuongHienTai.Text = n.ToString();
        }
    }
}
