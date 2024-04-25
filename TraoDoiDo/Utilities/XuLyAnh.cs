using System;
using System.Windows;

namespace TraoDoiDo.ViewModels
{
    public class XuLyAnh
    {
        public static string layDuongDanToiHinhDaiDien()
        {
            string thuMucHienTai = AppDomain.CurrentDomain.BaseDirectory; // Debug
            string thuMucLui1 = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(thuMucHienTai)); //Bin
            string thuMucLui2 = System.IO.Path.GetDirectoryName(thuMucLui1); //TraoDoiDo
            string thuMucHinhSanPham = System.IO.Path.Combine(thuMucLui2, "MyResources", "Hinh", "HinhDaiDien");
            return thuMucHinhSanPham;
        }
        public static string layDuongDanToiHinhSanPham()
        {
            string thuMucHienTai = AppDomain.CurrentDomain.BaseDirectory; // Debug
            string thuMucLui1 = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(thuMucHienTai)); //Bin
            string thuMucLui2 = System.IO.Path.GetDirectoryName(thuMucLui1); //TraoDoiDo
            string thuMucHinhSanPham = System.IO.Path.Combine(thuMucLui2, "MyResources", "Hinh", "HinhSanPham");
            return thuMucHinhSanPham;
        }
        public static string layDuongDanDayDuToiFileAnh(string tenFileAnh)
        {
            string thuMucHinhSanPham = layDuongDanToiHinhSanPham();
            return System.IO.Path.Combine(thuMucHinhSanPham, tenFileAnh);
        }
        public static string layDuongDanDayDuToiFileAnhDaiDien(string tenFileAnh)
        {
            string thuMucHinhSanPham = layDuongDanToiHinhDaiDien();
            return System.IO.Path.Combine(thuMucHinhSanPham, tenFileAnh);
        }
        public static void LuuAnhVaoThuMuc(string duongDanAnh)
        {
            try
            {
                // Kiểm tra xem đường dẫn ảnh có tồn tại không
                if (!System.IO.File.Exists(duongDanAnh))
                {
                    MessageBox.Show("Không tìm thấy tệp ảnh.");
                    return;
                }

                string thuMucHinhCuaToi = layDuongDanToiHinhSanPham();

                // Kiểm tra xem thư mục có tồn tại không, nếu không thì tạo mới
                if (!System.IO.Directory.Exists(thuMucHinhCuaToi))
                {
                    System.IO.Directory.CreateDirectory(thuMucHinhCuaToi);
                }

                // Lấy tên tệp ảnh từ đường dẫn
                string tenFile = System.IO.Path.GetFileName(duongDanAnh);

                // Tạo đường dẫn mới cho tệp ảnh trong thư mục "HinhCuaToi"
                string duongDanMoi = System.IO.Path.Combine(thuMucHinhCuaToi, tenFile);

                // Kiểm tra xem tệp ảnh đã tồn tại trong thư mục chưa
                if (System.IO.File.Exists(duongDanMoi))
                {
                    //MessageBox.Show("Tệp ảnh đã tồn tại trong thư mục HinhSanPham.");
                    return;
                }

                // Sao chép tệp ảnh vào thư mục "HinhCuaToi"
                System.IO.File.Copy(duongDanAnh, duongDanMoi, true);

                //MessageBox.Show("Ảnh đã được lưu vào thư mục HinhSanPham.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu ảnh: " + ex.Message);
            }
        }
    }
}
