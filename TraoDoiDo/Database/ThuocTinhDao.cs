namespace TraoDoiDo.Database
{
    public abstract class ThuocTinhDao
    {
        // Class này dùng để truy cập các thuộc tính của bảng dể hơn khi chỉ cần sửa ở đây

        // Table TaiKhoan
        protected const string taiKhoanHeader = "TaiKhoan";
        public const string taiKhoanTenDangNhap = "TenDangNhap";
        public const string taiKhoanMatKhau = "MatKhau";
        public const string taiKhoanIdNguoiDung = "IdNguoiDung";

        // Table NguoiDung
        protected const string nguoiDungHeader = "NguoiDung";
        public const string nguoiDungID = "IdNguoiDung";
        public const string nguoiDungTen = "HoTenNguoiDung";
        public const string nguoiDungGioiTinh = "GioiTinhNguoiDung";
        public const string nguoiDungNgaySinh = "NgaySinhNguoiDung";
        public const string nguoiDungCMND = "CMNDNguoiDung";
        public const string nguoiDungEmail = "EmailNguoiDung";
        public const string nguoiDungSdt = "SdtNguoiDung";
        public const string nguoiDungDiaChi = "DiaChiNguoiDung";
        public const string nguoiDungAnh = "AnhNguoiDung";
        public const string nguoiDungTien = "TienNguoiDung";

        //Table GiaoDich
        protected const string giaoDichHeader = "GiaoDich";
        public const string giaoDichID = "IdGiaoDich";
        public const string giaoDichLoai = "loaiGiaoDich";
        public const string giaoDichSoTien = "soTien";
        public const string giaoDichTuNguon = "tuNguonGiaoDich";
        public const string giaoDichDenNguon = "denNguonGiaoDich";
        public const string giaoDichNgay = "ngayGiaoDich";

        //Table SanPham
        protected const string sanPhamHeader = "SanPham";
        public const string sanPhamID = "IdSanPham";
        public const string sanPhamIdNguoiDang = "IdNguoiDang";
        public const string sanPhamTen = "Ten";
        public const string sanPhamAnh = "LinkAnhBia";
        public const string sanPhamLoai = "Loai";
        public const string sanPhamSoLuong = "SoLuong";
        public const string sanPhamSLDaBan = "SoLuongDaBan";
        public const string sanPhamGiaGoc = "GiaGoc";
        public const string sanPhamGiaBan = "GiaBan";
        public const string sanPhamPhiShip = "PhiShip";
        public const string sanPhamTrangThai = "TrangThai";
        public const string sanPhamNoiBan = "NoiBan";
        public const string sanPhamXuatXu = "XuatXu";
        public const string sanPhamNgayMua = "NgayMua";
        public const string sanPhamPhamTramMoi = "PhanTramMoi";
        public const string sanPhamMoTaChung = "MoTaChung";
        public const string sanPhamSoLuotXem = "SoLuotXem";

        //Table MoTaAnhSanPham
        protected const string moTaSanPhamHeader = "MoTaAnhSanPham";
        public const string moTaSanPhamIdAnh = "IdAnhMinhHoa";
        public const string moTaSanPhamLinkAnh = "LinkAnhMinhHoa";
        public const string moTaSanPhamMoTa = "MoTa";

        //Table GioHang
        protected const string gioHangHeader = "GioHang";
        public const string gioHangSoLuongMua = "SoLuongMua";

        //Table DanhGiaNguoiDung
        protected const string danhGiaHeader = "DanhGiaNguoiDang";
        public const string danhGiaIdNguoiMua = "IdNguoiMua";
        public const string danhGiaSoSao = "SoSao";
        public const string danhGiaNhanXet = "NhanXet";

        //Table TrangThaiDonHang
        protected const string trangThaiHeader = "TrangThaiDonHang";
        public const string trangThaiIdNguoiMua = "IdNguoiMua";
        public const string trangThaiIdSanPham = "IdSanPham";
        public const string trangThaiSoLuongMua = "SoLuongMua";
        public const string trangThaiTongThanhToan = "TongThanhToan";
        public const string trangThaiNgay = "Ngay";
        public const string trangThaiTrangThai = "TrangThai";

        //Table QuanLyDonHang
        protected const string quanLyHeader = "QuanLyDonHang";
        public const string quanLyIdDonHang = "IdDonHang";
        public const string quanLyIdNguoiDang = "IdNguoiDang";
        public const string quanLyIdNguoiMua = "IdNguoiMua";
        public const string quanLyIdSanPham = "IdSanPham";
        public const string quanLyTrangThai = "TrangThai";
        public const string quanLyLyDo = "LyDoTraHang";

        //Table DanhMucYeuThich
        protected const string danhMucHeader = "DanhMucYeuThich";
        public const string danhMucNguoiMua = "IdNguoiMua";


        protected DbConnection dbConnection = new DbConnection();
    }
}
