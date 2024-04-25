using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for ThemAnhKhiDang.xaml
    /// </summary>
    public partial class ThemAnhKhiDang : UserControl
    {
        public ThemAnhKhiDang()
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
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imgAnhSP.Source = null;
        }
    }
}
