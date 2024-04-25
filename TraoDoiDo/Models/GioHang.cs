using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public  class GioHang
    {
        private string idNguoiMua;
        private string idSanPham;
        private string soLuongMua;
        private string ten;
        private string anhSP;
        private string giaBan;
        private string phiShip;
        private string soLuongTong;
        private string soLuongDaBan;


        public GioHang() { }

        public GioHang(string idNguoiMua, string idSanPham, string soLuongMua, string ten, string anhSP, string giaBan,string phiShip, string soLuongTong, string soLuongDaBan)
        {
            this.idNguoiMua = idNguoiMua;
            this.idSanPham = idSanPham;
            this.soLuongMua = soLuongMua;
            this.ten = ten;
            this.anhSP = anhSP;
            this.giaBan = giaBan;
            this.phiShip = phiShip;
            this.soLuongTong = soLuongTong;
            this.soLuongDaBan = soLuongDaBan;
        }

        public string IdNguoiMua { get => idNguoiMua; set => idNguoiMua = value; }
        public string IdSanPham { get => idSanPham; set => idSanPham = value; }
        public string SoLuongMua { get => soLuongMua; set => soLuongMua = value; }
        public string Ten { get => ten; set => ten = value; }
        public string AnhSP { get => anhSP; set => anhSP = value; }
        public string GiaBan { get => giaBan; set => giaBan = value; }
        public string PhiShip { get => phiShip; set => phiShip = value; }
        public string SoLuongTong { get => soLuongTong; set => soLuongTong = value; }
        public string SoLuongDaBan { get => soLuongDaBan; set => soLuongDaBan = value; }
  
    }
}
