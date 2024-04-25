using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;
using static TraoDoiDo.QuanLyUC;

namespace TraoDoiDo.Database
{
    public class TrangThaiDonHangDao:ThuocTinhDao
    {
        List<TrangThaiDonHang> dsTrangThaiDonHang;
        List<string> dongKetQua;
        List<List<string>> bangKetQua;
        public TrangThaiDonHang TimThongTinNguoiMuaTheoIdNguoiMuaVaIdSanPham(string idNguoiMua,string idSanPham)
        {
            string sqlStr = $" SELECT distinct {nguoiDungTen}, {nguoiDungSdt}, {nguoiDungEmail}, {nguoiDungDiaChi} FROM {trangThaiHeader}" +
                            $" INNER JOIN {nguoiDungHeader} ON {trangThaiHeader}.{trangThaiIdNguoiMua} = {nguoiDungHeader}.{nguoiDungID}" +
                            $" WHERE {trangThaiHeader}.{trangThaiIdNguoiMua} = '{idNguoiMua}' AND {trangThaiHeader}.{trangThaiIdSanPham} = '{idSanPham}' ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);  

            return new TrangThaiDonHang(null, null, null, null, null, null, null, null, null, null, dongKetQua[0], dongKetQua[1], dongKetQua[2], dongKetQua[3]);
        }
        public void CapNhat(TrangThaiDonHang trangThaiDon)
        {
            string sqlStr = $@"
                                UPDATE {trangThaiHeader} 
                                SET {trangThaiTrangThai} = N'{trangThaiDon.TrangThai}'
                                WHERE {sanPhamID} = '{trangThaiDon.IdSanPham}' AND {nguoiDungID} = '{trangThaiDon.IdNguoiMua}' ";
            dbConnection.ThucThi(sqlStr);
        }
        public void Them(TrangThaiDonHang trangThaiDon)
        {
            string sqlStr = $"INSERT INTO {trangThaiHeader} ({trangThaiIdNguoiMua},{trangThaiIdSanPham},{trangThaiSoLuongMua},{trangThaiTongThanhToan},{trangThaiNgay},{trangThaiTrangThai})"
            + $"VALUES ('{trangThaiDon.IdNguoiMua}',N'{trangThaiDon.IdSanPham}','{trangThaiDon.SoLuongMua}','{trangThaiDon.TongThanhToan}','{trangThaiDon.Ngay}',N'{trangThaiDon.TrangThai}')";
            dbConnection.ThucThi(sqlStr);
        }
        public void Xoa(TrangThaiDonHang trangThaiDon)
        {
            string sqlStr = $"DELETE FROM {trangThaiHeader} WHERE {trangThaiIdSanPham} = {trangThaiDon.IdSanPham} AND {trangThaiIdNguoiMua} = {trangThaiDon.IdNguoiMua}";
            dbConnection.ThucThi(sqlStr);
        }
        public List<TrangThaiDonHang> LoadTrangThaiDonHang(string idNguoiMua, string trangThai)
        {
            string sqlStr = $@"
                    SELECT {sanPhamHeader}.{sanPhamID},{sanPhamHeader}.{sanPhamTen},{sanPhamHeader}.{sanPhamAnh}, {trangThaiHeader}.{trangThaiSoLuongMua}, {sanPhamHeader}.{sanPhamGiaBan}, {sanPhamHeader}.{sanPhamPhiShip}, {trangThaiHeader}.{trangThaiTongThanhToan}, {trangThaiHeader}.{trangThaiNgay}
                    FROM {trangThaiHeader}
                    INNER JOIN {nguoiDungHeader} ON {trangThaiHeader}.{trangThaiIdNguoiMua} = {nguoiDungHeader}.{nguoiDungID}
                    INNER JOIN  {sanPhamHeader} ON {trangThaiHeader}.{trangThaiIdSanPham} = {sanPhamHeader}.{sanPhamID}
                    WHERE {nguoiDungHeader}.{nguoiDungID} = {idNguoiMua} AND {trangThaiHeader}.{trangThaiTrangThai} = N'{trangThai}' ";
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsTrangThaiDonHang = new List<TrangThaiDonHang>();
            foreach (var dong in bangKetQua)
                dsTrangThaiDonHang.Add(new TrangThaiDonHang(idNguoiMua, dong[0], dong[3], dong[6], dong[7], trangThai, dong[1], dong[2], dong[4], dong[5],null, null, null, null));

            return dsTrangThaiDonHang;
        }
        
    
    }
}
