using System;
using System.Collections.Generic;


namespace Web2023Project.Models
{

    public partial class Diachi
    {
        public int Id { get; set; }

        public int? IdNd { get; set; }

        public string Sdt { get; set; }

        public string Ten { get; set; }

        public string Ghichu { get; set; }

        public string Xa { get; set; }

        public string Huyen { get; set; }

        public string Tinh { get; set; }

        public sbyte? Trangthai { get; set; }

        public virtual ICollection<Donhang> Donhangs { get; set; } = new List<Donhang>();

        public virtual Nguoidung IdNdNavigation { get; set; }
    }

}

