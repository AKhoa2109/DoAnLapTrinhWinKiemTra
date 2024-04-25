using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using TraoDoiDo.ViewModels;
namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for ThemAnhKhiDang.xaml
    /// </summary>
    public partial class ThemAnhKhiDangUC : UserControl
    {
        public ThemAnhKhiDangUC()
        {
            InitializeComponent();
        }

        private void btnChonAnh_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                imgAnhSP.Source = new BitmapImage(new Uri(selectedFileName)); 
                txtbTenFileAnh.Text = System.IO.Path.GetFileName(selectedFileName); // Lưu tên file
                txtbDuongDanAnh.Text = selectedFileName;

                // Gọi hàm để lưu ảnh vào thư mục "HinhCuaToi"
                //LuuAnhVaoThuMuc(selectedFileName);
               
            }
        }

        private void btnXoaAnh_Click(object sender, RoutedEventArgs e)
        {
            string tenFileAnh = "no_image.jpg";
            txtbTenFileAnh.Text = tenFileAnh;

            string duongDanAnh = XuLyAnh.layDuongDanDayDuToiFileAnh("no_image.jpg");
            txtbDuongDanAnh.Text = duongDanAnh;
            imgAnhSP.Source = new BitmapImage(new Uri(duongDanAnh));

            txtbMoTa.Text = "";
        }
    }
}
