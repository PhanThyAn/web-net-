using System;
using System.Collections.Generic;

namespace Web2023Project.Models
{

    public partial class Nhacungcap
    {
        public int Id { get; set; }

        public string TenNcc { get; set; }

        public string Diachi { get; set; }

        public sbyte Trangthai { get; set; }

        public virtual ICollection<Sanphams> Sanphams { get; set; } = new List<Sanphams>();
    }
}
