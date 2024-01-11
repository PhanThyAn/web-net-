using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Chitietgiohang
{
    public int IdNd { get; set; }

    public int IdSp { get; set; }

    public int? Soluong { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual Nguoidung IdNdNavigation { get; set; } = null!;

    public virtual Sanpham IdSpNavigation { get; set; } = null!;
}
