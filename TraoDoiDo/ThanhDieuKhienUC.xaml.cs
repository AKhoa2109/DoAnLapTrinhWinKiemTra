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
    /// Interaction logic for ThanhDieuKhien.xaml
    /// </summary>
    public partial class ThanhDieuKhienUC : UserControl
    {
        public ThanhDieuKhienUC()
        {
            InitializeComponent();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            mainwindows.Close();
        }

        private void btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            if (mainwindows != null)
            {
                if (mainwindows.WindowState != WindowState.Maximized)
                {
                    mainwindows.WindowState = WindowState.Maximized;
                    btn_maximize.ToolTip = "Normal";
                    thanhDieuKhien.MinHeight = 1;
                }
                else
                {
                    mainwindows.WindowState = WindowState.Normal;
                    btn_maximize.ToolTip = "Maximize";
                    thanhDieuKhien.MinHeight = 0;
                }
            }
        }
        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            Button btn_close = sender as Button;
            Window mainwindows = Window.GetWindow(btn_close);
            mainwindows.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            Window mainwindows = Window.GetWindow(grid);
            mainwindows.DragMove();
        }
    }
}
