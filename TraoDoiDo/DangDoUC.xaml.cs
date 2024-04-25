using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TraoDoiDo.Database;
using TraoDoiDo.Models;
using TraoDoiDo.ViewModels;

namespace TraoDoiDo
{

    public partial class DangDoUC : UserControl
    {

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        NguoiDung nguoiDung = new NguoiDung();
        List<SanPham> dsSanPham;
        SanPhamDao sanPhamDao = new SanPhamDao();
        MoTaHangHoaDao moTaDao = new MoTaHangHoaDao();
        QuanLyDonHangDao quanLyDonHangDao = new QuanLyDonHangDao();
        TrangThaiDonHangDao trangThaiHangDao = new TrangThaiDonHangDao();
        DanhGiaNguoiDangDao danhGiaNguoiDungDao = new DanhGiaNguoiDangDao();

        public DangDoUC()
        {
            InitializeComponent();
            Loaded += QuanLySanPham_Load;
            Loaded += QuanLyDonHang_Load;
            Loaded += ThongKe_Load;
        }

        public DangDoUC(NguoiDung kh)
        {
            InitializeComponent();
            Loaded += QuanLySanPham_Load;
            Loaded += QuanLyDonHang_Load;
            Loaded += ThongKe_Load;
            nguoiDung = kh;
        }
        #region TAB1 QUẢN LÝ SẢN PHẨM
        private void QuanLySanPham_Load(object sender, RoutedEventArgs e) //Form load của Tab1
        {
            HienThi_QuanLySanPham();
        }

        private void HienThi_QuanLySanPham()
        {
            lsvQuanLySanPham.Items.Clear();
            dsSanPham = sanPhamDao.timKiemBangId(nguoiDung.Id.ToString());
            foreach (var sanPham in dsSanPham)
            { 
                string tenAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(sanPham.LinkAnh);
                lsvQuanLySanPham.Items.Add(new { Id = sanPham.Id.ToString(), Ten = sanPham.Ten.ToString(), LinkAnh = tenAnh, Loai = sanPham.Loai.ToString(), SoLuong = sanPham.SoLuong.ToString(), SoLuongDaBan = sanPham.SoLuongDaBan.ToString(), GiaGoc = sanPham.GiaGoc.ToString(), GiaBan = sanPham.GiaBan.ToString(), PhiShip = sanPham.PhiShip.ToString() });
            }
        }


        private void txbTiemKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lsvQuanLySanPham.Items.Clear(); 
                if (txbTimKiem.Text == null)
                    HienThi_QuanLySanPham();
                dsSanPham = sanPhamDao.timKiemBangTen(txbTimKiem.Text.Trim(), nguoiDung.Id);
                foreach (var sanPham in dsSanPham)
                {
                    string tenAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(sanPham.LinkAnh);
                    lsvQuanLySanPham.Items.Add(new { Id = sanPham.Id.ToString(), Ten = sanPham.Ten.ToString(), LinkAnh = tenAnh, Loai = sanPham.Loai.ToString(), SoLuong = sanPham.SoLuong.ToString(), SoLuongDaBan = sanPham.SoLuongDaBan.ToString(), GiaGoc = sanPham.GiaGoc.ToString(), GiaBan = sanPham.GiaBan.ToString(), PhiShip = sanPham.PhiShip.ToString() });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbLocLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; 
            try
            {
                lsvQuanLySanPham.Items.Clear(); 
                string selectedItemContent = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
                if (string.Equals(selectedItemContent, "Tất cả"))
                    HienThi_QuanLySanPham();
                else
                {
                    dsSanPham = sanPhamDao.timKiemBangLoai(selectedItemContent, nguoiDung.Id);
                    foreach (var sanPham in dsSanPham)
                    {
                        string tenAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(sanPham.LinkAnh);
                        lsvQuanLySanPham.Items.Add(new { Id = sanPham.Id.ToString(), Ten = sanPham.Ten.ToString(), LinkAnh = tenAnh, Loai = sanPham.Loai.ToString(), SoLuong = sanPham.SoLuong.ToString(), SoLuongDaBan = sanPham.SoLuongDaBan.ToString(), GiaGoc = sanPham.GiaGoc.ToString(), GiaBan = sanPham.GiaBan.ToString(), PhiShip = sanPham.PhiShip.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
      
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button btn = sender as Button;
            bool xoaSp = false;
            bool xoaMt = false;
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
                            // Xóa dữ liệu từ bảng SanPham
                            sanPhamDao.Xoa(dataItem);
                            // Xóa dữ liệu từ lsvQuanLySanPham
                            lsvQuanLySanPham.Items.Remove(dataItem);
                            xoaSp = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa sản phẩm: " + ex.Message);
                        }
                        try
                        {
                            // Xóa dữ liệu  khỏi bảng MoTaAnhSanPham
                            moTaDao.Xoa(dataItem);
                            xoaMt = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xảy ra khi xóa sản phẩm: " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                        if (xoaSp && xoaMt)
                            MessageBox.Show("Xóa thành công");
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
        private int timIdMaxTrongBangSanPham()
        { 
            List<string> danhSachId = sanPhamDao.timKiemId();
            int max = 0;
            for (int i = 0; i < danhSachId.Count; i++)
            {
                int value = Convert.ToInt32(danhSachId[i]);
                if (value > max)
                {
                    max = value;
                }
            }
            return max;
        }
        private void btnThemSanPhamMoi_Click(object sender, RoutedEventArgs e)
        {
            DangDo_Dang f = new DangDo_Dang(nguoiDung);

            f.txtbIdSanPham.Text = (timIdMaxTrongBangSanPham() + 1).ToString();

            // DƯỚI ĐÂY LÀ CÁCH Load lại lsvQuanLySanPham sau khi (thêm sản phẩm, và đóng cái DangDo_Dang)
            f.Closed += (s, ev) =>
            {
                HienThi_QuanLySanPham();
            };
            f.Show();

        }

        private void btnSuaDo_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button btn = sender as Button;
            
            // Lấy ListViewItem chứa button
            ListViewItem item = FindAncestor<ListViewItem>(btn);

            if (item != null)
            {
                // Lấy dữ liệu của ListViewItem
                dynamic dataItem = item.DataContext;
                DangDo_Sua f = new DangDo_Sua();
                if (dataItem != null)
                {
                    //Đổ dữ liệu qua form sửa_BEGIN
                    try
                    {
                        //Đổ thông tin lên 
                        SanPham sp = sanPhamDao.timKiemToanBoBangId(dataItem.Id);
                        f = new DangDo_Sua(sp); 
                        List<MoTaHangHoa> dsMoTaHangHoa = moTaDao.TimKiemBangId(dataItem.Id);

                        //Đổ ảnh và mô tả lên
                        foreach(var moTaHangHoa in dsMoTaHangHoa) // i : item, tí tìm cái tên khác để đặt
                        {
                            f.DanhSachAnhVaMoTa[f.soLuongAnh] = new ThemAnhKhiDangUC();
                             
                            f.DanhSachAnhVaMoTa[f.soLuongAnh].txtbTenFileAnh.Text = moTaHangHoa.LinkAnh; // Rãnh sửa thuộc tính LinkAnh thành TenFileAnh

                            string duongDanAnh = XuLyAnh.layDuongDanDayDuToiFileAnh(moTaHangHoa.LinkAnh);
                            f.DanhSachAnhVaMoTa[f.soLuongAnh].txtbDuongDanAnh.Text = duongDanAnh;

                            f.DanhSachAnhVaMoTa[f.soLuongAnh].imgAnhSP.Source = new BitmapImage(new Uri(duongDanAnh));
                             
                            f.DanhSachAnhVaMoTa[f.soLuongAnh].txtbMoTa.Text = moTaHangHoa.MoTa;

                            f.wpnlChuaAnh.Children.Add(f.DanhSachAnhVaMoTa[f.soLuongAnh]);
                            f.soLuongAnh++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        f.Closed += (s, ev) =>
                        {
                            HienThi_QuanLySanPham();
                        };
                        f.Show();
                    }
                    //Đổ dữ liệu qua form sửa_END
                }
            }
        }


        #endregion


        #region TAB2 QUẢN LÝ ĐƠN HÀNG
        private void QuanLyDonHang_Load(object sender, RoutedEventArgs e)
        {
            LoadLsvTrongTabQuanLyDonHang("lsvChoDongGoi", "Chờ đóng gói");
            LoadLsvTrongTabQuanLyDonHang("lsvDangGiao", "Đang giao");
            LoadLsvTrongTabQuanLyDonHang("lsvDaGiao", "Đã giao");
            LoadLsvTrongTabQuanLyDonHang("lsvDonHangBiHoanTra", "Bị hoàn trả");
        }
        private void LoadLsvTrongTabQuanLyDonHang(string tenLsv, string trangthai)
        {
            try
            {
                List<QuanLyDonHang> dsQuanLyDonHang = new List<QuanLyDonHang>();
                dsQuanLyDonHang = quanLyDonHangDao.TimKiemTheoIdNguoiDang(nguoiDung.Id, trangthai);
                if (tenLsv == "lsvChoDongGoi")
                    lsvChoDongGoi.Items.Clear();
                else if (tenLsv == "lsvDangGiao")
                    lsvDangGiao.Items.Clear();
                else if (tenLsv == "lsvDaGiao")
                    lsvDaGiao.Items.Clear();
                else if (tenLsv == "lsvDonHangBiHoanTra")
                    lsvDonHangBiHoanTra.Items.Clear();

                foreach(var dong in dsQuanLyDonHang)
                {
                    string tenFileAnh = dong.LinkAnhBia;
                    string linkAnhBia = XuLyAnh.layDuongDanDayDuToiFileAnh(tenFileAnh);

                    if (tenLsv == "lsvChoDongGoi")
                        lsvChoDongGoi.Items.Add(new { IdSP = dong.IdSanPham, IdNguoiMua = dong.IdNguoiMua, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, SoLuongMua = dong.SoLuongMua, Gia = dong.Gia, PhiShip = dong.PhiShip, TongTien = dong.TongTien});
                    else if (tenLsv == "lsvDangGiao")
                        lsvDangGiao.Items.Add(new { IdSP = dong.IdSanPham, IdNguoiMua = dong.IdNguoiMua, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, SoLuongMua = dong.SoLuongMua, Gia = dong.Gia, PhiShip = dong.PhiShip, TongTien = dong.TongTien });
                    else if (tenLsv == "lsvDaGiao")
                        lsvDaGiao.Items.Add(new { IdSP = dong.IdSanPham, IdNguoiMua = dong.IdNguoiMua, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, SoLuongMua = dong.SoLuongMua, Gia = dong.Gia, PhiShip = dong.PhiShip, TongTien = dong.TongTien });
                    else if (tenLsv == "lsvDonHangBiHoanTra")
                        lsvDonHangBiHoanTra.Items.Add(new { IdSP = dong.IdSanPham, IdNguoiMua = dong.IdNguoiMua, TenSP = dong.TenSanPham, LinkAnhBia = linkAnhBia, SoLuongMua = dong.SoLuongMua, Gia = dong.Gia, PhiShip = dong.PhiShip, TongTien = dong.TongTien, LyDoTraHang = dong.LyDo });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnDiaChiGuiHang_Click(object sender, RoutedEventArgs e)
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
                    DiaChi f = new DiaChi();
                    try
                    {
                        //Cái dưới chưa tối ưu lắm
                        TrangThaiDonHang thongTinNguoiMua =  trangThaiHangDao.TimThongTinNguoiMuaTheoIdNguoiMuaVaIdSanPham(dataItem.IdNguoiMua, dataItem.IdSP);
                        f.txtbTieuDe.Text = "Địa chỉ khách hàng"; 

                        NguoiDung kh = new NguoiDung(dataItem.IdNguoiMua, thongTinNguoiMua.HoTen, null, null, null, thongTinNguoiMua.Email, thongTinNguoiMua.Sdt, thongTinNguoiMua.DiaChi,null,null,null);
                        f = new DiaChi(kh); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        f.cboHinhThucThanhToan.IsEnabled = false;
                        f.txtHoTen.IsReadOnly = true;
                        f.txtDiaChi.IsReadOnly = true;
                        f.txtSoDienThoai.IsReadOnly = true;
                        f.txtEmail.IsReadOnly = true;
                        f.btnXacNhanThanhToan.Visibility = Visibility.Collapsed;
                        f.Show();
                    }
                }
            }
        }
        private void btnGuiHang_Click(object sender, RoutedEventArgs e)
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
                    //DiaChi f = new DiaChi(idNguoiMua);
                    try
                    {
                        QuanLyDonHang quanLy = new QuanLyDonHang(null,null,dataItem.IdNguoiMua,dataItem.IdSP,"Đang giao",null);
                        quanLyDonHangDao.CapNhat(quanLy);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        QuanLyDonHang_Load(sender, e);
                    }


                }
            }

        }


        #endregion

        #region TAB3 TỔNG QUAN VÀ THỐNG KÊ
        private void ThongKe_Load(object sender, RoutedEventArgs e)
        {
            LoadTongKhachHang();
            LoadTongSoLuongSanPhamDaBan();
            LoadTongDoanhThu();

            LoadDoanhThuTheoSanPham();
            LoadTiLePhanTramDoanhThuTheoSanPham();
            LoadSoLuongSanPhamDaBan();

            LoadDanhGiaNguoiMua();
        }

        private void LoadDanhGiaNguoiMua()
        {
            try
            {
                int soLuong1Sao = 0;
                int soLuong2Sao = 0;
                int soLuong3Sao = 0;
                int soLuong4Sao = 0;
                int soLuong5Sao = 0;
                List<DanhGiaNguoiDang> dsSoLuotDanhGiaTheoTungSoSao = danhGiaNguoiDungDao.DemSoLuotDanhGiaTheoTungSoSao(nguoiDung.Id);
                foreach(var dong in dsSoLuotDanhGiaTheoTungSoSao)
                {
                    int soSao = Convert.ToInt32(dong.SoSao);
                    int soLuongDanhGia = Convert.ToInt32(dong.SoLuotDanhGia);
                    switch (soSao)
                    {
                        case 1:
                            soLuong1Sao = soLuongDanhGia;
                            break;
                        case 2:
                            soLuong2Sao = soLuongDanhGia;
                            break;
                        case 3:
                            soLuong3Sao = soLuongDanhGia;
                            break;
                        case 4:
                            soLuong4Sao = soLuongDanhGia;
                            break;
                        case 5:
                            soLuong5Sao = soLuongDanhGia;
                            break;
                    }
                }

                int tongSoLuotDanhGia = soLuong1Sao + soLuong2Sao + soLuong3Sao + soLuong4Sao + soLuong5Sao;
                txtbSoLuotDanhGia.Text = tongSoLuotDanhGia.ToString();

                ratingBar_SoSao.Value = (soLuong1Sao * 1 + soLuong2Sao * 2 + soLuong3Sao * 3 + soLuong4Sao * 4 + soLuong5Sao * 5) / tongSoLuotDanhGia;
                txtbSoSaoTrungBinh.Text = ratingBar_SoSao.Value.ToString("0.0");


                progressBar1Sao.Value = ((double)soLuong1Sao / tongSoLuotDanhGia) * 100;
                progressBar2Sao.Value = ((double)soLuong2Sao / tongSoLuotDanhGia) * 100;
                progressBar3Sao.Value = ((double)soLuong3Sao / tongSoLuotDanhGia) * 100;
                progressBar4Sao.Value = ((double)soLuong4Sao / tongSoLuotDanhGia) * 100;
                progressBar5Sao.Value = ((double)soLuong5Sao / tongSoLuotDanhGia) * 100;

                txtbSoLuong1Sao.Text = soLuong1Sao.ToString();
                txtbSoLuong2Sao.Text = soLuong2Sao.ToString();
                txtbSoLuong3Sao.Text = soLuong3Sao.ToString();
                txtbSoLuong4Sao.Text = soLuong4Sao.ToString();
                txtbSoLuong5Sao.Text = soLuong5Sao.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadSoLuongSanPhamDaBan()
        {
            try
            {
                List<string> dsNhan = new List<string>();
                List<int> dsCotSoLuongDaBan = new List<int>();
                List<int> dsCotSoLuongTong = new List<int>();
                List<SanPham> dsSLDaBan = sanPhamDao.LoadThongSoSanPhamDeThongKe(nguoiDung.Id);
               
                foreach(var dong in dsSLDaBan)
                {
                    string ten = dong.Ten;
                    int soLuongDaban = Convert.ToInt32(dong.SoLuongDaBan);
                    int soLuongTong = Convert.ToInt32(dong.SoLuong);

                    dsNhan.Add(ten);
                    dsCotSoLuongDaBan.Add(soLuongDaban);
                    dsCotSoLuongTong.Add(soLuongTong);
                }

                // Tạo 2 cột cạnh nhau
                SeriesCollection DoanhThuTheoSanPham_SC = new SeriesCollection
                {
                     new ColumnSeries
                     {
                         Title = "Số lượng đã bán",
                         Values = new ChartValues<int>(dsCotSoLuongDaBan),

                         Fill = Brushes.LightSkyBlue, // Màu của cột số lượng đã bán
                         ScalesYAt = 0 // Đặt cột này ở trục Y thứ nhất

                     },
                     new ColumnSeries
                     {
                         Title = "Số lượng tổng",
                         Values = new ChartValues<int>(dsCotSoLuongTong),
                         Fill = Brushes.Teal, // Màu của cột số lượng tổng
                         ScalesYAt = 0 // Đặt cột này ở trục Y thứ hai
                     }
                };

                // Đặt dữ liệu vào Chart
                chart_SoLuongSanPhamDaBan.Series = DoanhThuTheoSanPham_SC;

                var xAxis = new LiveCharts.Wpf.Axis
                {
                    Title = "Tên sản phẩm",
                    Labels = dsNhan.ToArray(),
                    FontSize = 9.5,
                    Separator = LiveCharts.Wpf.DefaultAxes.CleanSeparator

                };
                chart_SoLuongSanPhamDaBan.AxisX.Add(xAxis);

                var yAxis = new LiveCharts.Wpf.Axis
                {
                    Title = "Số lượng",
                    FontSize = 9.5,
                    LabelFormatter = value => value.ToString("#,##0") // Định dạng label cho trục Y thứ nhất
                };
                chart_SoLuongSanPhamDaBan.AxisY.Add(yAxis);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTiLePhanTramDoanhThuTheoSanPham()
        {
            try
            {
                List<string> dsNhan = new List<string>();
                List<int> dsCotDoanhThu = new List<int>();
                List<SanPham> dsTiLeDoanhThu = sanPhamDao.LoadThongSoSanPhamDeThongKe(nguoiDung.Id);
                
                foreach(var dong in dsTiLeDoanhThu)
                {
                    string ten = dong.Ten;
                    int soLuongDaban = Convert.ToInt32(dong.SoLuongDaBan);
                    int giaBan = Convert.ToInt32(dong.GiaBan);
                    int doanhThu = soLuongDaban * giaBan;
                    dsNhan.Add(ten);
                    dsCotDoanhThu.Add(doanhThu);
                }

                // Tạo SeriesCollection cho biểu đồ PieChart
                SeriesCollection TiLePhanTramDoanhThuTheoSanPham_SC = new SeriesCollection();

                // Tạo các Slice (phần chia) cho PieChart từ dữ liệu
                for (int i = 0; i < dsNhan.Count; i++)
                {
                    TiLePhanTramDoanhThuTheoSanPham_SC.Add(new PieSeries
                    {
                        Title = dsNhan[i], // Tên sản phẩm
                        Values = new ChartValues<int> { dsCotDoanhThu[i] } // Doanh thu tương ứng với sản phẩm

                    });
                }

                // Đặt dữ liệu vào PieChart
                chart_TiLePhanTramDoanhThuTheoSanPham.Series = TiLePhanTramDoanhThuTheoSanPham_SC;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void LoadDoanhThuTheoSanPham()
        {
            try
            {
                List<string> dsNhan = new List<string>();
                List<int> dsCotDoanhThu = new List<int>();
                List<SanPham> dsDoanhThuSanPham = sanPhamDao.LoadThongSoSanPhamDeThongKe(nguoiDung.Id);

                foreach(var dong in dsDoanhThuSanPham)
                {
                    string ten = dong.Ten;
                    int soLuongDaban = Convert.ToInt32(dong.SoLuongDaBan);
                    int giaBan = Convert.ToInt32(dong.GiaBan);
                    int doanhThu = soLuongDaban * giaBan;

                    dsNhan.Add(ten);
                    dsCotDoanhThu.Add(doanhThu);
                }
                SeriesCollection DoanhThuTheoSanPham_SC = new SeriesCollection
                {
                     new ColumnSeries
                     {
                         Title = "Doanh thu",
                         Values = new ChartValues<int> (dsCotDoanhThu)
                     }
                };


                // Đặt dữ liệu vào Chart
                chart_TongDoanhThuTheoSanPham.Series = DoanhThuTheoSanPham_SC;

                var xAxis = new LiveCharts.Wpf.Axis
                {
                    Title = "Tên sản phẩm",
                    FontSize = 9.5,
                    Labels = dsNhan.ToArray(),
                    Separator = LiveCharts.Wpf.DefaultAxes.CleanSeparator
                };
                chart_TongDoanhThuTheoSanPham.AxisX.Add(xAxis);

                var yAxis = new LiveCharts.Wpf.Axis
                {
                    Title = "Doanh thu",
                    FontSize = 9.5,
                    LabelFormatter = value => value.ToString("#,##0,##0") // Định dạng label
                };
                chart_TongDoanhThuTheoSanPham.AxisY.Add(yAxis);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTongDoanhThu()
        {
            try
            {
                int tongDoanhThu = 0;
                List<SanPham> dsTongDoanhThu = sanPhamDao.LoadThongSoSanPhamDeThongKe(nguoiDung.Id);
                foreach(var dong in dsTongDoanhThu)
                {
                    int soLuongDaBan = Convert.ToInt32(dong.SoLuongDaBan);
                    int giaBan = Convert.ToInt32(dong.GiaBan);
                    tongDoanhThu += soLuongDaBan * giaBan;
                }
                txtbTongDoanhThu.Text = tongDoanhThu.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadTongSoLuongSanPhamDaBan()
        {
            try
            {
                int tongSLSanPhamDaBan = 0;
                List<SanPham> dsTongSLDaBan = sanPhamDao.LoadThongSoSanPhamDeThongKe(nguoiDung.Id);

                foreach(var dong in dsTongSLDaBan)
                {
                    tongSLSanPhamDaBan += Convert.ToInt32(dong.SoLuongDaBan);
                }
                txtbTongSoLuongSanPhamDaBan.Text = tongSLSanPhamDaBan.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadTongKhachHang()
        {
            try
            { 
                txtbTongSoLuongKhachHang.Text = sanPhamDao.tinhTongKhachHang(nguoiDung.Id); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnThongTinChiTietDanhGia_Click(object sender, RoutedEventArgs e)
        {
            ThongTinNguoiDang f = new ThongTinNguoiDang(nguoiDung.Id);
            f.txtbTieuDe.Text = "Đánh giá của khách hàng";
            f.Show();
        }

        #endregion

        #region LOAD TabItem
        private void tabItemQuanLySP_Loaded(object sender, RoutedEventArgs e)
        {
            //HienThi_QuanLySanPham();
            Loaded += QuanLySanPham_Load;
        }
        private void tabItemQuanLyDonHang_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += QuanLyDonHang_Load;
        }
        private void tabItemThongKe_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += ThongKe_Load;
        }


        #endregion

        private void tabItemQuanLySanPham_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += QuanLySanPham_Load;
        }

        private void tabItemQuanLyDonHang_Loaded_1(object sender, RoutedEventArgs e)
        {
            Loaded += QuanLyDonHang_Load;
        }

        private void tabItemThongKe_Loaded_1(object sender, RoutedEventArgs e)
        {
            Loaded += ThongKe_Load;
        }
    }
}

