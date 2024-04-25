using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraoDoiDo.Models;

namespace TraoDoiDo.Database
{
    public class MoTaAnhSanPhamDao:ThuocTinhDao
    {
        public void Them(MoTaAnhSanPham moTaAnhSp)
        { 
            string sqlStr = $" INSERT INTO {moTaSanPhamHeader} ({sanPhamID}, {moTaSanPhamIdAnh} ,{moTaSanPhamLinkAnh}, {moTaSanPhamMoTa}) "+  
                            $" VALUES ('{moTaAnhSp.IdSanPham}', '{moTaAnhSp.IdAnhMinhHoa}','{moTaAnhSp.LinkAnhMinhHoa}', N'{moTaAnhSp.MoTa}') ";
            dbConnection.ThucThi(sqlStr);
        }
        public void Xoa(MoTaAnhSanPham moTaAnhSp)
        {
            string sqlStr = $"DELETE FROM {moTaSanPhamHeader} WHERE {sanPhamID} = {moTaAnhSp.IdSanPham}";
            dbConnection.ThucThi(sqlStr);
        }
        
    }
}
