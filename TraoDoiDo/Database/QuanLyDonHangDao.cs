using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class QuanLyDonHangDao:ThuocTinhDao
    {
        //List<string> dongKetQua;
        List<List<string>> bangKetQua;
        public List<QuanLyDonHang> TimKiemTheoIdNguoiDang(string idNguoiDang,string trangThai)
        {
            string sqlStr = $@" SELECT {sanPhamHeader}.{sanPhamID},{quanLyHeader}.{quanLyIdNguoiMua}, {sanPhamHeader}.{sanPhamTen}, {sanPhamHeader}.{sanPhamAnh}, {trangThaiHeader}.{trangThaiSoLuongMua}, {sanPhamHeader}.{sanPhamGiaBan}, {sanPhamHeader}.{sanPhamPhiShip}, {trangThaiHeader}.{trangThaiTongThanhToan}, {quanLyHeader}.{quanLyLyDo}"+
                    $" FROM {quanLyHeader} INNER JOIN {nguoiDungHeader} ON {quanLyHeader}.{quanLyIdNguoiMua} = {nguoiDungHeader}.{nguoiDungID} INNER JOIN {sanPhamHeader} ON {quanLyHeader}.{quanLyIdSanPham} = {sanPhamHeader}.{sanPhamID} "+
                    $" INNER JOIN {trangThaiHeader} ON {quanLyHeader}.{quanLyIdNguoiMua} = {trangThaiHeader}.{trangThaiIdNguoiMua} and  {quanLyHeader}.{quanLyIdSanPham} = {trangThaiHeader}.{trangThaiIdSanPham} "+
                    $" WHERE {quanLyHeader}.{quanLyIdNguoiDang} = '{idNguoiDang}' and {quanLyHeader}.{quanLyTrangThai}=N'{trangThai}' ";
            List<QuanLyDonHang> dsDonHang = new List<QuanLyDonHang> ();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach(var dong in bangKetQua)
            {
                QuanLyDonHang donHang = new QuanLyDonHang("", idNguoiDang, dong[1], dong[0],"", dong[8], dong[2], dong[3], dong[4], dong[5], dong[6], dong[7]); 
                
               
                
                dsDonHang.Add(donHang);
            }

            return dsDonHang;
        }  

        public void Them(QuanLyDonHang ql)
        {
            string sqlStr = $"INSERT INTO {quanLyHeader} ({quanLyIdNguoiDang},{quanLyIdNguoiMua},{quanLyIdSanPham},{quanLyTrangThai},{quanLyLyDo})"
            + $"VALUES ('{ql.IdNguoiDang}',N'{ql.IdNguoiMua}','{ql.IdSanPham}',N'{ql.TrangThai}',N'{ql.LyDo}')";
            dbConnection.ThucThi(sqlStr);
        }
        public void Xoa(QuanLyDonHang ql)
        {
            string sqlStr = $"DELETE FROM {quanLyHeader} WHERE {quanLyIdSanPham} = {ql.IdSanPham} AND {quanLyIdNguoiMua} = {ql.IdNguoiMua}";
            dbConnection.ThucThi(sqlStr);
        }
        public void CapNhat(QuanLyDonHang ql)
        {
            string sqlStr = $@"UPDATE {quanLyHeader} 
                        SET {quanLyTrangThai} = N'{ql.TrangThai}'   
                        WHERE {quanLyIdNguoiMua} = '{ql.IdNguoiMua}' AND {quanLyIdSanPham} = '{ql.IdSanPham}'";
            dbConnection.ThucThi(sqlStr);
        } 
        public void CapNhatTraHang(QuanLyDonHang ql)
        {
            string sqlStr = $@"UPDATE {quanLyHeader} 
                        SET {quanLyTrangThai} = N'{ql.TrangThai}' , {quanLyLyDo} = N'{ql.LyDo}'   
                        WHERE {quanLyIdNguoiMua} = '{ql.IdNguoiMua}' AND {quanLyIdSanPham} = '{ql.IdSanPham}'";
            dbConnection.ThucThi(sqlStr);
        }
        public string timIdNguoiDangTheoIdNguoiMuaVaIdSanPham(string idNguoiMua,string idSanPham)
        {
            string sqlStr = $@"SELECT {quanLyHeader}.{quanLyIdNguoiDang} FROM {quanLyHeader} WHERE {quanLyHeader}.{quanLyIdNguoiMua} = '{idNguoiMua}' AND {quanLyHeader}.{quanLyIdSanPham} = '{idSanPham}' ";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{quanLyIdNguoiDang}");
        }
        public string timIdNguoiMuaTheoIdNguoiDangVaIdSanPham(string idNguoiDang,string idSanPham)
        {
            string sqlStr = $@"SELECT {quanLyHeader}.{quanLyIdNguoiMua} FROM {quanLyHeader} WHERE {quanLyHeader}.{quanLyIdNguoiDang} = '{idNguoiDang}' AND {quanLyHeader}.{quanLyIdSanPham} = '{idSanPham}' ";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{quanLyIdNguoiMua}");
        }
    }
}
