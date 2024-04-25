using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TraoDoiDo.ViewModels
{
    public class KiemTraDinhDang
    {
        private const int DoDaiCMND = 12;
        private const int TuoiMin = 18;

        public bool kiemTraEmail(string email)
        {
            try
            {
                MailAddress kiemTraEmail = new MailAddress(email);
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public bool kiemTraSoDienThoai(string sdt)
        {
            string query = @"^0[0-9]{9}$";
            return Regex.IsMatch(sdt, query);
        }

        public bool kiemTraCMND(string cmnd)
        {
            return cmnd.Length == DoDaiCMND;
        }

        public bool kiemTraNgaySinh(DateTime ngaySinh)
        {
            DateTime today = DateTime.Now;
            int tuoi = today.Year - ngaySinh.Year;
            return tuoi >= TuoiMin;
        }
    }
}
