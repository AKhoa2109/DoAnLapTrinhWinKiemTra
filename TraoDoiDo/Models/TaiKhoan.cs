using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class TaiKhoan
    {
        private string tenDangNhap = "";
        private string matKhau = "";
        private string iDNguoiDung = "";

        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public string IDNguoiDung { get => iDNguoiDung; set => iDNguoiDung = value; }

        public TaiKhoan() { }

        public TaiKhoan(string tenDangNhap, string matKhau, string iDNguoiDung)
        {
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
            this.IDNguoiDung = iDNguoiDung;
        }

        public TaiKhoan(IDataRecord reader)
        {
            try
            {
                //tenDangNhap = (string)reader[];
                //matKhau = (string)reader[];
                //iDNguoiDung = (string)reader[];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
