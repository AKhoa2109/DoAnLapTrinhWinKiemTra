using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class TaiKhoanDao : ThuocTinhDao
    {
        public void Them(TaiKhoan tk)
        {
            string sqlStr = $"INSERT INTO {taiKhoanHeader} ({taiKhoanTenDangNhap}, {taiKhoanMatKhau})" + $"VALUES ('{tk.TenDangNhap}','{tk.MatKhau}')";
            dbConnection.ThucThi(sqlStr);
        }
        public void CapNhat(TaiKhoan tk)
        {
            string sql = $"UPDATE {taiKhoanHeader} SET {taiKhoanMatKhau}='{tk.MatKhau}' WHERE {taiKhoanTenDangNhap}='{tk.TenDangNhap}'";
            dbConnection.ThucThi(sql);
        }
        public TaiKhoan TimKiemBangTenDangNhap(string tenDangNhap)
        {
            string sqlStr = $"SELECT * FROM {taiKhoanHeader} WHERE {taiKhoanTenDangNhap}='{tenDangNhap}'";
            string matKhau = dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanMatKhau}");
            string iDNguoiDung = dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanIdNguoiDung}"); 
            return new TaiKhoan(tenDangNhap, matKhau, iDNguoiDung);
        }
        public TaiKhoan TimKiemBangId(string id)
        {
            string sqlStr = $"SELECT * FROM {taiKhoanHeader} WHERE {taiKhoanIdNguoiDung}='{id}'";
            string ten = dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanTenDangNhap}");
            string mk = dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanMatKhau}"); 
            return new TaiKhoan(ten, mk, id);
        }
    }
}
