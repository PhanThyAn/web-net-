using System;
using System.Collections.Generic;


namespace Web2023Project.Models
{

    public partial class Binhluan
    {
        public int Id { get; set; }

        public int? IdNd { get; set; }

        public int? IdSp { get; set; }

        public string Noidung { get; set; }

        public int Danhgia { get; set; }

        public DateTime Ngaybinhluan { get; set; }

        public sbyte? Trangthai { get; set; }

        public virtual Nguoidung IdNdNavigation { get; set; }

        public virtual Sanphams IdSpNavigation { get; set; }
    }
}
