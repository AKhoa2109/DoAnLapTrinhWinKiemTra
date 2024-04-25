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
using TraoDoiDo.Models;
using TraoDoiDo.ViewModels;
using TraoDoiDo.Database;
namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for DangDo_Sua.xaml
    /// </summary>
    public partial class DangDo_Sua : Window
    {
        public int soLuongAnh = 0; 
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);
        public ThemAnhKhiDangUC[] DanhSachAnhVaMoTa = new ThemAnhKhiDangUC[100]; //Khi dùng thì phải khai báo là DanhSachAnhVaMoTa[i] = new ThemAnhKhiDangUC();
        SanPham sanPham = new SanPham();
        MoTaAnhSanPhamDao moTaAnhSanPhamDao = new MoTaAnhSanPhamDao();
        SanPhamDao sanPhamDao = new SanPhamDao();
        public DangDo_Sua()
        {
            InitializeComponent(); 
        }
        public DangDo_Sua(SanPham sp)
        {
            InitializeComponent();
            sanPham = sp;
        }
        private void FDangDoSua_Loaded(object sender, RoutedEventArgs e)
        {
            txtbIdSanPham.Text = sanPham.Id;
            txtbTen.Text = sanPham.Ten;
            txtbLoai.Text = sanPham.Loai;

            string dateString = sanPham.NgayMua;
            DateTime selectedDate = DateTime.Parse(dateString);
            dtpNgayMua.SelectedDate = selectedDate;

            txtbGiaBan.Text = sanPham.GiaBan;
            txtbGiaGoc.Text = sanPham.GiaGoc;
            cboXuatXu.Text = sanPham.XuatXu;
            txtbMoTaChung.Text = sanPham.MoTaChung;
            progressSlidere_PhanTramMoi.Value = Convert.ToInt32(sanPham.PhanTramMoi);
            txtbPhiShip.Text = sanPham.PhiShip;
            cboNoiBan.Text = sanPham.NoiBan;
            ucTangGiamSoLuongTong.txtbSoLuong.Text = sanPham.SoLuong;
            ucTangGiamSoLuongDaBan.txtbSoLuong.Text = sanPham.SoLuongDaBan;
        }
        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            suaAnhVaMoTaTrongCSDL(); //Phải để cái này ở trên cái dưới
            suaThongTinSanPhamTrongCSDL();
        }
        private void suaAnhVaMoTaTrongCSDL()
        {
            bool coSanPham = false;
            bool coMoTa = false;
            try
            {
                // Xóa dữ liệu cũ khỏi bảng MoTaAnhSanPham
                MoTaAnhSanPham moTaAnhSP = new MoTaAnhSanPham(txtbIdSanPham.Text, null, null, null,null);
                moTaAnhSanPhamDao.Xoa(moTaAnhSP);
                coMoTa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            //Cập  nhật dữ liệu mới
            for (int i = 0; i < soLuongAnh; i++)
            {
                coSanPham = false;
                if (DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text != null && !string.IsNullOrEmpty(DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text.Trim()) && DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text != "no_image.jpg")
                    {

                        try
                        {
                            if (coSanPham==false)
                            {
                                MoTaAnhSanPham moTaAnhSP = new MoTaAnhSanPham(txtbIdSanPham.Text, (i + 1).ToString(), null, DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text, DanhSachAnhVaMoTa[i].txtbMoTa.Text);
                                moTaAnhSanPhamDao.Them(moTaAnhSP);
                                coSanPham = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }
                        
                        string noiLuAnh = DanhSachAnhVaMoTa[i].txtbDuongDanAnh.Text;
                        XuLyAnh.LuuAnhVaoThuMuc(noiLuAnh);
                    }
                    else
                        continue;
            }
            if (coMoTa && coSanPham)
            {
                MessageBox.Show("Thành công");
            }
        }
        
        private void suaThongTinSanPhamTrongCSDL()
        {
            try
            {
                conn.Open();

                // Dữ liệu cần chèn
                string tenFileAnh = "no_image.jpg";
                for (int i = 0; i < soLuongAnh; i++)
                    if (DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text != null && !string.IsNullOrEmpty(DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text.Trim()) && DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text != "no_image.jpg")
                    {
                        tenFileAnh = DanhSachAnhVaMoTa[i].txtbTenFileAnh.Text;
                        break;
                    }
                string luotXem = sanPhamDao.LayLuotXem(txtbIdSanPham.Text);
                SanPham sp= new SanPham(txtbIdSanPham.Text, sanPham.IdNguoiDang, txtbTen.Text, tenFileAnh, txtbLoai.Text, ucTangGiamSoLuongTong.txtbSoLuong.Text, ucTangGiamSoLuongDaBan.txtbSoLuong.Text, txtbGiaGoc.Text,
                    txtbGiaBan.Text, txtbPhiShip.Text, "Đã duyệt", cboNoiBan.Text, cboXuatXu.Text, dtpNgayMua.SelectedDate.Value.ToString("dd/MM/yyyy"), txtbMoTaChung.Text, progressSlidere_PhanTramMoi.Value.ToString(), luotXem, null);
                sanPhamDao.CapNhat(sp);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void btnThemAnh_Click(object sender, RoutedEventArgs e)
        { 
            DanhSachAnhVaMoTa[soLuongAnh] = new ThemAnhKhiDangUC();
            wpnlChuaAnh.Children.Add(DanhSachAnhVaMoTa[soLuongAnh]);
            soLuongAnh++;
        }
    }
}

