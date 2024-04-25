using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class DanhMucYeuThich // Thấy k cần
    {
        private string idNguoiMua;
        private string idSanPham;
        public DanhMucYeuThich() { }
        public DanhMucYeuThich(string idNguoiMua, string idSanPham)
        {
            this.IdNguoiMua = idNguoiMua;
            this.IdSanPham = idSanPham;
        }

        public string IdNguoiMua { get => idNguoiMua; set => idNguoiMua = value; }
        public string IdSanPham { get => idSanPham; set => idSanPham = value; }
    }
}
