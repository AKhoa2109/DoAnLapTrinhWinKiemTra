using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TraoDoiDo
{
    /// <summary>
    /// Interaction logic for TrangChu.xaml
    /// </summary>
    public partial class TrangChuUC : UserControl
    {
        private List<string> imagePaths = new List<string>
        {
            "HinhCuaToi/TrangChu2.png",
            "HinhCuaToi/TrangChu3.png",
            "HinhCuaToi/TrangChu1.png"
            // Add more image paths as needed
        };

        private int currentIndex = 0;
        private DispatcherTimer timer;

        public TrangChuUC()
        {
            InitializeComponent();

            if (imagePaths.Count > 0)
            {
                // Khởi động timer cho slideshow
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1.8); // Thay đổi khoảng thời gian theo ý muốn
                timer.Tick += Timer_Tick;
                timer.Start();

                // Hiển thị ảnh đầu tiên
                DisplayImage();
            }
        }

        // Các phương thức khác của lớp
        private void DisplayImage()
        {
            // Load and display the current image
            string imagePath = imagePaths[currentIndex];
            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            imageControl.Source = bitmapImage;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Move to the next image
            currentIndex = (currentIndex + 1) % imagePaths.Count;
            DisplayImage();
        }

        // Sự kiện khi nhấn nút Previous
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            // Move to the previous image
            currentIndex--;

            // Check if we have reached the beginning of the list
            if (currentIndex < 0)
            {
                // Set currentIndex to the last image index
                currentIndex = imagePaths.Count - 1;
            }

            DisplayImage();
        }

        // Sự kiện khi nhấn nút Next
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Move to the next image
            currentIndex++;

            // Check if we have reached the end of the list
            if (currentIndex >= imagePaths.Count)
            {
                // Set currentIndex back to the first image index
                currentIndex = 0;
            }

            DisplayImage();
        }
    }
}
