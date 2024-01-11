using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Chitietdonhang
{
    public int IdDh { get; set; }

    public int IdSp { get; set; }

    public int? Soluong { get; set; }

    public double? Dongia { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual Donhang IdDhNavigation { get; set; } = null!;

    public virtual Sanpham IdSpNavigation { get; set; } = null!;
}
