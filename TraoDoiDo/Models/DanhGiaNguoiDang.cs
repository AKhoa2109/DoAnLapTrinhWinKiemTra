using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class DanhGiaNguoiDang
    {
        private string idNguoiDang;
        private string tenNguoiDang;
        private string idNguoiMua;
        private string tenNguoiMua; 
        private string soSao;
        private string nhanXet;
        private string soLuotDanhGia;
        private string anhNguoiMua;


        public DanhGiaNguoiDang() { }

            public DanhGiaNguoiDang(string idNguoiDang, string tenNguoiDang, string idNguoiMua, string tenNguoiMua, string soSao, string nhanXet, string soLuotDanhGia, string anhNguoiMua)
            {
                this.idNguoiDang = idNguoiDang;
                this.tenNguoiDang = tenNguoiDang;
                this.idNguoiMua = idNguoiMua;
                this.tenNguoiMua = tenNguoiMua;
                this.soSao = soSao;
                this.nhanXet = nhanXet;
                this.soLuotDanhGia = soLuotDanhGia;
                this.anhNguoiMua = anhNguoiMua;
            }

        public string IdNguoiDang { get => idNguoiDang; set => idNguoiDang = value; }
        public string TenNguoiDang { get => tenNguoiDang; set => tenNguoiDang = value; }
        public string IdNguoiMua { get => idNguoiMua; set => idNguoiMua = value; }
        public string TenNguoiMua { get => tenNguoiMua; set => tenNguoiMua = value; }
        public string SoSao { get => soSao; set => soSao = value; }
        public string NhanXet { get => nhanXet; set => nhanXet = value; }
        public string SoLuotDanhGia { get => soLuotDanhGia; set => soLuotDanhGia = value; }
        public string AnhNguoiMua { get => anhNguoiMua; set => anhNguoiMua = value; }
    }
}
