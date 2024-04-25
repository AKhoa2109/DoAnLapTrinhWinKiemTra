using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace TraoDoiDo.Database
{
    public class DbConnection
    {
        private SqlConnection conn;

        public DbConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.connStr);
        }

        public bool ThucThi(string sqlStr) // dùng cho mấy cái lệnh DELETE và UPDATE
        {
            bool co = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    co = true;

                    MessageBoxButton messageBox = MessageBoxButton.OK;
                    if ((messageBox & MessageBoxButton.OK)!=0)
                    {
                        MessageBox.Show("Thực thi thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại" + ex);
            }
            finally
            {
                conn.Close();
            }   
                  
            return co;
        }
      

        public string LayMotDoiTuong(string sqlStr, string tenCot) // chuỗi truy vấn và tên cột cần truy vấn (nói cách khác là trả về 1 ô //Trong 1 cột, lấy ra thuộc tính đầu tiên thỏa sqlStr  (đã xài)
        {
            List<string> list = LayDanhSachMotDoiTuong(sqlStr, tenCot);
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        public List<string> LayDanhSachMotDoiTuong(string sqlStr, string tenCot)  // Trong một cột, lấy ra các dòng thỏa sqlStr // (chỉ có timIDmax là xài thôi, tìm cách bỏ hàm tìm idmax đi) ; (còn cái  TinhTienNguoiDung) rãnh rãnh bỏ sau
        {
            List<string> list = new List<string>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[tenCot].ToString());
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public List<List<T>> LayDanhSachNhieuPhanTu<T>(string sqlStr) // Lấy ra nhiều dòng dữ liệu thỏa sqlStr (Đã xài-Nữa sửa T thành string :v) (đã xài)
        {
            List<List<T>> listResult = new List<List<T>>(); // T hoặc là int, hoặc là string, hoặc là đối tượng(cho phép lưu nhiều kiểu dữ liệu khác nhau)

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<T> list = new List<T>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = (T)Convert.ChangeType(reader.GetValue(i), typeof(T));
                        list.Add(value);
                    }
                    listResult.Add(list);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return listResult;
        }

        public List<T> LayDanhSach<T>(string sqlStr) // Lấy ra 1 dòng dữ liệu thỏa sqlStr (Đã xài)
        {
            List<T> list = new List<T>();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = (T)Convert.ChangeType(reader.GetValue(i), typeof(T));
                        list.Add(value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

    }
}
