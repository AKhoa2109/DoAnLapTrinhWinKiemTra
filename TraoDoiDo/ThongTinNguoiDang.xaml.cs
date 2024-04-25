using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ThongTinNguoiDang.xaml
    /// </summary>
    /// 
     
    public partial class ThongTinNguoiDang : Window
    { 
        DanhGiaNguoiDangDao danhGiaNguoiDungDao = new DanhGiaNguoiDangDao();
        public ThongTinNguoiDang(string idNguoiDang)// K dùng constructor mà dùng public int id=1 toàn cục thì sẽ bị fail, do contrustor chạy trước khi gán
        {
            InitializeComponent();
            LoadThongTinNguoiDang(idNguoiDang);
            LoadDSDanhGia(idNguoiDang); 
        }

        private void LoadThongTinNguoiDang(string idNguoiDang)
        {
            try
            {
                NguoiDung nguoiDang = danhGiaNguoiDungDao.LoadThongTinNguoiDang(idNguoiDang);
                itemsControlDSDanhGia.Items.Clear(); 
                txtHoTen.Text = nguoiDang.HoTen;
                txtSoDienThoai.Text = nguoiDang.Sdt;
                txtEmail.Text = nguoiDang.Email;
                txtDiaChi.Text = nguoiDang.DiaChi;
                imgNguoiDang.Source = new BitmapImage(new Uri(XuLyAnh.layDuongDanDayDuToiFileAnhDaiDien(nguoiDang.Anh))); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDSDanhGia(string idNguoiDang)
        {
            try
            {
                List<DanhGiaNguoiDang> dsDanhGia = danhGiaNguoiDungDao.LoadDanhSachDanhGia(idNguoiDang);
                itemsControlDSDanhGia.Items.Clear();
                foreach(var dong in dsDanhGia)
                {
                    itemsControlDSDanhGia.Items.Add(new {Ten = dong.TenNguoiMua, SoSao = dong.SoSao, NhanXet = dong.NhanXet, LinkAnhDaiDienNguoiDanhGia = XuLyAnh.layDuongDanDayDuToiFileAnhDaiDien(dong.AnhNguoiMua)}); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
