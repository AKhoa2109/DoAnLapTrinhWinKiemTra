using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class GiaoDich
    {
        private string id;
        private string idNguoiDung;
        private string loaiGiaoDich;
        private string soTien;
        private string tuNguonTien;
        private string denNguonTien;
        private string ngayGiaoDich;

        public GiaoDich() { }

        public GiaoDich(string id, string idNguoiDung, string loaiGiaoDich, string soTien, string tuNguonTien, string denNguonTien, string ngayGiaoDich)
        {
            this.id = id;
            this.idNguoiDung = idNguoiDung;
            this.loaiGiaoDich = loaiGiaoDich;
            this.soTien = soTien;
            this.tuNguonTien = tuNguonTien;
            this.denNguonTien = denNguonTien;
            this.ngayGiaoDich = ngayGiaoDich;
        }

        public string Id { get => id; set => id = value; }
        public string LoaiGiaoDich { get => loaiGiaoDich; set => loaiGiaoDich = value; }
        public string SoTien { get => soTien; set => soTien = value; }
        public string TuNguonTien { get => tuNguonTien; set => tuNguonTien = value; }
        public string DenNguonTien { get => denNguonTien; set => denNguonTien = value; }
        public string NgayGiaoDich { get => ngayGiaoDich; set => ngayGiaoDich = value; }
        public string IdNguoiDung { get => idNguoiDung; set => idNguoiDung = value; }
    }
}
