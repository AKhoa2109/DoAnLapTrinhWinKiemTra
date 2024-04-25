using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class DanhGiaNguoiDangDao:ThuocTinhDao
    {
        List<string> dongKetQua;
        List<List<string>> bangKetQua;
        List<DanhGiaNguoiDang> dsDanhGiaNguoiDang; 


        public DanhGiaNguoiDang timTenNguoiDangVaNhanXet(string idNguoi)
        {
            string sqlStr = $@"
                    SELECT {nguoiDungTen}, COUNT({danhGiaNhanXet}) as SLNhanXet
                    FROM {nguoiDungHeader}
                    INNER JOIN {danhGiaHeader} ON {nguoiDungHeader}.{nguoiDungID} = {danhGiaHeader}.{quanLyIdNguoiDang}
                    GROUP BY {nguoiDungID},{nguoiDungTen}
                    HAVING {nguoiDungID} = '{idNguoi}' ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new DanhGiaNguoiDang(null, dongKetQua[0], null, null, null, null, dongKetQua[1], null);
        }
        public void Xoa(DanhGiaNguoiDang danhGiaNguoiDung)
        {
            string sqlStr = $@" DELETE {danhGiaHeader} WHERE {sanPhamIdNguoiDang} = {danhGiaNguoiDung.IdNguoiDang} AND {danhGiaIdNguoiMua} = {danhGiaNguoiDung.IdNguoiMua}";
            dbConnection.ThucThi(sqlStr);
        }
        public void Them(DanhGiaNguoiDang danhGiaNguoiDung)
        {
            string sqlStr = $@" INSERT INTO {danhGiaHeader} ({sanPhamIdNguoiDang}, {danhGiaIdNguoiMua} ,{danhGiaSoSao}, {danhGiaNhanXet})  
                                        VALUES ({danhGiaNguoiDung.IdNguoiDang}, {danhGiaNguoiDung.IdNguoiMua}, N'{danhGiaNguoiDung.SoSao}', N'{danhGiaNguoiDung.NhanXet}')";
            dbConnection.ThucThi(sqlStr);
        }
        public List<DanhGiaNguoiDang> DemSoLuotDanhGiaTheoTungSoSao(string idNguoiDang)
        {
            string sqlStr = $@"
                SELECT {danhGiaSoSao}, COUNT({danhGiaIdNguoiMua}) 
                FROM {danhGiaHeader}
                WHERE {sanPhamIdNguoiDang}= '{idNguoiDang}'
                GROUP BY {danhGiaSoSao} ";
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsDanhGiaNguoiDang = new List<DanhGiaNguoiDang>();
            foreach (var dong in bangKetQua)
                dsDanhGiaNguoiDang.Add(new DanhGiaNguoiDang(null, null, null, null, dong[0], null, dong[1], null));
            return dsDanhGiaNguoiDang;
        }
        public NguoiDung LoadThongTinNguoiDang(string idNguoiDang)
        {
            string sqlStr = $@" 
                SELECT distinct {nguoiDungTen}, {nguoiDungSdt}, {nguoiDungEmail}, {nguoiDungDiaChi}, {nguoiDungHeader}.{nguoiDungAnh}
                FROM {danhGiaHeader}
                INNER JOIN {nguoiDungHeader} ON {danhGiaHeader}.{sanPhamIdNguoiDang} = {nguoiDungHeader}.{nguoiDungID} 
                WHERE {danhGiaHeader}.{sanPhamIdNguoiDang} =  '{idNguoiDang}'
                ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new NguoiDung(null, dongKetQua[0], null, null, null, dongKetQua[2], dongKetQua[1], dongKetQua[3], dongKetQua[4], null, null);
        }
        public NguoiDung LoadThongTinNguoiDangUyTin(string idNguoiDang)
        {
            string sqlStr = $@" 
                SELECT distinct  {nguoiDungID},{nguoiDungTen}, {nguoiDungSdt}, {nguoiDungCMND}, {nguoiDungGioiTinh}, {nguoiDungSdt}, {nguoiDungNgaySinh}, {nguoiDungDiaChi},{nguoiDungEmail} 
                FROM {danhGiaHeader}
                INNER JOIN {nguoiDungHeader} ON {danhGiaHeader}.{sanPhamIdNguoiDang} = {nguoiDungHeader}.{nguoiDungID} 
                WHERE {danhGiaHeader}.{sanPhamIdNguoiDang} =  '{idNguoiDang}'
                ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new NguoiDung(dongKetQua[0], dongKetQua[1], dongKetQua[4], dongKetQua[6], dongKetQua[3], dongKetQua[8], dongKetQua[2], dongKetQua[7], null, null, null);
        }
        public List<DanhGiaNguoiDang> LoadDanhSachDanhGia(string idNguoiDang)
        {
            string sqlStr = $@" 
                SELECT {nguoiDungHeader}.{nguoiDungTen}, {danhGiaHeader}.{danhGiaSoSao}, {danhGiaHeader}.{danhGiaNhanXet}, {nguoiDungHeader}.{nguoiDungAnh} 
                FROM {danhGiaHeader}
                INNER JOIN {nguoiDungHeader} ON {danhGiaHeader}.{danhGiaIdNguoiMua} = {nguoiDungHeader}.{nguoiDungID} 
                WHERE {danhGiaHeader}.{sanPhamIdNguoiDang} = '{idNguoiDang}' ";
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsDanhGiaNguoiDang = new List<DanhGiaNguoiDang>();
            foreach (var dong in bangKetQua)
                dsDanhGiaNguoiDang.Add(new DanhGiaNguoiDang(null, null, null, dong[0], dong[1], dong[2], null, dong[3]));     
            return dsDanhGiaNguoiDang; 
        }
        public List<DanhGiaNguoiDang> TinhTrungBinhSoSao(string soSao)
        {
            string sqlStr = $@"SELECT {danhGiaHeader}.{quanLyIdNguoiDang}
                                FROM {danhGiaHeader}
                                GROUP BY {danhGiaHeader}.{quanLyIdNguoiDang}
                                HAVING AVG(CONVERT(int,{danhGiaSoSao})) = {soSao}";
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsDanhGiaNguoiDang = new List<DanhGiaNguoiDang>();
            foreach (var dong in bangKetQua)
                dsDanhGiaNguoiDang.Add(new DanhGiaNguoiDang(dong[0], null, null, null,null,null,null,null));
            return dsDanhGiaNguoiDang;
        }
        public List<DanhGiaNguoiDang> DemSoSao(string soSao)
        {
            string sqlStr = $@"SELECT {danhGiaHeader}.{quanLyIdNguoiDang}, {danhGiaSoSao}
                                FROM {danhGiaHeader}
                                WHERE {danhGiaSoSao} = '{soSao}' GROUP BY {quanLyIdNguoiDang}";
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsDanhGiaNguoiDang = new List<DanhGiaNguoiDang>();
            foreach (var dong in bangKetQua)
                dsDanhGiaNguoiDang.Add(new DanhGiaNguoiDang(dong[0], null, null, null, dong[1],null,null,null));
            return dsDanhGiaNguoiDang;
        }
    }
}
