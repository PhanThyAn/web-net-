using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{

    public partial class Log
    {
        public int Id { get; set; }

        public int IdNd { get; set; }

        public string Capdo { get; set; }

        public string Nguon { get; set; }

        public string Ip { get; set; }

        public string Noidung { get; set; }

        public sbyte Trangthai { get; set; }

        public DateTime Ngaytao { get; set; }

        public virtual Nguoidung IdNdNavigation { get; set; }
    }
}
