using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraoDoiDo.Models;
using TraoDoiDo.ViewModels;
using TraoDoiDo.Database;
using System.Xml.Linq;
namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for MuaDo.xaml
    /// </summary>
    public partial class MuaDoUC : UserControl
    {
        private int soLuongSP = 0;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        private SanPhamUC[] DanhSachSanPham = new SanPhamUC[100];
        private SanPhamUC[] DanhSachSanPhamYeuThich = new SanPhamUC[100];
        NguoiDung ngDung = new NguoiDung(); //Người mua
        SanPhamDao sanPhamDao = new SanPhamDao();
        GioHangDao gioHangDao = new GioHangDao();
        TrangThaiDonHangDao trangThaiDonHangDao = new TrangThaiDonHangDao();
        QuanLyDonHangDao quanLyDonHangDao = new QuanLyDonHangDao();
        List<TrangThaiDonHang> dsSanPhamDeThanhToan = new List<TrangThaiDonHang>(); 
        public MuaDoUC()
        {
            InitializeComponent();
            Loaded += MuaSam_Load; // TAB1
            Loaded += GioHang_Load; //TAB2
            Loaded += TrangThaiDonHang_Load; //TAB3
        } 
        public MuaDoUC(NguoiDung kh)
        {
            InitializeComponent();
            Loaded += MuaSam_Load; // TAB1
            Loaded += GioHang_Load; //TAB2
            Loaded += TrangThaiDonHang_Load; //TAB3
            ngDung = kh;
        }

        #region TAB1 MUA SẮM
        private void MuaSam_Load(object sender, RoutedEventArgs e)
        {
            LoadSanPhamlenWpnlHienThi();
            LoadSanPhamTheoYeuThich();
            SapXepGiamDanTheoSoLuotXem();
            //SapXeoTheoYeuThich();
        }
        private void txbTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbTimKiem.Text.Trim() != "")
            {
                wpnlHienThi.Children.Clear();
                for (int i = 0; i < soLuongSP; i++)
                {
                    string tenSP = DanhSachSanPham[i].txtbTen.Text.ToLower();
                    string timKiem = txbTimKiem.Text.Trim().ToLower();
                    if (tenSP.Contains(timKiem))
                    {
                        wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                    }
                }
            }
            else
            {
                wpnlHienThi.Children.Clear();
                for (int i = 0; i < soLuongSP; i++)
                {
                    wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                }
            }
        }

        private void SapXeoTheoYeuThich()
        {
            LoadSanPhamlenWpnlHienThi(); //Cái nói chung cái gì dính tới yêu thích thì cứ load lại 1 cái, mấy cái lọc với tìm kiếm không cần là do tính năng ẩn hiện cái nút của bên UC có sẵn r :)))
            wpnlHienThi.Children.Clear();
            for (int i = 0; i < soLuongSP; i++)
                if (DanhSachSanPham[i].yeuThich == 1)
                {
                    wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                }
            for (int i = 0; i < soLuongSP; i++)
                if (DanhSachSanPham[i].yeuThich == 0)
                {
                    wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                }
        }
        private void LoadSanPhamTheoYeuThich()
        {
            soLuongSP = 0;
            try
            {
                
                List<SanPham> dsSanPham = sanPhamDao.LoadSanPham(ngDung.Id); // Đây là idNguoiMua 

                foreach (var dong in dsSanPham)
                {
                    int yeuThich = 1;

                    DanhSachSanPhamYeuThich[soLuongSP] = new SanPhamUC(yeuThich, ngDung.Id); // Khởi tạo mỗi phần tử của mảng (KHÔNG CÓ LÀ LỖI)

                    DanhSachSanPhamYeuThich[soLuongSP].txtbIdSanPham.Text = dong.Id;
                    DanhSachSanPhamYeuThich[soLuongSP].txtbTen.Text = dong.Ten;

                    string tenFileAnh = dong.LinkAnh;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    string duongDanhAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);
                    bitmap.UriSource = new Uri(duongDanhAnh);
                    bitmap.EndInit();
                    // Gán BitmapImage tới Source của Image control
                    DanhSachSanPhamYeuThich[soLuongSP].imgSP.Source = bitmap;

                    DanhSachSanPhamYeuThich[soLuongSP].txtbGiaGoc.Text = dong.GiaGoc;
                    DanhSachSanPhamYeuThich[soLuongSP].txtbGiaBan.Text = dong.GiaBan;
                    DanhSachSanPhamYeuThich[soLuongSP].txtbNoiBan.Text = dong.NoiBan;
                    DanhSachSanPhamYeuThich[soLuongSP].txtbSoLuotXem.Text = dong.LuotXem;
                    DanhSachSanPhamYeuThich[soLuongSP].idNguoiDang = dong.IdNguoiDang;
                    DanhSachSanPhamYeuThich[soLuongSP].txtbLoai.Text = dong.Loai;

                    //SanPham sanPham = new SanPham(id, name, imageUrl); 
                    //lsvQuanLySanPham.Items.Add(sanPham);
                    DanhSachSanPhamYeuThich[soLuongSP].Margin = new Thickness(8);
                    if (DanhSachSanPham[soLuongSP].yeuThich == 1)
                    {
                            wpnlHienThuUaChuong.Children.Add(DanhSachSanPhamYeuThich[soLuongSP]);
                     }
                    soLuongSP++;
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSapXepTheoLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
                if (selectedItemContent == "Tất cả")
                {
                    wpnlHienThi.Children.Clear();
                    for (int i = 0; i < soLuongSP; i++)
                    {
                        wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                    }
                }
                else
                {
                    wpnlHienThi.Children.Clear();
                    for (int i = 0; i < soLuongSP; i++)
                    {
                        if (DanhSachSanPham[i].txtbLoai.Text.Contains(selectedItemContent))
                        {
                            wpnlHienThi.Children.Add(DanhSachSanPham[i]);
                        }
                    }
                }
            }
        }


        private void cboSapXepTheoYeuThich_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
                if (selectedItemContent == "Yêu thích")
                {
                    SapXeoTheoYeuThich();
                }
                else
                {
                    SapXepGiamDanTheoSoLuotXem();
                }
            }
        }
        private void LoadSanPhamlenWpnlHienThi()
        {
            soLuongSP = 0;
            try
            {
                List<SanPham> dsSanPham = sanPhamDao.LoadSanPham(ngDung.Id); // Đây là idNguoiMua 

                foreach(var dong in dsSanPham)
                {
                    int yeuThich = 0;
                  
                    if (!string.IsNullOrEmpty(dong.IdNguoiMua)) // Id người mua này của bảng yêu thích ,Neu nguoi mua co trong bang yeu thich (tức đang yêu thich một sản phẩm nào đó)
                    {
                        yeuThich = 1;

                    }
                    
                    DanhSachSanPham[soLuongSP] = new SanPhamUC(yeuThich, ngDung.Id); // Khởi tạo mỗi phần tử của mảng (KHÔNG CÓ LÀ LỖI)

                    DanhSachSanPham[soLuongSP].txtbIdSanPham.Text = dong.Id;
                    DanhSachSanPham[soLuongSP].txtbTen.Text = dong.Ten;

                    string tenFileAnh = dong.LinkAnh;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    string duongDanhAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);
                    bitmap.UriSource = new Uri(duongDanhAnh);
                    bitmap.EndInit();
                    // Gán BitmapImage tới Source của Image control
                    DanhSachSanPham[soLuongSP].imgSP.Source = bitmap;

                    DanhSachSanPham[soLuongSP].txtbGiaGoc.Text = dong.GiaGoc;
                    DanhSachSanPham[soLuongSP].txtbGiaBan.Text = dong.GiaBan;
                    DanhSachSanPham[soLuongSP].txtbNoiBan.Text = dong.NoiBan;
                    DanhSachSanPham[soLuongSP].txtbSoLuotXem.Text = dong.LuotXem;
                    DanhSachSanPham[soLuongSP].idNguoiDang = dong.IdNguoiDang;
                    DanhSachSanPham[soLuongSP].txtbLoai.Text = dong.Loai;

                    //SanPham sanPham = new SanPham(id, name, imageUrl); 
                    //lsvQuanLySanPham.Items.Add(sanPham);
                    DanhSachSanPham[soLuongSP].Margin = new Thickness(8);
                    wpnlHienThi.Children.Add(DanhSachSanPham[soLuongSP]);
                    soLuongSP++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SapXepGiamDanTheoSoLuotXem()
        {
            for (int i = 0; i < soLuongSP - 1; i++)
                for (int j = i + 1; j < soLuongSP; j++)
                    if (Convert.ToInt32(DanhSachSanPham[i].txtbSoLuotXem.Text) < Convert.ToInt32(DanhSachSanPham[j].txtbSoLuotXem.Text))
                    {
                        int yeuThich = 0;
                        SanPhamUC spTam = new SanPhamUC(yeuThich, ngDung.Id);
                        spTam = DanhSachSanPham[i];
                        DanhSachSanPham[i] = DanhSachSanPham[j];
                        DanhSachSanPham[j] = spTam;
                    }
            wpnlHienThi.Children.Clear();
            for (int i = 0; i < soLuongSP; i++)
            {
                wpnlHienThi.Children.Add(DanhSachSanPham[i]);
            }
        }
        #endregion

        #region TAB2 GIỎ HÀNG


        private void GioHang_Load(object sender, RoutedEventArgs e)
        {
            LsvGioHang_Load(sender, e);
        }

        private void LsvGioHang_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                List<GioHang> dsGioHang = gioHangDao.timThongTinSanPhamTheoIDNguoiMua(ngDung.Id);
                lsvGioHang.Items.Clear();
                foreach(var dong in dsGioHang)
                {
                    string tenFileAnh =dong.AnhSP;
                    string linkAnhBia = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);

                    int soLuong = Convert.ToInt32(dong.SoLuongTong); // số lượng Tổng
                    int soLuongDaBan = Convert.ToInt32(dong.SoLuongDaBan);
                    string trangThai = "";
                    
                    if (soLuongDaBan >= soLuong)
                        trangThai = "Hết hàng";
                    else
                        trangThai = "Còn hàng";
                    lsvGioHang.Items.Add(new { IdSP = dong.IdSanPham, TenSP = dong.Ten, LinkAnhBia = linkAnhBia, Gia = dong.GiaBan, PhiShip = dong.PhiShip, SoLuongMua = dong.SoLuongMua, TrangThaiConHayHet = trangThai });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaKhoiGioHang_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button btn = sender as Button;

            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;

                if (dataItem != null)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục đã chọn?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        { 
                            gioHangDao.Xoa(dataItem.IdSP, ngDung.Id);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa sản phẩm: " + ex.Message);
                        }
                        finally
                        {
                            GioHang_Load(sender, e);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xác định dòng chứa nút 'Xóa'.");
            }
        }

        // Hàm trợ giúp để tìm thành phần cha của một kiểu cụ thể
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null)
                return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            DiaChi f = new DiaChi(ngDung, dsSanPhamDeThanhToan); //truyền vào người mua
            f.tongThanhToan = txtbTongThanhToan.Text;
            f.ShowDialog();
        }

        private void ChonTatCaCacDong_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem CheckBox đã được chọn hay không
            if (sender is CheckBox selectAllCheckBox && selectAllCheckBox.IsChecked.HasValue)
            {
                // Lặp qua mỗi mục trong ListView để đặt trạng thái chọn tương ứng
                foreach (var item in lsvGioHang.Items)
                {
                    // Lấy ListViewItem từ mỗi mục
                    ListViewItem listViewItem = lsvGioHang.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                    if (listViewItem != null)
                    {
                        // Lấy CheckBox từ CellTemplate của mỗi mục
                        CheckBox itemCheckBox = FindVisualChild<CheckBox>(listViewItem);
                        if (itemCheckBox != null)
                        {
                            // Đặt trạng thái của CheckBox theo trạng thái của CheckBox chọn tất cả
                            itemCheckBox.IsChecked = selectAllCheckBox.IsChecked;
                        }
                    }
                }
            }
            TinhTienCuaNhungDongDuocChon(sender, e);
        }
        // Phương thức hỗ trợ để tìm kiếm một đối tượng theo kiểu trong một VisualTree
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void TinhTienCuaNhungDongDuocChon(object sender, RoutedEventArgs e)
        {
            double tongTienHang = 0;
            double tongTienShip = 0;
            double tongThanhToan = 0;
            foreach (var dongDuocChon in lsvGioHang.SelectedItems)
            {
                dynamic dong = dongDuocChon;

                string idSP = dong.IdSP;
                string tenSP = dong.TenSP;

                string giaBan = dong.Gia;
                string phiShip = dong.PhiShip;
                string soLuongMua = dong.SoLuongMua;
                //Không cần cột Ảnh (tên cũng k cần ha j á, lấy cho zui :))


                string ngayThanhToan = DateTime.Now.ToString("dd/MM/yyyy");
                string trangThai = "Chờ xác nhận";

                 
                tongTienHang += Convert.ToDouble(giaBan) * Convert.ToInt32(soLuongMua);
                tongTienShip += Convert.ToDouble(phiShip); 

                tongThanhToan = tongTienHang + tongTienShip;

                //mang[0] = ngDung.Id;
                //mang[1] = item.IdSP;
                //mang[2] = soLuongMua.ToString();
                //mang[3] = (tongTienhang + tongTienShip).ToString();
                //mang[4] = DateTime.Now.ToString();
                //mang[5] = "Chờ xác nhận"; 
                dsSanPhamDeThanhToan.Add(new TrangThaiDonHang(ngDung.Id, idSP, soLuongMua, tongThanhToan.ToString(), ngayThanhToan, trangThai, tenSP, null, giaBan, phiShip, null, null, null, null));
            }

            txtbTongTienHang.Text = tongTienHang.ToString();
            txtbTongTienShip.Text = tongTienShip.ToString();
            txtbTongThanhToan.Text = tongThanhToan.ToString();
        }
        #endregion


        #region TAB3 TRẠNG THÁI ĐƠN HÀNG

        private void TrangThaiDonHang_Load(object sender, RoutedEventArgs e)
        {
            LoadLsvTrongTabTrangThaiDonHang("lsvChoNguoiBanXacNhan", "Chờ xác nhận");
            LoadLsvTrongTabTrangThaiDonHang("lsvChoGiaoHang", "Chờ giao hàng");
            LoadLsvTrongTabTrangThaiDonHang("lsvDaNhan", "Đã nhận");
        }
        private void LoadLsvTrongTabTrangThaiDonHang(string tenLsv, string trangthai)
        {
            try
            {
                //Load Form TrangThaiDonHang 
                List<TrangThaiDonHang> dsTrangThaiDon = trangThaiDonHangDao.LoadTrangThaiDonHang(ngDung.Id, trangthai);

                if (tenLsv == "lsvChoNguoiBanXacNhan")
                    lsvChoNguoiBanXacNhan.Items.Clear();
                else if (tenLsv == "lsvChoGiaoHang")
                    lsvChoGiaoHang.Items.Clear();
                else if (tenLsv == "lsvDaNhan")
                    lsvDaNhan.Items.Clear();

                foreach(var dong in dsTrangThaiDon)
                {
                    string tenFileAnh = dong.AnhSP;
                    string linkAnhBia = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);

                    if (tenLsv == "lsvChoNguoiBanXacNhan")
                        lsvChoNguoiBanXacNhan.Items.Add(new { IdSP = dong.IdSanPham, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, Gia = dong.GiaBan, PhiShip = dong.PhiShip, SoLuongMua =dong.SoLuongMua, TongThanhToan = dong.TongThanhToan, Ngay = dong.Ngay });
                    else if (tenLsv == "lsvChoGiaoHang")
                        lsvChoGiaoHang.Items.Add(new { IdSP = dong.IdSanPham, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, Gia = dong.GiaBan, PhiShip = dong.PhiShip, SoLuongMua = dong.SoLuongMua, TongThanhToan = dong.TongThanhToan, Ngay = dong.Ngay });
                    else if (tenLsv == "lsvDaNhan")
                        lsvDaNhan.Items.Add(new { IdSP = dong.IdSanPham, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, Gia = dong.GiaBan, PhiShip = dong.PhiShip, SoLuongMua = dong.SoLuongMua, TongThanhToan = dong.TongThanhToan, Ngay = dong.Ngay });
                    //SanPham sanPham = new SanPham(id, name, imageUrl); 
                    //lsvQuanLySanPham.Items.Add(sanPham);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHuyDatHang_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button btn = sender as Button;

            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;

                if (dataItem != null)
                {
                    if (MessageBox.Show("Bạn có chắc là muốn hủy đặt hàng 0_o\nĐơn hàng mà bạn hủy sẽ được hoàn tiền", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            // Xóa dữ liệu từ bảng TrangThaiDongHang
                            TrangThaiDonHang trangThaiDonHang = new TrangThaiDonHang(ngDung.Id,dataItem.IdSP,dataItem.SoLuongMua,dataItem.TongThanhToan,dataItem.Ngay,null, null, null, null, null, null, null, null, null);
                            trangThaiDonHangDao.Xoa(trangThaiDonHang);

                            // Xóa dữ liệu  khỏi bảng QuanLyDonHang 
                            QuanLyDonHang quanLyDonHang = new QuanLyDonHang(null, null, ngDung.Id, dataItem.IdSP, null, null);
                            quanLyDonHangDao.Xoa(quanLyDonHang);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa: " + ex.Message);
                        }
                        finally
                        {
                            TrangThaiDonHang_Load(sender, e);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xác định dòng chứa nút 'Xóa'.");
            }
        }


        private void btnDaNhanHang_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button btn = sender as Button;

            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;

                if (dataItem != null)
                {
                    if (MessageBox.Show("Bạn có chắc là đã nhận được hàng 0_o", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            conn.Open();
                            TrangThaiDonHang trangThaiDonHang = new TrangThaiDonHang(ngDung.Id,dataItem.IdSP,dataItem.SoLuongMua,dataItem.TongThanhToan,dataItem.Ngay,"Đã nhận", null, null, null, null, null, null, null, null);
                            trangThaiDonHangDao.CapNhat(trangThaiDonHang);

                            QuanLyDonHang quanLyDonHang = new QuanLyDonHang(null,null,ngDung.Id,dataItem.IdSP,"Đã giao",null);
                            quanLyDonHangDao.CapNhat(quanLyDonHang);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra: " + ex.Message);
                        }
                        finally
                        {
                            TrangThaiDonHang_Load(sender, e);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xác định dòng chứa nút .");
            }
        }

        private void btnDanhGia_Click(object sender, RoutedEventArgs e)
        {
            DanhGia f = new DanhGia();
            f.idNguoiMua = ngDung.Id;

            // Lấy button được click
            Button btn = sender as Button;

            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;

                if (dataItem != null)
                {

                    try
                    {
                        NguoiDung nguoiDang = sanPhamDao.timKiemNguoiDangTheoIdSP(dataItem.IdSP);
                        f.idNguoiDang = nguoiDang.Id;
                        f.txtbTenNguoiDang.Text = nguoiDang.HoTen;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xảy ra: " + ex.Message);
                    }
                    finally
                    {
                        TrangThaiDonHang_Load(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xác định dòng chứa nút .");
            }

            f.ShowDialog();
        }



        private void btnTraHang_Click(object sender, RoutedEventArgs e)
        {
            ucLyDoTraHang.idNguoiMua = ngDung.Id;

            // Lấy button được click
            Button btn = sender as Button;

            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;

                if (dataItem != null)
                {
                    ucLyDoTraHang.idSP = dataItem.IdSP;
                    ucLyDoTraHang.DrawerClosed += (btnSender, args) =>
                    {

                        LoadLsvTrongTabTrangThaiDonHang("lsvChoGiaoHang", "Chờ giao hàng");
                    };
                }
            }
            else
            {
                MessageBox.Show("Không thể xác định dòng chứa nút 'Xóa'.");
            }
        }


        #endregion

        private void tabItemMuaSam_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += MuaSam_Load;
        }

        private void tabItemGioHang_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += GioHang_Load;
        }

        private void tabViewTrangThaiDonHang_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += TrangThaiDonHang_Load;
        }
    }
}
