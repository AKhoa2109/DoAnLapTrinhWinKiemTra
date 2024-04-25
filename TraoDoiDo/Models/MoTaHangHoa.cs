namespace TraoDoiDo.Models
{
    public class MoTaHangHoa
    {
        private string idSanPham = "";
        private string idAnhMinhHoa = "";
        private string linkAnh = "";
        private string moTa = "";

        public MoTaHangHoa() { }
        public MoTaHangHoa(string idSanPham, string idAnhMinhHoa, string linkAnh, string moTa)
        {
            this.idSanPham = idSanPham;
            this.idAnhMinhHoa = idAnhMinhHoa;
            this.linkAnh = linkAnh;
            this.moTa = moTa;

        }

        public string IddSanPham { get => idSanPham; set => idSanPham = value; }
        public string IdAnhMinhHoa { get => idAnhMinhHoa; set => idAnhMinhHoa = value; }
        public string LinkAnh { get => linkAnh; set => linkAnh = value; }
        public string MoTa { get => moTa; set => moTa = value; }
    }
}
