using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web2023Project.Models
{
    public class ProductShow
    {
        public string TenVietTat { set; get; }
        public Sanphams ThongTin { set; get; }

        public ProductShow() { }

        public ProductShow(string tenVietTat, Sanphams thongTin)
        {
            TenVietTat = tenVietTat;
            ThongTin = thongTin;
        }
    }
}
