using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class QuanLyDonHang
    {
        private string idDonHang = "";
        private string idNguoiDang = "";
        private string idNguoiMua = "";
        private string idSanPham = "";
        private string trangThai = "";
        private string lyDo = "";

        string tenSanPham;
        string linkAnhBia;
        string soLuongMua;
        string gia;
        string phiShip;
        string tongTien;

        public QuanLyDonHang() { }
        public QuanLyDonHang(string idDonHang, string idNguoiDang, string idNguoiMua, string idSanPham, string trangThai, string lyDo)
        {
            this.idDonHang = idDonHang;
            this.idNguoiDang = idNguoiDang;
            this.idNguoiMua = idNguoiMua;
            this.idSanPham = idSanPham;
            this.trangThai = trangThai;
            this.lyDo = lyDo;
        }

        public QuanLyDonHang(string idDonHang, string idNguoiDang, string idNguoiMua, string idSanPham, string trangThai, string lyDo, string tenSanPham, string linkAnhBia, string soLuongMua, string gia, string phiShip, string tongTien)
        {
            this.idDonHang = idDonHang;
            this.idNguoiDang = idNguoiDang;
            this.idNguoiMua = idNguoiMua;
            this.idSanPham = idSanPham;
            this.trangThai = trangThai;
            this.lyDo = lyDo;
            this.tenSanPham = tenSanPham;
            this.linkAnhBia = linkAnhBia;
            this.soLuongMua = soLuongMua;
            this.gia = gia;
            this.phiShip = phiShip;
            this.tongTien = tongTien;
        }

        public string IdDonHang { get => idDonHang; set => idDonHang = value; }
        public string IdNguoiDang { get => idNguoiDang; set => idNguoiDang = value; }
        public string IdNguoiMua { get => idNguoiMua; set => idNguoiMua = value; }
        public string IdSanPham { get => idSanPham; set => idSanPham = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public string LinkAnhBia { get => linkAnhBia; set => linkAnhBia = value; }
        public string SoLuongMua { get => soLuongMua; set => soLuongMua = value; }
        public string Gia { get => gia; set => gia = value; }
        public string PhiShip { get => phiShip; set => phiShip = value; }
        public string TongTien { get => tongTien; set => tongTien = value; }
        public string LyDo { get => lyDo; set => lyDo = value; }
    }
}
