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
    /// Interaction logic for TangGiamUC.xaml
    /// </summary>
    public partial class TangGiamUC : UserControl
    {
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(TangGiamUC), new PropertyMetadata("Số lượng"));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }
        public TangGiamUC()
        {
            InitializeComponent();
        }

        private void btnTang_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtbSoLuong.Text);
            n += 1;
            txtbSoLuong.Text = n.ToString();
        }

        private void btnGiam_Click(object sender, RoutedEventArgs e)
        {
            int n = Convert.ToInt32(txtbSoLuong.Text);
            if (n - 1 > 0)
            {
                n -= 1;
            }
            txtbSoLuong.Text = n.ToString();
        }
    }
}
