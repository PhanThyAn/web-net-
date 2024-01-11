using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{
    public partial class Donhang
    {
        public int Id { get; set; }

        public int? IdNd { get; set; }

        public int? IdDc { get; set; }

        public double? Tongtien { get; set; }

        public DateTime? Ngaydathang { get; set; }

        public DateTime? Ngaythanhtoan { get; set; }

        public sbyte? Trangthai { get; set; }

        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

        public virtual Diachi IdDcNavigation { get; set; }

        public virtual Nguoidung IdNdNavigation { get; set; }
    }
}