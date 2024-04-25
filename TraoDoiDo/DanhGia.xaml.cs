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

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for DanhGia.xaml
    /// </summary>
    public partial class DanhGia : Window
    {
        public string idNguoiDang;
        public string idNguoiMua;
        DanhGiaNguoiDangDao danhGiaNguoiDungDao = new DanhGiaNguoiDangDao();
        public DanhGia()
        {
            InitializeComponent();
        }

        private void btnGuiDanhGia_Click(object sender, RoutedEventArgs e)
        {
            bool coXoa = false;
            bool coThem = false;
            DanhGiaNguoiDang danhGiaNguoiDung = new DanhGiaNguoiDang(idNguoiDang, null, idNguoiMua, null,ratingBarSoSao.Value.ToString(), txtbDanhGia.Text, null, null);
            try
            {
                danhGiaNguoiDungDao.Xoa(danhGiaNguoiDung);
                coXoa = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                danhGiaNguoiDungDao.Them(danhGiaNguoiDung);
                coThem = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            if (coThem && coXoa)
            {
                MessageBox.Show("Cảm ơn bạn đã gửi đánh giá\nChúc bạn một ngày vui :)", "Thông báo", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
                


        }
    }
}
