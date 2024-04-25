using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TraoDoiDo.Database;
using TraoDoiDo.Models;
using TraoDoiDo.ViewModels;

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for ViDienTuUC.xaml
    /// </summary>
    public partial class ViDienTuUC : UserControl
    {
        
        NguoiDung nguoiDung = new NguoiDung();
        NguoiDungDao nguoiDungDao = new NguoiDungDao();
        GiaoDich gd;
        GiaoDichDao gdDao = new GiaoDichDao();
        List<string> listTienNap = new List<string>();
        List<string> listTienRut = new List<string>(); 
        public ViDienTuUC()
        {
            InitializeComponent();
        }
        public ViDienTuUC(NguoiDung kh)
        {
            InitializeComponent();
            nguoiDung = kh;
            imageHinhDaiDien.Source = new BitmapImage(new Uri(XuLyAnh.layDuongDanDayDuToiFileAnhDaiDien(kh.Anh)));
        }

        private void btnNapTien_Click(object sender, RoutedEventArgs e)
        {
            NapRutTien w = new NapRutTien(nguoiDung);
            w.Show();
        }

        private void btnRutTien_Click(object sender, RoutedEventArgs e)
        {
            NapRutTien w = new NapRutTien(nguoiDung);
            w.txtbTieuDe.Text = "Rút tiền";
            w.txtbTu.Text = "Rút tiền từ";
            w.txtbDen.Text = "Đến ngân hàng";
            w.Show();
        }

        private void UcViDienTU_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                HienThi_GiaoDich();
                string t = nguoiDungDao.TimKiemTienBangId(nguoiDung.Id);
                lblSoDu.Text = t;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void HienThi_GiaoDich()
        {
            try
            {
                List<GiaoDich> dsGiaoDich = gdDao.LoadDSGiaoDichTheoIdNguoiDung(nguoiDung.Id);
                foreach(var dong in dsGiaoDich)
                    lsvLichSuGiaoDich.Items.Add(new { Id = dong.Id, Type = dong.LoaiGiaoDich, Money = dong.SoTien, Initial = dong.TuNguonTien, End = dong.DenNguonTien, Date = dong.NgayGiaoDich });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

    }
}
