using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TraoDoiDo.Models; 

namespace TraoDoiDo.Database
{
    public class NguoiDungDao : ThuocTinhDao
    {
        //List<NguoiDung> dsNguoi;
        List<string> dongKetQua;
        //List<List<string>> bangKetQua;

        public void Them(NguoiDung user)
        {
            string sqlStr = $"INSERT INTO {nguoiDungHeader} ({nguoiDungTen},{nguoiDungGioiTinh},{nguoiDungNgaySinh},{nguoiDungSdt},{nguoiDungCMND},{nguoiDungDiaChi},{nguoiDungEmail},{nguoiDungAnh})"
                            + $"VALUES (N'{user.HoTen}',N'{user.GioiTinh}','{user.NgaySinh}','{user.Sdt}','{user.Cmnd}',N'{user.DiaChi}','{user.Email}','{user.Anh}')";
            dbConnection.ThucThi(sqlStr);
        }
        public void Xoa(string id)
        {
            string sqlStr = $"DELETE FROM {nguoiDungHeader} WHERE {nguoiDungID}='{id}'";
            dbConnection.ThucThi(sqlStr);
        }
        public void CapNhat(NguoiDung user)
        {
            string sqlStr = $"UPDATE {nguoiDungHeader} SET " +
                $"{nguoiDungTen}=N'{user.HoTen}', {nguoiDungGioiTinh}=N'{user.GioiTinh}', {nguoiDungNgaySinh}='{Convert.ToString(user.NgaySinh)}'," +
                $"{nguoiDungCMND} = '{user.Cmnd}', {nguoiDungEmail} = '{user.Email}',{nguoiDungSdt} = '{user.Sdt}'," +
                $"{nguoiDungDiaChi} = '{user.DiaChi}', {nguoiDungAnh} = '{user.Anh}' WHERE {nguoiDungID}='{user.Id}'";
            dbConnection.ThucThi(sqlStr);
        }
        public void CapNhatDiaChi(NguoiDung user)
        {
            string sqlStr = $@"
                    UPDATE {nguoiDungHeader}
                    SET {nguoiDungTen} = N'{user.HoTen}', 
                        {nguoiDungSdt}= '{user.Sdt}', 
                        {nguoiDungEmail}= '{user.Email}',
                        {nguoiDungDiaChi} = N'{user.DiaChi}'
                    WHERE {nguoiDungID} = '{user.Id}' ";
            dbConnection.ThucThi(sqlStr);
        }
        

        public NguoiDung TimKiemThongTinTheoIdNguoi(string idNguoi)
        {
            string sqlStr = $@"SELECT {nguoiDungTen},{nguoiDungSdt},{nguoiDungEmail},{nguoiDungDiaChi}
                            FROM {nguoiDungHeader} 
                            WHERE {nguoiDungID} = '{idNguoi}' ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);

            return new NguoiDung(null, dongKetQua[0], null, null, null, dongKetQua[2], dongKetQua[1], dongKetQua[3], null, null, null);
        }
         

        public string TimKiemLinkAnh(string id)
        {
            string sqlStr = $@"SELECT {nguoiDungAnh} FROM {nguoiDungHeader} WHERE {nguoiDungID} = '{id}' ";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{nguoiDungAnh}");
        }

        public NguoiDung TimKiemBangTenDangNhap(string tenDangNhap, string matKhau)
        {
            string sqlStr = $@"SELECT {nguoiDungTen},{nguoiDungCMND},{nguoiDungGioiTinh},{nguoiDungSdt},{nguoiDungNgaySinh},{nguoiDungDiaChi},{nguoiDungEmail},{nguoiDungAnh}, {nguoiDungTien}, {taiKhoanTenDangNhap}, {taiKhoanMatKhau}, {nguoiDungHeader}.{nguoiDungID} 
                               FROM {nguoiDungHeader}
                               INNER JOIN {taiKhoanHeader} ON {nguoiDungHeader}.{nguoiDungID} = {taiKhoanHeader}.{taiKhoanIdNguoiDung}
                               WHERE {taiKhoanTenDangNhap}='{tenDangNhap}' AND {taiKhoanMatKhau}='{matKhau}'";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new NguoiDung(dongKetQua[11], dongKetQua[0], dongKetQua[2], dongKetQua[4], dongKetQua[1], dongKetQua[6], dongKetQua[3], dongKetQua[5], dongKetQua[7], new TaiKhoan(dongKetQua[9], dongKetQua[10], dongKetQua[11]), dongKetQua[8]);
        }

        public string TimKiemTienBangId(string id)
        {
            string sqlStr = $@"SELECT {nguoiDungTien} FROM {nguoiDungHeader} WHERE {nguoiDungID} = '{id}' ";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{nguoiDungTien}");
        }

        public string TimKiemMatKhauBangEmail(string email)
        {
            string sqlStr = $"SELECT {taiKhoanMatKhau} FROM {taiKhoanHeader} INNER JOIN {nguoiDungHeader} ON {taiKhoanHeader}.{taiKhoanIdNguoiDung} = {nguoiDungHeader}.{nguoiDungID} WHERE {nguoiDungHeader}.{nguoiDungEmail}='{email}'";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanMatKhau}");
        }
        public string TimKiemMatKhauBangSdt(string sdt)
        {
            string sqlStr = $"SELECT {taiKhoanMatKhau} FROM {taiKhoanHeader} INNER JOIN {nguoiDungHeader} ON {taiKhoanHeader}.{taiKhoanIdNguoiDung} = {nguoiDungHeader}.{nguoiDungID} WHERE {nguoiDungHeader}.{nguoiDungSdt}='{sdt}'";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{taiKhoanMatKhau}");
        }
    }
}
