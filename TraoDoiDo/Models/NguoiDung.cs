namespace TraoDoiDo.Models
{
    public class NguoiDung
    {
        private string id = "";
        private string hoTen = "";
        private string gioiTinh = "";
        private string ngaySinh = "";
        private string cmnd = "";
        private string email = "";
        private string sdt = "";
        private string diaChi = "";
        private string anh = "";
        private TaiKhoan taiKhoan = new TaiKhoan();
        private string tien = "";
         

        public NguoiDung(string id, string hoTen, string gioiTinh, string ngaySinh, string cmnd, string email, string sdt, string diaChi, string anh, TaiKhoan taiKhoan, string tien)
        {
            this.id = id;
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.cmnd = cmnd;
            this.email = email;
            this.sdt = sdt;
            this.diaChi = diaChi;
            this.anh = anh;
            this.taiKhoan = taiKhoan;
            this.tien = tien;
        } 


        public NguoiDung ()
        { }

        public string Id { get => id; set => id = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }
        public string Email { get => email; set => email = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string Anh { get => anh; set => anh = value; }
        public string Tien { get => tien; set => tien = value; }
        public TaiKhoan TaiKhoan { get => taiKhoan; set => taiKhoan = value; }
    }
}
