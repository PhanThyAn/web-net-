using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{

    public partial class Lienhe
    {
        public int Id { get; set; }

        public string Hoten { get; set; }

        public string Sdt { get; set; }

        public string Email { get; set; }

        public string Tieude { get; set; }

        public string Noidung { get; set; }

        public DateTime Ngaytao { get; set; }

        public sbyte Trangthai { get; set; }
    }
}