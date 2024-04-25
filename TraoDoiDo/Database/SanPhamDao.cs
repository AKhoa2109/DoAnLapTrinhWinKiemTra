using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class SanPhamDao : ThuocTinhDao
    {
        List<SanPham> dsSanPham;
        List<MoTaAnhSanPham> dsMoTaAnhSanPham;
        List<string> dongKetQua;
        List<List<string>> bangKetQua;


        public void Them(SanPham sp)
        {
            string sqlStr = $@"INSERT INTO SanPham ({sanPhamID},{sanPhamIdNguoiDang},{sanPhamTen}, {sanPhamAnh}, {sanPhamLoai}, {sanPhamSoLuong}, {sanPhamSLDaBan}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamPhiShip}, {sanPhamTrangThai}, {sanPhamNoiBan}, {sanPhamXuatXu}, {sanPhamNgayMua}, {sanPhamPhamTramMoi}, {sanPhamMoTaChung}, {sanPhamSoLuotXem}) 
                   VALUES ('{sp.Id}','{sp.IdNguoiDang}' , N'{sp.Ten}', '{sp.LinkAnh}', N'{sp.Loai}', '{sp.SoLuong}', '{sp.SoLuongDaBan}', '{sp.GiaGoc}', '{sp.GiaBan}', '{sp.PhiShip}', N'{sp.TrangThai}', N'{sp.NoiBan}', N'{sp.XuatXu}', '{sp.NgayMua}', '{sp.PhanTramMoi}', N'{sp.MoTaChung}','{0}')";
            dbConnection.ThucThi(sqlStr);
        }

        public void Xoa(SanPham sp)
        {
            string sqlStr = $"DELETE FROM {sanPhamHeader} WHERE {sanPhamID} = {sp.Id}";
            dbConnection.ThucThi(sqlStr);
        }

        public void CapNhat(SanPham sp)
        {
            string sqlStr = $@"UPDATE {sanPhamHeader} SET {sanPhamTen} = N'{sp.Ten}', {sanPhamAnh} = '{sp.LinkAnh}', {sanPhamLoai} = N'{sp.Loai}', {sanPhamSoLuong} = '{sp.SoLuong}', " +
                      $" {sanPhamSLDaBan} = '{sp.SoLuongDaBan}', {sanPhamGiaGoc} = '{sp.GiaGoc}', {sanPhamGiaBan} = '{sp.GiaBan}', {sanPhamPhiShip} = '{sp.PhiShip}', " +
                     $" {sanPhamTrangThai} = N'{sp.TrangThai}', {sanPhamNoiBan} = N'{sp.NoiBan}', {sanPhamXuatXu} = N'{sp.XuatXu}', {sanPhamNgayMua} = '{sp.NgayMua}', " +
                      $" {sanPhamPhamTramMoi} = '{sp.PhanTramMoi}', {sanPhamMoTaChung} = N'{sp.MoTaChung}' WHERE {sanPhamID} = '{sp.Id}'";
            dbConnection.ThucThi(sqlStr);
        }

        public List<SanPham> LoadSanPhamCungLoai(SanPham sp)
        {
            //MessageBox.Show("id nguoi mua trong LoadSanPhamCungLoai"+sp.IdNguoiMua);
            string sqlStr = $@"
                        SELECT {sanPhamHeader}.{sanPhamID}, {sanPhamTen}, {sanPhamAnh}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamNoiBan} , {sanPhamLoai}
                        FROM {sanPhamHeader} 
                       
                        WHERE {sanPhamLoai} = N'{sp.Loai}' AND {sanPhamHeader}.{sanPhamID} != '{sp.Id}' AND {sanPhamHeader}.{sanPhamIdNguoiDang} != '{sp.IdNguoiMua}' 
                        ";

            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
            {
                dsSanPham.Add(new SanPham(dong[0], null, dong[1], dong[2], dong[6], null, null, dong[3], dong[4], null, null, dong[5], null, null, null, null, null, sp.IdNguoiMua));
            }


            return dsSanPham;
        }

        public List<MoTaAnhSanPham> LoadAnhVaMoTa(SanPham sp)
        {
            string sqlStr = $@"
                    SELECT {sanPhamHeader}.{sanPhamAnh}, {moTaSanPhamHeader}.{moTaSanPhamLinkAnh}, {moTaSanPhamHeader}.{moTaSanPhamMoTa} 
                    FROM {sanPhamHeader} INNER JOIN {moTaSanPhamHeader} 
                    ON {sanPhamHeader}.{sanPhamID} = {moTaSanPhamHeader}.{sanPhamID}
                    WHERE {sanPhamHeader}.{sanPhamID} = '{sp.Id}'
                ";
            dsMoTaAnhSanPham = new List<MoTaAnhSanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
                dsMoTaAnhSanPham.Add(new MoTaAnhSanPham(null, null, dong[0], dong[1], dong[2]));

            return dsMoTaAnhSanPham;
        }

        public SanPham LoadThongTinMotSanPhamTheoid(string idSanPham)
        {
            string sqlStr = $@"
                    SELECT {sanPhamTen}, {sanPhamLoai}, {sanPhamSoLuong}, {sanPhamSLDaBan}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamPhiShip}, {sanPhamNoiBan}, {sanPhamXuatXu}, {sanPhamNgayMua}, {sanPhamPhamTramMoi}, {sanPhamMoTaChung} 
                    FROM {sanPhamHeader}
                    WHERE {sanPhamID} = '{idSanPham}' ";

            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new SanPham(null, null, dongKetQua[0], null, dongKetQua[1], dongKetQua[2], dongKetQua[3], dongKetQua[4], dongKetQua[5], dongKetQua[6], null, dongKetQua[7], dongKetQua[8], dongKetQua[9], dongKetQua[11], dongKetQua[10], null, null);
        }

        public List<SanPham> LoadSanPham(string idNguoiMua) //Xét yêu thích (lên wpnl_hienThi)
        {
            // Cái idNguoiMua chỗ dong[9] đại diện cho biến người đó có đang thích sản phẩm k, chứ nó k phải là id của người mua
            string sqlStr = $@" SELECT {sanPhamHeader}.{sanPhamID}, {sanPhamHeader}.{sanPhamTen}, {sanPhamHeader}.{sanPhamAnh}, {sanPhamHeader}.{sanPhamGiaGoc}, {sanPhamHeader}.{sanPhamGiaBan}, {sanPhamHeader}.{sanPhamNoiBan}, {sanPhamHeader}.{sanPhamSoLuotXem}, {nguoiDungHeader}.{nguoiDungID}, {sanPhamHeader}.{sanPhamLoai}, {danhMucHeader}.{danhMucNguoiMua} 
                    FROM {sanPhamHeader} 
                    INNER JOIN {nguoiDungHeader} ON {sanPhamHeader}.{sanPhamIdNguoiDang} = {nguoiDungHeader}.{nguoiDungID} 
                    LEFT OUTER JOIN {danhMucHeader} ON {danhMucHeader}.{danhMucNguoiMua} = '{idNguoiMua}' AND {danhMucHeader}.{sanPhamID} = {sanPhamHeader}.{sanPhamID} 
                    WHERE {sanPhamIdNguoiDang} != '{idNguoiMua}'
                ";

            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
            {
                dsSanPham.Add(new SanPham(dong[0], dong[7], dong[1], dong[2], dong[8], null, null, dong[3], dong[4], null, null, dong[5], null, null, null, null, dong[6], dong[9]));
            }
            return dsSanPham;
        }
        public List<SanPham> LoadSanPhamCu(string cu)
        {
            string sqlStr = $@"SELECT NgayDang FROM {sanPhamHeader} ";
            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
            {
                dsSanPham.Add(new SanPham(dong[0], dong[7], dong[1], dong[2], dong[8], null, null, dong[3], dong[4], null, null, dong[5], null, null, null, null, dong[6], dong[9]));
            }
            return dsSanPham;
        }
        public string LayLuotXem(string idSP)
        {
            string sqlStr = $"SELECT {sanPhamSoLuotXem} FROM {sanPhamHeader} WHERE {sanPhamID}='{idSP}'";
            return dbConnection.LayMotDoiTuong(sqlStr, $"{sanPhamSoLuotXem}");
        }

        public void CapNhatLuotXem(string idSP,int soLuotXem)
        {
            string sqlStr = $@" UPDATE {sanPhamHeader} SET {sanPhamSoLuotXem} = '{(soLuotXem + 1)}' WHERE IdSanPham = '{idSP}' ";
            dbConnection.ThucThi(sqlStr);

        }

        public List<SanPham> timKiemBangId(string idNguoiDang) // Nên sửa tên lại là timDSSanPhamBangId
        {
            string sqlStr = $" SELECT {sanPhamID}, {sanPhamTen}, {sanPhamAnh}, {sanPhamLoai}, {sanPhamSoLuong}, {sanPhamSLDaBan}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamPhiShip}, {sanPhamTrangThai}" +
                $" FROM {sanPhamHeader} WHERE {sanPhamIdNguoiDang} = '{idNguoiDang}' ";
            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach(var dong in bangKetQua)
            { 
                dsSanPham.Add(new SanPham(dong[0], idNguoiDang, dong[1], dong[2], dong[3], dong[4], dong[5], dong[6], dong[7], dong[8], dong[9], null, null, null, null, null, "0", null));
            }     
            return dsSanPham;
        }
      
        public SanPham timKiemToanBoBangId(string id)
        {
            string sqlStr = $" SELECT * FROM {sanPhamHeader} WHERE {sanPhamID} = '{id}' ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new SanPham(dongKetQua[0], dongKetQua[1], dongKetQua[2], dongKetQua[3], dongKetQua[4], dongKetQua[5], dongKetQua[6], dongKetQua[7], dongKetQua[8], dongKetQua[9], dongKetQua[10], dongKetQua[11], dongKetQua[12], dongKetQua[13], dongKetQua[15], dongKetQua[14], dongKetQua[16], null);
        }

        public List<SanPham> timKiemBangTen(string ten, string idNguoiDang)
        {
            string sqlStr = $" SELECT {sanPhamID}, {sanPhamTen}, {sanPhamAnh}, {sanPhamLoai}, {sanPhamSoLuong}, {sanPhamSLDaBan}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamPhiShip}, {sanPhamTrangThai}" +
                    $" FROM {sanPhamHeader} WHERE {sanPhamTen} LIKE N'{ten}%' AND {sanPhamIdNguoiDang}='{idNguoiDang}'";
            
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            dsSanPham = new List<SanPham>();
            foreach (var dong in bangKetQua)
            { 
                dsSanPham.Add(new SanPham(dong[0], idNguoiDang, dong[1], dong[2], dong[3], dong[4], dong[5], dong[6], dong[7], dong[8], dong[9], null, null, null, null, null, "0", null)); 
            }
            return dsSanPham; 
        }

        public List<SanPham> timKiemBangLoai(string loai, string idNguoiDang)
        {
            string sqlStr = $" SELECT {sanPhamID}, {sanPhamTen}, {sanPhamAnh}, {sanPhamLoai}, {sanPhamSoLuong}, {sanPhamSLDaBan}, {sanPhamGiaGoc}, {sanPhamGiaBan}, {sanPhamPhiShip}, {sanPhamTrangThai}" +
                    $" FROM {sanPhamHeader} WHERE {sanPhamLoai} LIKE N'{loai}%' AND {sanPhamIdNguoiDang}='{idNguoiDang}'";
            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
                dsSanPham.Add(new SanPham(dong[0], idNguoiDang, dong[1], dong[2], dong[3], dong[4], dong[5], dong[6], dong[7], dong[8], dong[9], null, null, null, null, null, "0",null));
            return dsSanPham;
        }

        public List<string> timKiemId()
        {
            string sqlStr = $"SELECT {sanPhamID} FROM {sanPhamHeader}";
            return dbConnection.LayDanhSachMotDoiTuong(sqlStr, $"{sanPhamID}");
        }

        public NguoiDung timKiemNguoiDangTheoIdSP(string idSP)
        {
            string sqlStr = $@"
                            SELECT {sanPhamIdNguoiDang}, {nguoiDungTen} 
                            FROM {sanPhamHeader}
                            INNER JOIN {nguoiDungHeader} ON {sanPhamHeader}.{sanPhamID} = {nguoiDungHeader}.{nguoiDungID}
                            WHERE {sanPhamID} = '{idSP}' ";
            dongKetQua = dbConnection.LayDanhSach<string>(sqlStr);
            return new NguoiDung(dongKetQua[0], dongKetQua[1], null, null, null, null, null, null, null, null, null);
        }

        public List<SanPham> LoadThongSoSanPhamDeThongKe(string idNguoiDang)   //code là gộp mấy cái đồ thị dô chung, rồi gộp lên 2 ô tổng doanh thu và bán được dô chung, còn tong khach hang thì sài riêng
        {
            string sqlStr = $@"
            SELECT {sanPhamTen}, {sanPhamSLDaBan}, {sanPhamSoLuong} , {sanPhamGiaBan}, {sanPhamIdNguoiDang}
            FROM {sanPhamHeader}
            WHERE {sanPhamIdNguoiDang} = '{idNguoiDang}' ";
            dsSanPham = new List<SanPham>();
            bangKetQua = dbConnection.LayDanhSachNhieuPhanTu<string>(sqlStr);
            foreach (var dong in bangKetQua)
                dsSanPham.Add(new SanPham(null, dong[4], dong[0], null, null, dong[2], dong[1], null, dong[3], null, null, null, null, null, null, null, null, null));

            return dsSanPham;
        }
        public string tinhTongKhachHang(string idNguoiDang) // Tính cái ô Tổng số lượng Khách hàng bên thống kê
        {
            string sqlStr = $@"
            SELECT DISTINCT COUNT({trangThaiHeader}.{trangThaiIdNguoiMua}) as TongSoKhachHang
            FROM {trangThaiHeader}
            INNER JOIN {sanPhamHeader} ON {trangThaiHeader}.{trangThaiIdSanPham} = {sanPhamHeader}.{sanPhamID}
            GROUP BY {sanPhamHeader}.{sanPhamIdNguoiDang}
            HAVING {sanPhamIdNguoiDang} = '{idNguoiDang}' ";
            return dbConnection.LayMotDoiTuong(sqlStr, "TongSoKhachHang");
        }
    }
}
