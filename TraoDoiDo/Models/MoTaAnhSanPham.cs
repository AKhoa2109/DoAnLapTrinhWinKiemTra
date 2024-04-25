using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiDo.Models
{
    public class MoTaAnhSanPham
    {
        private string idSanPham ="";
        private string idAnhMinhHoa = ""; 
        private string linkAnhBia = "";
        private string linkAnhMinhHoa = "";
        private string moTa = "";
        public MoTaAnhSanPham() { }
        public MoTaAnhSanPham(string idSanPham, string idAnhMinhHoa, string linkAnhBia, string linkAnhMinhHoa, string moTa)
        {
            this.idSanPham = idSanPham;
            this.idAnhMinhHoa = idAnhMinhHoa;
            this.LinkAnhBia = linkAnhBia; 
            this.linkAnhMinhHoa = linkAnhMinhHoa;
            this.moTa = moTa;
        }

        public string IdSanPham { get => idSanPham; set => idSanPham = value; }
        public string IdAnhMinhHoa { get => idAnhMinhHoa; set => idAnhMinhHoa = value; }
        public string LinkAnhBia { get => linkAnhBia; set => linkAnhBia = value; }
        public string LinkAnhMinhHoa { get => linkAnhMinhHoa; set => linkAnhMinhHoa = value; }
        public string MoTa { get => moTa; set => moTa = value; }
    }
}
