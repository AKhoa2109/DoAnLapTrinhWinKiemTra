using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class GiaoDichDao : ThuocTinhDao
    {
        List<GiaoDich> dsGiaoDich;
        //List<string> dongKetQua;
        List<List<string>> bangKetQua;

        public void Them(GiaoDich gd)
        {
            string sqlStr = $"INSERT INTO {giaoDichHeader} ({taiKhoanIdNguoiDung}, {giaoDichLoai},{giaoDichSoTien},{giaoDichTuNguon},{giaoDichDenNguon},{giaoDichNgay})"
                            + $"VALUES ('{gd.IdNguoiDung}',N'{gd.LoaiGiaoDich}','{gd.SoTien}',N'{gd.TuNguonTien}',N'{gd.DenNguonTien}','{gd.NgayGiaoDich}')";
            dbConnection.ThucThi(sqlStr);
        }

        public void CapNhatSoTien(string soTien, string idNguoiDung)
        {
            string sqlStr = $@"UPDATE {nguoiDungHeader} SET {nguoiDungTien} = '{soTien}' WHERE {nguoiDungID} = '{idNguoiDung}' ";
            dbConnection.ThucThi(sqlStr);
        }

        public List<GiaoDich> LoadDSGiaoDichTheoIdNguoiDung(string idNguoiDung)
        {
            string sqlStr = $"SELECT {giaoDichID}, {giaoDichLoai},{giaoDichSoTien},{giaoDichTuNguon},{giaoDichDenNguon},{giaoDichNgay} FROM {giaoDichHeader} WHERE {nguoiDungID}= '{idNguoiDung}' ";
            dsGiaoDich = new List<GiaoDich>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach(var dong in bangKetQua)
                dsGiaoDich.Add(new GiaoDich(dong[0], idNguoiDung, dong[1], dong[2], dong[3], dong[4], dong[5])); 
            return dsGiaoDich;
        }
    }
}
